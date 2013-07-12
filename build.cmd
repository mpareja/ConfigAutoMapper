@echo off
set OUTDIR=%~dp0build_output
SET MSBUILD_ARGUMENTS=ConfigAutoMapper.sln  /t:Clean;Build /p:Configuration=Release "/p:Platform=Any CPU" /v:m
set NUNIT=packages\NUnit.Runners.2.6.2\tools\nunit-console.exe
set TEST_RESULTS=build_output\test_results

rem Try to find the highest version of MSBuild available...
set MSBUILD=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if not exist %MSBUILD% set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v3.5\msbuild.exe
if not exist %MSBUILD% (
	echo MSBuild not found, please update %0
	exit /b 1
)

rem Clear out the output directory and generate the test results directory
rmdir /S /Q "%OUTDIR%"
mkdir %TEST_RESULTS%

rem Install test runner tooling
.nuget\nuget.exe install .nuget\packages.config -OutputDirectory packages

rem Compile and test each supported version
SET VERSION=net35& SET FWV=v3.5
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=%FWV% "/p:OutDir=%OUTDIR%\%VERSION%\\"
%NUNIT% %OUTDIR%\%VERSION%\ConfigAutoMapper.Tests.dll /framework=%FWV% /xml=%TEST_RESULTS%\ConfigAutoMapper.%VERSION%.xml

SET VERSION=net40& SET FWV=v4.0
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=%FWV% "/p:OutDir=%OUTDIR%\%VERSION%\\"
%NUNIT% %OUTDIR%\%VERSION%\ConfigAutoMapper.Tests.dll /framework=%FWV% /xml=%TEST_RESULTS%\ConfigAutoMapper.%VERSION%.xml

SET VERSION=net45& SET FWV=v4.5
"%MSBUILD%" %MSBUILD_ARGUMENTS% /p:TargetFrameworkVersion=%FWV% "/p:OutDir=%OUTDIR%\%VERSION%\\"
%NUNIT% %OUTDIR%\%VERSION%\ConfigAutoMapper.Tests.dll /framework=%FWV% /xml=%TEST_RESULTS%\ConfigAutoMapper.%VERSION%.xml

echo Generate NuGet package

SET VERSION=net35
mkdir %OUTDIR%\package\lib\%VERSION%
copy %OUTDIR%\%VERSION%\ConfigAutoMapper.dll %OUTDIR%\package\lib\%VERSION%\

SET VERSION=net40
mkdir %OUTDIR%\package\lib\%VERSION%
copy %OUTDIR%\%VERSION%\ConfigAutoMapper.dll %OUTDIR%\package\lib\%VERSION%\

SET VERSION=net45
mkdir %OUTDIR%\package\lib\%VERSION%
copy %OUTDIR%\%VERSION%\ConfigAutoMapper.dll %OUTDIR%\package\lib\%VERSION%\

pushd %OUTDIR%\package
copy ..\..\ConfigAutoMapper.nuspec .
..\..\.nuget\NuGet.exe pack ConfigAutoMapper.nuspec
popd
