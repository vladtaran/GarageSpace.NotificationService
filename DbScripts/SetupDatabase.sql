 -- ===========================================================
-- Setup Test Database for GarageSpace Notification Service
-- ============================================================

-- Create SQL Login for test user
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'GarageSpaceNotificationUser')
BEGIN
    CREATE LOGIN GarageSpaceNotificationUser WITH PASSWORD = 'Passw0rd12345';
    PRINT 'Login GarageSpaceNotificationUser created successfully.';
END
ELSE
BEGIN
    PRINT 'Login GarageSpaceNotificationUser already exists.';
END

-- Create test database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GarageSpaceNotification')
BEGIN
    CREATE DATABASE GarageSpaceNotification;
    PRINT 'Database GarageSpaceNotification created successfully.';
END
ELSE
BEGIN
    PRINT 'Database GarageSpaceNotification already exists.';
END

GO

-- Switch to the test database
USE GarageSpaceNotification;

-- Create database user
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'GarageSpaceNotificationUser')
BEGIN
    CREATE USER GarageSpaceNotificationUser FOR LOGIN GarageSpaceNotificationUser;
    PRINT 'User GarageSpaceNotificationUser created successfully in GarageSpaceNotification.';
END
ELSE
BEGIN
    PRINT 'User GarageSpaceNotificationUser already exists in GarageSpaceNotification.';
END

-- Grant necessary permissions to the test user
-- db_owner role for full access during testing
ALTER ROLE db_owner ADD MEMBER GarageSpaceNotificationUser;
PRINT 'Granted db_owner permissions to GarageSpaceNotificationUser in GarageSpaceNotification.';

-- Additional specific permissions if needed
GRANT CREATE TABLE TO GarageSpaceNotificationUser;
GRANT ALTER ON SCHEMA::dbo TO GarageSpaceNotificationUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO GarageSpaceNotificationUser;
PRINT 'Granted additional permissions to GarageSpaceNotificationUser in GarageSpaceNotification.';

-- Verify the setup
SELECT 
    'Login Status' as Check_Type,
    CASE 
        WHEN EXISTS (SELECT * FROM sys.server_principals WHERE name = 'GarageSpaceNotificationUser') 
        THEN 'GarageSpaceNotificationUser login exists' 
        ELSE 'GarageSpaceNotificationUser login missing' 
    END as Status
UNION ALL
SELECT 
    'Database Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.databases WHERE name = 'GarageSpaceNotification') 
        THEN 'GarageSpaceNotification database exists' 
        ELSE 'GarageSpaceNotification database missing' 
    END
UNION ALL
SELECT 
    'User Status',
    CASE 
        WHEN EXISTS (SELECT * FROM sys.database_principals WHERE name = 'GarageSpaceNotificationUser') 
        THEN 'GarageSpaceNotificationUser user exists in GarageSpaceNotification' 
        ELSE 'GarageSpaceNotificationUser user missing in GarageSpaceNotification' 
    END;

PRINT 'Test database setup completed successfully!';
PRINT 'Connection string: Server=(localdb)\mssqllocaldb;Database=GarageSpaceNotification;User Id=GarageSpaceNotificationUser;Password=Passw0rd12345;MultipleActiveResultSets=true';