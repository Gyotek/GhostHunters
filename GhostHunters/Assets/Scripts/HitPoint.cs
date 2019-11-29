using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class HitPoint : MonoBehaviour
{
    [SerializeField] GameEvent GhostFound;
    [SerializeField] GameEvent GhostLost;
    [SerializeField] GameEvent GhostWasShot;

    [SerializeField] PSMoveController m_PSMoveController;
    [SerializeField] GameObject particleGreen;
    [SerializeField] GameObject particleRed;

    bool ghostPointed = false;
    float ghostPointedTimer = 0f;

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
        if (m_PSMoveController.TriggerValue < 0.2 && particleGreen.activeSelf == false)
        {
            particleGreen.SetActive(true);
            particleRed.SetActive(false);
        }
        else if (m_PSMoveController.TriggerValue > 0.2 && particleRed.activeSelf == false)
        {
            particleRed.SetActive(true);
            particleGreen.SetActive(false);
        }

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

        sprite.transform.localScale = new Vector3(2.8f * 1 + (m_PSMoveController.TriggerValue*2), 2.8f * 1 + (m_PSMoveController.TriggerValue * 2), 2.8f * 1 + (m_PSMoveController.TriggerValue * 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ghost>())
        {
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
            Debug.Log("Ghost Lost !");
            sprite.color = Color.green;
            ghostPointedTimer = 0f;
            GhostLost.Raise();
            ghostPointed = false;
        }
    }
}
