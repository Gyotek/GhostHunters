using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<WaveArrival> numberOfWaves;

    private int waveCount = 0;
    private int ghostCount = 0;

    private int numberOfWavesLength;
    private int numberOfGhostsLength;

    private void Start()
    {
        numberOfWavesLength = numberOfWaves.Count;
    }

    public void CallWave()
    { 
        if (waveCount <= numberOfWavesLength)
        {
            numberOfGhostsLength = numberOfWaves[waveCount].numberOfGhosts.Count;
            CallGhost();
        }
    }

    public void CallGhost()
    {
        
        if (ghostCount < numberOfGhostsLength)
        {
            //attendre le temps
            //Spawn the ghost

            ghostCount += 1;
        }
        else if (ghostCount == numberOfGhostsLength)
        {
            waveCount += 1;
            ghostCount = 0;
        }
    }
}

[System.Serializable]
public class WaveArrival
{
    public List<GhostSpawn> numberOfGhosts;
}

[System.Serializable]
public class GhostSpawn
{
    public GameObject typeOfGhost;
    public Transform ghostSpawn;
    public float timeUntilSpawn;
}

