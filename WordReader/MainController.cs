using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace WordReader
{
    class MainController
    {
        public string SelectedDocument { get; set; }
        public string PathDB { get; set; }
        public string PathForComparedDB { get; set; }
        public readonly string ApplicationPath;
        List<Consultation> consultations = new List<Consultation>();

        private DbProvider dbProvider;

        List<string> lecturers = new List<string>();
        
        public string[] Lecturers
        {
            get
            {
                return lecturers.ToArray();
            }
        }

        List<string> groups = new List<string>();

        public string[] Groups
        {
            get
            {
                return groups.ToArray();
            }
        }

        List<string> subjects = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public string[] Subjects
        {
            get
            {
                return subjects.ToArray();
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainController() 
        {
            ApplicationPath = Directory.GetCurrentDirectory();
            dbProvider = new DbProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SaveToDB(string path)
        {
            bool alreadyExist = File.Exists(path);
            if (alreadyExist)
            {
                return false;
            }

            return this.dbProvider.SaveToDB(path, consultations.ToArray());
            //saveToDBButton.Enabled = false;
            //MessageBox.Show("Готово");
        }

        public DataTable FillDB(string pathToDB)
        {
            return this.dbProvider.FillDB(pathToDB);
        }

        internal void ParseDocument()
        {
            List<Word.Range> TablesRanges = new List<Word.Range>();

            try
            {
                Word.Application word = new Word.Application();
                object missing = Type.Missing;
                object filename = SelectedDocument;
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
            }
            catch (Exception ex)
            {
            }
        }
    }
}
