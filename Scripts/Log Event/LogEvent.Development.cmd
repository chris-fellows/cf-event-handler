@echo off
powershell -File "GetHTTPResponse.ps1" -ConfigFile "Config.Production.json" -RequestFile "LogEvent.Request.json"
echo Done
pause