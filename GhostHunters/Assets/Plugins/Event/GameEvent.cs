// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    [System.Serializable]
    public class SubGameEvent
    {
        public string name;
        public GameEvent gameEvent;

        public SubGameEvent(string name, GameEvent gameEvent)
        {
            this.name = name;
            this.gameEvent = gameEvent;
        }
    }

    [CreateAssetMenu(menuName = AssetMenuFolder.GameEvent + "Game Event")]
    public class GameEvent : ScriptableObject
    {
        public List<SubGameEvent> subEvents = new List<SubGameEvent>();
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IGameEventListener> eventListeners = 
            new List<IGameEventListener>();

        public void Raise()
        {
            for(int i = eventListeners.Count -1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(IGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}