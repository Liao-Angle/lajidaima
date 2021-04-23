@echo off
set _Srvname="ECP Job Queue Service"
for /f "tokens=3* delims=\ " %%a in ('sc query %_Srvname% ^| findstr STATE') do (
SET _srvStat=%%b
)
if %_srvStat% NEQ RUNNING (echo %_Srvname% is not RUNNING && net start %_Srvname%) else (echo %_Srvname% RUNNING)

