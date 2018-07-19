using System;
using System.Collections.Generic;

namespace Happy_Zoo
{
    /// <summary>
    /// sets and returns the goals to win the game
    /// </summary>
    public class MainGoal
    {
        private int totalReputationGoal { get; set; }
        private float coinsGoal { get; set; }
        private int currentVisitorsGoal { get; set; }

        public MainGoal(){
            totalReputationGoal = 800;
            coinsGoal = 10000;
            currentVisitorsGoal = 3000;

        }
    }
}
