using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class Ghost : MonoBehaviour
{
    public enum GhostType { Green, Blue, Red }
    public GhostType myGhostType;
    public enum State { Hide, Revealed, Stun, Dead }
    public State myState;
    [SerializeField] float life = 50;

    public bool isMoving = false;
    bool isPointed = false;

    [SerializeField] GameEvent ghostKilledEvent;
    [SerializeField] GameEvent ghostAppearEvent;
    [SerializeField] GameEvent ghostDisappearEvent;
    [SerializeField] GameEvent ghostStunnedEvent;


    bool hideCoroutineBool = false;
    bool revealCoroutineBool = false;
    bool stunCoroutineBool = false;
    bool unstunCoroutineBool = false;

    [SerializeField] float revealTimer = 0.1f;
    [SerializeField] float unstunTimer = 2;
    [SerializeField] float stunTimer = 0.5f;
    [SerializeField] float minHideAgainTimer = 2.5f;
    [SerializeField] float maxHideAgainTimer = 5f;

    [SerializeField] SpriteRenderer sprite;
    public Animator anim;
    //private GhostMovement ghostMovement;

    // Start is called before the first frame update
    void Start()
    {
        //ghostMovement = GetComponent<GhostMovement>();
        myState = State.Hide;
    }

    // Update is called once per frame
    void Update()
    {
        CoroutinesChecker();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hided"))
        {
            if (isPointed && !revealCoroutineBool)
                StartCoroutine(RevealCoroutine());
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
        {
            if (!hideCoroutineBool)
                StartCoroutine(HideCoroutine());
            if (isPointed && !stunCoroutineBool && (HitPoint.instance.m_PSMoveController.TriggerValue > 0.2 || Input.GetButton("Fire1")))
                StartCoroutine(StunCoroutine());
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stunned2"))
        {
            if (!isPointed && !unstunCoroutineBool)
                StartCoroutine(UnstunCoroutine());
            else if (HitPoint.instance.m_PSMoveController.TriggerValue > 0.2 || Input.GetButton("Fire1"))
            {
                life -= Time.deltaTime;
                if (life <= 0)
                    Kill();
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            if(HitPoint.instance.sprite.color == Color.red)
                HitPoint.instance.KilledAGhost();
            StopAllCoroutines();
            Destroy(this.gameObject);
        }

    }

    void StartMoving() { if (!isMoving) isMoving = true; }
    void StopMoving() { if (isMoving) isMoving = false; }

    public void Pointed()
    {
        if (!isPointed)
            isPointed = true;
    }
    public void Unpointed()
    {
        if (isPointed)
            isPointed = false;
        StartCoroutine(UnstunCoroutine());
    }

    public void Hide()
    {
        if (myState == State.Hide) return;

        //if(sprite.enabled == true)
        //    sprite.enabled = false;
        if (!isMoving)
            isMoving = true;
        anim.SetTrigger("disparitionTrigger");
        myState = State.Hide;
    }

    public void Reveal()
    {
        if (myState == State.Revealed) return;

        if (sprite.enabled == false)
            sprite.enabled = true;
        anim.SetTrigger("apparitionTrigger");
        myState = State.Revealed;
        StartCoroutine(HideCoroutine());
    }

    void Stun()
    {
        if (myState == State.Stun) return;

        StopMoving();
        anim.SetTrigger("stunnedTrigger");
        myState = State.Stun;
        StopCoroutine(HideCoroutine());
        ghostStunnedEvent.Raise();
    }

    void Unstun()
    {
        StartMoving();
        anim.SetTrigger("unstunnedTrigger");
        myState = State.Revealed;
        StartCoroutine(HideCoroutine());
    }

    public void Kill()
    {
        if (myState == State.Dead) return;

        StopMoving();
        anim.SetTrigger("deathTrigger");
        myState = State.Dead;
        StopAllCoroutines();
    }

    void CoroutinesChecker()
    {
        if (hideCoroutineBool && isPointed)
        {
            hideCoroutineBool = false;
            StopCoroutine(HideCoroutine());
        }
        if (stunCoroutineBool && !isPointed)
        {
            stunCoroutineBool = false;
            StopCoroutine(StunCoroutine());
        }
        if (unstunCoroutineBool && isPointed)
        {
            unstunCoroutineBool = false;
            StopCoroutine(UnstunCoroutine());
        }
    }

    IEnumerator HideCoroutine()
    {
        hideCoroutineBool = true;
        float hideAgainTimer = Random.Range(minHideAgainTimer, maxHideAgainTimer);
        yield return new WaitForSeconds(hideAgainTimer);
        Hide();
        hideCoroutineBool = false;
    }

    IEnumerator RevealCoroutine()
    {
        revealCoroutineBool = true;
        yield return new WaitForSeconds(revealTimer);
        Reveal();
        revealCoroutineBool = false;
    }

    IEnumerator StunCoroutine()
    {
        stunCoroutineBool = true;
        yield return new WaitForSeconds(stunTimer);
        Stun();
        stunCoroutineBool = false;
    }

    IEnumerator UnstunCoroutine()
    {
        unstunCoroutineBool = true;
        yield return new WaitForSeconds(unstunTimer);
        Unstun();
        unstunCoroutineBool = false;
    }
}
