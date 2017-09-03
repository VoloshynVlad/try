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
		#region Properties.

        /// <summary>
        /// Путь выбранного документа.
        /// </summary>
        public string SelectedDocument { get; set; }      

        /// <summary>
        /// Путь к базе данных.
        /// </summary>
        public string PathDB { get; set; }

        /// <summary>
        /// Путь к базе данных, с которой будет происходить
        /// сравнение.
        /// </summary>
        public string PathForComparedDB { get; set; }

        /// <summary>
        /// Обьект WordProvider, отвечает за парсинг документа.
        /// </summary>
        public WordProvider wp { get; set; }

        /// <summary>
        /// Путь по которому находится программа.
        /// </summary>
        public readonly string ApplicationPath;

        /// <summary>
        /// Список для хранения консультаций считанных из документа или загруженных
        /// из базы данных, которая будет сравниваться с информацие из второй базы данных.
        /// </summary>
        List<Consultation> consultations = new List<Consultation>();

        /// <summary>
        /// Свойство get возвращающее массив консультаций первой базы данных.
        /// </summary>
        public Consultation[] Consultations
        {
            get
            {
                return consultations.ToArray();
            }
        }

        /// <summary>
        /// Список для хранения консультаций считанных из документа или загруженных
        /// из базы данных, с которым будет сравниваться информация в первой базе данных.
        /// </summary>
        List<Consultation> consultationsSecondary = new List<Consultation>();

        /// <summary>
        /// Свойство get возвращающее массив консультаций второй базы данных.
        /// </summary>
        public Consultation[] ConsultationsSecondary
        {
            get
            {
                return consultationsSecondary.ToArray();
            }
        }

        private DbProvider dbProvider;

        private List<string> lecturers = new List<string>();

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

        private List<string> groups = new List<string>();

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

        private List<string> subjects = new List<string>();
        
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
		
		#endregion

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

        /// <summary>
        /// Проверка правильности БД.
        /// </summary>
        /// <param name="pathToDB">Путь к БД.</param>
        /// <returns></returns>
        public bool CheckDB(string pathToDB)
        {
            if (this.dbProvider.isDBCorrect(pathToDB))
                return true;
            else
                return false;
        }
    }
}