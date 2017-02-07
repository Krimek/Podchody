using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Podchody
{
    public partial class ScoresTeamComplete : Window
    {
        bool save = false;
        Label[] specialTaskTableLabel;
        CheckBox[] specialTaskTableCheckBox;
        Team team;
        int index;

        public ScoresTeamComplete(int index)
        {
            InitializeComponent();
            team = TeamList.ListOfTeam.ElementAt(index);
            specialTaskTableLabel = new Label[PropertiesStalking.numberOfSpecialTask];
            specialTaskTableCheckBox = new CheckBox[PropertiesStalking.numberOfSpecialTask];
            this.index = index;
            createSpecialTaskCheckBox();
            completeField();
        }

        private void completeField()
        {
            nameTeamLabel.Content = team.TeamName;
            startTimeTextBox.Text = team.StartTime;
            finishTimeTextBox.Text = team.FinishTime;
            numberOfHintTextBox.Text = team.NumberOfHint.ToString();
            numberOfNextPlaceTextBox.Text = team.NumberOfNextPlace.ToString();
            if (team.AdditionalPenaltiesOrBonuses < 0)
            {
                extraBonusTextBox.Text = (team.AdditionalPenaltiesOrBonuses * -1).ToString();
            }
            else if (team.AdditionalPenaltiesOrBonuses > 0)
            {
                extraPenaltiesTextBox.Text = team.AdditionalPenaltiesOrBonuses.ToString();
            }
            finalScoreLabel.Content = "Ogólny wynik: " + team.OverallTime;
        }

        private void createSpecialTaskCheckBox()
        {
            const int shiftWidth = 50;
            const int fieldInWidth = 5;
            const int sizeFieldWidth = 85;
            const int sizeFielsHeight = 70;
            specialTaskGrid.Height = sizeFielsHeight * (((PropertiesStalking.numberOfSpecialTask -1 )/ fieldInWidth)+1);
            Height = 370 + specialTaskGrid.Height;
            for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
            {
                specialTaskTableLabel[i] = AddControl.addLabel(sizeFieldWidth - 5, 25, (i % fieldInWidth) * sizeFieldWidth + shiftWidth, (i / fieldInWidth) * sizeFielsHeight + 1, 0, 0, PropertiesStalking.specialTaskNames[i]);
                specialTaskTableLabel[i].ToolTip = PropertiesStalking.specialTaskNames[i];
                specialTaskGrid.Children.Add(specialTaskTableLabel[i]);
                specialTaskTableCheckBox[i] = AddControl.addCheckBox((i % fieldInWidth) * sizeFieldWidth + shiftWidth + 25, (i / fieldInWidth) * sizeFielsHeight + 30, 0, 0);
                specialTaskTableCheckBox[i].ToolTip = "Bonifikata czasowa: " + PropertiesStalking.bonusesSpecialTask[i].ToString() + " minut";
                specialTaskGrid.Children.Add(specialTaskTableCheckBox[i]);
                if (team.SpecialTask[i])
                    specialTaskTableCheckBox[i].IsChecked = true;
                else
                    specialTaskTableCheckBox[i].IsChecked = false;
            }
        }

        /*********************************************OBSŁUGA PRZYCISKÓW*****************************************************************/
        private void editTeamNameButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditTeam addEditTeam = new AddEditTeam(nameTeamLabel.Content.ToString(), index, true);
            addEditTeam.ShowDialog();
            nameTeamLabel.Content = team.TeamName;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            team.TeamName = nameTeamLabel.Content.ToString();
            if (startTimeTextBox.Text.Length == 5)
                team.StartTime = startTimeTextBox.Text;
            else
                team.StartTime = "";
            if (finishTimeTextBox.Text.Length == 5)
                team.FinishTime = finishTimeTextBox.Text;
            else
                team.FinishTime = "";
            team.NumberOfHint = Convert.ToInt32(numberOfHintTextBox.Text);
            team.NumberOfNextPlace = Convert.ToInt32(numberOfNextPlaceTextBox.Text);
            for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
            {
                if (specialTaskTableCheckBox[i].IsChecked == true)
                    team.SpecialTask[i] = true;
                else
                    team.SpecialTask[i] = false;
            }
            team.AdditionalPenaltiesOrBonuses = (-1 * Convert.ToInt32(extraBonusTextBox.Text)) + Convert.ToInt32(extraPenaltiesTextBox.Text);
            save = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*********************************************OBSŁUGA ZDARZEŃ**************************************************/


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!save)
            {
                MessageBoxResult closing = MessageBox.Show("Czy chcesz zamknąć okno bez zapisywania?", "Zapis", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (closing == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void isNumber(object sender, TextCompositionEventArgs e)
        {
            Validate.isNumber(e);
        }

        private void isTime(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Validate.isTime(textBox.Text, e);
        }

        private void timeKeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length == 2 && e.Key != Key.Back)
            {
                textBox.Text += ":";
                textBox.SelectionStart = 3;
            }
        }

        private void textBoxGotFocus(object sender, RoutedEventArgs e)
        {
            Validate.textBoxGotFocus(sender, e);
        }

        private void textBoxLostFocus(object sender, RoutedEventArgs e)
        {
            Validate.textBoxLostFocus(sender, e);
        }

        private void deleteZero(object sender, KeyEventArgs e)
        {
            Validate.deleteFirstZero(sender);
        }
    }
}
