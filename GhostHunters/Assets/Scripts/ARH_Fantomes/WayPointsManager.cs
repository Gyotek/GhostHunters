using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{
    public static WayPointsManager instance;
    private void Awake() { instance = this; }


    [SerializeField] private List<Transform> wayPoints;
    private Transform wayPointToReturn;

    private Transform closestWayPoint;
    private int closestWayPointID;

    public Vector3 GetWayPoint(int wayPointID)
    {
        wayPointToReturn = wayPoints[wayPointID - 1];
        return (wayPointToReturn.position);
    }

    public int CheckClosestWayPoint (Vector3 ghostPosition)
    {
        closestWayPoint = wayPoints[0];
        closestWayPointID = 0;
        for (int i = 1; i < wayPoints.Count; i++)
        {
            if (Vector3.Distance(wayPoints[i].position, ghostPosition) < Vector3.Distance(closestWayPoint.position, ghostPosition))
            {
                closestWayPoint = wayPoints[i];
                closestWayPointID = i;
            }
        }
        return closestWayPointID;
    }
}
