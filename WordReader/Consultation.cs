using System;

namespace WordReader
{
    /// <summary>
    /// Класс описывающий консультацию
    /// </summary>
    class Consultation
    {
        public string Lecturer { get; set; }
        public string Subject { get; set; }
        public string Group { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Addition { get; set; }

        /// <summary>
        /// Конструктор класс Consultation
        /// </summary>
        /// <param name="lecturer">Имя лектора</param>
        /// <param name="subject">Название предмета</param>
        /// <param name="group">Академическая группа</param>
        /// <param name="date">Дата проведения консультации</param>
        /// <param name="time">Пара проведения консультации</param>
        /// <param name="place">Место проведения консультации</param>
        /// <param name="addition">Дополнение</param>
        public Consultation( string lecturer, string subject, string group, string date, string time,
                            string place, string addition)
        {
            Lecturer = lecturer;
            Subject = subject;
            Group = group;
            Date = date;
            Time = time;
            Place = place;
            Addition = addition;
        }
    }
}