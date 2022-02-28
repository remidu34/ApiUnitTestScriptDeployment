@echo off
title Git download by students of epsi

echo "Bonjour placez ce script sur le bureau puis mettez remidu34 pour l username et ApiUnitTestScriptDeployment pour le projet. Mettez /swagger a votre url pour acceder a l app"
set /p user=Username:
set /p proj=Project Name:

git clone https://github.com/%user%/%proj%.git

cd %proj%\ApiEpsi
dotnet run

