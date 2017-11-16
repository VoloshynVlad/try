namespace WordReader
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.parseDocButton = new System.Windows.Forms.Button();
            this.saveToDBButton = new System.Windows.Forms.Button();
            this.selectFirstDBButton = new System.Windows.Forms.Button();
            this.lecturersComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectDocButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupsComboBox = new System.Windows.Forms.ComboBox();
            this.subjectsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.firstDBViewer = new System.Windows.Forms.DataGridView();
            this.secondDBViewer = new System.Windows.Forms.DataGridView();
            this.secondDBPathLabel = new System.Windows.Forms.Label();
            this.selectSecondDBButton = new System.Windows.Forms.Button();
            this.compareTablesButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.parsingStatusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.parsingStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.filters = new System.Windows.Forms.GroupBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.firstBDPath = new System.Windows.Forms.Label();
            this.comparationCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.paresTextBox = new System.Windows.Forms.TextBox();
            this.docPathTextBox = new System.Windows.Forms.TextBox();
            this.dbPathTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.db2PathTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.firstDBViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondDBViewer)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // parseDocButton
            // 
            this.parseDocButton.Location = new System.Drawing.Point(3, 32);
            this.parseDocButton.Name = "parseDocButton";
            this.parseDocButton.Size = new System.Drawing.Size(75, 23);
            this.parseDocButton.TabIndex = 0;
            this.parseDocButton.Text = "Parse Doc";
            this.parseDocButton.UseVisualStyleBackColor = true;
            this.parseDocButton.Click += new System.EventHandler(this.parseDocButton_Click);
            // 
            // saveToDBButton
            // 
            this.saveToDBButton.Location = new System.Drawing.Point(87, 32);
            this.saveToDBButton.Name = "saveToDBButton";
            this.saveToDBButton.Size = new System.Drawing.Size(85, 23);
            this.saveToDBButton.TabIndex = 2;
            this.saveToDBButton.Text = "Save parsed to DB";
            this.saveToDBButton.UseVisualStyleBackColor = true;
            this.saveToDBButton.Click += new System.EventHandler(this.saveToDBButton_Click);
            // 
            // selectFirstDBButton
            // 
            this.selectFirstDBButton.Location = new System.Drawing.Point(3, 61);
            this.selectFirstDBButton.Name = "selectFirstDBButton";
            this.selectFirstDBButton.Size = new System.Drawing.Size(75, 23);
            this.selectFirstDBButton.TabIndex = 3;
            this.selectFirstDBButton.Text = "Open DB";
            this.selectFirstDBButton.UseVisualStyleBackColor = true;
            this.selectFirstDBButton.Click += new System.EventHandler(this.selectFirstDBButton_Click);
            // 
            // lecturersComboBox
            // 
            this.lecturersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lecturersComboBox.FormattingEnabled = true;
            this.lecturersComboBox.Location = new System.Drawing.Point(20, 35);
            this.lecturersComboBox.Name = "lecturersComboBox";
            this.lecturersComboBox.Size = new System.Drawing.Size(221, 21);
            this.lecturersComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "List of lecturers";
            // 
            // selectDocButton
            // 
            this.selectDocButton.Location = new System.Drawing.Point(3, 3);
            this.selectDocButton.Name = "selectDocButton";
            this.selectDocButton.Size = new System.Drawing.Size(75, 23);
            this.selectDocButton.TabIndex = 6;
            this.selectDocButton.Text = "Select Doc";
            this.selectDocButton.UseVisualStyleBackColor = true;
            this.selectDocButton.Click += new System.EventHandler(this.selectDocButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(84, 8);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(54, 13);
            this.pathLabel.TabIndex = 7;
            this.pathLabel.Text = "Doc path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "List of groups";
            // 
            // groupsComboBox
            // 
            this.groupsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupsComboBox.FormattingEnabled = true;
            this.groupsComboBox.Location = new System.Drawing.Point(247, 35);
            this.groupsComboBox.Name = "groupsComboBox";
            this.groupsComboBox.Size = new System.Drawing.Size(102, 21);
            this.groupsComboBox.TabIndex = 9;
            // 
            // subjectsComboBox
            // 
            this.subjectsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subjectsComboBox.FormattingEnabled = true;
            this.subjectsComboBox.Location = new System.Drawing.Point(20, 79);
            this.subjectsComboBox.Name = "subjectsComboBox";
            this.subjectsComboBox.Size = new System.Drawing.Size(221, 21);
            this.subjectsComboBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "List of subjects";
            // 
            // firstDBViewer
            // 
            this.firstDBViewer.AllowUserToAddRows = false;
            this.firstDBViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.firstDBViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.firstDBViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstDBViewer.Location = new System.Drawing.Point(0, 0);
            this.firstDBViewer.Name = "firstDBViewer";
            this.firstDBViewer.ReadOnly = true;
            this.firstDBViewer.Size = new System.Drawing.Size(650, 313);
            this.firstDBViewer.TabIndex = 12;
            this.firstDBViewer.Scroll += new System.Windows.Forms.ScrollEventHandler(this.firstDBViewer_Scroll);
            this.firstDBViewer.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.firstDBViewer_SortCompare);
            this.firstDBViewer.Sorted += new System.EventHandler(this.firstDBViewer_Sorted);
            this.firstDBViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.firstDBViewer_KeyDown);
            // 
            // secondDBViewer
            // 
            this.secondDBViewer.AllowUserToAddRows = false;
            this.secondDBViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondDBViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.secondDBViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secondDBViewer.Location = new System.Drawing.Point(3, 32);
            this.secondDBViewer.Name = "secondDBViewer";
            this.secondDBViewer.ReadOnly = true;
            this.secondDBViewer.Size = new System.Drawing.Size(641, 191);
            this.secondDBViewer.TabIndex = 1;
            this.secondDBViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.secondDBViewer_KeyDown);
            // 
            // secondDBPathLabel
            // 
            this.secondDBPathLabel.AutoSize = true;
            this.secondDBPathLabel.Location = new System.Drawing.Point(79, 8);
            this.secondDBPathLabel.Name = "secondDBPathLabel";
            this.secondDBPathLabel.Size = new System.Drawing.Size(55, 13);
            this.secondDBPathLabel.TabIndex = 13;
            this.secondDBPathLabel.Text = "DB2path: ";
            // 
            // selectSecondDBButton
            // 
            this.selectSecondDBButton.Location = new System.Drawing.Point(3, 3);
            this.selectSecondDBButton.Name = "selectSecondDBButton";
            this.selectSecondDBButton.Size = new System.Drawing.Size(75, 23);
            this.selectSecondDBButton.TabIndex = 3;
            this.selectSecondDBButton.Text = "Open DB 2";
            this.selectSecondDBButton.UseVisualStyleBackColor = true;
            this.selectSecondDBButton.Click += new System.EventHandler(this.selectSecondDBButton_Click);
            // 
            // compareTablesButton
            // 
            this.compareTablesButton.Location = new System.Drawing.Point(3, 90);
            this.compareTablesButton.Name = "compareTablesButton";
            this.compareTablesButton.Size = new System.Drawing.Size(131, 24);
            this.compareTablesButton.TabIndex = 15;
            this.compareTablesButton.Text = "Compare tables";
            this.compareTablesButton.UseVisualStyleBackColor = true;
            this.compareTablesButton.Visible = false;
            this.compareTablesButton.Click += new System.EventHandler(this.compareTablesButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parsingStatusBar,
            this.parsingStatusStrip});
            this.statusStrip.Location = new System.Drawing.Point(0, 440);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(928, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 16;
            this.statusStrip.Text = "statusStrip1";
            // 
            // parsingStatusBar
            // 
            this.parsingStatusBar.Name = "parsingStatusBar";
            this.parsingStatusBar.Size = new System.Drawing.Size(100, 16);
            // 
            // parsingStatusStrip
            // 
            this.parsingStatusStrip.Name = "parsingStatusStrip";
            this.parsingStatusStrip.Size = new System.Drawing.Size(0, 17);
            // 
            // filters
            // 
            this.filters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filters.Controls.Add(this.label4);
            this.filters.Controls.Add(this.subjectsComboBox);
            this.filters.Controls.Add(this.groupsComboBox);
            this.filters.Controls.Add(this.filterButton);
            this.filters.Controls.Add(this.label3);
            this.filters.Controls.Add(this.label1);
            this.filters.Controls.Add(this.lecturersComboBox);
            this.filters.Location = new System.Drawing.Point(279, 4);
            this.filters.Name = "filters";
            this.filters.Size = new System.Drawing.Size(365, 110);
            this.filters.TabIndex = 17;
            this.filters.TabStop = false;
            this.filters.Text = "Filters";
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(247, 77);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(102, 23);
            this.filterButton.TabIndex = 19;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // firstBDPath
            // 
            this.firstBDPath.AutoSize = true;
            this.firstBDPath.Location = new System.Drawing.Point(82, 66);
            this.firstBDPath.Name = "firstBDPath";
            this.firstBDPath.Size = new System.Drawing.Size(52, 13);
            this.firstBDPath.TabIndex = 13;
            this.firstBDPath.Text = "DB path: ";
            // 
            // comparationCheckBox
            // 
            this.comparationCheckBox.AutoSize = true;
            this.comparationCheckBox.Location = new System.Drawing.Point(178, 36);
            this.comparationCheckBox.Name = "comparationCheckBox";
            this.comparationCheckBox.Size = new System.Drawing.Size(85, 17);
            this.comparationCheckBox.TabIndex = 18;
            this.comparationCheckBox.Text = "Comparation";
            this.comparationCheckBox.UseVisualStyleBackColor = true;
            this.comparationCheckBox.CheckedChanged += new System.EventHandler(this.comparationCheckBox_CheckedChanged);
            // 
            // paresTextBox
            // 
            this.paresTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paresTextBox.Location = new System.Drawing.Point(0, 0);
            this.paresTextBox.Multiline = true;
            this.paresTextBox.Name = "paresTextBox";
            this.paresTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.paresTextBox.Size = new System.Drawing.Size(274, 440);
            this.paresTextBox.TabIndex = 20;
            // 
            // docPathTextBox
            // 
            this.docPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.docPathTextBox.Location = new System.Drawing.Point(141, 4);
            this.docPathTextBox.Name = "docPathTextBox";
            this.docPathTextBox.ReadOnly = true;
            this.docPathTextBox.Size = new System.Drawing.Size(132, 20);
            this.docPathTextBox.TabIndex = 21;
            // 
            // dbPathTextBox
            // 
            this.dbPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbPathTextBox.Location = new System.Drawing.Point(141, 61);
            this.dbPathTextBox.Name = "dbPathTextBox";
            this.dbPathTextBox.ReadOnly = true;
            this.dbPathTextBox.Size = new System.Drawing.Size(132, 20);
            this.dbPathTextBox.TabIndex = 21;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 650;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.paresTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(928, 440);
            this.splitContainer1.SplitterDistance = 650;
            this.splitContainer1.TabIndex = 22;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.saveToDBButton);
            this.splitContainer2.Panel1.Controls.Add(this.dbPathTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.parseDocButton);
            this.splitContainer2.Panel1.Controls.Add(this.docPathTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.selectFirstDBButton);
            this.splitContainer2.Panel1.Controls.Add(this.comparationCheckBox);
            this.splitContainer2.Panel1.Controls.Add(this.selectDocButton);
            this.splitContainer2.Panel1.Controls.Add(this.filters);
            this.splitContainer2.Panel1.Controls.Add(this.pathLabel);
            this.splitContainer2.Panel1.Controls.Add(this.firstBDPath);
            this.splitContainer2.Panel1.Controls.Add(this.compareTablesButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.splitContainer2.Size = new System.Drawing.Size(650, 440);
            this.splitContainer2.SplitterDistance = 123;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.firstDBViewer);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.secondDBViewer);
            this.splitContainer3.Panel2.Controls.Add(this.db2PathTextBox);
            this.splitContainer3.Panel2.Controls.Add(this.selectSecondDBButton);
            this.splitContainer3.Panel2.Controls.Add(this.secondDBPathLabel);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Size = new System.Drawing.Size(650, 313);
            this.splitContainer3.SplitterDistance = 83;
            this.splitContainer3.TabIndex = 14;
            // 
            // db2PathTextBox
            // 
            this.db2PathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.db2PathTextBox.Location = new System.Drawing.Point(136, 5);
            this.db2PathTextBox.Name = "db2PathTextBox";
            this.db2PathTextBox.ReadOnly = true;
            this.db2PathTextBox.Size = new System.Drawing.Size(132, 20);
            this.db2PathTextBox.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.Text = "Consultations";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.firstDBViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondDBViewer)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button parseDocButton;
        private System.Windows.Forms.Button saveToDBButton;
        private System.Windows.Forms.Button selectFirstDBButton;
        private System.Windows.Forms.ComboBox lecturersComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectDocButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox groupsComboBox;
        private System.Windows.Forms.ComboBox subjectsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView firstDBViewer;
        private System.Windows.Forms.DataGridView secondDBViewer;
        private System.Windows.Forms.Label secondDBPathLabel;
        private System.Windows.Forms.Button selectSecondDBButton;
        private System.Windows.Forms.Button compareTablesButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar parsingStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel parsingStatusStrip;
        private System.Windows.Forms.GroupBox filters;
        private System.Windows.Forms.Label firstBDPath;
        private System.Windows.Forms.CheckBox comparationCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.TextBox paresTextBox;
        private System.Windows.Forms.TextBox docPathTextBox;
        private System.Windows.Forms.TextBox dbPathTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox db2PathTextBox;
    }
}