//Copyright (c) Ewan Argouse - http://narudgi.github.io/

//using System.Collections;
//using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    [System.Serializable]
    public class GameEventListener : IGameEventListener
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}