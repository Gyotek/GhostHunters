using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWasFound : MonoBehaviour
{
    public Animator ghostAnimator;


    public void GhostWasFoundFunction()
    {
        ghostAnimator.SetBool("PhantomFound", true);
    }
}
