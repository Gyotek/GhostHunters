//Copyright (c) Ewan Argouse - http://narudgi.github.io/

//using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    public class GameEventListenerMultiple : MonoBehaviour
    {
#if UNITY_EDITOR
        public string Title = "";
#endif

        public List<GameEventListener> gameEventListeners;
        private void OnEnable()
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; i--)
            {
                gameEventListeners[i].Event?.RegisterListener(this.gameEventListeners[i]);
            }
        }

        private void OnDisable()
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; i--)
            {
                gameEventListeners[i].Event?.UnregisterListener(this.gameEventListeners[i]);
            }
        }
    }
}