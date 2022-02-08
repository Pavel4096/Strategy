using System.Collections.Generic;

namespace Strategy.Core
{
    public sealed class MatterAmounts
    {
        private Dictionary<int, float> _matterAmounts = new Dictionary<int, float>();

        private const float _initialAmount = 200.0f;

        public void AddMatter(int teamID, float matterAmount)
        {
            if(_matterAmounts.ContainsKey(teamID))
                _matterAmounts[teamID] += matterAmount;
            else
                _matterAmounts.Add(teamID, matterAmount);
        }

        public bool RemoveMatter(int teamID, float matterAmount)
        {
            if(!_matterAmounts.ContainsKey(teamID))
                _matterAmounts.Add(teamID, _initialAmount);

            float currentAmount = _matterAmounts[teamID];
            currentAmount -= matterAmount;

            if(currentAmount >= 0)
            {
                _matterAmounts[teamID] = currentAmount;
                return true;
            }

            return false;
        }
    }
}
