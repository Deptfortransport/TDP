use TransientPortal
go

--clear data
DELETE FROM ContextSuggestionLink WHERE ContextId >= 1000
DELETE FROM Context WHERE ContextId >= 1000
DELETE FROM SuggestionLink WHERE SuggestionLinkId >= 1000
DELETE FROM Resource WHERE ResourceNameId >= 1000
DELETE FROM ResourceName WHERE ResourceNameId >= 1000
DELETE FROM LinkCategory WHERE LinkCategoryId >= 1000
DELETE FROM InternalLink WHERE InternalLinkId >= 1000

--insert into InternalLink table
INSERT INTO [InternalLink] ([InternalLinkId], [RelativeURL] , [Description])
SELECT 1000, '/URL1000.aspx', NULL
UNION
SELECT 1001, '/URL1001.aspx', NULL
UNION
SELECT 1002, '/URL1002.aspx', NULL
UNION
SELECT 1003, '/URL1003.aspx', NULL
UNION
SELECT 1004, '/URL1004.aspx', NULL
UNION
SELECT 1005, '/URL1005.aspx', NULL
UNION
SELECT 1006, '/URL1006.aspx', NULL

--insert into LinkCategory table
INSERT INTO [LinkCategory] ([LinkCategoryId], [Priority], [Name], [Description])
SELECT 1000, 210, 'Category1', NULL
UNION
SELECT 1001, 240, 'Category2', NULL
UNION
SELECT 1002, 230, 'Category3', NULL
UNION
SELECT 1003, 220, 'Category4', NULL

--insert into ResourceName table
INSERT INTO[ResourceName] ([ResourceNameId], [ResourceName]) 
SELECT 1000, 'RESOURCE1000'
UNION
SELECT 1001, 'RESOURCE1001'
UNION
SELECT 1002, 'RESOURCE1002'
UNION
SELECT 1003, 'RESOURCE1003'
UNION
SELECT 1004, 'RESOURCE1004'
UNION
SELECT 1005, 'RESOURCE1005'
UNION
SELECT 1006, 'RESOURCE1006'

--insert into Resource table
INSERT INTO [Resource] ([ResourceId], [ResourceNameId], [Culture], [Text])
SELECT 1001, 1000, 'cy-GB', '(WELSH) Need information on attractions at {DestinationLocation}'
UNION
SELECT 1002, 1001, 'en-GB', '(ENGLISH) Did you know you can fly from {OriginLocation} to {DestinationLocation} in a baloon?'
UNION
SELECT 1003, 1001, 'cy-GB', '(WELSH) Did you know you can fly from {OriginLocation} to {DestinationLocation} in a baloon?'
UNION
SELECT 1004, 1002, 'en-GB', '(ENGLISH) Click here for information on ticket prices'
UNION
SELECT 1005, 1002, 'cy-GB', '(WELSH) Click here for information on ticket prices'
UNION
SELECT 1006, 1003, 'en-GB', '(ENGLISH) Check travel news in and around {OriginLocation}'
UNION
SELECT 1007, 1003, 'cy-GB', '(WELSH) Check travel news in and around {OriginLocation}'
UNION
SELECT 1008, 1004, 'en-GB', '(ENGLISH) Check weather conditions for your journey'
UNION
SELECT 1009, 1004, 'cy-GB', '(WELSH) Check weather conditions for your journey'
UNION
SELECT 1010, 1005, 'en-GB', '(ENGLISH) Check ticket prices for trains departing from {OriginLocation}'
UNION
SELECT 1011, 1005, 'cy-GB', '(WELSH) Check ticket prices for trains departing from {OriginLocation}'
UNION
SELECT 1012, 1006, 'en-GB', '(ENGLISH) Check special weekend discount offers'
UNION
SELECT 1013, 1006, 'cy-GB', '(WELSH) Check special weekend discount offers'

--insert into SuggestionLink table
INSERT INTO [SuggestionLink] ([SuggestionLinkId], [LinkCategoryId], [Priority], [ResourceNameId], 
[ExternalInternalLinkId], [ExternalInternalLinkType])
SELECT 1000, 1000, 10, 1000, 1000, 'Internal'
UNION
SELECT 1001, 1001, 20, 1001, 1001, 'Internal'
UNION
SELECT 1002, 1001, 10, 1002, 1002, 'Internal'
UNION
SELECT 1003, 1002, 70, 1006, 1000, 'Internal'
UNION
SELECT 1004, 1002, 50, 1003, 1003, 'Internal'
UNION
SELECT 1005, 1003, 40, 1004, 1006, 'Internal'
UNION
SELECT 1006, 1003, 30, 1005, 1006, 'Internal'

--insert into context table
INSERT INTO [Context] ([ContextId], [Name], [Description]) 
SELECT 1000, 'CONTEXT1', NULL
UNION
SELECT 1001, 'CONTEXT2', NULL
UNION
SELECT 1002, 'CONTEXT3', NULL

--insert into ContextSuggestionLink table
INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId])
SELECT 1000, 1000, 1000
UNION
SELECT 1001, 1000, 1001
UNION
SELECT 1002, 1000, 1002
UNION
SELECT 1003, 1001, 1003
UNION
SELECT 1004, 1001, 1004
UNION
SELECT 1005, 1001, 1005
UNION
SELECT 1006, 1002, 1006