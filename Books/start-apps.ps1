#!/usr/bin/env pwsh

# Start both client and server
Write-Host "Starting Books Application..." -ForegroundColor Green

# Start the .NET server in the background
Write-Host "Starting .NET Server..." -ForegroundColor Yellow
$serverJob = Start-Job -ScriptBlock {
    Set-Location "c:\Users\PC\Desktop\Books\books.Server"
    dotnet build
    dotnet bin/Debug/net9.0/books.Server.dll
}

# Start the React client in the background
Write-Host "Starting React Client..." -ForegroundColor Yellow
$clientJob = Start-Job -ScriptBlock {
    Set-Location "c:\Users\PC\Desktop\Books\books.client"
    npm start
}

Write-Host "Both applications are starting..." -ForegroundColor Green
Write-Host "Client: http://localhost:3000" -ForegroundColor Cyan
Write-Host "Server: http://localhost:5000" -ForegroundColor Cyan
Write-Host "Press Ctrl+C to stop both applications" -ForegroundColor Red

# Wait for user to press Ctrl+C
try {
    while ($true) {
        Start-Sleep -Seconds 1
    }
}
finally {
    Write-Host "Stopping applications..." -ForegroundColor Red
    Stop-Job $serverJob, $clientJob
    Remove-Job $serverJob, $clientJob
    Write-Host "Applications stopped." -ForegroundColor Green
}
