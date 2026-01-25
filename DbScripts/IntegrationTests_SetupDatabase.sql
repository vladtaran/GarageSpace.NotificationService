 -- =========================================================================
-- Setup Test Database for GarageSpace Notification Service Integration Tests
-- ==========================================================================

-- Create SQL Login for test user
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'GarageSpaceNotificationTestsUser')
BEGIN
    CREATE LOGIN GarageSpaceNotificationTestsUser WITH PASSWORD = 'Passw0rd123456';
    PRINT 'Login GarageSpaceNotificationTestsUser created successfully.';
END
ELSE
BEGIN
    PRINT 'Login GarageSpaceNotificationTestsUser already exists.';
END

-- Create test database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GarageSpaceNotificationTests')
BEGIN
    CREATE DATABASE GarageSpaceNotificationTests;
    PRINT 'Database GarageSpaceNotificationTests created successfully.';
END
ELSE
BEGIN
    PRINT 'Database GarageSpaceNotificationTests already exists.';
END

GO

-- Switch to the test database
USE GarageSpaceNotificationTests;

-- Create database user
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'GarageSpaceNotificationTestsUser')
BEGIN
    CREATE USER GarageSpaceNotificationTestsUser FOR LOGIN GarageSpaceNotificationTestsUser;
    PRINT 'User GarageSpaceNotificationTestsUser created successfully in GarageSpaceNotificationTests.';
END
ELSE
BEGIN
    PRINT 'User GarageSpaceNotificationTestsUser already exists in GarageSpaceNotificationTests.';
END

-- Grant necessary permissions to the test user
-- db_owner role for full access during testing
ALTER ROLE db_owner ADD MEMBER GarageSpaceNotificationTestsUser;
PRINT 'Granted db_owner permissions to GarageSpaceNotificationTestsUser in GarageSpaceNotificationTests.';

-- Additional specific permissions if needed
GRANT CREATE TABLE TO GarageSpaceNotificationTestsUser;
GRANT ALTER ON SCHEMA::dbo TO GarageSpaceNotificationTestsUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO GarageSpaceNotificationTestsUser;
PRINT 'Granted additional permissions to GarageSpaceNotificationTestsUser in GarageSpaceNotificationTests.';

-- Verify the setup
SELECT 
    'Login Status' as Check_Type,
    CASE 
        WHEN EXISTS (SELECT * FROM sys.server_principals WHERE name = 'GarageSpaceNotificationTestsUser') 
        THEN 'GarageSpaceNotificationTestsUser login exists' 
        ELSE 'GarageSpaceNotificationTestsUser login missing' 
    END as Status
UNION ALL
SELECT 
    'Database Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.databases WHERE name = 'GarageSpaceNotificationTests') 
        THEN 'GarageSpaceNotificationTests database exists' 
        ELSE 'GarageSpaceNotificationTests database missing' 
    END
UNION ALL
SELECT 
    'User Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.database_principals WHERE name = 'GarageSpaceNotificationTestsUser') 
        THEN 'GarageSpaceNotificationTestsUser user exists in GarageSpaceNotificationTests' 
        ELSE 'GarageSpaceNotificationTestsUser user missing in GarageSpaceNotificationTests' 
    END;

PRINT 'Test database setup completed successfully!';
PRINT 'Connection string: Server=(localdb)\mssqllocaldb;Database=GarageSpaceNotificationTests;User Id=GarageSpaceNotificationTestsUser;Password=Passw0rd123456;MultipleActiveResultSets=true';