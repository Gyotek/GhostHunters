using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public Transform[] waypoints;

    public float ghostSpeed;

    [SerializeField]
    int wayPointIndex = 0;

    [SerializeField]
    private bool canMove = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    private void Update()
    {
        GhostMoveFunction();
        SwitchSprite();
    }

    private void GhostMoveFunction()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[wayPointIndex].transform.position, ghostSpeed * Time.deltaTime);

            if (transform.position == waypoints[wayPointIndex].transform.position || Input.GetKeyDown(KeyCode.J))
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

    void SwitchSprite()
    {
        if (wayPointIndex < 5 && wayPointIndex > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
