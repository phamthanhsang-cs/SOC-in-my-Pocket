copy /z /y "\\socimp-dc1\sysmon\olafconfig.xml" "C:\"
copy /z /y "\\socimp-dc1\sysmon\Sysmon.exe" "C:\"

sc query "Sysmon" | Find "RUNNING"
If "%ERRORLEVEL%" EQU "1" (
    goto startsysmon
)

:startsysmon
sc start Sysmon
If "%ERRORLEVEL%" EQU "1" (
    goto installsysmon
)

:installsysmon
"c:\Sysmon.exe" /accepteula -i c:\olafconfig.xml
