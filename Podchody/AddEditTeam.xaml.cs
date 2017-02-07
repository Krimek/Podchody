using System.Linq;
using System.Windows;

namespace Podchody
{
    public partial class AddEditTeam : Window
    {
        bool newTeam = false;
        int numberTeam;
        public AddEditTeam(string nameTeam, int numberOfTeam, bool edit)
        {
            InitializeComponent();
            if (edit)
            {
                Title = "Edytuj nazwę drużyny";
                nameTeamLabel.Content = nameTeam;
                numberTeam = numberOfTeam;
            }
            else
            {
                Title = "Dodaj nową drużynę";
                nameTeamLabel.Content = "Drużyna " + (PropertiesStalking.numberOfTeam + 1);
                newTeam = true;
            }
        }

        private void apllyButton_Click(object sender, RoutedEventArgs e)
        {
            if (newTeam)
            {
                PropertiesStalking.numberOfTeam++;
                if (textBox.Text == "")
                    new Team("Drużyna " + PropertiesStalking.numberOfTeam);
                else
                    new Team(textBox.Text);
                Close();
            }
            else
            {
                if (textBox.Text == "")
                {
                    TeamList.ListOfTeam.ElementAt(numberTeam).TeamName = "Drużyna " + (numberTeam + 1);
                }
                else
                {
                    TeamList.ListOfTeam.ElementAt(numberTeam).TeamName = textBox.Text;
                }
                Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
