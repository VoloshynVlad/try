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
    /// <summary>
    /// Класс описывающий контроллер
    /// </summary>
    class MainController
    {
        public string SelectedDocument { get; set; }
        public string PathDB { get; set; }
        public string PathForComparedDB { get; set; }
        public WordProvider wp { get; set; }
        public readonly string ApplicationPath;
        List<Consultation> consultations = new List<Consultation>();

        /// <summary>
        /// Свойство get возвращающее массив консультаций.
        /// </summary>
        public Consultation[] Consultations
        {
            get
            {
                return consultations.ToArray();
            }
        }

        List<Consultation> consultationsSecondary = new List<Consultation>();

        public Consultation[] ConsultationsSecondary
        {
            get
            {
                return consultationsSecondary.ToArray();
            }
        }

        private DbProvider dbProvider;

        List<string> lecturers = new List<string>();

        /// <summary>
        /// Свойство get возвращающее массив лекторов.
        /// </summary>
        public string[] Lecturers
        {
            get
            {
                return lecturers.ToArray();
            }
        }

        List<string> groups = new List<string>();

        /// <summary>
        /// Свойство get возвращающее массив групп.
        /// </summary>
        public string[] Groups
        {
            get
            {
                return groups.ToArray();
            }
        }

        List<string> subjects = new List<string>();

        /// <summary>
        /// Свойство get возвращающее массив предметов.
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
        /// Очистка списка консультаций
        /// </summary>
        public void ClearConsultationArray()
        {
            this.consultations.Clear();
        }

        /// <summary>
        /// Сохранение в базу данных.
        /// </summary>
        /// <param name="path">Путь к базе данных.</param>
        /// <returns>Истина, если сохранен успешно. Ложь, если база уже существует.</returns>
        public bool SaveToDB(string path)
        {
            bool alreadyExist = File.Exists(path);
            if (alreadyExist)
            {
                return false;
            }

            return this.dbProvider.SaveToDB(path, consultations.ToArray());
        }

        /// <summary>
        /// Заполнение DataTable из базы данных.
        /// </summary>
        /// <param name="pathToDB">Путь к базе данных.</param>
        /// <returns>DataTable.</returns>
        public DataTable FillDB(string pathToDB)
        {
            return this.dbProvider.FillDB(pathToDB);
        }

        /// <summary>
        /// Выполняет парсинг документа.
        /// </summary>
        internal string ParseDocument()
        {
            wp = new WordProvider();
            return wp.ReadDoc(SelectedDocument, lecturers, subjects, groups, consultations);
        }
    }
}