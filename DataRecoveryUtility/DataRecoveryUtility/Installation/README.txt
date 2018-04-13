********* First Time Installation ****************

Below mentioned steps are one time activity on Oracle server.

1) Create a folder on Oracle server for Downloading csv files on the server.
2) Mention the path of that folder in DirectoyCreation.sql file and then Execute.
3) Mention the ILS user of database which is used for Application access in GrantAccess.sql file and then Execute.

***************************************************

************* Oracle Scripts **********************

Execute the below mention scripts on Oracle server.

1) Execute dump_table_to_csv.sql file.

***************************************************

************* SQL Scripts **********************

Execute the below mention scripts on Sql server.
 
1) BulkInsert.SQL
2) REPLACECHARACTERCONVERSION.SQL
3) GetTableData.SQL

***************************************************

**************** Application Changes **************

Please mention the folder paths in App.Config.

1) csvfilepath -- Add folder path of folder created on oracle server for CSV Download.

2) errorLogFilePath - Path of Error folder in Application folder. Do not mention the file name.

3) xmlfilepath - Path of XML file in which tables are mentioned for conversion. Set the path of XML folder which is in Application folder. Also mention the file name after the path.

4) tablestatementfilepath - Path of XML file in which select statements of tables are mentioned. Set the path of XML folder which is in Application folder. Also mention the file name after the path.

5) configFilepath - Set the path of XML folder which is in Application folder. Also mention the config file name after the path.

4) location - Set the location where locationwise data needs to be fetched

7) isLocationFilterRequired - Set to TRUE for tables where locationwise data needs to be fetched. Else set to FALSE.

****************************************************

