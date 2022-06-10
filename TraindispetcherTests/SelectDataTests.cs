using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows;
using static Traindispetcher.GlobalClass;

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
            List<Trainride> expected = new List<Trainride>(85);

            expected.Add(new Trainride(4, "MD-874", "Paris", TimeSpan.Parse("14:23:00"), 17));

            var target = new MainWindow();

            object sender = target;
            RoutedEventArgs e = null;

            target.InitializeComponent();
            target.InfoTrainrideForm_Loaded(sender, e);

            target.SelectXMenuItem_Click(sender, e);

            string cityX = "Paris";
            TimeSpan deadLine;
            TimeSpan.TryParse("14:23", out deadLine);

            SelectData selXYtest = new SelectData();
            selXYtest.SelectX(cityX);
            selXYtest.SelectXY(deadLine);

            List<Trainride> actual;
            actual = selXYtest.selectedCityTimeList;

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].id, actual[i].id);
                Assert.AreEqual(expected[i].number, actual[i].number);
                Assert.AreEqual(expected[i].city, actual[i].city);
                Assert.AreEqual(expected[i].departure_time, actual[i].departure_time);
                Assert.AreEqual(expected[i].free_seats, actual[i].free_seats);
            }
        }

        [TestMethod()]
        public void FillCityListTest()
        {
            List<string> expected = new List<string>();

            expected.Add("Kiyv");
            expected.Add("Berlin");
            expected.Add("Odessa");
            expected.Add("Paris");
            expected.Add("London");
            expected.Add("Sumy");
            expected.Add("Lviv");
            expected.Add("Uzgorod");
            expected.Add("Kryvyi Rih");
            expected.Add("Luhansk");
            expected.Add("Kherson");
            expected.Add("Poltava");


            var target = new MainWindow();

            object sender = target;
            RoutedEventArgs e = null;

            target.InitializeComponent();
            target.InfoTrainrideForm_Loaded(sender, e);

            target.SelectXMenuItem_Click(sender, e);

            List<string> actual = target.cList;


            Assert.IsTrue(expected.Count == actual.Count);
            for (int i = 0; i < actual.Count; i++) Assert.AreEqual(expected[i], actual[i]);
        }
    }
}