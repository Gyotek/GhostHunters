using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWasShot : MonoBehaviour
{
    public Animator ghostAnimator;

    public void GhostWasShotFunction()
    {
        ghostAnimator.SetBool("PhantomWasShot", true);
    }
}
