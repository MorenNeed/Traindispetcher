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
                using (MySqlConnection conn = new MySqlConnection(MainWindow.DataConnection.connectionString))
                using (MySqlCommand cmd = new MySqlCommand("UPDATE rozklad SET number = ?, city = ?, " + "departure_time = ?, free_seats = ? WHERE id = ?", conn))
                {
                    cmd.Parameters.Add("@number",MySqlDbType.VarChar)
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
