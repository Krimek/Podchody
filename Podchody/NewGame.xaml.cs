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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Podchody
{
    public partial class NewGame : Window
    {
        Label[] specialTaskNameTableLabel;
        TextBox[] specialTaskNameTableTextBox;
        Label[] specialTaskBonusesTableLabel;
        TextBox[] specialTaskBonusesTableTextBox;
        Label[] teamNamesTableLabel;
        TextBox[] teamNamesTableTextBox;

        private const int widthMovement = 15, heightMovement = 70, heightJumpMovement = 30, widthJumpMovement = 255, widthLabelMovement = 85;

        private int page;
        private int numberOfColumnsSpecialTask;
        private int numberOfColumnsTeam;
        private int lastAmountSpecialTask = 0;
        private int lastAmountTeam = 0;
        private bool changeNumberSpecialTask = false;
        private bool changeNumberTeam = false;

        public NewGame()
        {
            InitializeComponent();
            page = 1;
            showPage();
        }

        private void showPage()
        {
            mainGrid.Children.Clear();
            buttonGrid.Children.Remove(previouwButton);
            switch (page)
            {
                case 1:
                    {
                        animation(400, 350);
                        mainGrid.Children.Add(firstPageGrid);
                        break;
                    }
                case 2:
                    {
                        setHeightAndWidth(Convert.ToInt32(numberOfTeamTextBox.Text), false);

                        if (changeNumberTeam)
                        {
                            secondPageGrid.Children.Clear();
                            createSecondPage();
                        }
                        else
                        {
                            mainGrid.Children.Add(secondPageGrid);
                        }
                        buttonGrid.Children.Add(previouwButton);
                        nextButton.Content = "Dalej";
                        break;
                    }
                case 3:
                    {
                        changeNumberTeam = false;

                        setHeightAndWidth(Convert.ToInt32(numberOfSpecialTaskTextBox.Text), true);

                        if (changeNumberSpecialTask || thirdPageGrid.Children.Count == 1)
                        {
                            thirdPageGrid.Children.Clear();
                            createThirdPage();
                        }
                        else
                        {
                            mainGrid.Children.Add(thirdPageGrid);
                        }

                        buttonGrid.Children.Add(previouwButton);
                        nextButton.Content = "Dalej";
                        break;
                    }
                case 4:
                    {
                        setHeightAndWidth(Convert.ToInt32(numberOfSpecialTaskTextBox.Text), true);

                        if (changeNumberSpecialTask)
                        {
                            fourthPageGrid.Children.Clear();
                            createFourthPage();
                        }
                        else
                        {
                            mainGrid.Children.Add(fourthPageGrid);
                        }

                        changeNumberTeam = false;
                        changeNumberSpecialTask = false;

                        buttonGrid.Children.Add(previouwButton);
                        nextButton.Content = "Zakończ";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Nastąpił nieoczekiwany bład. Nastąpi zamknięcie kreatora.", "Bład", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                        break;
                    }
            }
        }

        private void createSecondPage()
        {
            secondPageGrid.Children.Add(secondPageLabel);
            teamNamesTableLabel = new Label[lastAmountTeam];
            teamNamesTableTextBox = new TextBox[lastAmountTeam];

            for (int i = 0, positionX = 0; i < lastAmountTeam; i++, positionX++)
            {
                teamNamesTableLabel[i] = AddControl.addLabel(70, 25, widthMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsTeam * heightJumpMovement, 0, 0, "Drużyna " + (i + 1).ToString());
                secondPageGrid.Children.Add(teamNamesTableLabel[i]);

                teamNamesTableTextBox[i] = AddControl.addTextBox(180, 20, widthLabelMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsTeam * heightJumpMovement, 0, 0, "");
                teamNamesTableTextBox[i].ToolTip = "W przypadku pozostawienia pustego pola, zostanie nadana nazwa domyślna";
                teamNamesTableTextBox[i].MaxLength = 40;
                teamNamesTableTextBox[i].MaxLines = 1;
                secondPageGrid.Children.Add(teamNamesTableTextBox[i]);

                if (positionX == numberOfColumnsTeam - 1)
                    positionX = -1;
            }
            mainGrid.Children.Add(secondPageGrid);
        }

        private void createThirdPage()
        {
            thirdPageGrid.Children.Add(thirdPageLabel);
            specialTaskNameTableLabel = new Label[lastAmountSpecialTask];
            specialTaskNameTableTextBox = new TextBox[lastAmountSpecialTask];

            for (int i = 0, positionX = 0; i < lastAmountSpecialTask; i++, positionX++)
            {
                specialTaskNameTableLabel[i] = AddControl.addLabel(70, 25, widthMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsSpecialTask * heightJumpMovement, 0, 0, "Zadanie " + (i + 1).ToString());
                thirdPageGrid.Children.Add(specialTaskNameTableLabel[i]);

                specialTaskNameTableTextBox[i] = AddControl.addTextBox(180, 20, widthLabelMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsSpecialTask * heightJumpMovement, 0, 0, "");
                specialTaskNameTableTextBox[i].ToolTip = "W przypadku pozostawienia pustego pola, zostanie nadana nazwa domyślna";
                specialTaskNameTableTextBox[i].MaxLines = 1;
                specialTaskNameTableTextBox[i].MaxLength = 50;
                thirdPageGrid.Children.Add(specialTaskNameTableTextBox[i]);

                if (positionX == numberOfColumnsSpecialTask - 1)
                    positionX = -1;
            }
            mainGrid.Children.Add(thirdPageGrid);
        }

        private void createFourthPage()
        {
            fourthPageGrid.Children.Add(fourthPageLabel);
            specialTaskBonusesTableLabel = new Label[lastAmountSpecialTask];
            specialTaskBonusesTableTextBox = new TextBox[lastAmountSpecialTask];

            for (int i = 0, positionX = 0; i < lastAmountSpecialTask; i++, positionX++)
            {
                if (specialTaskNameTableTextBox!=null && specialTaskNameTableTextBox[i].Text != "")
                    specialTaskBonusesTableLabel[i] = AddControl.addLabel(150, 25, widthMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsSpecialTask * heightJumpMovement, 0, 0, specialTaskNameTableTextBox[i].Text);
                else
                    specialTaskBonusesTableLabel[i] = AddControl.addLabel(70, 25, widthMovement + positionX * widthJumpMovement, heightMovement + i / numberOfColumnsSpecialTask * heightJumpMovement, 0, 0, "Zadanie " + (i + 1).ToString());
                fourthPageGrid.Children.Add(specialTaskBonusesTableLabel[i]);

                specialTaskBonusesTableTextBox[i] = AddControl.addTextBox(50, 20, widthLabelMovement + positionX * widthJumpMovement+30, heightMovement + i / numberOfColumnsSpecialTask * heightJumpMovement, 0, 0, "0");
                specialTaskBonusesTableTextBox[i].PreviewTextInput += new TextCompositionEventHandler(isNumber);
                specialTaskBonusesTableTextBox[i].GotFocus += new RoutedEventHandler(gotFocus);
                specialTaskBonusesTableTextBox[i].LostFocus += new RoutedEventHandler(textBoxLostFocus);
                specialTaskBonusesTableTextBox[i].PreviewKeyUp += new KeyEventHandler(deleteZero);
                specialTaskBonusesTableTextBox[i].MaxLength = 2;
                specialTaskBonusesTableTextBox[i].MaxLines = 1;
                fourthPageGrid.Children.Add(specialTaskBonusesTableTextBox[i]);

                if (positionX == numberOfColumnsSpecialTask - 1)
                    positionX = -1;
            }

            mainGrid.Children.Add(fourthPageGrid);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            bool nextPage = true;
            if (page == 1)
            {
                if (numberOfTeamTextBox.Text == "0" || numberOfSpecialTaskTextBox.Text == "0")
                {
                    MessageBox.Show("Liczba drużyn i zadań specjalnych musi być różna od 0", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    nextPage = false;
                }
                if (nextPage)
                {
                    if (lastAmountSpecialTask != Convert.ToInt32(numberOfSpecialTaskTextBox.Text))
                    {
                        changeNumberSpecialTask = true;
                        lastAmountSpecialTask = Convert.ToInt32(numberOfSpecialTaskTextBox.Text);
                    }

                    if (lastAmountTeam != Convert.ToInt32(numberOfTeamTextBox.Text))
                    {
                        changeNumberTeam = true;
                        lastAmountTeam = Convert.ToInt32(numberOfTeamTextBox.Text);
                    }
                }
            }
            else if (page == 2 && ownNameSpecialTaskCheckBox.IsChecked == false)
            {
                page++;
            }
            else if (page == 4)
            {
                for (int i = 0; i < Convert.ToInt32(numberOfSpecialTaskTextBox.Text); i++)
                {
                    if (specialTaskBonusesTableTextBox[i].Text == "")
                    {
                        MessageBox.Show("Żadne pole nie może pozostać puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        nextPage = false;
                    }
                }
            }
            if (nextPage)
            {
                page++;

                if (page < 5)
                {
                    showPage();
                }
                else
                {
                    finishCreateNewStalking();
                }
            }
            nextPage = true;
        }

        private void deleteZero(object sender, KeyEventArgs e)
        {
            Validate.deleteFirstZero(sender);
        }

        private void previouwButton_Click(object sender, RoutedEventArgs e)
        {
            if (page == 4 && ownNameSpecialTaskCheckBox.IsChecked == false)
                page--;
            page--;
            showPage();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void gotFocus(object sender, RoutedEventArgs e)
        {
            Validate.textBoxGotFocus(sender, e);
        }

        private void textBoxLostFocus(object sender, RoutedEventArgs e)
        {
            Validate.textBoxLostFocus(sender, e);
        }

        private void finishCreateNewStalking()
        {
            string[] nameTable = new string[lastAmountSpecialTask];
            int[] bonusesTable = new int[lastAmountSpecialTask];
            string[] nameTeamTable = new string[lastAmountTeam];
            for (int i = 0; i < lastAmountSpecialTask; i++)
            {
                if (ownNameSpecialTaskCheckBox.IsChecked == true && specialTaskNameTableTextBox[i].Text != "")
                    nameTable[i] = specialTaskNameTableTextBox[i].Text;
                else
                    nameTable[i] = "Zadanie " + (i + 1).ToString();

                bonusesTable[i] = Convert.ToInt32(specialTaskBonusesTableTextBox[i].Text);
            }
            for (int i = 0; i < lastAmountTeam; i++)
            {
                if (teamNamesTableTextBox[i].Text == "")
                    nameTeamTable[i] = "Drużyna " + (i + 1).ToString();
                else
                    nameTeamTable[i] = teamNamesTableTextBox[i].Text;
            }
            PropertiesStalking.numberOfTeam = Convert.ToInt32(numberOfTeamTextBox.Text);
            PropertiesStalking.numberOfSpecialTask = Convert.ToInt32(numberOfSpecialTaskTextBox.Text);
            PropertiesStalking.penaltiesHint = Convert.ToInt32(penaltiesHintTextBox.Text);
            PropertiesStalking.penaltiesNextPlace = Convert.ToInt32(penaltiesNextPlaceTextBox.Text);
            PropertiesStalking.completeBonusesTable(bonusesTable, nameTable, nameTeamTable);
            PropertiesStalking.finishCreateNewStalking = true;
            Close();
        }

        private void isNumber(object sender, TextCompositionEventArgs e)
        {
            Validate.isNumber(e);
        }

        private void setHeightAndWidth(int numberOfFields, bool specialTask)
        {
            int column,  newWidth, newHeight;

            if (numberOfFields < 20)
                column = 2;
            else if (numberOfFields < 35)
                column = 3;
            else
                column = 4;

            if (specialTask)
                numberOfColumnsSpecialTask = column;
            else
                numberOfColumnsTeam = column;

            newWidth = column * widthJumpMovement + widthMovement + 50;
            newHeight = (((numberOfFields / column) + 1) * heightJumpMovement) + heightMovement + 100;
            if (newHeight < 350)
                newHeight = 350;
            animation(newWidth, newHeight);
        }

        private void animation(int newWidth, int newHeight)
        {
            Width = newWidth;
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = Height;
            anim.To = newHeight;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(250));
            BeginAnimation(HeightProperty, anim);
        }
    }
}
