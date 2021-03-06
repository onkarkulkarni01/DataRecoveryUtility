IF OBJECT_ID(N'[dbo].BULKINSERT') IS NOT NULL
	BEGIN
		DROP PROCEDURE [dbo].BULKINSERT
	END;
GO
CREATE PROCEDURE [dbo].[BULKINSERT]
(
	@tablename VARCHAR(1000),
	@filename VARCHAR(1000),
	@errorfile VARCHAR(1000)
)
AS
BEGIN
	DECLARE @sql AS VARCHAR(4000)

	SET @sql = 'TRUNCATE TABLE ' + @tablename;
	EXEC (@sql)

	SET @sql = 'BULK INSERT ' + @tablename + ' FROM ''' + @filename + ''' WITH 
	( FIRSTROW = 2, 
	  FIELDTERMINATOR = ''","'', 
	  ROWTERMINATOR = ''"\n"'', 
	  KEEPIDENTITY,
	  KEEPNULLS,
	  MAXERRORS = 15,
	  ERRORFILE = '''+ @errorfile +''', 
	  TABLOCK)';

	EXEC (@sql)

END;