using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public int actualWayPoint;
    public int closestWayPoint;
    private Ghost me;
    private Rigidbody rb;
    private Vector3 nextWayPoint;

    [SerializeField] float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        actualWayPoint = Random.Range(1, 32) - 1;
        nextWayPoint = WayPointsManager.instance.GetWayPoint(actualWayPoint);
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

        closestWayPoint = WayPointsManager.instance.CheckClosestWayPoint(transform.position)+1;

        if (WayPointsManager.instance.CheckClosestWayPoint(transform.position)+1 == actualWayPoint)
        {
            actualWayPoint = Random.Range(1, 32) - 1;
            nextWayPoint = WayPointsManager.instance.GetWayPoint(actualWayPoint);
        }
    }

    void SelectPath()
    {
        //Random suivant le me.myGhostType
    }
}

//navMeshAgent.remainingDistance
//navMeshAgent.destination
//NavMeshAgent.pathPending
//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(selectCible.NewCible().RuntimeValue.position - transform.position), rotationForce * Time.deltaTime);