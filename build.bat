@echo off
set results=report

msbuild.exe ConfigAutoMapper.sln /t:Rebuild /p:Configuration=Release

if not exist %results% mkdir %results%
lib\nunit\net-2.0\nunit-console.exe build\ConfigAutoMapper.Tests.dll /xml=%results%\ConfigAutoMapper.xml

