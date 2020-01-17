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
    private bool isFlipped = false;

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
        if(me.isMoving)
            Move();
    }

    void FlipSprite()
    {
        isFlipped = true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
    }
    void UnFlipSprite()
    {
        isFlipped = false;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
    }

    void Move()
    {
        //transform.LookAt(nextWayPoint, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, nextWayPoint, speed * Time.deltaTime);

        //flip
        if (transform.position.x < nextWayPoint.x && !isFlipped)
            FlipSprite();
        else if (transform.position.x > nextWayPoint.x && isFlipped)
            UnFlipSprite();

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

        if (me.myGhostType == Ghost.GhostType.Green)
        {
            switch (randomPath)
            {
                case (1):
                    if (nextWayPointID == 30 || nextWayPointID == 20 || nextWayPointID == 10)
                    {
                        nextWayPointID = nextWayPointID - 9;
                    }
                    else if (nextWayPointID == 39)
                    {
                        nextWayPointID = nextWayPointID - 11;
                    }
                    else if (nextWayPointID == 0)
                    {
                        nextWayPointID = nextWayPointID + 11;
                    }
                    else if (30 < nextWayPointID && nextWayPointID < 39)
                    {
                        nextWayPointID = nextWayPointID - 9;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID + 9;
                    }
                    break;

                case (2):
                    if (nextWayPointID == 29 || nextWayPointID == 19 || nextWayPointID == 9)
                    {
                        nextWayPointID = nextWayPointID + 9;
                    }
                    else if (nextWayPointID == 0)
                    {
                        nextWayPointID = nextWayPointID + 11;
                    }
                    else if (nextWayPointID == 39)
                    {
                        nextWayPointID = nextWayPointID - 11;
                    }
                    else if (0 < nextWayPointID && nextWayPointID < 9)
                    {
                        nextWayPointID = nextWayPointID + 9;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID - 9;
                    }
                    break;

                case (3):
                    if (nextWayPointID == 39 || nextWayPointID == 29 || nextWayPointID == 19)
                    {
                        nextWayPointID = nextWayPointID - 11;
                    }
                    else if (nextWayPointID == 9)
                    {
                        nextWayPointID = nextWayPointID + 9;
                    }
                    else if (nextWayPointID == 30)
                    {
                        nextWayPointID = nextWayPointID - 9;
                    }
                    else if (30 < nextWayPointID && nextWayPointID < 39)
                    {
                        nextWayPointID = nextWayPointID - 11;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID + 11;
                    }
                    break;

                case (4):
                    if (nextWayPointID == 20 || nextWayPointID == 10 || nextWayPointID == 0)
                    {
                        nextWayPointID = nextWayPointID + 11;
                    }
                    else if (nextWayPointID == 30)
                    {
                        nextWayPointID = nextWayPointID - 9;
                    }
                    else if (nextWayPointID == 9)
                    {
                        nextWayPointID = nextWayPointID + 9;
                    }
                    else if (0 < nextWayPointID && nextWayPointID < 9)
                    {
                        nextWayPointID = nextWayPointID + 11;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID - 11;
                    }
                    break;

                default:
                    break;
            }
        }

        else if (me.myGhostType == Ghost.GhostType.Red)
        {
            switch (randomPath)
            {
                case (1):
                    if (nextWayPointID > 34)
                    {
                        nextWayPointID = nextWayPointID - 5;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID + 5;
                    }
                    break;

                case (2):
                    if (nextWayPointID < 5)
                    {
                        nextWayPointID = nextWayPointID + 5; 
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID - 5;
                    }
                    break;

                case (3):
                    if (nextWayPointID > 19)
                    {
                        nextWayPointID = nextWayPointID - 20;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID + 20;
                    }
                    break;

                case (4):
                    if (nextWayPointID < 20)
                    {
                        nextWayPointID = nextWayPointID + 20;
                    }
                    else
                    {
                        nextWayPointID = nextWayPointID - 20;
                    }
                    break;

                default:
                    break;
            }
        }

        if (nextWayPointID < 0 || nextWayPointID >= 40)
            SelectPath();

        nextWayPoint = WayPointsManager.instance.GetWayPoint(nextWayPointID);
    }
}