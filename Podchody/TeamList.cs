using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podchody
{
    static class TeamList
    {
        public static List<Team> ListOfTeam = new List<Team>();

        public static int getOnRoad()
        {
            int amount = 0;
            foreach(Team team in ListOfTeam)
            {
                if (team.OnRoad)
                    amount++;
            }
            return amount;
        }
        public static int getOnFinish()
        {
            int amount = 0;
            foreach(Team team in ListOfTeam)
            {
                if (team.OnFinish)
                    amount++;
            }
            return amount;
        }
        public static int getBeforeStart()
        {
            int amount = 0;
            foreach(Team team in ListOfTeam)
            {
                if (team.BeforeStart)
                    amount++;
            }
            return amount;
        }

        public static List<Team> sortTable()
        {
            return ListOfTeam.OrderBy(Team => Team.OverallTime).ToList();
        }

        public static int getTeamPosition(string teamName)
        {
            List<Team> list = sortTable();
            int i = 1;
            foreach(Team team in list)
            {
                if (team.TeamName == teamName)
                    if (team.OverallTime == 0)
                        return 0;
                    else
                        return i;
                if (team.OverallTime != 0)
                    i++;
            }
            return 0;
        }
    }
}
