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
        /// 
        /// </summary>
        MainController mainController;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            mainController = new MainController();
        }

        #region Обработчики событий.

        /// <summary>
        /// Обработка события нажатия кнопки
        /// которая выбирает текстовый документ 
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

            this.mainController.ClearConsultationArray();

            lecturersComboBox.Items.Clear();
            subjectsComboBox.Items.Clear();
            groupsComboBox.Items.Clear();

            ParseDocument();


            if (firstDBViewer.RowCount == 0)
            {
                BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                firstDBViewer.DataSource = bind;
            }
            else
            {
                BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                secondDBViewer.DataSource = bind;
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

                if (!this.mainController.SaveToDB(path))
                    MessageBox.Show("База данных c таким названием уже существует");
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show("Пустое имя пути не допускается.");
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки
        /// для выбора первой базы данных
        /// с которой будет сравниваться вторая база данных
        /// Заполнение элемента FirstDBViewer данными из базы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void selectFirstDBButton_Click(object sender, EventArgs e)
        {
            string path = SelectDB();
            this.mainController.PathDB = path;
            firstDBViewer.DataSource = mainController.FillDB(path);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки
        /// для выбора второй базы данных
        /// которую необходимо сравнить
        /// Заполнение элемента SecondDBViewer данными из базы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSecondDBButton_Click(object sender, EventArgs e)
        {
            string path = SelectDB();
            this.mainController.PathForComparedDB = path;
            secondDBViewer.DataSource = mainController.FillDB(path);
        }

        /// <summary>
        /// Обработка события нажатия кнопки
        /// для создания запроса ко второй базе данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void makeQueryToSecondDBButton_Click(object sender, EventArgs e)
        { }

        /// <summary>
        /// Обработка события нажатия кнопки
        /// которая сравнивает две выборки из таблиц
        /// и отображает разницу в них
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compareTablesButton_Click(object sender, EventArgs e)
        {
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
                      
                        while(true)
                        {
                            for (int k = 0; k < secondDBViewer.RowCount; k++)
                            {
                                for (int l = 0; l < secondDBViewer.ColumnCount; l++)
                                { 
                                    secondTableData += secondDBViewer.Rows[k].Cells[l].Value.ToString() + " ";
                                }

                                if (firstTableData == secondTableData)
                                {
                                    for (int a = 0; a < firstDBViewer.ColumnCount; a++ )
                                        firstDBViewer.Rows[i].Cells[a].Style.BackColor = Color.Green;
                                  
                                    for (int c = 0; c < secondDBViewer.ColumnCount; c++ )
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
                            break;
                        }
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
        /// Обработка события нажатия кнопки
        /// для создания запроса к первой базе данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeQueryToFirstDBButton_Click(object sender, EventArgs e)
        { }

        #endregion

        /// <summary>
        /// Выбор базы данных и загрузка в datGridView.
        /// </summary>
        private string SelectDB()
        {
            //TODO dialogresult
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = this.mainController.ApplicationPath;
            ofd.ShowDialog();

            return ofd.FileName;
        }

        /// <summary>
        /// Создание и запись данных в базу данных.
        /// </summary>
        private string SelectPathToSaveDB()
        {
            SaveFileDialog sfd = new SaveFileDialog();
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
            label2.Text = "";
            this.mainController.SelectedDocument = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = this.mainController.ApplicationPath;
            ofd.Filter = "Файлы Word (*.doc; *.docx) | *.doc; *.docx";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string path = ofd.FileName;
                label2.Text = path;
                this.mainController.SelectedDocument = path;
            }
            else
            {
                string path = null;
            }
        }

        /// <summary>
        /// Парсинг документа Word.
        /// </summary>
        private void ParseDocument()
        {
            parsingStatusStrip.Text = "Parsing started...";
            //нужно сделать проверку выполнения правильности работы функции ParseDocument
            if (this.mainController.ParseDocument() == "OK")
            {
                lecturersComboBox.Items.Clear();
                groupsComboBox.Items.Clear();
                subjectsComboBox.Items.Clear();

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
            else
            {
                MessageBox.Show(this.mainController.ParseDocument());
            }
        }
    }
}