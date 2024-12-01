if [ -z "$1" ]; then
  # If empty, assign a default value to arg1
  arg1="InitialMigration"
else
  # If not empty, assign the provided argument to arg1
  arg1="$1"
fi
rm -r  Migrations/* 

dotnet ef database drop -f -v

dotnet ef migrations add $arg1

dotnet ef database update