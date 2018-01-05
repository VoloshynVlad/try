using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace WordReader
{
    /// <summary>
    /// Класс для работы с файлами Microsoft Office Word
    /// </summary>
    class WordProvider
    {
        /// <summary>
        /// Перечисление столбцов в таблице.
        /// </summary>
        enum tableColumns
        {
            name = 2,
            subject,
            group,
            date,
            time,
            place,
            addition,
            consultation
        }

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
        /// Создание приложения Word
        /// </summary>
        /// <returns></returns>
        private Word.Application OpenWordApplication()
        {
            Word.Application word = new Word.Application();

            return word;
        }

        /// <summary>
        /// Открытие указанного документа.
        /// </summary>
        /// <param name="selectedDocument">Путь к документу.</param>
        /// <param name="wordApp"></param>
        /// <returns></returns>
        private Word.Document OpenDoc(string selectedDocument, Word.Application wordApp)
        {

            object filename = selectedDocument;
            Word.Document doc = null;

            try
            {
                doc = wordApp.Documents.Open(ref filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                doc.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                wordApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
            return doc;
        }

        /// <summary>
        /// Получить все интервалы, содержащиеся в документе.
        /// </summary>
        /// <param name="doc"></param>
        private void GetTableRanges(Word.Document doc)
        {
            TablesRanges = new List<Word.Range>();

            if (doc != null)
            {
                try
                {
                    for (int i = 1; i <= doc.Tables.Count; i++)
                    {
                        Word.Range TRange = doc.Tables[i].Range;
                        TablesRanges.Add(TRange);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Добавить значение из таблицы в коллекцию.
        /// </summary>
        /// <param name="range"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        private string GetToCollection(Word.Range range, List<string> collection)
        {
            string value = range.Text.ToString().Trim(new Char[] { '\r', '\a' });

            if (!collection.Contains(value))
                collection.Add(value);

            return value;
        }

        /// <summary>
        /// Получить значение из табличного интервала.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private string GetValueFromRange(Word.Range range)
        {
            string value = "";

            if (range.Text.ToString() == "\r\a")
                value = "-";
            else
                value = range.Text.ToString().Trim(new Char[] { '\r', '\a' });

            return value;
        }

        /// <summary>
        /// Функция чтения документа.
        /// </summary>
        /// <param name="selectedDocument">Путь к документу для парсинга.</param>
        /// <param name="lecturers">Список считанных лекторов.</param>
        /// <param name="subjects">Список считанных предметов.</param>
        /// <param name="groups">Список считанных групп.</param>
        /// <param name="consultations">Список с консультациями.</param>
        /// <returns></returns>
        public string ReadDoc(string selectedDocument, List<string> lecturers, List<string> subjects,
                             List<string> groups, List<Consultation> consultations)
        {
            int colNumber = 0;
            string name = "", subject = "", group = "", date = "", time = "", place = "", addition = "";
            
            Word.Application wordApp = OpenWordApplication();
            Word.Document doc = OpenDoc(selectedDocument, wordApp);

            GetTableRanges(doc);

            try
            {
                for (int par = 1; par <= doc.Paragraphs.Count; par++)
                {
                    Word.Range r = doc.Paragraphs[par].Range;

                    foreach (Word.Range range in TablesRanges)
                    {
                        if (r.Start >= range.Start && r.Start <= range.End)
                        {
                            colNumber++;

                            if (colNumber == (int)tableColumns.name && r.Text.ToString() != "\r\a")
                            {
                                name = GetToCollection(r, lecturers);
                            }

                            if (colNumber == (int)tableColumns.subject && r.Text.ToString() != "\r\a")
                            {
                                subject = GetToCollection(r, subjects);
                            }

                            if (colNumber == (int)tableColumns.group && r.Text.ToString() != "\r\a")
                            {
                                group = GetToCollection(r, groups);
                            }

                            if (colNumber == (int)tableColumns.date)
                            {
                                date = GetValueFromRange(r);
                            }

                            if (colNumber == (int)tableColumns.time)
                            {
                                time = GetValueFromRange(r);
                            }

                            if (colNumber == (int)tableColumns.place)
                            {
                                place = GetValueFromRange(r);
                            }

                            if (colNumber == (int)tableColumns.addition)
                            {
                                addition = GetValueFromRange(r);
                            }

                            if (colNumber == (int)tableColumns.consultation)
                            {
                                Consultation cons = new Consultation(name, subject, group, date,
                                                                     time, place, addition);
                                consultations.Add(cons);
                                colNumber = 0;
                            }
                        }
                    }
                }

                //Удаление первой записи в коллекции
                //где хранятся заголовки столбцов таблицы.
                consultations.RemoveAt(0);

                return "OK";
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                return ex.Message;
            }
            finally
            {
                doc.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                wordApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }
    }
}