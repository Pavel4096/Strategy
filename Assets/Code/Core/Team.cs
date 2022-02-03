using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class Team : MonoBehaviour, ITeam
    {
        public int TeamID => _teamID;

        [SerializeField] private int _teamID;

        public void SetTeamID(int teamID)
        {
            if(_teamID == 0)
                _teamID = teamID;
        }
    }
}
