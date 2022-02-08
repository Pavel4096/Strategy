using Strategy.Abstractions;
using System.Collections.Generic;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Strategy.Core
{
    public sealed class RemainingUnits : INearestBuilding
    {
        private Dictionary<int, int> _remainingUnits = new Dictionary<int, int>();
        private Dictionary<int, List<Transform>> _remainingBuildings = new Dictionary<int, List<Transform>>();

        public RemainingUnits()
        {
            Team[] units = Object.FindObjectsOfType<Team>();

            foreach(Team unit in units)
            {
                AddUnit(unit);
            }
        }

        public void AddUnit(ITeam team, Transform buildingTransform = null)
        {
            if(_remainingUnits.ContainsKey(team.TeamID))
                _remainingUnits[team.TeamID]++;
            else
                _remainingUnits.Add(team.TeamID, 1);
            
            if(buildingTransform != null)
            {
                if(!_remainingBuildings.ContainsKey(team.TeamID))
                    _remainingBuildings.Add(team.TeamID, new List<Transform>());
                
                _remainingBuildings[team.TeamID].Add(buildingTransform);
            }
        }

        public void RemoveUnit(ITeam team, Transform buildingTransform = null)
        {
            if(_remainingUnits.ContainsKey(team.TeamID))
            {
                _remainingUnits[team.TeamID]--;
                if(_remainingUnits[team.TeamID] == 0)
                    Debug.Log("No units left");
                
                if((buildingTransform != null) && _remainingBuildings.ContainsKey(team.TeamID))
                    _remainingBuildings[team.TeamID].Remove(buildingTransform);
            }
        }

        public Vector3 GetNearestBuildingPosition(int teamID, Vector3 position)
        {
            float minDistance = Single.MaxValue;
            Vector3 minDistancePosition = new Vector3(0.0f, 0.0f, 0.0f);

            if(_remainingBuildings.ContainsKey(teamID))
            {
                List<Transform> buildings = _remainingBuildings[teamID];


                for(var i = 0; i < buildings.Count; i++)
                {
                    float currentSqrDistance = (position - buildings[i].position).sqrMagnitude;
                    if( currentSqrDistance < minDistance)
                    {
                        minDistance = currentSqrDistance;
                        minDistancePosition = buildings[i].position;
                    }
                }
            }

            return minDistancePosition;
        }
    }
}
