using System.Collections.Generic;

namespace Strategy.Core
{
    public sealed class MatterAmounts
    {
        private Dictionary<int, float> _matterAmounts = new Dictionary<int, float>();

        public void AddMatter(int teamID, float matterAmount)
        {
            if(_matterAmounts.ContainsKey(teamID))
                _matterAmounts[teamID] += matterAmount;
            else
                _matterAmounts.Add(teamID, matterAmount);
            
            UnityEngine.Debug.Log(_matterAmounts[teamID]);
        }
    }
}
