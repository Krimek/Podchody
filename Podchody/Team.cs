using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podchody
{
    class Team
    {
        string teamName;
        string startTime;
        string finishTime;
        bool[] specialTask;
        int additionalPenaltiesOrBonuses;
        int overallTime;
        int numberOfHint;
        int numberOfNextPlace;
        bool onRoad;
        bool onFinish;
        bool beforeStart;

        public string TeamName
        {
            get
            {
                return teamName;
            }
            set
            {
                PropertiesStalking.change = true;
                teamName = value;
            }
        }
        public string StartTime
        {
            get
            {
                if (startTime == "")
                    return "0";
                return startTime;
            }
            set
            {
                PropertiesStalking.change = true;
                if (value == "0")
                    value = "";
                startTime = value;
                teamStatus();
                updateOverallTime();
            }
        }
        public string FinishTime
        {
            get
            {
                if (finishTime == "")
                    return "0";
                return finishTime;
            }
            set
            {
                PropertiesStalking.change = true;
                if (value == "0")
                    value = "";
                finishTime = value;
                teamStatus();
                updateOverallTime();
            }
        }
        public bool[] SpecialTask
        {
            get
            {
                return specialTask;
            }
            set
            {
                PropertiesStalking.change = true;
                specialTask = value;
                updateOverallTime();
            }
        }
        public int AdditionalPenaltiesOrBonuses
        {
            get
            {
                return additionalPenaltiesOrBonuses;
            }
            set
            {
                PropertiesStalking.change = true;
                additionalPenaltiesOrBonuses = value;
                updateOverallTime();
            }
        }
        public int OverallTime
        {
            get
            {
                return overallTime;
            }
            private set
            {
                PropertiesStalking.change = true;
                overallTime = value;
            }
        }
        public int NumberOfNextPlace
        {
            get
            {
                return numberOfNextPlace;
            }
            set
            {
                PropertiesStalking.change = true;
                numberOfNextPlace = value;
                updateOverallTime();
            }
        }
        public int NumberOfHint
        {
            get
            {
                return numberOfHint;
            }
            set
            {
                PropertiesStalking.change = true;
                numberOfHint = value;
                updateOverallTime();
            }
        }
        public bool OnRoad
        {
            get
            {
                return onRoad;
            }
            private set
            {
                PropertiesStalking.change = true;
                onRoad = value;
            }
        }
        public bool OnFinish
        {
            get
            {
                return onFinish;
            }
            private set
            {
                PropertiesStalking.change = true;
                onFinish = value;
            }
        }

        public bool BeforeStart
        {
            get
            {
                return beforeStart;
            }

            private set
            {
                PropertiesStalking.change = true;
                beforeStart = value;
            }
        }

        public Team() { }

        public Team(string nameOfTeam)
        {
            teamName = nameOfTeam;
            newTeam();
        }

        private void newTeam()
        {
            startTime = "";
            finishTime = "";
            specialTask = new bool[PropertiesStalking.numberOfSpecialTask];
            additionalPenaltiesOrBonuses = 0;
            overallTime = 0;
            onRoad = false;
            onFinish = false;
            TeamList.ListOfTeam.Add(this);
            PropertiesStalking.change = true;
            teamStatus();
        }

        private void updateOverallTime()
        {
            if (startTime.Length == 5 && finishTime.Length == 5)
            {
                int start = Convert.ToInt32(startTime.Substring(0, 2)) * 60 + Convert.ToInt32(startTime.Substring(3));
                int finish = Convert.ToInt32(finishTime.Substring(0, 2)) * 60 + Convert.ToInt32(finishTime.Substring(3));
                if (finish < start)
                    finish += (60 * 24);
                finish = finish - start;
                finish += (NumberOfHint * PropertiesStalking.penaltiesHint);
                finish += (NumberOfNextPlace * PropertiesStalking.penaltiesNextPlace);
                finish += (AdditionalPenaltiesOrBonuses);
                for (int i = 0; i < PropertiesStalking.numberOfSpecialTask; i++)
                {
                    if (SpecialTask[i])
                        finish = finish - PropertiesStalking.bonusesSpecialTask[i];
                }
                OverallTime = finish;
            }
            else
            {
                OverallTime = 0;
            }
        }

        private void teamStatus()
        {
            if (startTime.Length != 5)
            {
                BeforeStart = true;
                OnRoad = false;
                OnFinish = false;
            }
            else if (startTime.Length == 5 && finishTime.Length != 5)
            {
                BeforeStart = false;
                OnRoad = true;
                OnFinish = false;
            }
            else if (startTime.Length == 5 && finishTime.Length == 5)
            {
                BeforeStart = false;
                OnFinish = true;
                OnRoad = false;
            }
        }
    }
}
