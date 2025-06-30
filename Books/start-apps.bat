@echo off
echo Starting Books Application...
echo.

echo Starting .NET Server...
start "Books Server" cmd /k "cd /d books.Server && dotnet build && dotnet bin/Debug/net9.0/books.Server.dll"

echo Starting React Client...
start "Books Client" cmd /k "cd /d books.client && npm start"

echo.
echo Both applications are starting in separate windows.
echo Client: http://localhost:3000
echo Server: http://localhost:5000
echo.
echo Press any key to exit this window...
pause > nul
