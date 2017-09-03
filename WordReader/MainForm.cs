using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace WordReader
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Контроллер всей формы.
        /// </summary>
        MainController mainController;

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            mainController = new MainController();
        }

        #region Обработчики событий.

        /// <summary>
        /// Обработка события нажатия кнопки, которая выбирает текстовый документ 
        /// формата .doc или .docx, который необходимо распарсить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectDocButton_Click(object sender, EventArgs e)
        {
            SelectDocument();
        }

        /// <summary>
        /// Обработка события нажатия кнопки
        /// которая занимается считыванием документа 
        /// и созданием List'a объектов Consultation
        /// по считанным данным заполняет 
        /// LecturersComboBox, SubjectsComboBox и GroupsComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parseDocButton_Click(object sender, EventArgs e)
        {
            // TODO: вынести в отдельный метод
            int exitCode = 0;

            parsingStatusStrip.Text = "Parsing started...";

            this.mainController.ClearConsultationArray();

            lecturersComboBox.Items.Clear();
            subjectsComboBox.Items.Clear();
            groupsComboBox.Items.Clear();

            try
            {
                ParseDocument();

                //if (firstDBViewer.RowCount == 0)
                //{
                BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                firstDBViewer.DataSource = bind;
                //}
                //else
                //{
                //    BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                //    secondDBViewer.DataSource = bind;
                //}
            }
            catch
            {
                System.Runtime.InteropServices.COMException exp;
                {
                    firstDBViewer.DataSource = null;
                    secondDBViewer.DataSource = null;
                    MessageBox.Show("You must choose file first!");
                    exitCode = -1;
                };
            }
            finally
            {
                switch (exitCode)
                {
                    case 0:
                        parsingStatusStrip.Text = "Done!";
                        break;

                    case -1:
                        parsingStatusStrip.Text = "Error!";
                        firstDBViewer.DataSource = null;

                        break;
                }
            }
        }

        /// <summary>
        /// Обработка события нажатия кнопки
        /// которая создает базу данных и записывает 
        /// в нее считанную информацию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToDBButton_Click(object sender, EventArgs e)
        {
            // TODO: вынести в отдельный метод
            try
            {
                label5.Text = "";
                this.mainController.PathDB = "";
                string path = SelectPathToSaveDB();
                label5.Text = path;
                this.mainController.PathDB = path;

                if (firstDBViewer.RowCount == 0)
                {
                    MessageBox.Show("Nothing to save to DB");
                }
                else
                {
                    if (!this.mainController.SaveToDB(path))
                        MessageBox.Show("DB with such name already exists");
                }
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show("DB must have name.");
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки для выбора первой базы данных,
        /// с которой будет сравниваться вторая база данных.
        /// Заполнение элемента FirstDBViewer данными из базы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void selectFirstDBButton_Click(object sender, EventArgs e)
        {
            string path = SelectDB();
            this.mainController.PathDB = path;

            if (path != "")
            {
                try
                {
                    if (this.mainController.CheckDB(path))
                        firstDBViewer.DataSource = this.mainController.FillDB(path);
                    else
                        MessageBox.Show("The DB is incorrect!");
                }
                catch
                {
                    Exception exp;
                }
            }
            else
                MessageBox.Show("Choose BD first");
        }

        /// <summary>
        /// Обработчик события выбора пользователя режима "Сравнения"
        /// или "Просмотр"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comparationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (comparationCheckBox.Checked)
            {
                selectSecondDBButton.Visible = true;
                compareTablesButton.Visible = true;
                secondDBViewer.Visible = true;
                label6.Visible = true;
                this.Size = new System.Drawing.Size(1146, 666);
            }
            else
            {
                selectSecondDBButton.Visible = false;
                compareTablesButton.Visible = false;
                secondDBViewer.Visible = false;
                label6.Visible = false;
                this.Size = new System.Drawing.Size(1146, 424);
            }
        }

        /// <summary>
        /// Нажатие кнопки для выбора второй базы данных для сранения.
        /// Заполнение элемента SecondDBViewer данными из базы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSecondDBButton_Click(object sender, EventArgs e)
        {
            string path = SelectDB();
            this.mainController.PathForComparedDB = path;

            if (path != "")
                secondDBViewer.DataSource = this.mainController.FillDB(path);
            else
                ;
        }

        /// <summary>
        /// Нажатие кнопки сравнения содержимого таблиц и отображения различий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compareTablesButton_Click(object sender, EventArgs e)
        {
            // TODO: вынести в отдельный метод

            string firstTableData = "";
            string secondTableData = "";
            List<string> firstTableCollection = new List<string>();
            //if (firstDBViewer.RowCount == secondDBViewer.RowCount)
            //{        
            for (int i = 0; i < firstDBViewer.RowCount; i++)
            {
                for (int j = 0; j < firstDBViewer.ColumnCount; j++)
                {
                    firstTableData += firstDBViewer.Rows[i].Cells[j].Value.ToString() + " ";

                    // ИГС: в чем смысл внешнего цикла, который прерывается сразу после внутреннего цикла? Внутренние скипы его все равно не тронут.
                    // и стоит добавить обычных комментариев в происходящее здесь. Мол, при совпадении занчений - такой-то результат, при различии - такой-то.
                    //while (true)
                    //{
                    for (int k = 0; k < secondDBViewer.RowCount; k++)
                    {
                        for (int l = 0; l < secondDBViewer.ColumnCount; l++)
                        {
                            secondTableData += secondDBViewer.Rows[k].Cells[l].Value.ToString() + " ";
                        }

                        if (firstTableData == secondTableData)
                        {
                            for (int a = 0; a < firstDBViewer.ColumnCount; a++)
                                firstDBViewer.Rows[i].Cells[a].Style.BackColor = Color.Green;

                            for (int c = 0; c < secondDBViewer.ColumnCount; c++)
                                secondDBViewer.Rows[k].Cells[c].Style.BackColor = Color.Green;

                            secondTableData = "";
                        }
                        else
                        {
                            //firstDBViewer.Rows[i].Cells[j].Style.BackColor = Color.Red;

                            secondTableData = "";
                            continue;
                        }
                    }
                    // break;
                    // }
                    //secondTableData += secondDBViewer.Rows[i].Cells[j].Value.ToString();

                    //if (first == second) 
                    //{
                    //    firstDBViewer.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    //    secondDBViewer.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    //}
                    //else
                    //{
                    //    for (int l = 0; l < secondDBViewer.ColumnCount; l++)
                    //    {
                    //        secondDBViewer.Rows[i].Cells[k].Style.BackColor = Color.Wheat;
                    //        firstDBViewer.Rows[i].Cells[k].Style.BackColor = Color.Red;
                    //    }
                    //}
                }
                firstTableCollection.Add(firstTableData);
                firstTableData = "";
            }
        }

        /// <summary>
        /// Синхронная прокрутка данных в dataGridView'ах.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstDBViewer_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                secondDBViewer.FirstDisplayedScrollingRowIndex = firstDBViewer.FirstDisplayedScrollingRowIndex;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        #endregion

        #region Логика.

        /// <summary>
        /// Выбор базы данных.
        /// </summary>
        private string SelectDB()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = this.mainController.ApplicationPath;
            ofd.Filter = "Файлы SQLite (*.db) | *.db";

            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return ofd.FileName;
            }
        }

        /// <summary>
        /// Запись данных в базу данных.
        /// </summary>
        private string SelectPathToSaveDB()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = this.mainController.ApplicationPath;
            sfd.Filter = "Файлы SQLite (*.db) | *.db";
            sfd.FileName = DateTime.Now.ToString().Replace(':', '-') + ".db";
            DialogResult dr = sfd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Выбор документа формата .doc или .docx, который необходимо распарсить.
        /// </summary>
        private void SelectDocument()
        {
            this.mainController.SelectedDocument = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();//this.mainController.ApplicationPath;
            ofd.Filter = "Файлы Word (*.doc; *.docx) | *.doc; *.docx";
            DialogResult dr = ofd.ShowDialog();
            string path = "";

            if (dr == DialogResult.OK)
            {
                pathLabel.Text = "";
                path = ofd.FileName;
                pathLabel.Text = path;
                this.mainController.SelectedDocument = path;
            }
            else if (dr == DialogResult.Cancel || dr == DialogResult.Abort)
            {
                path = pathLabel.Text;
                this.mainController.SelectedDocument = path;
            }
            else
            {
                MessageBox.Show("Something went wrong :c");
            }
        }

        /// <summary>
        /// Парсинг документа Word.
        /// </summary>
        private void ParseDocument()
        {
            if (this.mainController.ParseDocument() == "OK")
            {
                lecturersComboBox.Items.Clear();
                groupsComboBox.Items.Clear();
                subjectsComboBox.Items.Clear();

                lecturersComboBox.Items.Add("All");
                groupsComboBox.Items.Add("All");
                subjectsComboBox.Items.Add("All");

                string[] lecturers = this.mainController.Lecturers;
                string[] groups = this.mainController.Groups;
                string[] subjects = this.mainController.Subjects;

                for (int i = 1; i < lecturers.Length; i++)
                    lecturersComboBox.Items.Add(lecturers[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < groups.Length; i++)
                    groupsComboBox.Items.Add(groups[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < subjects.Length; i++)
                    subjectsComboBox.Items.Add(subjects[i].Trim(new Char[] { '\r', '\a' }));

                lecturersComboBox.SelectedIndex = 0;
                groupsComboBox.SelectedIndex = 0;
                subjectsComboBox.SelectedIndex = 0;

                parsingStatusStrip.Text = "Done!";
            }
        }
        #endregion

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)      
            {
                if (e.KeyCode == Keys.S)
                {
                    saveToDBButton.PerformClick();
                    e.SuppressKeyPress = true;  
                }
                else if (e.KeyCode == Keys.O)
                {
                    selectDocButton.PerformClick();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.P)
                {
                    parseDocButton.PerformClick();
                    e.SuppressKeyPress = true;  
                }
                else if (e.KeyCode == Keys.C)
                {
                    if (compareTablesButton.Visible == true)
                    {
                        compareTablesButton.PerformClick();
                        e.SuppressKeyPress = true;  
                    }
                    else
                        MessageBox.Show("Unabled function");
                }
                else if (e.KeyCode == Keys.B)
                {
                        selectFirstDBButton.PerformClick();
                        e.SuppressKeyPress = true;
                    //else if ((e.KeyCode == Keys.D2) && (selectSecondDBButton.Visible = true))
                    //{
                    //    selectSecondDBButton.PerformClick();
                    //    e.SuppressKeyPress = true;
                    //}
                    //else
                    //    e.SuppressKeyPress = false;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }


    }
}