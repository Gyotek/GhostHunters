﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public Transform[] waypoints;

    public float ghostSpeed;

    int wayPointIndex = 0;

    private bool canMove = false;

    private void Start()
    {
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    private void Update()
    {
        GhostMoveFunction();
    }

    private void GhostMoveFunction()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[wayPointIndex].transform.position, ghostSpeed * Time.deltaTime);

            if (transform.position == waypoints[wayPointIndex].transform.position)
            {
                wayPointIndex += 1;
            }
            if (wayPointIndex == waypoints.Length)
            {
                wayPointIndex = 0;
            }
        }
    }

    public void GhostCanMove()
    {
        canMove = true;
    }

    public void GhostCantMove()
    {
        canMove = false;
    }
}