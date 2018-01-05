using System;
using System.Data;
using System.Data.SQLite;

namespace WordReader
{
    /// <summary>
    /// Класс описывающий работу с базой данных
    /// </summary>
    class DbProvider
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DbProvider()
        {

        }

        /// <summary>
        /// Сохранение в базу данных.
        /// </summary>
        /// <param name="path">Путь, где хранить базу данных.</param>
        /// <param name="consultations">Массив консультаций.</param>
        /// <returns>Истина, если данные записаны успешно.</returns>
        internal bool SaveToDB(string path, Consultation[] consultations)
        {
            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", path));

            string createTableQuery = "CREATE TABLE consultations ("
                      + "name TEXT, subject TEXT, groop TEXT, date TEXT, time TEXT, place TEXT, addition TEXT);";

            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            string insertRowToDB = "INSERT INTO 'consultations' ('name', 'subject', 'groop', 'date', 'time'," +
                                   "'place', 'addition') VALUES (?, ?, ?, ?, ?, ?, ?);";

            SQLiteCommand insert_command = new SQLiteCommand(insertRowToDB, connection);

            for (int i = 0; i < consultations.Length; i++)
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
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            connection.Close();
            return true;
        }

        /// <summary>
        /// Заполнение DataTable из базы данных.
        /// </summary>
        /// <param name="pathToDB">Путь к базе данных.</param>
        /// <returns>DataTable.</returns>
        internal DataTable FillDB(string pathToDB)
        {
            string databaseName = pathToDB;
            DataTable dt = null;
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            SQLiteCommand cmd = new SQLiteCommand("SELECT name, subject, groop, date, time," +
                                   "place, addition FROM 'consultations'", connection);
            DataSet ds = new DataSet();

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return dt;
        }

        /// <summary>
        /// Метод для проверки корректности открываемой БД.
        /// </summary>
        /// <param name="path">Путь к БД.</param>
        /// <returns></returns>
        internal bool isDBCorrect(string path)
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", path));
            SQLiteCommand cmd = new SQLiteCommand("SELECT name, subject, groop, date, time," +
                                   "place, addition FROM 'consultations'", connection);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }
}