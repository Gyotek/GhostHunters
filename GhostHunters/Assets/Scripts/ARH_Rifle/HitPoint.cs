using System.Collections;
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

    public PSMoveController m_PSMoveController;
    [SerializeField] GameObject particleGreen;
    [SerializeField] GameObject particleRed;

    public bool ghostPointed = false;
    public bool usePsMove = true;
    float ghostPointedTimer = 0f;
    [SerializeField] Camera myCamera;

    bool isScalingUp = false;
    bool isScalingDown = false;
    Vector3 MaxScale;
    Vector3 MinScale;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;

        MaxScale = transform.localScale;
        MinScale = transform.localScale / 2;

        particleGreen.SetActive(true);
        particleRed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isScalingDown)
        {
            Debug.Log("down");
            transform.localScale = Vector3.Lerp(transform.localScale, MinScale, 0.2f);
            if (transform.localScale.x <= MinScale.x + 0.1f) isScalingDown = false;
        }
        if (isScalingUp)
        {
            Debug.Log("up");
            transform.localScale = Vector3.Lerp(transform.localScale, MaxScale, 0.2f);
            if (transform.localScale.x >= MaxScale.x - 0.1f) isScalingUp = false;
        }

        transform.Rotate(Vector3.forward, 20);

        //position du pointeur
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
                transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z + 0.2f);

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
        if (usePsMove)
            sprite.transform.localScale = new Vector3(2.8f * 1 - (m_PSMoveController.TriggerValue*2), 2.8f * 1 - (m_PSMoveController.TriggerValue * 2), 2.8f * 1 - (m_PSMoveController.TriggerValue * 2));

        if ((usePsMove && m_PSMoveController.TriggerValue > 0.2) || (!usePsMove && Input.GetButtonDown("Fire1")))
        {
            if (!usePsMove)
            {
                isScalingDown = true;
                if (isScalingUp) isScalingUp = false;
            }
            AudioManager.instance.PlaySFX(AudioManager.SFX.Laser);
            AudioManager.instance.PlaySFX(AudioManager.SFX.LaserBurst);
        }
        else if ((usePsMove && m_PSMoveController.TriggerValue < 0.2) || (!usePsMove && Input.GetButtonUp("Fire1")))
        {
            if (!usePsMove)
            {
                isScalingUp = true;
                if (isScalingDown) isScalingDown = false;
            }
            AudioManager.instance.StopSfx(AudioManager.SFX.Laser);
        }
    }


    public void KilledAGhost()
    {
        sprite.color = Color.green;
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
