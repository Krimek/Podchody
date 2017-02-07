using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podchody
{
    static class PropertiesStalking
    {
        static public bool stalking = false;
        static public int numberOfTeam = 0;
        static public int numberOfSpecialTask = 0;
        static public int penaltiesHint;
        static public int penaltiesNextPlace;
        static public int[] bonusesSpecialTask;
        static public string[] specialTaskNames;
        static public string[] nameTeams;
        static public string pathSaveFile = "";
        static public string pathLoadFile = "";
        static public bool finishCreateNewStalking = false;
        static public bool change = false;
        static public bool createTimer = false;
        static public bool load;
        static public bool finishLoad = false;
        static public bool welcomeWindow = true;

        public static void completeBonusesTable(int[] bonusTable, string[] namesTable, string[] nameTeam)
        {
            bonusesSpecialTask = new int[numberOfSpecialTask];
            specialTaskNames = new string[numberOfSpecialTask];
            nameTeams = new string[numberOfTeam];
            for (int i = 0; i < numberOfSpecialTask; i++)
            {
                specialTaskNames[i] = namesTable[i];
                bonusesSpecialTask[i] = bonusTable[i];
            }
            for (int i = 0; i < numberOfTeam; i++)
            {
                nameTeams[i] = nameTeam[i];
            }
        }
    }
}
