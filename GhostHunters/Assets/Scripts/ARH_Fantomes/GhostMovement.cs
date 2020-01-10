using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public int actualWayPoint;
    private Ghost me;
    private Rigidbody rb;
    private Vector3 nextWayPoint;
    private float rotationForce = 50;

    // Start is called before the first frame update
    void Start()
    {
        nextWayPoint = WayPointsManager.instance.GetWayPoint(Random.Range(1, 32));
        rb = GetComponent<Rigidbody>();
        me = GetComponent<Ghost>();
        SelectPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (me.myState == Ghost.State.Dead)
            Destroy(this.gameObject);


        rb.AddForce(transform.up * 0.05f, ForceMode.Impulse);
        transform.LookAt(nextWayPoint, Vector3.up);
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