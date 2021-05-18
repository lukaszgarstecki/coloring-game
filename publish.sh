#!/usr/bin/env bash

dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -p:PublishReadyToRun=false -p:PublishTrimmed=true -o release-linux-x64
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -p:PublishReadyToRun=false -p:PublishTrimmed=true -o release-win-x64
