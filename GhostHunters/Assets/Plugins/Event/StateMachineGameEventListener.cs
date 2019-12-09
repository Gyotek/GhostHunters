using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    public class StateMachineGameEventListener : StateMachineBehaviour, IGameEventListener
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => Event.RegisterListener(this);

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => Event.UnregisterListener(this);

        public void OnEventRaised() => Response.Invoke();
    }
}