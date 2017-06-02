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

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            List<Word.Range> TablesRanges = new List<Word.Range>();

            try
            {
                Word.Application word = new Word.Application();
                object missing = Type.Missing;
                object filename = label2.Text;
                Word.Document doc = word.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                var wordApp = new Microsoft.Office.Interop.Word.Application();

                for (int i = 1; i <= doc.Tables.Count; i++)
                {
                    Word.Range TRange = doc.Tables[i].Range;
                    TablesRanges.Add(TRange);
                }

                int c = 0;
                string Name = "", Subject = "", Group = "", Date = "", Time = "", Place = "", Addition = "";

                Boolean bInTable;
                int p = doc.Paragraphs.Count;
                for (int par = 1; par <= doc.Paragraphs.Count; par++)
                {
                    bInTable = false;
                    Word.Range r = doc.Paragraphs[par].Range;


                    foreach (Word.Range range in TablesRanges)
                    {
                        if (r.Start >= range.Start && r.Start <= range.End)
                        {
                            c++;

                            if (c == 2 && r.Text.ToString() != "\r\a")
                            {
                                Name = r.Text.ToString();
                                if (!lecturers.Contains(Name))
                                    lecturers.Add(Name);
                            }
                            //проверяем название предмета
                            if (c == 3 && r.Text.ToString() != "\r\a")
                            {
                                Subject = r.Text.ToString();
                                if (!subjects.Contains(Subject))
                                    subjects.Add(Subject);
                            }
                            //проверяем название группы
                            if (c == 4 && r.Text.ToString() != "\r\a")
                            {
                                Group = r.Text.ToString();
                                if (!groups.Contains(Group))
                                    groups.Add(Group);
                            }
                            //проверяем дату
                            if (c == 5 && r.Text.ToString() != "\r\a")
                            {
                                Date = r.Text.ToString();
                            }
                            //проверяем пару
                            if (c == 6 && r.Text.ToString() != "\r\a")
                            {
                                Time = r.Text.ToString();
                            }
                            //проверяем место проведения конс
                            if (c == 7 && r.Text.ToString() != "\r\a")
                            {
                                Place = r.Text.ToString();
                            }
                            //примечание
                            if (c == 8)
                            {
                                if (r.Text.ToString() == "\r\a")
                                    Addition = "-";
                                else
                                    Addition = r.Text.ToString();
                            }
                            if (c == 9)
                            {
                                Consultation cons = new Consultation(Name, Subject, Group, Date, Time, Place, Addition);
                                consultations.Add(cons);
                                c = 0;
                            }

                            bInTable = true;
                            break;
                        }
                    }

                    //                if (!bInTable)
                    //                  MessageBox.Show("!!!!!! Not In Table - Paragraph number " + par.ToString() + ":" + r.Text);
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
                MessageBox.Show("done");


                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();

                for (int i = 1; i < lecturers.Count; i++)
                    comboBox1.Items.Add(lecturers[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < groups.Count; i++)
                    comboBox2.Items.Add(groups[i].Trim(new Char[] { '\r', '\a' }));

                for (int i = 1; i < subjects.Count; i++)
                    comboBox3.Items.Add(subjects[i].Trim(new Char[] { '\r', '\a' }));

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
            }
            catch { Exception ex; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            string name = DateTime.Now.ToString();
            string databaseName = Application.StartupPath + @"\" + name.Replace(':', '-') + ".db";
            //  label5.Text = "DB name: " + databaseName;
            label5.Text = databaseName;


            SQLiteConnection.CreateFile(databaseName);
            MessageBox.Show(File.Exists(databaseName) ? "База данных создана" : "Возникла ошибка при создании базы данных");

            SQLiteConnection connection =
                           new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            SQLiteCommand command =
                    new SQLiteCommand("CREATE TABLE consultations (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, subject TEXT, groop TEXT, date TEXT, time TEXT, place TEXT, addition TEXT);", connection);
            connection.Open();
            command.ExecuteNonQuery();


            SQLiteCommand insert_command = new SQLiteCommand("INSERT INTO 'consultations' ('name', 'subject', 'groop', 'date', 'time', 'place', 'addition') VALUES (?, ?, ?, ?, ?, ?, ?);", connection);

            for (int i = 1; i < consultations.Count; i++)
            {

                insert_command.Parameters.Add("@Name", DbType.String);
                insert_command.Parameters.AddWithValue("@Name", consultations[i].Lecturer.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Subject", DbType.String);
                insert_command.Parameters.AddWithValue("@Subject", consultations[i].Subject.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Groop", DbType.String);
                insert_command.Parameters.AddWithValue("@Groop", consultations[i].Group.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Date", DbType.String);
                insert_command.Parameters.AddWithValue("@Date", consultations[i].Date.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Time", DbType.String);
                insert_command.Parameters.AddWithValue("@Time", consultations[i].Time.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Place", DbType.String);
                insert_command.Parameters.AddWithValue("@Place", consultations[i].Place.Trim(new Char[] { '\r', '\a' }));

                insert_command.Parameters.Add("@Addition", DbType.String);
                insert_command.Parameters.AddWithValue("@Addition", consultations[i].Addition.Trim(new Char[] { '\r', '\a' }));

                insert_command.ExecuteNonQuery();
            }
            connection.Close();
            MessageBox.Show("Готово");
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string databaseName = ofd.FileName.ToString();

            SQLiteConnection connection =
                         new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM 'consultations'", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            //WHERE time = '2'
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                this.dataGridView1.DataSource = dt;
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


        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы Word (*.doc; *.docx) | *.doc; *.docx";
            ofd.ShowDialog();
            label2.Text = ofd.FileName.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string databaseName = ofd.FileName.ToString();

            SQLiteConnection connection =
                         new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM 'consultations'", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            //WHERE time = '2'
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                this.dataGridView2.DataSource = dt;
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

        private void button8_Click(object sender, EventArgs e)
        {
         
        }
    }
}

