using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using static Traindispetcher.GlobalClass;

namespace Traindispetcher
{
    public class DataAccess
    {
        public string connectionString;

        public List<Trainride> fList = new List<Trainride>(85);

        public DataAccess()
        {
            OpenDbFile();
        }
        private void OpenDbFile()
        {
            try
            {
                connectionString = "server = localhost; port=3306; username=root; password=root;database=traindispetcher"; ;
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand();
                MySqlDataReader reader;

                string commandString = "SELECT * FROM rozklad;";

                command.CommandText = commandString;
                command.Connection = conn;

                command.Connection.Open();
                reader = command.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    fList.Add(new Trainride((int)reader["id"], (string)reader["number"], (string)reader["city"], (System.TimeSpan)reader["departure_time"], (int)reader["free_seats"]));
                    i += 1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex, "Для завантаження файлу " + "виконайте команду Файл-Завантажити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
