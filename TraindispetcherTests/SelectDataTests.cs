using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traindispetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Traindispetcher.GlobalClass;
using System.Windows;

namespace Traindispetcher.Tests
{
    [TestClass()]
    public class SelectDataTests
    {
        [TestMethod()]
        public void SelectXTest()
        {
            List<Trainride> expected = new List<Trainride>(85);

            expected.Add(new Trainride(1, "KA-123", "Kiyv", TimeSpan.Parse("21:25:00"), 80));
            expected.Add(new Trainride(15, "KA-199", "Kiyv", TimeSpan.Parse("00:55:00"), 6));

            var target = new MainWindow();

            object sender = target;
            RoutedEventArgs e = null;

            target.InitializeComponent();
            target.InfoTrainrideForm_Loaded(sender, e);

            target.SelectXMenuItem_Click(sender, e);

            string cityX = "Kiyv";

            SelectData selXYtest = new SelectData();
            selXYtest.SelectX(cityX);

            List<Trainride> actual;
            actual = selXYtest.selectedCityList;

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(expected[i].id, actual[i].id);
                Assert.AreEqual(expected[i].number, actual[i].number);
                Assert.AreEqual(expected[i].city, actual[i].city);
                Assert.AreEqual(expected[i].departure_time, actual[i].departure_time);
                Assert.AreEqual(expected[i].free_seats, actual[i].free_seats);
            }
        }

        [TestMethod()]
        public void SelectXYTest()
        {

        }
    }
}