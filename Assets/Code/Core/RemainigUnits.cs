using Strategy.Abstractions;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class RemainingUnits
    {
        private Dictionary<int, int> _remainingUnits = new Dictionary<int, int>();

        public RemainingUnits()
        {
            Team[] units = Object.FindObjectsOfType<Team>();

            foreach(Team unit in units)
            {
                AddUnit(unit);
            }
        }

        public void AddUnit(ITeam team)
        {
            if(_remainingUnits.ContainsKey(team.TeamID))
                _remainingUnits[team.TeamID]++;
            else
                _remainingUnits.Add(team.TeamID, 1);
        }

        public void RemoveUnit(ITeam team)
        {
            if(_remainingUnits.ContainsKey(team.TeamID))
            {
                _remainingUnits[team.TeamID]--;
                if(_remainingUnits[team.TeamID] == 0)
                    Debug.Log("No units left");
            }
        }
    }
}
