# Database Scripts for TaranSoft.MyGarage

This folder contains SQL scripts for setting up the database environment for the TaranSoft.MyGarage project.

## Scripts

### SetupTestDatabase.sql
This script sets up the test database environment for integration tests:

1. **Creates SQL Login**: `MyGarageAPI` with password `Passw0rd123`
2. **Creates Test Database**: `CarsDbTests`
3. **Creates Database User**: `MyGarageAPI` in the test database
4. **Grants Permissions**: Full access for testing purposes

## How to Run

### Option 1: SQL Server Management Studio (SSMS)
1. Open SSMS and connect to `(localdb)\mssqllocaldb`
2. Open `SetupTestDatabase.sql`
3. Execute the script

### Option 2: Command Line
```bash
sqlcmd -S "(localdb)\mssqllocaldb" -i "SetupTestDatabase.sql"
```

### Option 3: Azure Data Studio
1. Open Azure Data Studio
2. Connect to `(localdb)\mssqllocaldb`
3. Open and execute `SetupTestDatabase.sql`

## Connection String
After running the setup script, use this connection string in your integration tests:
```
Server=(localdb)\mssqllocaldb;Database=CarsDbTests;User Id=MyGarageAPI;Password=Passw0rd123;MultipleActiveResultSets=true
```

## Next Steps
After running the database setup script:
1. Run Entity Framework migrations: `dotnet ef database update --project TaranSoft.MyGarage.Repository.EntityFramework --startup-project TaranSoft.MyGarage.IntegrationTests`
2. Run your integration tests

## Security Note
The test user has `db_owner` permissions for testing purposes. In production, use more restrictive permissions. 