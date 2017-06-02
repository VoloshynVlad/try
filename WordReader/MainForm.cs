using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SQLite;
using System.IO;
using System.Drawing;

namespace WordReader
{
    public partial class MainForm : Form
    {
        List<Consultation> consultations = new List<Consultation>();
        List<string> lecturers = new List<string>();
        List<string> groups = new List<string>();
        List<string> subjects = new List<string>();
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
            ParseDocument();
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
            label5.Text = "";
            this.mainController.PathDB = "";
            string path = SelectPathToSaveDB();
            label5.Text = path;
            this.mainController.PathDB = path;

            SaveToDB(path);
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
            FillDB(path, firstDBViewer);
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
            FillDB(path, secondDBViewer);
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
        { }
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
        /// Выбор документа формата .doc или .docx, который необходимо распарсить.
        /// </summary>
        private void SelectDocument()
        {
            label2.Text = "";
            this.mainController.SelectedDocument = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы Word (*.doc; *.docx) | *.doc; *.docx";
            ofd.ShowDialog();
            string path = ofd.FileName;
            label2.Text = path;
            this.mainController.SelectedDocument = path;
        }

        /// <summary>
        /// Парсинг документа Word.
        /// </summary>
        private void ParseDocument()
        {
            saveToDBButton.Enabled = true;
            List<Word.Range> TablesRanges = new List<Word.Range>();

            try
            {
                Word.Application word = new Word.Application();
                object missing = Type.Missing;
                object filename = label2.Text;
                Word.Document doc = word.Documents.Open(ref filename, ref missing, ref missing,
                                                        ref missing, ref missing, ref missing,
                                                        ref missing, ref missing, ref missing,
                                                        ref missing, ref missing, ref missing,
                                                        ref missing, ref missing, ref missing,
                                                        ref missing);

                var wordApp = new Microsoft.Office.Interop.Word.Application();

                for (int i = 1; i <= doc.Tables.Count; i++)
                {
                    Word.Range TRange = doc.Tables[i].Range;
                    TablesRanges.Add(TRange);
                }

                int cellCounter = 0;
                string name = "", subject = "", group = "", date = "", time = "", place = "", addition = "";

                int p = doc.Paragraphs.Count;
                for (int par = 1; par <= doc.Paragraphs.Count; par++)
                {
                    Word.Range r = doc.Paragraphs[par].Range;

                    foreach (Word.Range range in TablesRanges)
                    {
                        if (r.Start >= range.Start && r.Start <= range.End)
                        {
                            cellCounter++;

                            if (cellCounter == 2 && r.Text.ToString() != "\r\a")
                            {
                                name = r.Text.ToString();
                                if (!lecturers.Contains(name))
                                    lecturers.Add(name);
                            }

                            if (cellCounter == 3 && r.Text.ToString() != "\r\a")
                            {
                                subject = r.Text.ToString();
                                if (!subjects.Contains(subject))
                                    subjects.Add(subject);
                            }

                            if (cellCounter == 4 && r.Text.ToString() != "\r\a")
                            {
                                group = r.Text.ToString();
                                if (!groups.Contains(group))
                                    groups.Add(group);
                            }

                            if (cellCounter == 5 && r.Text.ToString() != "\r\a")
                            {
                                date = r.Text.ToString();
                            }

                            if (cellCounter == 6 && r.Text.ToString() != "\r\a")
                            {
                                time = r.Text.ToString();
                            }

                            if (cellCounter == 7 && r.Text.ToString() != "\r\a")
                            {
                                place = r.Text.ToString();
                            }

                            if (cellCounter == 8)
                            {
                                if (r.Text.ToString() == "\r\a")
                                    addition = "-";
                                else
                                    addition = r.Text.ToString();
                            }
                            if (cellCounter == 9)
                            {
                                Consultation cons = new Consultation(name, subject, group, date,
                                                                     time, place, addition);
                                consultations.Add(cons);
                                cellCounter = 0;
                            }
                        }
                    }
                }

                doc.Close(false);
                word.Quit(false);
                wordApp.Quit(false);

                if (word != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(word);
                if (doc != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                if (wordApp != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);

                doc = null;
                word = null;
                wordApp = null;
                GC.Collect();
                //MessageBox.Show("done");

                lecturersComboBox.Items.Clear();
                groupsComboBox.Items.Clear();
                subjectsComboBox.Items.Clear();

                for (int i = 1; i < lecturers.Count; i++)
                    lecturersComboBox.Items.Add(lecturers[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < groups.Count; i++)
                    groupsComboBox.Items.Add(groups[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < subjects.Count; i++)
                    subjectsComboBox.Items.Add(subjects[i].Trim(new Char[] { '\r', '\a' }));

                lecturersComboBox.SelectedIndex = 0;
                groupsComboBox.SelectedIndex = 0;
                subjectsComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Выбор базы данных и загрузка в datGridView.
        /// </summary>
        private string SelectDB()
        {//TODO dialogresult
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
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

        private void SaveToDB(string path)
        {
            bool alreadyExist = File.Exists(path);

            if (alreadyExist)
            {
                MessageBox.Show("База данных c таким названием уже существует");
                return;
            }
            //MessageBox.Show(File.Exists(path) ? "База данных создана" : "Возникла ошибка при создании базы данных");

            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", path));

            string createTableQuery = "CREATE TABLE consultations (id INTEGER PRIMARY KEY AUTOINCREMENT,"
                        + "name TEXT, subject TEXT, groop TEXT, date TEXT, time TEXT, place TEXT, addition TEXT);";

            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }

            string insertRowToDB = "INSERT INTO 'consultations' ('name', 'subject', 'groop', 'date', 'time'," +
                                   "'place', 'addition') VALUES (?, ?, ?, ?, ?, ?, ?);";

            SQLiteCommand insert_command = new SQLiteCommand(insertRowToDB, connection);

            for (int i = 1; i < consultations.Count; i++)
            {
                insert_command.Parameters.Add("@Name", DbType.String);
                insert_command.Parameters.AddWithValue("@Name", consultations[i].Lecturer.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Subject", DbType.String);
                insert_command.Parameters.AddWithValue("@Subject", consultations[i].Subject.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Groop", DbType.String);
                insert_command.Parameters.AddWithValue("@Groop", consultations[i].Group.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Date", DbType.String);
                insert_command.Parameters.AddWithValue("@Date", consultations[i].Date.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Time", DbType.String);
                insert_command.Parameters.AddWithValue("@Time", consultations[i].Time.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Place", DbType.String);
                insert_command.Parameters.AddWithValue("@Place", consultations[i].Place.Trim('\r', '\a'));

                insert_command.Parameters.Add("@Addition", DbType.String);
                insert_command.Parameters.AddWithValue("@Addition", consultations[i].Addition.Trim('\r', '\a'));

                try
                {
                    insert_command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
            connection.Close();
            //saveToDBButton.Enabled = false;
            //MessageBox.Show("Готово");
        }

        private void FillDB(string pathToDB, DataGridView dataGridView)
        {
            string databaseName = pathToDB;

            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM 'consultations'", connection);
            DataSet ds = new DataSet();

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }
}