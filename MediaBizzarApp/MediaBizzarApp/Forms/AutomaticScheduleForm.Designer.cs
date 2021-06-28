
namespace MediaBizzarApp
{
    partial class AutomaticScheduleForm
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
            this.lvShiftsView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDepartments = new System.Windows.Forms.ComboBox();
            this.btnGenerating = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvShiftsLimit = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvEmployees = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.lblFTE = new System.Windows.Forms.Label();
            this.lblCurrentFTE = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxWeeks = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftsLimit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvShiftsView
            // 
            this.lvShiftsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvShiftsView.FullRowSelect = true;
            this.lvShiftsView.HideSelection = false;
            this.lvShiftsView.Location = new System.Drawing.Point(6, 19);
            this.lvShiftsView.Name = "lvShiftsView";
            this.lvShiftsView.Size = new System.Drawing.Size(342, 635);
            this.lvShiftsView.TabIndex = 0;
            this.lvShiftsView.UseCompatibleStateImageBehavior = false;
            this.lvShiftsView.View = System.Windows.Forms.View.Details;
            this.lvShiftsView.SelectedIndexChanged += new System.EventHandler(this.lvShiftsView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Session";
            this.columnHeader2.Width = 86;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Employee ";
            this.columnHeader3.Width = 145;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvShiftsView);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 660);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shifts";
            // 
            // cbxDepartments
            // 
            this.cbxDepartments.FormattingEnabled = true;
            this.cbxDepartments.Location = new System.Drawing.Point(379, 26);
            this.cbxDepartments.Name = "cbxDepartments";
            this.cbxDepartments.Size = new System.Drawing.Size(121, 21);
            this.cbxDepartments.TabIndex = 2;
            // 
            // btnGenerating
            // 
            this.btnGenerating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerating.Location = new System.Drawing.Point(379, 621);
            this.btnGenerating.Name = "btnGenerating";
            this.btnGenerating.Size = new System.Drawing.Size(102, 51);
            this.btnGenerating.TabIndex = 3;
            this.btnGenerating.Text = "Generating";
            this.btnGenerating.UseVisualStyleBackColor = true;
            this.btnGenerating.Click += new System.EventHandler(this.btnGenerating_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(487, 621);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 51);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Department";
            // 
            // dgvShiftsLimit
            // 
            this.dgvShiftsLimit.AllowUserToAddRows = false;
            this.dgvShiftsLimit.AllowUserToDeleteRows = false;
            this.dgvShiftsLimit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShiftsLimit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvShiftsLimit.Location = new System.Drawing.Point(587, 57);
            this.dgvShiftsLimit.MultiSelect = false;
            this.dgvShiftsLimit.Name = "dgvShiftsLimit";
            this.dgvShiftsLimit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShiftsLimit.Size = new System.Drawing.Size(327, 522);
            this.dgvShiftsLimit.TabIndex = 8;
            this.dgvShiftsLimit.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShiftsLimit_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Date";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Session";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Employee Limit";
            this.Column3.Name = "Column3";
            // 
            // lvEmployees
            // 
            this.lvEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.lvEmployees.FullRowSelect = true;
            this.lvEmployees.HideSelection = false;
            this.lvEmployees.Location = new System.Drawing.Point(379, 57);
            this.lvEmployees.Name = "lvEmployees";
            this.lvEmployees.Size = new System.Drawing.Size(202, 558);
            this.lvEmployees.TabIndex = 9;
            this.lvEmployees.UseCompatibleStateImageBehavior = false;
            this.lvEmployees.View = System.Windows.Forms.View.Details;
            this.lvEmployees.DoubleClick += new System.EventHandler(this.lvEmployees_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Employee";
            this.columnHeader4.Width = 130;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblEmployeeName);
            this.groupBox2.Controls.Add(this.lblFTE);
            this.groupBox2.Controls.Add(this.lblCurrentFTE);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(587, 585);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 87);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Overview";
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.AutoSize = true;
            this.lblEmployeeName.Location = new System.Drawing.Point(101, 16);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(35, 13);
            this.lblEmployeeName.TabIndex = 5;
            this.lblEmployeeName.Text = "label9";
            // 
            // lblFTE
            // 
            this.lblFTE.AutoSize = true;
            this.lblFTE.Location = new System.Drawing.Point(101, 38);
            this.lblFTE.Name = "lblFTE";
            this.lblFTE.Size = new System.Drawing.Size(35, 13);
            this.lblFTE.TabIndex = 4;
            this.lblFTE.Text = "label8";
            // 
            // lblCurrentFTE
            // 
            this.lblCurrentFTE.AutoSize = true;
            this.lblCurrentFTE.Location = new System.Drawing.Point(101, 57);
            this.lblCurrentFTE.Name = "lblCurrentFTE";
            this.lblCurrentFTE.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentFTE.TabIndex = 3;
            this.lblCurrentFTE.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Current FTE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Employee FTE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Employee Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(587, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Weeks";
            // 
            // cbxWeeks
            // 
            this.cbxWeeks.FormattingEnabled = true;
            this.cbxWeeks.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.cbxWeeks.Location = new System.Drawing.Point(587, 26);
            this.cbxWeeks.Name = "cbxWeeks";
            this.cbxWeeks.Size = new System.Drawing.Size(143, 21);
            this.cbxWeeks.TabIndex = 11;
            this.cbxWeeks.SelectedIndexChanged += new System.EventHandler(this.cbxWeeks_SelectedIndexChanged);
            // 
            // AutomaticScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 679);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxWeeks);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lvEmployees);
            this.Controls.Add(this.dgvShiftsLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerating);
            this.Controls.Add(this.cbxDepartments);
            this.Controls.Add(this.groupBox1);
            this.Name = "AutomaticScheduleForm";
            this.Text = "Automatic Scheduling Form";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftsLimit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvShiftsView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxDepartments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnGenerating;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvShiftsLimit;
        private System.Windows.Forms.ListView lvEmployees;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label lblFTE;
        private System.Windows.Forms.Label lblCurrentFTE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxWeeks;
    }
}