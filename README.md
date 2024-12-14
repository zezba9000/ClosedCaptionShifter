# Closed Caption Shifter
Shift closed caption times to match video files with wrong offsets.<br>
Sometimes you can find cheaper DVDs/BDs with languages not in your region then rip these disks and add the .srt files after the fact in your language but their time offsets may be wrong.<br>
This tool can be used to offset/shift the entire files captions to match your video file.

## Building (NOTE: you can swap '\*-x64' with '\*-arm64' or '\*-arm')
* Windows (needs .NET installed): ```dotnet publish -r win-x64 -c Release```
* Windows (doesn't need .NET installed): ```dotnet publish -r win-x64 --self-contained -c Release```
* Windows (AOT doesn't need .NET installed): ```dotnet publish -r win-x64 -c Release /p:PublishAot=true```
</br></br>
* macOS (needs .NET installed): ```dotnet publish -r osx-x64 -c Release```
* macOS (doesn't need .NET installed): ```dotnet publish -r osx-x64 --self-contained -c Release```
* macOS (AOT doesn't need .NET installed): ```dotnet publish -r osx-x64 -c Release /p:PublishAot=true```
</br></br>
* Linux (needs .NET installed): ```dotnet publish -r linux-x64 -c Release```
* Linux (doesn't need .NET installed): ```dotnet publish -r linux-x64 --self-contained -c Release```
* Linux (AOT doesn't need .NET installed): ```dotnet publish -r linux-x64 -c Release /p:PublishAot=true```