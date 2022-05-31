using MySql.Data;
using MySql.Data.MySqlClient;

namespace Traindispetcher
{
    public class GlobalClass
    {
        public class Trainride
        {
            public int id { get; set; }
            public string number { get; set; }
            public string city { get; set; }
            public System.TimeSpan departure_time { get; set; }
            public int free_seats { get; set; }
            public Trainride(int idNum, string nF, string cF, System.TimeSpan tF, int fS)
            {
                this.id = idNum;
                this.number = nF;
                this.city = cF;
                this.departure_time = tF;
                this.free_seats = fS;
            }
        }
    }
}
