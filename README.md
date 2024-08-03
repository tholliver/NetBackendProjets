### Dependencies


### Migrations

Installing EF dependencies:
```
dotnet tool install --global dotnet-ef
```

migrations: 
```
dotnet ef migrations add InitialCreate
```

Apply changes: 
```
dotnet ef database update
```