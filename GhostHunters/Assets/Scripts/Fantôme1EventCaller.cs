using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    public class Fantôme1EventCaller : MonoBehaviour
    {
        public GameEvent GhostWasFound;

        public GameEvent GhostWasShot;

        private void Update()
        {
            CallGhostWasFound();
            CallGhostWasShot();
        }

        void CallGhostWasFound()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GhostWasFound.Raise();
            }
        }

        void CallGhostWasShot()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                GhostWasShot.Raise();
            }
        }
    }
}
