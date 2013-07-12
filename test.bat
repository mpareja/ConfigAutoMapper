@echo off
set results=build_output\report

rem Try to find the highest version of MSBuild available...
set MSBUILD=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v3.5\msbuild.exe
if not exist %MSBUILD% (
	echo MSBuild not found, please update %0
	exit /b 1
) 

%MSBUILD% ConfigAutoMapper.sln /t:Rebuild /p:Configuration=Release

if not exist %results% mkdir %results%
lib\nunit\net-2.0\nunit-console.exe build\ConfigAutoMapper.Tests.dll /xml=%results%\ConfigAutoMapper.xml

