﻿using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace WordReader
{
    class WordProvider
    {

        /// <summary>
        /// Лист который хранит расположения начала и конца
        /// строк в таблице.
        /// </summary>
        public List<Word.Range> TablesRanges { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WordProvider()
        {

        }

        /// <summary>
        /// Функция чтения документа.
        /// </summary>
        /// <param name="selectedDocument">Путь к документу для парсинга.</param>
        /// <param name="lecturers">Лист считанных лекторов.</param>
        /// <param name="subjects">Лист считанных предметов.</param>
        /// <param name="groups">Лист считанных групп.</param>
        /// <param name="consultations">Лист со всеми считанным консультациями.</param>
        /// <returns></returns>
        public string ReadDoc(string selectedDocument, List<string> lecturers, List<string> subjects,
                             List<string> groups, List<Consultation> consultations)
        {
            TablesRanges = new List<Word.Range>();

            Word.Application word = new Word.Application();
            object missing = Type.Missing;
            object filename = selectedDocument;
            Word.Document doc = word.Documents.Open(ref filename, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing);

            var wordApp = new Microsoft.Office.Interop.Word.Application();

            try
            {
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
                                name = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
                                
                                if (!lecturers.Contains(name))
                                    lecturers.Add(name);
                            }

                            if (cellCounter == 3 && r.Text.ToString() != "\r\a")
                            {
                                    subject = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
                                
                                if (!subjects.Contains(subject))
                                    subjects.Add(subject);
                            }

                            if (cellCounter == 4 && r.Text.ToString() != "\r\a")
                            {                                
                                    group = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
    
                                if (!groups.Contains(group))
                                    groups.Add(group);
                            }

                            if (cellCounter == 5)
                            {
                                date = "";
                                if (r.Text.ToString() == "\r\a")
                                    date = "-";
                                else
                                    date = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
                            }

                            if (cellCounter == 6)
                            {
                                time = "";

                                if (r.Text.ToString() == "\r\a")
                                    time = "-";
                                else 
                                    time = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
                            }

                            if (cellCounter == 7)
                            {
                                place = "";
                                if (r.Text.ToString() == "\r\a")
                                    place = "-";
                                else    
                                    place = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
                            }

                            if (cellCounter == 8)
                            {
                                addition = "";
                                if (r.Text.ToString() == "\r\a")
                                    addition = "-";
                                else
                                    addition = r.Text.ToString().Trim(new Char[] { '\r', '\a' });
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
                consultations.RemoveAt(0);

                return "OK";
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                return ex.Message;
            }
            finally
            {
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
            }
        }
    }
}