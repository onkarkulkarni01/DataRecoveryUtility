using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;



namespace DataRecoveryUtility
{
    public partial class Utility : Form
    {
        string FilePath = ConfigurationManager.AppSettings["csvfilepath"];
        string POSXmlFile = ConfigurationManager.AppSettings["POSXmlfilepath"];
        string ILSXmlFile = ConfigurationManager.AppSettings["ILSXmlfilepath"];
        string OracleConnectionString = ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
        string SqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        string LocationExceptionFilter = ConfigurationManager.AppSettings["LocationExceptionFilterpath"];
        string TableStatementFile = ConfigurationManager.AppSettings["tablestatementfilepath"];
        Int32 CommandTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOut"]);
        CheckBox HeaderCheckBox = new CheckBox();
        bool IsLocationFilterRequired = false;
        
        public Utility()
        {
            InitializeComponent();

            FillGrid(POSXmlFile);
        }

        /// <summary>
        /// Fill Grid Data from XML.
        /// </summary>
        private void FillGrid(string xmlFilepath)
        {
            try
            {
                cmbTableList.SelectedIndex = 0;
                lblLocation.Visible = false;
                txtLocation.Visible = false;
                
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlFilepath);
                dataSet.Tables[0].Columns[0].ColumnName = "Table Names";

                DataView dataView = new DataView(dataSet.Tables[0]);
                grdTables.DataSource = dataView;

                //Place the Header CheckBox in the Location of the Header Cell.
                HeaderCheckBox.Location = new Point(423, 2);
                HeaderCheckBox.Size = new Size(18, 18);

                //Assign Click event to the Header CheckBox.
                HeaderCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                grdTables.Controls.Add(HeaderCheckBox);

                //Add a CheckBox Column to the DataGridView at the first position.
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Width = 20;
                checkBoxColumn.Name = "checkBoxColumn";
                checkBoxColumn.HeaderText = "CheckBox";
                grdTables.Columns.Insert(1, checkBoxColumn);

                //Assign Click event to the DataGridView Cell.
                grdTables.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error from FillGrid. Please Check the Error Log.");
                ErrorLog.LogFileWrite("FillGrid", ex);
            }
        }

        /// <summary>
        /// Check Box Column check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                grdTables.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in grdTables.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                    checkBox.Value = HeaderCheckBox.Checked;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("HeaderCheckBox_Clicked", ex);
            }
        }

        /// <summary>
        /// Check Box Column check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in grdTables.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                        {
                            isChecked = false;
                            break;
                        }
                    }
                    HeaderCheckBox.Checked = isChecked;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("DataGridView_CellClick", ex);
            }
        }

