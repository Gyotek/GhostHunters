using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public enum GhostType { Green, Blue, Red }
    public GhostType myGhostType;

    public enum State { Hide, Revealed, Stun, Dead }
    public State myState;

    // Start is called before the first frame update
    void Start()
    {
        myState = State.Hide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
