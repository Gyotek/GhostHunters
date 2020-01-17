using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<WaveArrival> numberOfWaves;

    public int waveCount = 0;
    public int ghostCount = 0;

    private int numberOfWavesLength;
    private int numberOfGhostsLength;

    public float timeSinceWaveStarted = 0;
    public bool waveStarted = false;

    public bool callWaves = false;

    [SerializeField] Transform GhostParent;

    private void Start()
    {
        callWaves = true;
        numberOfWavesLength = numberOfWaves.Count;
        Debug.Log(numberOfWavesLength);
    }

    private void Update()
    {
        if(!GhostParent.GetComponentInChildren<Ghost>() && waveCount > 0 && callWaves == false)
        {
            callWaves = true;
        }

        UpdateTime();

        if (callWaves == true)
        {
            CallWave();
        }
    }

    private void UpdateTime()
    {
        if (waveStarted == true)
        {
            timeSinceWaveStarted += Time.deltaTime;
        }
    }

    public void StartSpawningGhosts()
    {
        callWaves = true;
    }

    private void CallWave()
    { 
        if (waveCount < numberOfWavesLength)
        {
            Debug.Log("Wave Called");
            numberOfGhostsLength = numberOfWaves[waveCount].numberOfGhosts.Count;
            Debug.Log(numberOfGhostsLength);
            waveStarted = true;
            CallGhost();
        }
        else if (waveCount >= numberOfWavesLength)
        {
            callWaves = false;
        }
    }

    private void CallGhost()
    {
        Debug.Log("CallGhost");
        if (ghostCount < numberOfGhostsLength)
        {
            Debug.Log("Parcourt la liste de fantômes");
            if (timeSinceWaveStarted >= numberOfWaves[waveCount].numberOfGhosts[ghostCount].timeUntilSpawn)
            {
                Debug.Log("GhostSpawn");
                //Spawn the ghost
                Instantiate(numberOfWaves[waveCount].numberOfGhosts[ghostCount].typeOfGhost, WayPointsManager.instance.GetWayPoint(Random.Range(0,40)),Quaternion.Euler(0, 90, 90) , GhostParent);

                ghostCount += 1;
            }
        }
        else if (ghostCount >= numberOfGhostsLength)
        {
            Debug.Log("WaveEnd");
            waveCount += 1;
            ghostCount = 0;
            waveStarted = false;
            timeSinceWaveStarted = 0;
            callWaves = false;
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
    public float timeUntilSpawn;
}

