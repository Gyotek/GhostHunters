using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] Ghosts;

    public List<List<string>> letmeread = new List<List<string>>(2);
}
