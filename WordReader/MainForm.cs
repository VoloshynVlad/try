using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
        
        private void selectDocButton_Click(object sender, EventArgs e)
        {
            SelectDocument();
        }
                
        private void parseDocButton_Click(object sender, EventArgs e)
        {
            ParseDoc();
        }

        private void saveToDBButton_Click(object sender, EventArgs e)
        {
            SaveToDB();
        }
   
        private void selectFirstDBButton_Click(object sender, EventArgs e)
        {
            LoadFromDB();
        }

        /// <summary>
        /// Обработчик события выбора пользователя режима "Сравнения"
        /// или "Просмотр"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comparationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = !comparationCheckBox.Checked;
            if (compareTablesButton.Visible)
                compareTablesButton.Visible = false;
            else
                compareTablesButton.Visible = true;
        }

        /// <summary>
        /// Нажатие кнопки для выбора второй базы данных для сранения.
        /// Заполнение элемента SecondDBViewer данными из базы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSecondDBButton_Click(object sender, EventArgs e)
        {
            // TODO: вынести в отдельный метод
            // TODO: доработать до вида выбора первой бд
            //secondDBPathLabel.Text = "";
            //secondDBPathLabel.Text = "DB path:";

            string path = SelectDB();
            this.mainController.PathForComparedDB = path;

            //secondDBPathLabel.Text += path;
            db2PathTextBox.Text = path;

            if (path != "")
                secondDBViewer.DataSource = this.mainController.FillDB(path);
            else
                ;
        }
        
        private void compareTablesButton_Click(object sender, EventArgs e)
        {
            CompareTables();
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

        /// <summary>
        /// Обработка нажатия горячих клавиш
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift)
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
                else if (e.KeyCode == Keys.Q)
                {
                    selectFirstDBButton.PerformClick();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.W)
                {
                    if (compareTablesButton.Visible == true)
                    {
                        selectSecondDBButton.PerformClick();
                        e.SuppressKeyPress = true;
                    }
                    else
                        MessageBox.Show("Unabled function");
                }
                else if (e.KeyCode == Keys.F)
                {
                    filterButton.PerformClick();
                    e.SuppressKeyPress = true;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        /// <summary>
        /// Синхронный фокус на строках в двух dataGridView по нажатию стрелок
        /// в firstDBViewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstDBViewer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    secondDBViewer.CurrentCell =
                        secondDBViewer.Rows[firstDBViewer.CurrentCell.RowIndex + 1]
                        .Cells[firstDBViewer.CurrentCell.ColumnIndex];
                }

                if (e.KeyCode == Keys.Up)
                {
                    secondDBViewer.CurrentCell =
                        secondDBViewer.Rows[firstDBViewer.CurrentCell.RowIndex - 1]
                        .Cells[firstDBViewer.CurrentCell.ColumnIndex];
                }

                if (e.KeyCode == Keys.Left)
                {
                    secondDBViewer.CurrentCell =
                        secondDBViewer.Rows[firstDBViewer.CurrentCell.RowIndex]
                        .Cells[firstDBViewer.CurrentCell.ColumnIndex - 1];
                }

                if (e.KeyCode == Keys.Right)
                {
                    secondDBViewer.CurrentCell =
                        secondDBViewer.Rows[firstDBViewer.CurrentCell.RowIndex]
                        .Cells[firstDBViewer.CurrentCell.ColumnIndex + 1];
                }
            }
            catch
            {
                Exception exp;
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            PerformFilter();
            ViewFilteredData();
        }

        /// <summary>
        /// Синхронный фокус на строках в двух dataGridView по нажатию стрелок
        /// в secondDBViewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secondDBViewer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    firstDBViewer.CurrentCell =
                       firstDBViewer.Rows[secondDBViewer.CurrentCell.RowIndex + 1]
                        .Cells[secondDBViewer.CurrentCell.ColumnIndex];
                }

                if (e.KeyCode == Keys.Up)
                {
                    firstDBViewer.CurrentCell =
                        firstDBViewer.Rows[secondDBViewer.CurrentCell.RowIndex - 1]
                        .Cells[secondDBViewer.CurrentCell.ColumnIndex];
                }

                if (e.KeyCode == Keys.Left)
                {
                    firstDBViewer.CurrentCell =
                        firstDBViewer.Rows[secondDBViewer.CurrentCell.RowIndex]
                        .Cells[secondDBViewer.CurrentCell.ColumnIndex - 1];
                }

                if (e.KeyCode == Keys.Right)
                {
                    firstDBViewer.CurrentCell =
                        firstDBViewer.Rows[secondDBViewer.CurrentCell.RowIndex]
                        .Cells[secondDBViewer.CurrentCell.ColumnIndex + 1];
                }
            }
            catch
            {
                Exception exp;
            }
        }

        /// <summary>
        /// Индекс отсортированной колонки.
        /// </summary>
        /// <remarks>Для последовательной двухколоночной сортировки, учитывающей сортировку внутри групп.</remarks>
        int m_lastSortedColumnIndex = 0;

        /// <summary>
        /// Направление сортировки отсортированной колонки.
        /// </summary>
        /// <remarks>Для последовательной двухколоночной сортировки, учитывающей сортировку внутри групп.</remarks>
        bool m_lastSortedColumnAscending = true;

        private void firstDBViewer_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            // сортировка столбцов по умолчанию
            e.SortResult = System.String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());

            // сортировка столбца, значения которого числа, а не строки.
            if (e.Column.Index == 1)
            {
                double a = double.Parse(e.CellValue1.ToString());
                double b = double.Parse(e.CellValue2.ToString());
                e.SortResult = a.CompareTo(b);
            }

            // для последовательной двухколоночной сортировки, учитывающей сортировку внутри групп
            if (e.SortResult == 0 && e.Column.Index != m_lastSortedColumnIndex)
            {
                //сортировка столбца, значения которого числа, а не строки.
                if (m_lastSortedColumnIndex == 1)
                {
                    double a = double.Parse(firstDBViewer.Rows[m_lastSortedColumnAscending ? e.RowIndex1 : e.RowIndex2].Cells[m_lastSortedColumnIndex].Value.ToString());
                    double b = double.Parse(firstDBViewer.Rows[m_lastSortedColumnAscending ? e.RowIndex2 : e.RowIndex1].Cells[m_lastSortedColumnIndex].Value.ToString());
                    e.SortResult = a.CompareTo(b);
                }
                else
                {
                    string a = firstDBViewer.Rows[m_lastSortedColumnAscending ? e.RowIndex1 : e.RowIndex2].Cells[m_lastSortedColumnIndex].Value.ToString();
                    string b = firstDBViewer.Rows[m_lastSortedColumnAscending ? e.RowIndex2 : e.RowIndex1].Cells[m_lastSortedColumnIndex].Value.ToString();
                    e.SortResult = System.String.Compare(a, b);
                }
            }

            e.Handled = true;
        }

        private void firstDBViewer_Sorted(object sender, EventArgs e)
        {
            // сохранение параметров текщей сортировки - что за колонка и в каком направлении сортировалась
            m_lastSortedColumnIndex = firstDBViewer.SortedColumn.Index;
            m_lastSortedColumnAscending = firstDBViewer.SortedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? true : false;
        }

        #endregion

        #region Логика.

        #region Работа с ранее сохраненными данными в БД.

        /// <summary>
        /// Заполнение элемента FirstDBViewer данными из базы.
        /// Выбор первой базы данных, с которой будет сравниваться вторая база данных.
        /// </summary>        
        private void LoadFromDB()
        {
            //firstBDPath.Text = "";
            //firstBDPath.Text = "DB path:";

            string path = SelectDB();
            this.mainController.PathDB = path;
            dbPathTextBox.Text = path;

            //firstBDPath.Text += path;

            if (path != "")
            {
                this.mainController.ClearALL();
                ResetFilterComboboxes();

                try
                {
                    if (this.mainController.CheckDB(path))
                    {
                        this.mainController.ClearConsultationArray();
                        this.mainController.FillDB(path);
                        BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                        firstDBViewer.DataSource = bind;
                        FillFilterOptions();
                    }
                    else
                        MessageBox.Show("The DB is incorrect!");
                }
                catch(Exception exp)
                {
                    
                }
            }
            else
                MessageBox.Show("Choose BD first");
        }

        /// <summary>
        /// Выбор базы данных для считывания ранее сохраненных данных.
        /// </summary>
        private string SelectDB()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();//this.mainController.ApplicationPath;
            ofd.Filter = "Файлы SQLite (*.db) | *.db";

            DialogResult dr = ofd.ShowDialog();
            string path = "";

            if (dr == DialogResult.OK)
            {
                path = ofd.FileName;
                return path;
            }
            else if (dr == DialogResult.Cancel || dr == DialogResult.Abort)
            {
                path = docPathTextBox.Text;
                return path;
            }
            else
            {
                return "-";
            }
        }

        /// <summary>
        /// Сохранение в базу данных считанной информации.
        /// </summary>
        private void SaveToDB()
        {
            if (firstDBViewer.RowCount == 0) // привязка к отображению, а не к самой базе - спорно. Оправдано было бы, если сохраняться будет только то, что есть в гриде, но ведь сохраняется вся коллекция.
            {
                MessageBox.Show("Nothing to save to DB");
                return;
            }

            string path = "";
            //this.firstBDPath.Text = "";
            dbPathTextBox.Text = "";
            this.mainController.PathDB = "";
            path = SelectPathToSaveDB();
            if (path == "")
            {
                return;
            }
            this.mainController.PathDB = path;
            //this.firstBDPath.Text = path;
            dbPathTextBox.Text = path;

            if (!this.mainController.SaveToDB(path))
            {
                MessageBox.Show("Error save to DB");
                return;
            }
        }

        /// <summary>
        /// Выбор пути к файлу базы данных для записи.
        /// </summary>
        private string SelectPathToSaveDB()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            sfd.Filter = "Файлы SQLite (*.db) | *.db";
            sfd.FileName = DateTime.Now.ToString().Replace(':', '-') + ".db";
            DialogResult dr = sfd.ShowDialog();
            string path = "";

            if (dr == DialogResult.OK)
            {
                path = sfd.FileName;
                return path;
            }
            else
            {
                if (dr == DialogResult.Cancel || dr == DialogResult.Abort)
                {
                    path = "";
                    return path;
                }
                else
                {
                    MessageBox.Show("Error path to DB");
                    return "";
                }
            }
        }

        #endregion

        #region Парсинг вордовского документа.

        /// <summary>
        /// Функция парсинга документа.
        /// </summary>
        /// <remarks>занимается считыванием документа и созданием List'a объектов Consultation
        /// по считанным данным заполняет LecturersComboBox, SubjectsComboBox и GroupsComboBox.</remarks>
        private void ParseDoc()
        {
            int exitCode = 0;

            parsingStatusStrip.Text = "Parsing started...";

            //firstBDPath.Text = "";
            //firstBDPath.Text = "DB path:";

            secondDBPathLabel.Text = "";
            secondDBPathLabel.Text = "DB path:";

            this.mainController.ClearConsultationArray();
            ResetFilterComboboxes();

            try
            {
                ParseDocument();
                //if (firstDBViewer.DataSource == null)
                //{
                BindingSource bind = new BindingSource { DataSource = this.mainController.Consultations };
                firstDBViewer.DataSource = bind;
                //}
                //else
                //{
                //    BindingSource bind = new BindingSource { DataSource = this.mainController.ConsultationsSecondary };
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
                //pathLabel.Text = "";
                path = ofd.FileName;
                docPathTextBox.Text = path;
                //pathLabel.Text = path;
                this.mainController.SelectedDocument = path;
            }
            else if (dr == DialogResult.Cancel || dr == DialogResult.Abort)
            {
                path = docPathTextBox.Text;
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
            this.mainController.ClearALL();

            if (this.mainController.ParseDocument() == "OK")
            {
                FillFilterOptions();

                parsingStatusStrip.Text = "Done!";
            }
        }

        #endregion

        #region Filter.

        /// <summary>
        /// Выполнение фильтрации в таблицах по критериям.
        /// </summary>
        private void PerformFilter()
        {
            // TODO: фильтрация должна работать для второй таблицы тоже
            try
            {
                BindingSource bind = new BindingSource
                {
                    DataSource = this.mainController.FilterRecords(
                        lecturersComboBox.SelectedItem.ToString(),
                        subjectsComboBox.SelectedItem.ToString(),
                        groupsComboBox.SelectedItem.ToString())
                };
                firstDBViewer.DataSource = bind;
            }
            catch
            {
                Exception exp;
            }
        }

        /// <summary>
        /// Заполнение выпадающих списков, определяющих параметры фильтрации.
        /// </summary>
        private void FillFilterOptions()
        {
            ResetFilterComboboxes();

            string[] lecturers = this.mainController.Lecturers;
            string[] groups = this.mainController.Groups;
            string[] subjects = this.mainController.Subjects;

            for (int i = 0; i < lecturers.Length; i++)
                lecturersComboBox.Items.Add(lecturers[i].Trim(new Char[] { '\r', '\a' }));

            for (int i = 0; i < groups.Length; i++)
                groupsComboBox.Items.Add(groups[i].Trim(new Char[] { '\r', '\a' }));

            for (int i = 0; i < subjects.Length; i++)
                subjectsComboBox.Items.Add(subjects[i].Trim(new Char[] { '\r', '\a' }));

            lecturersComboBox.SelectedIndex = 0;
            groupsComboBox.SelectedIndex = 0;
            subjectsComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Сброс значений выпадающих списков фильтрации.
        /// </summary>
        private void ResetFilterComboboxes()
        {
            lecturersComboBox.Items.Clear();
            groupsComboBox.Items.Clear();
            subjectsComboBox.Items.Clear();

            lecturersComboBox.Items.Add("All");
            groupsComboBox.Items.Add("All");
            subjectsComboBox.Items.Add("All");
        }

        /// <summary>
        /// Вывести данные из таблице в формате, используемом в табеле почасовки.
        /// </summary>
        private void ViewFilteredData()
        {
            var filtered = this.mainController.FilterRecords(lecturersComboBox.SelectedItem.ToString(),
                subjectsComboBox.SelectedItem.ToString(),
                groupsComboBox.SelectedItem.ToString());

            paresTextBox.Text = "";

            // массив, описывающий время проведения занятий. Нулевой элемент зарезервирован для ошибки - да и нет в природе нулевой пары.
            string[] pares = {
                "Error",
                "7:45-9:20",
                "9:30-11:05",
                "11:15-12:50",
                "13:10-14:45",
                "14:55-16:30",
                "16:40-18:15",
                "18:25-20:00",
                "20:10-21:45"
            };

            // каждую строку из графика консультаций, прошедшую фильтр, привести к формату для табеля почасовки.
            foreach(var f in filtered)
            {
                int pareNumber = 0; // номер пары, на которой проводится консультация.

                // если в ячейке номера пары корректное число.
                if (Int32.TryParse(f.Time, out pareNumber))
                {
                    //Int32.TryParse(f.Time, out pareNumber);
                    paresTextBox.Text += f.Date + "\t" + pares[pareNumber] + ",\t" + f.Group + ";\r\n";
                    if (f.Addition != "-")
                    {
                        //MessageBox.Show("Pollypara!!!");
                        paresTextBox.Text += "HALF\r\n\r\n";
                    }
                }
                else
                {
                    // в некоторых случаях, ячейка номера может содержать больше одного значения. Попробуем их обработать...
                    if (f.Time.Contains(","))
                    {
                        var times = f.Time.Split(',');
                        foreach (var t in times)
                        {
                            Int32.TryParse(t, out pareNumber);
                            paresTextBox.Text += f.Date + "\t" + pares[pareNumber] + ",\t" + f.Group + ";\r\n";
                        }
                    }
                }
            }
        }

        #endregion

        #region Cравнениe записей в двух таблицах.

        /// <summary>
        /// Cравнениe записей в двух таблицах.
        /// </summary>
        private void CompareTables()
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

        #endregion

        #endregion

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}