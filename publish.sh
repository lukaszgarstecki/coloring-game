#!/usr/bin/env bash

dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -p:PublishReadyToRun=false -o release-linux-x64
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -p:PublishReadyToRun=false -o release-win-x64
