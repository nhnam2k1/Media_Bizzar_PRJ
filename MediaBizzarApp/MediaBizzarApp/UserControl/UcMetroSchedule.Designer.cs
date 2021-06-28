
namespace MediaBizzarApp 
{ 
    partial class UcMetroSchedule
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calSchedule = new System.Windows.Forms.MonthCalendar();
            this.btnEditShift = new MetroFramework.Controls.MetroButton();
            this.btnGeneratingShifts = new MetroFramework.Controls.MetroButton();
            this.lvShiftProblems = new MetroFramework.Controls.MetroListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvScheduleOverview = new MetroFramework.Controls.MetroListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxSearchName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cbxSession = new MetroFramework.Controls.MetroComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calSchedule
            // 
            this.calSchedule.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.calSchedule.Location = new System.Drawing.Point(9, 9);
            this.calSchedule.Name = "calSchedule";
            this.calSchedule.TabIndex = 0;
            this.calSchedule.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calSchedule_DateChanged);
            // 
            // btnEditShift
            // 
            this.btnEditShift.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEditShift.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnEditShift.Location = new System.Drawing.Point(9, 367);
            this.btnEditShift.Name = "btnEditShift";
            this.btnEditShift.Size = new System.Drawing.Size(227, 42);
            this.btnEditShift.TabIndex = 4;
            this.btnEditShift.Text = "Edit Shift";
            this.btnEditShift.UseCustomBackColor = true;
            this.btnEditShift.UseSelectable = true;
            this.btnEditShift.Click += new System.EventHandler(this.btnEditShift_Click);
            // 
            // btnGeneratingShifts
            // 
            this.btnGeneratingShifts.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGeneratingShifts.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnGeneratingShifts.Location = new System.Drawing.Point(9, 415);
            this.btnGeneratingShifts.Name = "btnGeneratingShifts";
            this.btnGeneratingShifts.Size = new System.Drawing.Size(227, 42);
            this.btnGeneratingShifts.TabIndex = 6;
            this.btnGeneratingShifts.Text = "Generating Shifts";
            this.btnGeneratingShifts.UseCustomBackColor = true;
            this.btnGeneratingShifts.UseSelectable = true;
            this.btnGeneratingShifts.Click += new System.EventHandler(this.btnGeneratingShifts_Click);
            // 
            // lvShiftProblems
            // 
            this.lvShiftProblems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvShiftProblems.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lvShiftProblems.FullRowSelect = true;
            this.lvShiftProblems.Location = new System.Drawing.Point(6, 19);
            this.lvShiftProblems.Name = "lvShiftProblems";
            this.lvShiftProblems.OwnerDraw = true;
            this.lvShiftProblems.Size = new System.Drawing.Size(415, 424);
            this.lvShiftProblems.TabIndex = 7;
            this.lvShiftProblems.UseCompatibleStateImageBehavior = false;
            this.lvShiftProblems.UseSelectable = true;
            this.lvShiftProblems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 84;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Session";
            this.columnHeader2.Width = 78;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Problem";
            this.columnHeader3.Width = 241;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lvShiftProblems);
            this.groupBox1.Location = new System.Drawing.Point(242, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 449);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Shifts Problems";
            // 
            // lvScheduleOverview
            // 
            this.lvScheduleOverview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvScheduleOverview.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lvScheduleOverview.FullRowSelect = true;
            this.lvScheduleOverview.Location = new System.Drawing.Point(675, 28);
            this.lvScheduleOverview.Name = "lvScheduleOverview";
            this.lvScheduleOverview.OwnerDraw = true;
            this.lvScheduleOverview.Size = new System.Drawing.Size(415, 430);
            this.lvScheduleOverview.TabIndex = 8;
            this.lvScheduleOverview.UseCompatibleStateImageBehavior = false;
            this.lvScheduleOverview.UseSelectable = true;
            this.lvScheduleOverview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date";
            this.columnHeader4.Width = 84;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Session";
            this.columnHeader5.Width = 78;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Employee Name";
            this.columnHeader6.Width = 241;
            // 
            // tbxSearchName
            // 
            // 
            // 
            // 
            this.tbxSearchName.CustomButton.Image = null;
            this.tbxSearchName.CustomButton.Location = new System.Drawing.Point(342, 1);
            this.tbxSearchName.CustomButton.Name = "";
            this.tbxSearchName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbxSearchName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbxSearchName.CustomButton.TabIndex = 1;
            this.tbxSearchName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbxSearchName.CustomButton.UseSelectable = true;
            this.tbxSearchName.CustomButton.Visible = false;
            this.tbxSearchName.Lines = new string[0];
            this.tbxSearchName.Location = new System.Drawing.Point(726, 3);
            this.tbxSearchName.MaxLength = 32767;
            this.tbxSearchName.Name = "tbxSearchName";
            this.tbxSearchName.PasswordChar = '\0';
            this.tbxSearchName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxSearchName.SelectedText = "";
            this.tbxSearchName.SelectionLength = 0;
            this.tbxSearchName.SelectionStart = 0;
            this.tbxSearchName.ShortcutsEnabled = true;
            this.tbxSearchName.Size = new System.Drawing.Size(364, 23);
            this.tbxSearchName.TabIndex = 9;
            this.tbxSearchName.UseSelectable = true;
            this.tbxSearchName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbxSearchName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbxSearchName.TextChanged += new System.EventHandler(this.tbxSearchName_TextChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(675, 6);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(45, 19);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "Name";
            // 
            // cbxSession
            // 
            this.cbxSession.FormattingEnabled = true;
            this.cbxSession.ItemHeight = 23;
            this.cbxSession.Location = new System.Drawing.Point(9, 332);
            this.cbxSession.Name = "cbxSession";
            this.cbxSession.Size = new System.Drawing.Size(227, 29);
            this.cbxSession.TabIndex = 3;
            this.cbxSession.UseSelectable = true;
            // 
            // UcMetroSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.tbxSearchName);
            this.Controls.Add(this.lvScheduleOverview);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGeneratingShifts);
            this.Controls.Add(this.btnEditShift);
            this.Controls.Add(this.cbxSession);
            this.Controls.Add(this.calSchedule);
            this.Name = "UcMetroSchedule";
            this.Size = new System.Drawing.Size(1379, 786);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar calSchedule;
        private MetroFramework.Controls.MetroButton btnEditShift;
        private MetroFramework.Controls.MetroButton btnGeneratingShifts;
        private MetroFramework.Controls.MetroListView lvShiftProblems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroListView lvScheduleOverview;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private MetroFramework.Controls.MetroTextBox tbxSearchName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cbxSession;
    }
}
