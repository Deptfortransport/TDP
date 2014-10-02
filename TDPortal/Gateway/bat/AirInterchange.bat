@ECHO OFF
ECHO Calling D:\Gateway\bin\utils\AirInterchangeImport\InterchangeImport.exe
D:\Gateway\bin\utils\AirInterchangeImport\InterchangeImport.exe

REM   <!--   DEFAULT Error Levels -->
REM    <add key="NoErrors" value="0" />
REM    <add key="AtLeastOneInsertFailed" value="1" />
REM    <add key="AtLeastOneInterchangeNotWellFormedXML" value="2" />
REM    <add key="IndexingFailure" value="3" />
REM    <add key="MajorError" value="4" />
REM    <!-- InitialisationError = 5 is hard coded - do not assign 5 to other error levels - if application fails on intialisation this will be returned -->
REM    <add key="NotWellFormedXML" value="6" />
REM    <add key="ActionMinimumInterchanges" value="7" />
REM    <add key="ActionMinimumLegsInInterchange" value="8" />

IF %errorlevel% GTR 8 GOTO answerX

GOTO answer%errorlevel%
    :answer
    ECHO WARNING: Program has not returned an error code
	GOTO Finish
    :answer0
    ECHO Program has returned code 0: NoErrors
    PUSHD D:\Gateway\bat\
    @CALL D:\Gateway\bat\switch_airinterchange_db.bat
    POPD
	GOTO Finish
    :answer1
    ECHO Program has returned code 1: AtLeastOneInsertFailed
	GOTO Finish
    :answer2
    ECHO Program has returned code 2: AtLeastOneInterchangeNotWellFormedXML
	GOTO Finish
    :answer3
    ECHO Program has returned code 3: IndexingFailure
	GOTO Finish
    :answer4
    ECHO Program has returned code 4: MajorError
	GOTO Finish
    :answer5
    ECHO Program has returned code 5: InitialisationError
	GOTO Finish
    :answer6
    ECHO Program has returned code 6: NotWellFormedXML
	GOTO Finish
    :answer7
    ECHO Program has returned code 7: ActionMinimumInterchanges
	GOTO Finish
    :answer8
    ECHO Program has returned code 8: ActionMinimumLegsInInterchanges
	GOTO Finish

    :answerX
    ECHO WARNING: Program has returned unknown error code : %errorlevel%
	GOTO Finish

:Finish
ECHO Batch file finished