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
    public partial class ShowResults : Window
    {
        List<Team> table;
        Label[,] tableLabel;
        public ShowResults()
        {
            InitializeComponent();
            MaxHeight = 700;
            MinHeight = 300;
            sortTable();
            generateControl();
        }
        private void sortTable()
        {
            table = TeamList.sortTable();
        }
        private void generateControl()
        {
            tableLabel = new Label[PropertiesStalking.numberOfTeam, 3];
            int i = 0;
            foreach (Team team in table)
            {
                if (team.OverallTime != 0)
                {
                    tableLabel[i, 0] = AddControl.addLabel(30, 25, 90, i * 35 + 10, 0, 0, (i + 1).ToString());
                    tableLabel[i, 1] = AddControl.addLabel(250, 25, 0, i * 35 + 10, 0, 0, team.TeamName);
                    tableLabel[i, 2] = AddControl.addLabel(40, 25, 0, i * 35 + 10, 85, 0, team.OverallTime.ToString());
                    tableLabel[i, 1].HorizontalAlignment = HorizontalAlignment.Center;
                    tableLabel[i, 1].FontWeight = FontWeights.Bold;
                    scoreGrid.Children.Add(tableLabel[i, 0]);
                    scoreGrid.Children.Add(tableLabel[i, 1]);
                    scoreGrid.Children.Add(tableLabel[i, 2]);
                    i++;
                }
                scoreGrid.Height = 35 * (i + 1) + 30;
                Height = scoreGrid.Height + titleGrid.Height;
            }
        }
    }
}
