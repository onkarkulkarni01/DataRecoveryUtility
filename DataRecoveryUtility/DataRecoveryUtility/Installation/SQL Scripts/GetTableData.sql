IF OBJECT_ID(N'[dbo].GetTableData') IS NOT NULL
	BEGIN
		DROP PROCEDURE [dbo].GetTableData
	END;
GO
CREATE PROCEDURE [dbo].[GetTableData]
(
	@tablename VARCHAR(100)
)
AS
BEGIN
	DECLARE @sql AS VARCHAR(500)

	SET @sql = 'select count(1) as NumberOfRows FROM ' + @tablename;
	EXEC (@sql)

END;