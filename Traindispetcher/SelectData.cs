using System;
using System.Collections.Generic;
using System.Windows;
using static Traindispetcher.GlobalClass;

namespace Traindispetcher
{
    public class SelectData
    {
        public List<Trainride> selectedCityList = new List<Trainride>(85);
        public List<Trainride> selectedCityTimeList = new List<Trainride>(85);
        Microsoft.Office.Interop.Word.Application wordApp;
        Microsoft.Office.Interop.Word.Document wordDoc;
        string filePath;

        public SelectData() { }
        public void SelectX(string cityX = "")
        {
            int j = 0;
            for (int i = 0; i < MainWindow.TrainrideCount; i++)
            {
                if (cityX == MainWindow.DataConnection.fList[i].city)
                {
                    selectedCityList.Add(MainWindow.DataConnection.fList[i]);
                    j++;
                }
            }
        }
        public void SelectXY(TimeSpan deadLine)
        {
            int j = 0;
            for (int i = 0; i < selectedCityList.Count; i++)
            {
                if (deadLine.Hours > selectedCityList[i].departure_time.Hours)
                {
                    selectedCityTimeList.Add(selectedCityList[i]);
                    j++;
                }
                if ((deadLine.Hours == selectedCityList[i].departure_time.Hours) && (deadLine.Minutes >= selectedCityList[i].departure_time.Minutes))
                {
                    selectedCityTimeList.Add(selectedCityList[i]);
                    j++;
                }
            }
        }
        public void WriteData(List<Trainride> selXList, List<Trainride> selXYList)
        {
            filePath = Environment.CurrentDirectory.ToString();
            try
            {
                wordApp = new Microsoft.Office.Interop.Word.Application();
                wordDoc = wordApp.Documents.Add(filePath + "\\Шаблон_Пошуку_подорожей.dot");
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex, "Помістіть файл Шаблон_Пошуку_подорожей.dot " +
                    "у каталог із exe-файлом програми і повторіть збереження", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            ReplaceText(MainWindow.selectedCity, "[X]");

            ReplaceText(selectedCityList, 1);

            ReplaceText(MainWindow.timeTrainride.ToString(@"hh\:mm"), "[Y]");

            ReplaceText(selXYList, 2);

            try
            {
                wordDoc.Save();
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex, "Помилка відібраних даних", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (wordDoc != null)
            {
                wordDoc.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdPromptToSaveChanges);
            }
            if (wordApp != null)
            {
                wordApp.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdPromptToSaveChanges);
            }
        }
        private void ReplaceText(string textToReplace, string replacedText)
        {
            Object missing = Type.Missing;

            Microsoft.Office.Interop.Word.Range selText;
            selText = wordDoc.Range(wordDoc.Content.Start, wordDoc.Content.End);

            Microsoft.Office.Interop.Word.Find find = wordApp.Selection.Find;
            find.Text = replacedText;
            find.Replacement.Text = textToReplace;
            Object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

            Object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

            find.Execute(FindText: Type.Missing,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                MatchSoundsLike: missing,
                MatchAllWordForms: false,
                Forward: true,
                Wrap: wrap,
                Format: false,
                ReplaceWith: missing, Replace: replace);
        }
        private void ReplaceText(List<Trainride> selectedList, int numTable)
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                wordDoc.Tables[numTable].Rows.Add();
                wordDoc.Tables[numTable].Cell(2 + i, 1).Range.Text = selectedList[i].number;
                wordDoc.Tables[numTable].Cell(2 + i, 2).Range.Text = selectedList[i].departure_time.ToString();

                if (numTable == 2)
                {
                    wordDoc.Tables[numTable].Cell(2 + i, 3).Range.Text = selectedList[i].free_seats.ToString();
                }
            }
        }
    }
}
