using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost2Move : MonoBehaviour
{
    public Transform destination;

    bool canMove = false;

    public float ghost2Speed;

    private void Update()
    {
        Ghost2IsMoving();
    }

    private void Ghost2IsMoving()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, ghost2Speed * Time.deltaTime);
        }
    }

    public void Ghost2CanMove()
    {
        canMove = true;
    }

    public void Ghost2CantMove()
    {
        canMove = false;
    }
}
