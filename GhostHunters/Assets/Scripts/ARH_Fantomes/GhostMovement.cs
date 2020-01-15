using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public int nextWayPointID;
    public int closestWayPointID;
    private Ghost me;
    private Rigidbody rb;
    private Vector3 nextWayPoint;

    [SerializeField] float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        nextWayPointID = Random.Range(0, 40);
        nextWayPoint = WayPointsManager.instance.GetWayPoint(nextWayPointID);
        rb = GetComponent<Rigidbody>();
        me = GetComponent<Ghost>();
        SelectPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (me.myState == Ghost.State.Dead)
            Destroy(this.gameObject);

        //transform.LookAt(nextWayPoint, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, nextWayPoint, speed * Time.deltaTime);

        closestWayPointID = WayPointsManager.instance.CheckClosestWayPoint(transform.position);

        if (WayPointsManager.instance.CheckClosestWayPoint(transform.position) == nextWayPointID)
        {
            nextWayPointID = closestWayPointID;
            SelectPath();
        }
    }

    void SelectPath()
    {
        //nextWayPointID % 10 == 9 -> bord droit
        //nextWayPointID % 10 == 0 -> bord gauche
        //nextWayPointID /10 < 1 -> bord haut
        //nextWayPointID /10 >= 3 -> bord bas

        int randomPath = Random.Range(1, 5);
        switch (randomPath)
        {
            case (1):
                nextWayPointID = nextWayPointID + 9;
                break;
            case (2):
                nextWayPointID = nextWayPointID - 9;
                break;
            case (3):
                nextWayPointID = nextWayPointID + 11;
                break;
            case (4):
                nextWayPointID = nextWayPointID - 11;
                break;
            default:
                break;
        }
        if (nextWayPointID < 0 || nextWayPointID >= 40)
            SelectPath();

        nextWayPoint = WayPointsManager.instance.GetWayPoint(nextWayPointID);
    }
}

//navMeshAgent.remainingDistance
//navMeshAgent.destination
//NavMeshAgent.pathPending
//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(selectCible.NewCible().RuntimeValue.position - transform.position), rotationForce * Time.deltaTime);