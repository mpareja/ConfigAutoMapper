@echo off
set OUTDIR=%~dp0build_output

rem Try to find the highest version of MSBuild available...
set MSBUILD=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v3.5\msbuild.exe
if not exist %MSBUILD% (
	echo MSBuild not found, please update %0
	exit /b 1
)

rem Clear out the output directory
rmdir /S /Q "%OUTDIR%"

SET MSBUILD_ARGUMENTS=ConfigAutoMapper.sln  /t:Clean;Build /p:Configuration=Release "/p:Platform=Any CPU" /v:m

"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=net20 "/p:OutDir=%OUTDIR%\lib\net20\\"
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=net30 "/p:OutDir=%OUTDIR%\lib\net30\\"
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=net35 "/p:OutDir=%OUTDIR%\lib\net35\\"
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=net40 "/p:OutDir=%OUTDIR%\lib\net40\\"
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=net45 "/p:OutDir=%OUTDIR%\lib\net45\\"
