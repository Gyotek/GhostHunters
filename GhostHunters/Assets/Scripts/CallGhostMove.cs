using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class CallGhostMove : StateMachineBehaviour
    {
        public GameEvent canMove;

        public GameEvent cantMove;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            canMove.Raise();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            cantMove.Raise();
        }
    }
}
