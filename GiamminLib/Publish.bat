cd %~dp0
dotnet msbuild /t:PublishNuget /fl /flp:"logfile=PublishNuget.log;verbosity=detailed"
pause