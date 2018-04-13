namespace DataRecoveryUtility
{
    partial class Utility
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblTableTypes = new System.Windows.Forms.Label();
            this.cmbTableList = new System.Windows.Forms.ComboBox();
            this.btnTableList = new System.Windows.Forms.Button();
            this.grdReport = new System.Windows.Forms.DataGridView();
            this.TableNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfRecordsInCSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfRecordsInSQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReport = new System.Windows.Forms.Button();
            this.grdTables = new System.Windows.Forms.DataGridView();
            this.pgbProgressReportBar = new System.Windows.Forms.ProgressBar();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbConvert = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbConvert.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.lblLocation);
            this.panel1.Controls.Add(this.lblTableTypes);
            this.panel1.Controls.Add(this.cmbTableList);
            this.panel1.Controls.Add(this.btnTableList);
            this.panel1.Controls.Add(this.grdReport);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.grdTables);
            this.panel1.Controls.Add(this.pgbProgressReportBar);
            this.panel1.Controls.Add(this.btnUpload);
            this.panel1.Controls.Add(this.btnConvert);
            this.panel1.Location = new System.Drawing.Point(29, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 619);
            this.panel1.TabIndex = 0;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(471, 21);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(132, 20);
            this.txtLocation.TabIndex = 12;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(340, 23);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(125, 14);
            this.lblLocation.TabIndex = 11;
            this.lblLocation.Text = "Please Enter Location";
            // 
            // lblTableTypes
            // 
            this.lblTableTypes.AutoSize = true;
            this.lblTableTypes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableTypes.Location = new System.Drawing.Point(68, 23);
            this.lblTableTypes.Name = "lblTableTypes";
            this.lblTableTypes.Size = new System.Drawing.Size(55, 14);
            this.lblTableTypes.TabIndex = 10;
            this.lblTableTypes.Text = "Table list";
            // 
            // cmbTableList
            // 
            this.cmbTableList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTableList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTableList.FormattingEnabled = true;
            this.cmbTableList.Items.AddRange(new object[] {
            "Master Tables",
            "Transaction Tables"});
            this.cmbTableList.Location = new System.Drawing.Point(144, 20);
            this.cmbTableList.Name = "cmbTableList";
            this.cmbTableList.Size = new System.Drawing.Size(166, 22);
            this.cmbTableList.TabIndex = 9;
            this.cmbTableList.SelectedIndexChanged += new System.EventHandler(this.cmbTableList_SelectedIndexChanged);
            // 
            // btnTableList
            // 
            this.btnTableList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTableList.Location = new System.Drawing.Point(505, 565);
            this.btnTableList.Name = "btnTableList";
            this.btnTableList.Size = new System.Drawing.Size(112, 31);
            this.btnTableList.TabIndex = 8;
            this.btnTableList.Text = "Get Table List";
            this.btnTableList.UseVisualStyleBackColor = true;
            this.btnTableList.Click += new System.EventHandler(this.btnTableList_Click);
            // 
            // grdReport
            // 
            this.grdReport.AllowUserToAddRows = false;
            this.grdReport.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TableNames,
            this.NumberOfRecordsInCSV,
            this.NumberOfRecordsInSQL});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdReport.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdReport.Location = new System.Drawing.Point(40, 69);
            this.grdReport.Name = "grdReport";
            this.grdReport.ReadOnly = true;
            this.grdReport.Size = new System.Drawing.Size(577, 429);
            this.grdReport.TabIndex = 7;
            this.grdReport.Visible = false;
            // 
            // TableNames
            // 
            this.TableNames.HeaderText = "Table Names";
            this.TableNames.Name = "TableNames";
            this.TableNames.ReadOnly = true;
            this.TableNames.Width = 213;
            // 
            // NumberOfRecordsInCSV
            // 
            this.NumberOfRecordsInCSV.HeaderText = "Number Of Records In CSV";
            this.NumberOfRecordsInCSV.Name = "NumberOfRecordsInCSV";
            this.NumberOfRecordsInCSV.ReadOnly = true;
            this.NumberOfRecordsInCSV.Width = 160;
            // 
            // NumberOfRecordsInSQL
            // 
            this.NumberOfRecordsInSQL.HeaderText = "Number Of Records In SQL";
            this.NumberOfRecordsInSQL.Name = "NumberOfRecordsInSQL";
            this.NumberOfRecordsInSQL.ReadOnly = true;
            this.NumberOfRecordsInSQL.Width = 160;
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(353, 565);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(112, 31);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Get Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // grdTables
            // 
            this.grdTables.AllowUserToAddRows = false;
            this.grdTables.AllowUserToDeleteRows = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grdTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdTables.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grdTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTables.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdTables.Location = new System.Drawing.Point(40, 69);
            this.grdTables.Name = "grdTables";
            this.grdTables.Size = new System.Drawing.Size(577, 429);
            this.grdTables.TabIndex = 5;
            // 
            // pgbProgressReportBar
            // 
            this.pgbProgressReportBar.Location = new System.Drawing.Point(149, 520);
            this.pgbProgressReportBar.Name = "pgbProgressReportBar";
            this.pgbProgressReportBar.Size = new System.Drawing.Size(330, 23);
            this.pgbProgressReportBar.TabIndex = 4;
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(198, 565);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(112, 31);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload to SQL";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(40, 565);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(112, 31);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert To CSV";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbConvert);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(29, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 669);
            this.tabControl1.TabIndex = 1;
            // 
            // tbConvert
            // 
            this.tbConvert.BackColor = System.Drawing.Color.Transparent;
            this.tbConvert.Controls.Add(this.panel1);
            this.tbConvert.Location = new System.Drawing.Point(4, 22);
            this.tbConvert.Name = "tbConvert";
            this.tbConvert.Padding = new System.Windows.Forms.Padding(3);
            this.tbConvert.Size = new System.Drawing.Size(702, 643);
            this.tbConvert.TabIndex = 1;
            this.tbConvert.Text = "Convert And Upload";
            // 
            // Utility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 702);
            this.Controls.Add(this.tabControl1);
            this.Name = "Utility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utility";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbConvert.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbConvert;
        private System.Windows.Forms.ProgressBar pgbProgressReportBar;
        private System.Windows.Forms.DataGridView grdTables;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DataGridView grdReport;
        private System.Windows.Forms.Button btnTableList;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfRecordsInCSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfRecordsInSQL;
        private System.Windows.Forms.ComboBox cmbTableList;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblTableTypes;
    }
}

