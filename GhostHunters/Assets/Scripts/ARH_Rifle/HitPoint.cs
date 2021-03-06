﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class HitPoint : MonoBehaviour
{
    public static HitPoint instance;
    private void Awake() { instance = this; }

    [SerializeField] GameEvent GhostFound;
    [SerializeField] GameEvent GhostLost;
    [SerializeField] GameEvent GhostWasShot;

    [SerializeField] PSMoveController m_PSMoveController;
    [SerializeField] GameObject particleGreen;
    [SerializeField] GameObject particleRed;

    bool ghostPointed = false;
    public bool usePsMove = true;
    float ghostPointedTimer = 0f;
    [SerializeField] Camera myCamera;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;


        particleGreen.SetActive(true);
        particleRed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (usePsMove)
        {
            transform.position = Curseur.instance.hitPointPosition;

            if (m_PSMoveController.TriggerValue < 0.2  && particleGreen.activeSelf == false)
            {
                particleGreen.SetActive(true);
                particleRed.SetActive(false);
            }
            else if (m_PSMoveController.TriggerValue > 0.2 && particleRed.activeSelf == false)
            {
                particleRed.SetActive(true);
                particleGreen.SetActive(false);
            }
        }
        else
        {
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 1 << 9)

            //Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);



            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << 9))
            {
                Debug.Log("hit something");
                transform.position = hit.point;

            }

            if (!Input.GetButton("Fire1") && particleGreen.activeSelf == false)
            {
                particleGreen.SetActive(true);
                particleRed.SetActive(false);
            }
            else if (Input.GetButton("Fire1") && particleRed.activeSelf == false)
            {
                particleRed.SetActive(true);
                particleGreen.SetActive(false);
            }
        }


        /*
        if (ghostPointed == true && m_PSMoveController.TriggerValue > 0.2)
        {
            ghostPointedTimer += Time.deltaTime;
            if (ghostPointedTimer >= 3f)
            {
                Debug.Log("Ghost Killed !");
                GhostWasShot.Raise();
                ghostPointed = false;
                ghostPointedTimer = 0f;
            }
        }
        */
        sprite.transform.localScale = new Vector3(2.8f * 1 + (m_PSMoveController.TriggerValue*2), 2.8f * 1 + (m_PSMoveController.TriggerValue * 2), 2.8f * 1 + (m_PSMoveController.TriggerValue * 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ghost>())
        {
            other.GetComponent<Ghost>().Pointed();
            Debug.Log("Ghost Found !");
            sprite.color = Color.red;
            GhostFound.Raise();
            ghostPointed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ghost>())
        {
            other.GetComponent<Ghost>().Unpointed();
            Debug.Log("Ghost Lost !");
            sprite.color = Color.green;
            ghostPointedTimer = 0f;
            GhostLost.Raise();
            ghostPointed = false;
        }
    }
}
