#!/bin/bash

if [ -z "$1" ]; then
  # If empty, assign a default value to arg1
  arg1="Modification"
else
  # If not empty, assign the provided argument to arg1
  arg1="$1"
fi

dotnet ef migrations add $arg1

dotnet ef database update