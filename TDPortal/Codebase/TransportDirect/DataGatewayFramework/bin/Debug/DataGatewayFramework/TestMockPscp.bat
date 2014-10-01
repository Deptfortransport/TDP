SET FILENAME=C:\TestMockPscpOutput.txt
echo > %FILENAME%
:LOOP
IF "%1" == "" GOTO END
echo %1 >> %FILENAME%
SHIFT
GOTO LOOP
:END

echo End of data
