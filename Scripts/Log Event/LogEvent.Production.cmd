@echo off
powershell -File "GetHTTPResponse.ps1" -ConfigFile "Config.Development.json" -RequestFile "LogEvent.Request.json"
echo Done
pause