#region Convert
        
        /// <summary>
        /// Convert table data to CSV.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            ErrorLog.LogFileWrite("*********************** Converion Started ***********************");
            
            List<string> tableList = GetTableList();

            if (tableList.Count == 0)
            { 
                MessageBox.Show("Please Select the tables from the list.");
                return;
            }

            if (cmbTableList.SelectedIndex == 1 && txtLocation.Text.Trim().Length == 0 )
            { 
                MessageBox.Show("Please Enter the Location for Transation Tables.");
                return;
            }

            Int32 progressBarCounter = tableList.Count;
            pgbProgressReportBar.Value = 0;
            pgbProgressReportBar.Maximum = progressBarCounter;
            
            try
            {
                foreach (string tableName in tableList)
                {
                    ErrorLog.LogFileWrite("------------- " + tableName + " -------------");

                    ConvertToCSV(tableName, progressBarCounter);
                    pgbProgressReportBar.Increment(1);

                    ErrorLog.LogFileWrite("---------------------------------------------");
                }

                ErrorLog.LogFileWrite("*********************** Converion Completed ***********************");
                MessageBox.Show("Conversion Completed.");
                pgbProgressReportBar.Value = 0;
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("btnConvert_Click", ex);
            }
        }

        /// <summary>
        /// Get list of Tables from XML
        /// </summary>
        private List<string> GetTableList()
        {            
            List<string> tableNames = new List<string>();
            
            try
            {
                var tables = (from DataGridViewRow r in grdTables.Rows
                              where Convert.ToBoolean(r.Cells["checkBoxColumn"].Value) == true
                              select r.Cells["Table Names"].Value).ToList();

                tableNames = tables.Select(s => (string)s).ToList(); 
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("GetTableList", ex);
            }
            return tableNames;
        }


        /// <summary>
        /// Convert table data to CSV. Call Multithreading for large number of records.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="progressBarCounter"></param>
        private void ConvertToCSV(string tableName, Int32 progressBarCounter)
        {
            string fileName = "";
            bool extraLocationFilter = false;
            Parameter parameter = new Parameter();

            try
            {
                string selectStatement = GetSelectStatement(tableName);

                if (selectStatement != "")
                {
                    fileName = tableName + ".csv";

                    parameter.tableName = tableName;
                    parameter.fileName = fileName;
                    if (IsLocationFilterRequired)
                        parameter.location = txtLocation.Text.Trim().ToUpper();
                    else
                        parameter.location = "";
                    parameter.selectStatement = selectStatement;

                    extraLocationFilter = CheckConfig(parameter.tableName);

                    using (OracleConnection oracleConnection = new OracleConnection(OracleConnectionString))
                    {
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandTimeout = CommandTimeOut;
                        oracleCommand.CommandText = "dump_table_to_csv";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("p_tname", OracleType.VarChar).Value = parameter.tableName;
                        oracleCommand.Parameters.Add("p_dir", OracleType.VarChar).Value = "EXTRACT_DIR";
                        oracleCommand.Parameters.Add("p_filename", OracleType.VarChar).Value = parameter.fileName;
                        oracleCommand.Parameters.Add("p_location", OracleType.VarChar).Value = parameter.location;
                        if (extraLocationFilter)
                            oracleCommand.Parameters.Add("p_extralocationfilter", OracleType.VarChar).Value = extraLocationFilter.ToString();
                        else
                            oracleCommand.Parameters.Add("p_extralocationfilter", OracleType.VarChar).Value = "";
                        oracleCommand.Parameters.Add("p_selectexpression", OracleType.VarChar).Value = parameter.selectStatement;

                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("ConvertToCSV", ex);
            }

        }

        /// <summary>
        /// Get Select Statement from config file for table for csv conversion.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetSelectStatement(string tableName)
        {
            string statement = "";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(TableStatementFile);

                if (IsLocationFilterRequired)
                {
                    if (xmlDocument.SelectSingleNode("//" + tableName.ToUpper() + "BYLOCATION") == null)
                        ErrorLog.LogFileWrite("Select Statement Not found. Please check the location filter.");
                    else
                        statement = xmlDocument.SelectSingleNode("//" + tableName.ToUpper() + "BYLOCATION").InnerText;
                }
                else
                {
                    if (xmlDocument.SelectSingleNode("//" + tableName.ToUpper()) == null)
                        ErrorLog.LogFileWrite("Select Statement Not found. Please check the location filter.");
                    else
                        statement = xmlDocument.SelectSingleNode("//" + tableName.ToUpper()).InnerText;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("GetSelectStatement", ex);
            }
            return statement;
        }
        
        /// <summary>
        /// Check Config file for Custom Configuration of tables. Check for Location Column in tables.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private bool CheckConfig(string tableName)
        {
            DataSet dsConfig = new DataSet();
            dsConfig.ReadXml(LocationExceptionFilter);

            if (dsConfig.Tables[0].Select("Name='" + tableName + "'").Count() > 0)
            {
                return true;
            }

            return false;

        }

#endregion Convert

#region Upload

        /// <summary>
        /// Upload event for Uploading CSV data into SQL tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            ErrorLog.LogFileWrite("*********************** Upload Started ***********************");

            List<string> tableList = new List<string>();

            try
            {
                tableList = GetTableList();

                if (tableList.Count == 0)
                { 
                    MessageBox.Show("Please Select the tables from the list.");
                    return;
                }

                pgbProgressReportBar.Value = 0;
                pgbProgressReportBar.Maximum = tableList.Count;

                foreach (string tableName in tableList)
                {
                    ErrorLog.LogFileWrite("------------- " + tableName + " -------------");
                    if (DeleteFiles(tableName))
                        if (InsertData(tableName))
                            ReplaceCharacters(tableName);
                                

                    pgbProgressReportBar.Increment(1);
                    ErrorLog.LogFileWrite("---------------------------------------------");
                }

                ErrorLog.LogFileWrite("*********************** Upload Completed ***********************");
                MessageBox.Show("Upload Completed.");
                pgbProgressReportBar.Value = 0;
                
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("btnUpload_Click", ex);
            }

        }

        /// <summary>
        /// Delete Error Files generated while Previous Uploading
        /// </summary>
        private bool DeleteFiles(string tableName)
        {
            string sourceFolder = FilePath;
            string[] filePaths = Directory.GetFiles(sourceFolder, tableName + ".txt*");
            int fileCounter;

            try
            {
                for (fileCounter = 0; fileCounter < filePaths.Length; fileCounter++)
                {
                    string file = filePaths[fileCounter];
                    File.Delete(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("DeleteFiles", ex);
                return false;
            }
        }
        
        /// <summary>
        /// Insert CSV data into SQL tables.
        /// </summary>
        /// <param name="strTableName"></param>
        private bool InsertData(string tableName)
        {
            try
            {
                string filename = FilePath + @"\" + tableName + ".csv";
                if (tableName.ToUpper() == "PUBLISHEDMASTERFILEDETAIL")
                {
                    tableName = "DOWNLOADFILEDETAIL";
                }
                using (SqlConnection sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("BulkInsert", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@tablename", SqlDbType.VarChar).Value = tableName;
                        sqlCommand.Parameters.AddWithValue("@filename", SqlDbType.VarChar).Value = filename;
                        sqlCommand.Parameters.AddWithValue("@errorfile", SqlDbType.VarChar).Value = FilePath + @"\" + tableName + ".txt";

                        sqlConnection.Open();
                        sqlCommand.CommandTimeout = CommandTimeOut;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("InsertData", ex);
                return false;
            }
        }

        /// <summary>
        /// Replace special characters like comma and line break.
        /// </summary>
        /// <param name="strTableName"></param>
        private bool ReplaceCharacters(string tableName)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("REPLACECHARACTERCONVERSION", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@tablename", SqlDbType.VarChar).Value = tableName;

                        sqlConnection.Open();
                        sqlCommand.CommandTimeout = CommandTimeOut;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("ReplaceCharacters", ex);
                return false;
            }
        }

#endregion Upload

#region Reports
        private void btnReport_Click(object sender, EventArgs e)
        {
            ErrorLog.LogFileWrite("*********************** Report Started ***********************");

            try
            {
                grdTables.Visible = false;
                pgbProgressReportBar.Visible = false;
                grdReport.Visible = true;

                grdReport.Rows.Clear() ;

                List<string> tableList = GetTableList();

                if (tableList.Count == 0)
                    MessageBox.Show("Please Select the tables from the list.");

                foreach (string table in tableList)
                {
                    ErrorLog.LogFileWrite("------------- " + table + " -------------");
                    GetRecordCount(table);
                    ErrorLog.LogFileWrite("---------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("btnReport_Click", ex);
            }
            finally
            {
                ErrorLog.LogFileWrite("*********************** Report Completed ***********************");
            }
        }

        private void GetRecordCount(string tableName)
        {
            try
            {
                string sourceFolder = FilePath;
                string destinationFile = FilePath + @"\" + tableName + ".csv";
                string file = Directory.GetFiles(sourceFolder, tableName + ".csv").Single();

                string[] lines = File.ReadAllLines(file);
                lines = lines.Skip(1).ToArray();
                lines = lines.Where(w => w != lines.Last()).ToArray();

                Int32 countLines = lines.Count();

                Int32 NumberOfRows = GetRowCount(tableName);

                ErrorLog.LogFileWrite("Number of Records in CSV: " + countLines);
                ErrorLog.LogFileWrite("Number of Records in SQL: " + NumberOfRows);

                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(grdReport);

                if(countLines != NumberOfRows)
                    dataGridViewRow.DefaultCellStyle.BackColor = Color.Yellow;
                else
                    dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
                
                dataGridViewRow.SetValues(tableName, countLines, NumberOfRows);

                grdReport.Rows.Add(dataGridViewRow);
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("GetRecordCount", ex);
            }
        }

        private Int32 GetRowCount(string tableName)
        {
            Int32 NumberOfRecords = 0;
            
            try
            {
                if (tableName.ToUpper() == "PUBLISHEDMASTERFILEDETAIL")
                {
                    tableName = "DOWNLOADFILEDETAIL";
                }
                using (SqlConnection sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("GetTableData", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@tablename", SqlDbType.VarChar).Value = tableName;
                        sqlConnection.Open();
                        using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                NumberOfRecords = Convert.ToInt32(rdr["NumberOfRows"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("GetRowCount", ex);
            }
            return NumberOfRecords;
        }

        private void btnTableList_Click(object sender, EventArgs e)
        {
            try
            {
                grdTables.Visible = true;
                pgbProgressReportBar.Visible = true;
                grdReport.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("btnTableList_Click", ex);
            }
        }

#endregion Reports

        void cmbTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cmbTableList.SelectedIndex)
                {
                    case 0:
                        lblLocation.Visible = false;
                        txtLocation.Visible = false;
                        DataSet dataSetPOS = new DataSet();
                        dataSetPOS.ReadXml(POSXmlFile);
                        dataSetPOS.Tables[0].Columns[0].ColumnName = "Table Names";
                        DataView dataViewPOS = new DataView(dataSetPOS.Tables[0]);
                        grdTables.DataSource = dataViewPOS;
                        HeaderCheckBox.Checked = false;
                        IsLocationFilterRequired = false;
                        break;
                    case 1:
                        lblLocation.Visible = true;
                        txtLocation.Visible = true;
                        DataSet dataSetILS = new DataSet();
                        dataSetILS.ReadXml(ILSXmlFile);
                        dataSetILS.Tables[0].Columns[0].ColumnName = "Table Names";
                        DataView dataViewILS = new DataView(dataSetILS.Tables[0]);
                        grdTables.DataSource = dataViewILS;
                        HeaderCheckBox.Checked = false;
                        IsLocationFilterRequired = true;
                        txtLocation.Text = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogFileWrite("cmbTableList_SelectedIndexChanged", ex);
            }
        }
    }
}

/// <summary>
/// Input Parameters for Conversion of Oracle data to CSV
/// </summary>
public class Parameter
{
    public string tableName { get; set; }
    public string fileName { get; set; }
    public string location { get; set; }
    public string selectStatement { get; set; }
}