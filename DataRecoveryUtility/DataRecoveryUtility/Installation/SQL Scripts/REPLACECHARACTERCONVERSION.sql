IF OBJECT_ID(N'[dbo].REPLACECHARACTERCONVERSION') IS NOT NULL
	BEGIN
		DROP PROCEDURE [dbo].REPLACECHARACTERCONVERSION
	END;
GO

CREATE PROCEDURE [dbo].[REPLACECHARACTERCONVERSION]
(
	@tablename varchar(100)
)
AS
BEGIN
	DECLARE @col varchar(100)
	DECLARE @SQL varchar(100)
	SELECT  @col = ''
	
	IF EXISTS (SELECT * from syscolumns where id = Object_ID(@tablename) and colstat & 1 = 1)
	BEGIN
		SELECT @SQL = name from syscolumns where id = Object_ID(@tablename) and colstat & 1 = 1
	END
	
	WHILE @col IS NOT NULL BEGIN
		SELECT @col = MIN(name) FROM syscolumns 
		WHERE id = (SELECT id FROM sysobjects WHERE type = 'U' AND name = @tablename) 
		AND name > @col
		
		IF @col IS NULL BREAK
		If @col = @SQL break
			execute ('UPDATE ' + @tablename + ' SET ' + @col + ' = REPLACE(' + @col + ', ''~'', CHAR(10))');
	END

END;