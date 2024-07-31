arg1=$1

rm -r  Migrations/* 

dotnet ef database drop -f -v

dotnet ef migrations add $arg1

dotnet ef database update