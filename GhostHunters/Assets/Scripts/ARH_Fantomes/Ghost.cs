using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public enum GhostType { Green, Blue, Red }
    public GhostType myGhostType;
    public enum State { Hide, Revealed, Stun, Dead }
    public State myState;
    [SerializeField] float life = 50;

    public bool isMoving = false;
    bool isPointed = false;

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
            if (isPointed && !stunCoroutineBool)
                StartCoroutine(StunCoroutine());
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stunned2"))
        {
            if (!isPointed && !unstunCoroutineBool)
                StartCoroutine(UnstunCoroutine());
            else
            {
                life -= Time.deltaTime;
                if (life <= 0)
                    Kill();
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Destroy(this.gameObject);
        }

    }

    void StartMoving() => isMoving = true;
    void StopMoving() => isMoving = false;

    public void Pointed()
    {
        isPointed = true;
    }
    public void Unpointed()
    {
        isPointed = false;
        StartCoroutine(UnstunCoroutine());
    }

    public void Hide()
    {
        //if(sprite.enabled == true)
        //    sprite.enabled = false;
        if (!isMoving)
            isMoving = true;
        anim.SetTrigger("disparitionTrigger");
        myState = State.Hide;
    }

    public void Reveal()
    {
        if (sprite.enabled == false)
            sprite.enabled = true;
        anim.SetTrigger("apparitionTrigger");
        myState = State.Revealed;
        StartCoroutine(HideCoroutine());
    }

    void Stun()
    {
        StopMoving();
        anim.SetTrigger("stunnedTrigger");
        myState = State.Stun;
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
        StopMoving();
        anim.SetTrigger("deathTrigger");
        myState = State.Dead;
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
