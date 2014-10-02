use permanentportal
GO

-- TestLoad1
-- Insert initial test values into the properties table
CREATE PROCEDURE DatabasePropertyProviderTestLoad1
AS
UPDATE properties SET pvalue=1 where pname = 'propertyservice.version'
UPDATE properties SET pvalue=1000 where pname = 'propertyservice.refreshrate'
INSERT INTO properties VALUES ('test.propertyservice.standard.message', 'hello group','' , '1111', 0) 
GO

-- TestLoad2
-- Add the same property for a different group (this should have no effect)
CREATE PROCEDURE DatabasePropertyProviderTestLoad2
AS
UPDATE properties SET pvalue=2 WHERE pname = 'propertyservice.version'
INSERT INTO properties VALUES ('test.propertyservice.standard.message', 'hello new group', '', '2222', 0)
GO

-- TestLoad3
-- Add the same property for a specific application id (should should affect the value read)
CREATE PROCEDURE DatabasePropertyProviderTestLoad3
AS
UPDATE properties SET pvalue=3 WHERE pname = 'propertyservice.version'
INSERT INTO properties VALUES ('test.propertyservice.standard.message', 'hello application', '1234', '', 0) 
GO

-- TestLoad4
-- Update one of the property values (the version number always has to be updated)
CREATE PROCEDURE DatabasePropertyProviderTestLoad4
AS
UPDATE properties SET pvalue=4 WHERE pname = 'propertyservice.version'
UPDATE properties SET pvalue=2000 WHERE pname = 'propertyservice.refreshrate'
GO



-- TidyUp
CREATE PROCEDURE DatabasePropertyProviderTidyUp
AS
DELETE FROM properties WHERE pname IN ('test.propertyservice.standard.message')
UPDATE properties SET pvalue=60000 WHERE pname = 'propertyservice.refreshrate'

drop procedure DatabasePropertyProviderTestload1
drop procedure DatabasePropertyProviderTestload2
drop procedure DatabasePropertyProviderTestload3
drop procedure DatabasePropertyProviderTestload4
drop procedure DatabasePropertyProviderTidyUp

GO
