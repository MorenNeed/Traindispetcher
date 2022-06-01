using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Traindispetcher
{
    public class EditDB
    {
        public int TrainrideNum { get; set; }
        public bool TrainrideAdd { get; set; }
        public EditDB() { }

        public void ChangeDBRow()
        {
            try
            {
                if (MainWindow.editedRow.TrainrideAdd)
                {
                    using (MySqlConnection conn = new MySqlConnection(MainWindow.DataConnection.connectionString))
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO rozklad (number,city,departure_time,free_seats) " + "VALUES (?,?,?,?)", conn))
                    {
                        cmd.Parameters.Add("@number", MySqlDbType.VarChar, 6).Value = MainWindow.editedTrainride.number;
                        cmd.Parameters.Add("@city", MySqlDbType.VarChar, 25).Value = MainWindow.editedTrainride.city;
                        cmd.Parameters.Add("@departure_time", MySqlDbType.Time).Value = MainWindow.editedTrainride.departure_time;
                        cmd.Parameters.Add("@free_seats", MySqlDbType.Int32).Value = MainWindow.editedTrainride.free_seats;
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = MainWindow.editedTrainride.id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(MainWindow.DataConnection.connectionString))
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE rozklad SET number = ?, city = ?, " + "departure_time = ?, free_seats = ? WHERE id = ?", conn))
                    {
                        cmd.Parameters.Add("@number", MySqlDbType.VarChar, 6).Value = MainWindow.editedTrainride.number;
                        cmd.Parameters.Add("@city", MySqlDbType.VarChar, 25).Value = MainWindow.editedTrainride.city;
                        cmd.Parameters.Add("@departure_time", MySqlDbType.Time).Value = MainWindow.editedTrainride.departure_time;
                        cmd.Parameters.Add("@free_seats", MySqlDbType.Int32).Value = MainWindow.editedTrainride.free_seats;
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = MainWindow.editedTrainride.id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) + "Помилка з'єднання з БД", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
