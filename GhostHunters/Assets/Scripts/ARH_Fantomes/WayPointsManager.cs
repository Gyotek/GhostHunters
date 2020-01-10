using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{
    public static WayPointsManager instance;
    private void Awake() { instance = this; }


    [SerializeField] private List<Transform> wayPoints;
    private Transform wayPointToReturn;

    public Vector3 GetWayPoint(int wayPointID)
    {
        wayPointToReturn = wayPoints[wayPointID - 1];
        return (wayPointToReturn.position);
    }
}
