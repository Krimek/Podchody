using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Podchody
{
    public partial class MainWindow : Window
    {
        Timer autoSaveTimer;
        private int lastSelectedIndex = -1;
        public MainWindow()
        {
            InitializeComponent();
            saveCompetitionMenuItem.IsEnabled = false;
            saveCompetitionAsMenuItem.IsEnabled = false;
            competitionMenuHeader.IsEnabled = false;
            if (!PropertiesStalking.stalking)
            {
                mainWindowGrid.Children.Clear();
                mainWindowGrid.Children.Add(welcomeGrid);
            }
            
        }

        private void createWindows()
        {
            if (!PropertiesStalking.load)
            {
                for (int i = 0; i < PropertiesStalking.numberOfTeam; i++)
                {
                    new Team(PropertiesStalking.nameTeams[i]);
                }
            }
            updateListBox(-1);
            updateGeneralInformation();
            PropertiesStalking.change = false;
            if (!PropertiesStalking.stalking)
            {
                leftWindowGrid.Width = 0.4 * Width;
                generalInformationWindowGrid.Width = 0.6 * Width;
                detailsTeamWindowGrid.Width = 0.6 * Width;
                mainWindowGrid.Children.Add(leftWindowGrid);
                mainWindowGrid.Children.Add(generalInformationWindowGrid);
                competitionMenuHeader.IsEnabled = true;
                saveCompetitionAsMenuItem.IsEnabled = true;
            }
            autoSaveTimer = new Timer();
            autoSaveTimer.Elapsed += new ElapsedEventHandler(autoSave);
            autoSaveTimer.Interval = 100000;
            autoSaveTimer.Start();
            PropertiesStalking.stalking = true;
        }

        private void autoSave(object sender, ElapsedEventArgs e)
        {
            SaveLoad.saveStalking(false, @"auto_save.podch", true);
        }

        private void updateListBox(int lastIndex)
        {
            teamListBox.Items.Clear();
            for (int i = 0; i < PropertiesStalking.numberOfTeam; i++)
            {
                teamListBox.Items.Add(TeamList.ListOfTeam.ElementAt(i).TeamName);
            }
        }

        private void updateGeneralInformation()
        {
            numberOfTeamBeforeStartLabel.Content = TeamList.getBeforeStart();
            numberOfTeamLabel.Content = PropertiesStalking.numberOfTeam;
            numberOfTeamOnFinishLabel.Content = TeamList.getOnFinish();
            numberOfTeamOnRoadLabel.Content = TeamList.getOnRoad();
            timeLabel.Content = DateTime.Now.ToLongTimeString();
        }

        private void showTeamDetails(int index)
        {
            Team team = TeamList.ListOfTeam.ElementAt(index);
            if (TeamList.ListOfTeam.ElementAt(lastSelectedIndex).StartTime == "0")
                startTimeButton.IsEnabled = true;
            else
                startTimeButton.IsEnabled = false;

            if (TeamList.ListOfTeam.ElementAt(lastSelectedIndex).FinishTime == "0" && TeamList.ListOfTeam.ElementAt(lastSelectedIndex).StartTime != "0")
                stopTimeButton.IsEnabled = true;
            else
                stopTimeButton.IsEnabled = false;

            nameTeamLabel.Content = team.TeamName;
            startTimeLabel.Content = team.StartTime;
            finishTimeLabel.Content = team.FinishTime;
            amountHint2Label.Content = team.NumberOfNextPlace;
            amountHintLabel.Content = team.NumberOfHint;
            generalTimeLabel.Content = team.OverallTime;
            actualPositionLabel.Content = TeamList.getTeamPosition(team.TeamName);
        }
        
        private void teamListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teamListBox.SelectedIndex == -1)
            {
                mainWindowGrid.Children.Remove(detailsTeamWindowGrid);
                mainWindowGrid.Children.Add(generalInformationWindowGrid);
                lastSelectedIndex = teamListBox.SelectedIndex;
                updateGeneralInformation();
            }
            else if (lastSelectedIndex == -1)
            {
                mainWindowGrid.Children.Add(detailsTeamWindowGrid);
                mainWindowGrid.Children.Remove(generalInformationWindowGrid);
                lastSelectedIndex = teamListBox.SelectedIndex;
                showTeamDetails(lastSelectedIndex);
            }
            else
            {
                lastSelectedIndex = teamListBox.SelectedIndex;
                showTeamDetails(lastSelectedIndex);
            }
        }

        

        /*********************************************OBSŁUGA BUTTONÓW*****************************************/

        private void editScoreButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lastSelectedIndex;
            ScoresTeamComplete scores = new ScoresTeamComplete(lastSelectedIndex);
            scores.ShowDialog();
            updateListBox(lastSelectedIndex);
            teamListBox.SelectedIndex = index;
            showTeamDetails(lastSelectedIndex);
        }

        private void deleteTeamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy napewno chcesz usunąć tę drużynę?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                TeamList.ListOfTeam.RemoveAt(lastSelectedIndex);
                PropertiesStalking.numberOfTeam--;
                PropertiesStalking.change = true;
                backButton_Click(sender, e);
                updateListBox(-1);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            teamListBox.SelectedIndex = -1;
        }

        private void editNameTeamButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditTeam addEditTeam = new AddEditTeam(nameTeamLabel.Content.ToString(), lastSelectedIndex, true);
            addEditTeam.ShowDialog();
            nameTeamLabel.Content = TeamList.ListOfTeam.ElementAt(lastSelectedIndex).TeamName;
            updateListBox(lastSelectedIndex);
        }

        private void startTimeButton_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ListOfTeam.ElementAt(lastSelectedIndex).StartTime = DateTime.Now.ToString().Substring(11, 5);
            showTeamDetails(lastSelectedIndex);
        }

        private void stopTimeButton_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ListOfTeam.ElementAt(lastSelectedIndex).FinishTime = DateTime.Now.ToString().Substring(11, 5);
            showTeamDetails(lastSelectedIndex);
        }

        private void addHintButton_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ListOfTeam.ElementAt(lastSelectedIndex).NumberOfHint++;
            showTeamDetails(lastSelectedIndex);
        }

        private void addHint2Button_Click(object sender, RoutedEventArgs e)
        {
            TeamList.ListOfTeam.ElementAt(lastSelectedIndex).NumberOfNextPlace++;
            showTeamDetails(lastSelectedIndex);
        }

        private void teamListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editScoreButton_Click(sender, e);
        }

        private void showScore_Click(object sender, RoutedEventArgs e)
        {
            showResultsListMenuItem_Click(sender, e);
        }
    /**************************************************OBSŁUGA MENU*******************************************/
        private void newCompetitionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewGame newGame = new NewGame();
            newGame.ShowDialog();
            if (PropertiesStalking.finishCreateNewStalking)
            {
                TeamList.ListOfTeam.Clear();
                createWindows();
                PropertiesStalking.finishCreateNewStalking = false;
                if (PropertiesStalking.welcomeWindow)
                {
                    mainWindowGrid.Children.Remove(welcomeGrid);
                    PropertiesStalking.welcomeWindow = false;
                }
            }
        }

        private void loadCompetitionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PropertiesStalking.load = true;
            SaveLoad.loadStalking();
            if (PropertiesStalking.finishLoad)
            {
                if (!PropertiesStalking.stalking)
                    createWindows();
                updateListBox(-1);
                saveCompetitionMenuItem.IsEnabled = true;
                PropertiesStalking.change = false;
                PropertiesStalking.finishLoad = false;
                if (PropertiesStalking.welcomeWindow)
                {
                    mainWindowGrid.Children.Remove(welcomeGrid);
                    PropertiesStalking.welcomeWindow = false;
                }
                MessageBox.Show("Poprawnie wczytano podchody", "Wczytywanie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            PropertiesStalking.load = false; 
        }

        private void saveCompetitionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (PropertiesStalking.stalking && PropertiesStalking.pathSaveFile != "")
            {
                SaveLoad.saveStalking(false, PropertiesStalking.pathSaveFile, false);
            }
        }
        
        private void saveCompetitionAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveLoad.saveCompetitionAs();
            saveCompetitionMenuItem.IsEnabled = true;
        }

        private void closeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addTeamMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddEditTeam addEditTeam = new AddEditTeam("", -1, false);
            addEditTeam.ShowDialog();
            teamListBox.SelectedIndex = -1;
            updateListBox(-1);
            updateGeneralInformation();
        }

        private void showResultsListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowResults showResults = new ShowResults();
            showResults.ShowDialog();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            newCompetitionMenuItem_Click(sender, e);
        }

        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            loadCompetitionMenuItem_Click(sender, e);
        }

        /************************************OBSŁUGA ZDARZEŃ***********************************/

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result;
            bool stop = false;
            if (PropertiesStalking.stalking)
            {
                if (PropertiesStalking.change)
                {
                    result = MessageBox.Show("Wprowadzono zmiany. Czy chcesz zapisać przed zamknięciem programu?", "Podchody", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (PropertiesStalking.pathSaveFile == "")
                            SaveLoad.saveCompetitionAs();
                        else
                            SaveLoad.saveStalking(false, PropertiesStalking.pathSaveFile, false);
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                        stop = true;
                    }
                }
                if (!stop)
                {
                    result = MessageBox.Show("Czy napewno chcesz zakończyć?", "Podchody", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                        e.Cancel = true;
                }
            }
        }
    }
}