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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupsComboBox = new System.Windows.Forms.ComboBox();
            this.subjectsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.firstDBViewer = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.secondDBViewer = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.selectSecondDBButton = new System.Windows.Forms.Button();
            this.makeQueryToFirstDBButton = new System.Windows.Forms.Button();
            this.makeQueryToSecondDBButton = new System.Windows.Forms.Button();
            this.compareTablesButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.parsingStatusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.parsingStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.firstDBViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondDBViewer)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parseDocButton
            // 
            this.parseDocButton.Location = new System.Drawing.Point(15, 89);
            this.parseDocButton.Name = "parseDocButton";
            this.parseDocButton.Size = new System.Drawing.Size(75, 23);
            this.parseDocButton.TabIndex = 0;
            this.parseDocButton.Text = "Parse Doc";
            this.parseDocButton.UseVisualStyleBackColor = true;
            this.parseDocButton.Click += new System.EventHandler(this.parseDocButton_Click);
            // 
            // saveToDBButton
            // 
            this.saveToDBButton.Location = new System.Drawing.Point(96, 89);
            this.saveToDBButton.Name = "saveToDBButton";
            this.saveToDBButton.Size = new System.Drawing.Size(75, 23);
            this.saveToDBButton.TabIndex = 2;
            this.saveToDBButton.Text = "Save to  DB";
            this.saveToDBButton.UseVisualStyleBackColor = true;
            this.saveToDBButton.Click += new System.EventHandler(this.saveToDBButton_Click);
            // 
            // selectFirstDBButton
            // 
            this.selectFirstDBButton.Location = new System.Drawing.Point(239, 88);
            this.selectFirstDBButton.Name = "selectFirstDBButton";
            this.selectFirstDBButton.Size = new System.Drawing.Size(75, 23);
            this.selectFirstDBButton.TabIndex = 3;
            this.selectFirstDBButton.Text = "Select DB";
            this.selectFirstDBButton.UseVisualStyleBackColor = true;
            this.selectFirstDBButton.Click += new System.EventHandler(this.selectFirstDBButton_Click);
            // 
            // lecturersComboBox
            // 
            this.lecturersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lecturersComboBox.FormattingEnabled = true;
            this.lecturersComboBox.Location = new System.Drawing.Point(15, 142);
            this.lecturersComboBox.Name = "lecturersComboBox";
            this.lecturersComboBox.Size = new System.Drawing.Size(221, 21);
            this.lecturersComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Список преподавателей";
            // 
            // selectDocButton
            // 
            this.selectDocButton.Location = new System.Drawing.Point(15, 13);
            this.selectDocButton.Name = "selectDocButton";
            this.selectDocButton.Size = new System.Drawing.Size(75, 23);
            this.selectDocButton.TabIndex = 6;
            this.selectDocButton.Text = "Select Doc";
            this.selectDocButton.UseVisualStyleBackColor = true;
            this.selectDocButton.Click += new System.EventHandler(this.selectDocButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Список групп";
            // 
            // groupsComboBox
            // 
            this.groupsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupsComboBox.FormattingEnabled = true;
            this.groupsComboBox.Location = new System.Drawing.Point(15, 190);
            this.groupsComboBox.Name = "groupsComboBox";
            this.groupsComboBox.Size = new System.Drawing.Size(221, 21);
            this.groupsComboBox.TabIndex = 9;
            // 
            // subjectsComboBox
            // 
            this.subjectsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subjectsComboBox.FormattingEnabled = true;
            this.subjectsComboBox.Location = new System.Drawing.Point(12, 241);
            this.subjectsComboBox.Name = "subjectsComboBox";
            this.subjectsComboBox.Size = new System.Drawing.Size(224, 21);
            this.subjectsComboBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Список предметов";
            // 
            // firstDBViewer
            // 
            this.firstDBViewer.AllowUserToAddRows = false;
            this.firstDBViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.firstDBViewer.Location = new System.Drawing.Point(239, 142);
            this.firstDBViewer.Name = "firstDBViewer";
            this.firstDBViewer.ReadOnly = true;
            this.firstDBViewer.Size = new System.Drawing.Size(1001, 214);
            this.firstDBViewer.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "DB name: ";
            // 
            // secondDBViewer
            // 
            this.secondDBViewer.AllowUserToAddRows = false;
            this.secondDBViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secondDBViewer.Location = new System.Drawing.Point(236, 431);
            this.secondDBViewer.Name = "secondDBViewer";
            this.secondDBViewer.ReadOnly = true;
            this.secondDBViewer.Size = new System.Drawing.Size(1004, 214);
            this.secondDBViewer.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 415);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "DB name: ";
            // 
            // selectSecondDBButton
            // 
            this.selectSecondDBButton.Location = new System.Drawing.Point(236, 380);
            this.selectSecondDBButton.Name = "selectSecondDBButton";
            this.selectSecondDBButton.Size = new System.Drawing.Size(75, 23);
            this.selectSecondDBButton.TabIndex = 3;
            this.selectSecondDBButton.Text = "Select DB";
            this.selectSecondDBButton.UseVisualStyleBackColor = true;
            this.selectSecondDBButton.Click += new System.EventHandler(this.selectSecondDBButton_Click);
            // 
            // makeQueryToFirstDBButton
            // 
            this.makeQueryToFirstDBButton.Location = new System.Drawing.Point(318, 88);
            this.makeQueryToFirstDBButton.Name = "makeQueryToFirstDBButton";
            this.makeQueryToFirstDBButton.Size = new System.Drawing.Size(75, 23);
            this.makeQueryToFirstDBButton.TabIndex = 14;
            this.makeQueryToFirstDBButton.Text = "Make Query";
            this.makeQueryToFirstDBButton.UseVisualStyleBackColor = true;
            this.makeQueryToFirstDBButton.Click += new System.EventHandler(this.makeQueryToFirstDBButton_Click);
            // 
            // makeQueryToSecondDBButton
            // 
            this.makeQueryToSecondDBButton.Location = new System.Drawing.Point(318, 380);
            this.makeQueryToSecondDBButton.Name = "makeQueryToSecondDBButton";
            this.makeQueryToSecondDBButton.Size = new System.Drawing.Size(75, 23);
            this.makeQueryToSecondDBButton.TabIndex = 14;
            this.makeQueryToSecondDBButton.Text = "Make Query";
            this.makeQueryToSecondDBButton.UseVisualStyleBackColor = true;
            this.makeQueryToSecondDBButton.Click += new System.EventHandler(this.makeQueryToSecondDBButton_Click);
            // 
            // compareTablesButton
            // 
            this.compareTablesButton.Location = new System.Drawing.Point(18, 402);
            this.compareTablesButton.Name = "compareTablesButton";
            this.compareTablesButton.Size = new System.Drawing.Size(96, 23);
            this.compareTablesButton.TabIndex = 15;
            this.compareTablesButton.Text = "Compare tables";
            this.compareTablesButton.UseVisualStyleBackColor = true;
            this.compareTablesButton.Click += new System.EventHandler(this.compareTablesButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parsingStatusBar,
            this.parsingStatusStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 641);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1249, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 663);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.compareTablesButton);
            this.Controls.Add(this.makeQueryToSecondDBButton);
            this.Controls.Add(this.makeQueryToFirstDBButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.secondDBViewer);
            this.Controls.Add(this.firstDBViewer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.subjectsComboBox);
            this.Controls.Add(this.groupsComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectDocButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lecturersComboBox);
            this.Controls.Add(this.selectSecondDBButton);
            this.Controls.Add(this.selectFirstDBButton);
            this.Controls.Add(this.saveToDBButton);
            this.Controls.Add(this.parseDocButton);
            this.Name = "MainForm";
            this.Text = "Работа с консультациями онлайн без регистрации и смс";
            ((System.ComponentModel.ISupportInitialize)(this.firstDBViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondDBViewer)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox groupsComboBox;
        private System.Windows.Forms.ComboBox subjectsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView firstDBViewer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView secondDBViewer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button selectSecondDBButton;
        private System.Windows.Forms.Button makeQueryToFirstDBButton;
        private System.Windows.Forms.Button makeQueryToSecondDBButton;
        private System.Windows.Forms.Button compareTablesButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar parsingStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel parsingStatusStrip;
    }
}

