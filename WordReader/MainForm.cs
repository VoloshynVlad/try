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
                firstDBViewer.DataSource = this.mainController.FillDB(path);
            else
                ;
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
            //TODO dialogresult

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
            else if (dr == DialogResult.Cancel || dr == DialogResult.Abort)
            {
                ;
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
        }

        #endregion
    }
}