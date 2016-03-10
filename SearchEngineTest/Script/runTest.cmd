@ECHO OFF
if "%*"=="/?" (
 echo Usage: runTest.cmd [Options]
 echo Options:
 echo.
 echo [/Browser:^<Browser Name^>]
 echo The browser used to access the site. The value should be one of following: IE32, IE64, Chrome, Firefox.
 echo.
 echo [/WaitTime:^<Wait Time^>]
 echo The wait time when finding element. 
 echo.
 echo [/Tests:^<Test Case Name[,Test Case Name]...^>]
 echo Run tests with names that match the provided values.
 echo If this option is present, all other options to choose tests should be ignored.
 echo More details see vstest.console.exe /?
 echo.
 echo [/TestCaseFilter:^<Expression^>]
 echo Run tests that match the given expression.
 echo If this option is present, all other options to choose tests should be ignored.
 echo ^<Expression^> is of the format ^<property^>Operator^<value^>[^|^&^<Expression^>]
 echo More details see vstest.console.exe /?
 echo.
 echo [/PlayList:^<PlayList File Name^>]
 echo A playList file which contains a list of tests to be run.
 echo If this option is present, all other options to choose tests should be ignored.
 echo.
 echo Note:
 echo      Each option should be separated with blank space.
 echo      All the options could be ignored. 
 echo      If the options appeared in App.config file are ignored, the default value in App.config file will be used.
 echo      If all the options to choose tests are ignored, all the tests will be run by default.
 echo.
 echo Examples:
 echo      To run tests in playlist file SearchEngine-BVTs.playlist
 echo        ^>runTest.cmd /PlayList:SearchEngineTest\SearchEngine-BVTs.playlist
 echo      To run tests which test name contains "TC01" with WaitTime set to 30
 echo        ^>runTest.cmd /WaitTime:30 /TestCaseFilter:Name~TC01
 goto end
)
cd ..
SETLOCAL ENABLEDELAYEDEXPANSION
set flag=0
FOR %%a IN (%*) DO (
set te=%%a
IF /i "!te:~1,7!"=="Browser" (
 set flag=1
 set browser=!te:~9!
PowerShell.exe -ExecutionPolicy ByPass .\Script\ModifyConfigFileNode.ps1 '.\SearchEngineTest\App.config' 'Browser' '!browser!'
 )
if /i "!te:~1,8!"=="WaitTime" (
 set flag=1
 set waitTime=!te:~10!
PowerShell.exe -ExecutionPolicy ByPass .\Script\ModifyConfigFileNode.ps1 '.\SearchEngineTest\App.config' 'WaitTime' '!waitTime!'
 )
)
if "!flag!"=="0" (
 endlocal
 goto TestRun
)
endlocal

powershell -command write-host "The related property value in App.config will be updated according to the input options." -ForegroundColor Yellow

MSBuild .\SearchEngineTest.sln /t:Rebuild /clp:ErrorsOnly /v:m

:TestRun
SETLOCAL ENABLEDELAYEDEXPANSION
set flag3=0
FOR %%b IN (%*) DO (
set te=%%b
IF /i "!te:~1,8!"=="PlayList" (
 set playList=!te:~10!
 )
IF /i "!te:~1,5!"=="Tests" (
 set flag3=1
 set testCases=!te!
 )
IF /i "!te:~1,14!"=="TestCaseFilter" (
 set testFilter=!te!
 )
if "!flag3!"=="1" (
 if not "!te:~0,1!"=="/" (
   set testCases=!testCases!,!te!
  )
 )
)
if defined testFilter (
 vstest.console.exe .\SearchEngineTest\bin\Debug\SearchEngineTest.dll /logger:trx !testFilter!
) else if defined testCases (
 vstest.console.exe .\SearchEngineTest\bin\Debug\SearchEngineTest.dll /logger:trx !testCases!
) else if defined playList (
rem Get names of test cases  
set tests=/Tests:
 for /f "delims=" %%c IN (!playList!) DO (
  for %%d in (%%c) do (
   set te=%%d
   if "!te:~1,16!"=="SearchEngineTest" (
    set tests=!tests!!te:~1,-1!,
   )
  )
 )
 vstest.console.exe .\SearchEngineTest\bin\Debug\SearchEngineTest.dll /logger:trx !tests:~0,-1!
) else (
 vstest.console.exe .\SearchEngineTest\bin\Debug\SearchEngineTest.dll /logger:trx
)
endlocal

cd .\Script
:end
PAUSE
@ECHO ON