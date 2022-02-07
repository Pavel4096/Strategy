using UnityEngine;

namespace Strategy.Core
{
    public interface INearestBuilding
    {
        Vector3 GetNearestBuildingPosition(int teamID, Vector3 position);
    }
}
