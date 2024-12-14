using System.Globalization;
using System.Text.RegularExpressions;

namespace ClosedCaptionShifter
{
    class Program
    {
        private static void Main(string[] args)
        {
            // get args
            if (args == null || args.Length != 3)
            {
                Console.WriteLine("Invalid args. Requires: (input.srt output.srt offset-in-sec)");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            int offsetInSeconds = int.Parse(args[2]);

            // validate paths
            if (Path.GetExtension(inputPath) != ".srt" || !File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found or invalid: {inputPath}");
                return;
            }

            // Regular expression for time line: "HH:MM:SS,mmm --> HH:MM:SS,mmm"
            var timeLineRegex = new Regex(@"(\d\d:\d\d:\d\d,\d\d\d) --> (\d\d:\d\d:\d\d,\d\d\d)");

            // shift time
            using (var sr = new StreamReader(inputPath))
            using (var sw = new StreamWriter(outputPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Check if the line matches the time format line
                    var match = timeLineRegex.Match(line);
                    if (match.Success)
                    {
                        // Parse times
                        string startTimeStr = match.Groups[1].Value;
                        string endTimeStr = match.Groups[2].Value;

                        TimeSpan startTime = ParseSrtTime(startTimeStr);
                        TimeSpan endTime = ParseSrtTime(endTimeStr);

                        // Shift times
                        startTime = ShiftTime(startTime, offsetInSeconds);
                        endTime = ShiftTime(endTime, offsetInSeconds);

                        // Rewrite the line
                        string newTimeLine = $"{FormatSrtTime(startTime)} --> {FormatSrtTime(endTime)}";
                        sw.WriteLine(newTimeLine);
                    }
                    else
                    {
                        // Just write the line as is (subtitle index, text, blank lines, etc.)
                        sw.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("SRT times have been shifted and written to output.srt");
        }

        private static TimeSpan ParseSrtTime(string srtTime)
        {
            // srtTime format: HH:MM:SS,mmm
            // Example: "00:00:02,000"
            if (TimeSpan.TryParseExact(srtTime, @"hh\:mm\:ss\,fff", CultureInfo.InvariantCulture, out TimeSpan result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Invalid SRT time format: " + srtTime);
            }
        }

        private static TimeSpan ShiftTime(TimeSpan original, int offsetInSeconds)
        {
            // Add offset
            TimeSpan shifted = original.Add(TimeSpan.FromSeconds(offsetInSeconds));

            // Ensure no negative times
            if (shifted < TimeSpan.Zero)
            {
                shifted = TimeSpan.Zero;
            }

            return shifted;
        }

        private static string FormatSrtTime(TimeSpan time)
        {
            // Format back to HH:MM:SS,mmm
            return string.Format("{0:00}:{1:00}:{2:00},{3:000}",
                (int)time.TotalHours,
                time.Minutes,
                time.Seconds,
                time.Milliseconds);
        }
    }
}
