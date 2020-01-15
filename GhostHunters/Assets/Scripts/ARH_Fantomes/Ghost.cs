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

    [SerializeField] float unstunTimer = 2;
    [SerializeField] float stunTimer = 0.5f;
    [SerializeField] float minHideAgainTimer = 2.5f;
    [SerializeField] float maxHideAgainTimer = 5f;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stunned2"))
        {
            life -= Time.deltaTime;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Destroy(this.gameObject);
        }
    }

    void StartMoving() => isMoving = true;
    void StopMoving() => isMoving = false;

    public void Hide()
    {
        if(sprite.enabled == false)
            sprite.enabled = true;
        anim.SetTrigger("disparitionTrigger");
        myState = State.Hide;
    }

    public void Pointed()
    {
        isPointed = true;
        StunCoroutine();
    }
    public void Unpointed()
    {
        isPointed = false;
        UnstunCoroutine();
    }

    public void Reveal()
    {
        if (sprite.enabled == true)
            sprite.enabled = false;
        anim.SetTrigger("apparitionTrigger");
        myState = State.Revealed;
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
    }

    public void Kill()
    {
        StopMoving();
        anim.SetTrigger("deathTrigger");
        myState = State.Dead;
    }

    IEnumerator HideCoroutine()
    {
        float hideAgainTimer = Random.Range(minHideAgainTimer, maxHideAgainTimer);
        yield return new WaitForSeconds(hideAgainTimer);
        Hide();
    }

    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(stunTimer);
        Stun();
    }

    IEnumerator UnstunCoroutine()
    {
        yield return new WaitForSeconds(unstunTimer);
        Unstun();
    }
}
