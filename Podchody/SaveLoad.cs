using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Podchody
{
    static class SaveLoad
    {
        public static void loadStalking()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Plik podchodów|*.podch",
                Title = "Otwórz plik podchodów"
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                PropertiesStalking.pathLoadFile = Path.GetFullPath(openFileDialog.FileName);
                PropertiesStalking.pathSaveFile = Path.GetFullPath(openFileDialog.FileName);

                string temp;
                string[] namesSpecialTask, namesTeam;
                int[] bonusesSpecialTask;
                string[] dataTeam;
                StreamReader sr;
                try
                {
                    sr = new StreamReader(PropertiesStalking.pathLoadFile);
                    TeamList.ListOfTeam.Clear();

                    PropertiesStalking.numberOfTeam = Convert.ToInt32(sr.ReadLine());
                    PropertiesStalking.numberOfSpecialTask = Convert.ToInt32(sr.ReadLine());
                    PropertiesStalking.penaltiesHint = Convert.ToInt32(sr.ReadLine());
                    PropertiesStalking.penaltiesNextPlace = Convert.ToInt32(sr.ReadLine());

                    bonusesSpecialTask = new int[PropertiesStalking.numberOfSpecialTask];
                    namesSpecialTask = new string[PropertiesStalking.numberOfSpecialTask];
                    namesTeam = new string[PropertiesStalking.numberOfTeam];

                    temp = sr.ReadLine();
                    namesSpecialTask = temp.Split();

                    for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
                    {
                        bonusesSpecialTask[i] = Convert.ToInt32(namesSpecialTask[i]);
                        namesSpecialTask[i] = sr.ReadLine();
                    }
                    for (int i = 0; i < PropertiesStalking.numberOfTeam; i++)
                    {
                        namesTeam[i] = sr.ReadLine();
                        new Team(namesTeam[i]);
                    }
                    PropertiesStalking.completeBonusesTable(bonusesSpecialTask, namesSpecialTask, namesTeam);
                    foreach (Team team in TeamList.ListOfTeam)
                    {
                        temp = sr.ReadLine();
                        dataTeam = temp.Split();
                        team.StartTime = dataTeam[0];
                        team.FinishTime = dataTeam[1];
                        team.NumberOfHint = Convert.ToInt32(dataTeam[2]);
                        team.NumberOfNextPlace = Convert.ToInt32(dataTeam[3]);
                        team.AdditionalPenaltiesOrBonuses = Convert.ToInt32(dataTeam[4]);
                        temp = sr.ReadLine();
                        dataTeam = temp.Split();
                        for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
                        {
                            if (dataTeam[i] == "True")
                                team.SpecialTask[i] = true;
                            else
                                team.SpecialTask[i] = false;
                        }
                    }
                    sr.Close();
                    PropertiesStalking.finishLoad = true;
                }
                catch
                {
                    MessageBox.Show("Problem z dostępem do pliku. Wczytywanie nieudane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static void saveStalking(bool saveAs, string pathToSave, bool autoSave)
        {
            if (!PropertiesStalking.change && !saveAs && !autoSave)
            {
                MessageBox.Show("Brak zmian do zapisania", "Podchody", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                StreamWriter sw;
                try
                {
                    sw = new StreamWriter(pathToSave);
                    sw.WriteLine(PropertiesStalking.numberOfTeam);
                    sw.WriteLine(PropertiesStalking.numberOfSpecialTask);
                    sw.WriteLine(PropertiesStalking.penaltiesHint);
                    sw.WriteLine(PropertiesStalking.penaltiesNextPlace);

                    for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++) // bonusy z zadań
                    {
                        sw.Write(PropertiesStalking.bonusesSpecialTask[i] + " ");
                    }
                    sw.WriteLine();

                    for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++) //nazwy zadań specjalnych
                    {
                        sw.WriteLine(PropertiesStalking.specialTaskNames[i]);
                    }

                    foreach (Team team in TeamList.ListOfTeam) // nazwy drużyn
                    {
                        sw.WriteLine(team.TeamName);
                    }

                    foreach (Team team in TeamList.ListOfTeam) // dane o drużynach
                    {
                        sw.WriteLine(team.StartTime + " " +
                            team.FinishTime + " " +
                            team.NumberOfHint + " " +
                            team.NumberOfNextPlace + " " +
                            team.AdditionalPenaltiesOrBonuses);
                        for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
                        {
                            sw.Write(team.SpecialTask[i] + " ");
                        }
                        sw.WriteLine();
                    }
                    sw.Close();
                    PropertiesStalking.change = false;
                    if (!autoSave)
                        MessageBox.Show("Poprawno zapisano podchody", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Problem z dostępem do pliku. Zapis nieudany", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
        public static void saveCompetitionAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Plik podchodów|*.podch",
                Title = "Zapisz Podchody jako..."
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                PropertiesStalking.pathSaveFile = Path.GetFullPath(saveFileDialog.FileName);
                saveStalking(true, Path.GetFullPath(saveFileDialog.FileName), false);
            }
        }
    }
}
