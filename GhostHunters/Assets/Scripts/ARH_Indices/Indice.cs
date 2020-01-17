using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
public class Indice : MonoBehaviour
{
    public enum Indices { Tableau, Poupee, Tirroir }
    public Indices indiceName;

    public int numberOfGhosting = 0;
    public bool isGhosted = false;
    private MeshRenderer meshRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material ghostedMaterial;

    [SerializeField] GameEvent indiceTableau_Activated;
    [SerializeField] GameEvent indiceTableau_Disactivated;
    [SerializeField] GameEvent indicePoupee_Activated;
    [SerializeField] GameEvent indicePoupee_Disactivated;
    [SerializeField] GameEvent indiceTirroir_Activated;
    [SerializeField] GameEvent indiceTirroir_Disactivated;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GhostEnter()
    {
        numberOfGhosting++;

        if (!isGhosted)
        {
            isGhosted = true;
            meshRenderer.material = ghostedMaterial;
            Debug.Log(indiceName);
            switch (indiceName)
            {
                case (Indices.Tableau):
                    indiceTableau_Activated.Raise();
                    break;
                case (Indices.Poupee):
                    indicePoupee_Activated.Raise();
                    break;
                case (Indices.Tirroir):
                    indiceTirroir_Activated.Raise();
                    break;
                default:
                    break;
            }
        }
    }

    public void GhostExit()
    {
        isGhosted = false;
        meshRenderer.material = defaultMaterial;
        Debug.Log(indiceName);
        switch (indiceName)
        {
            case (Indices.Tableau):
                indiceTableau_Disactivated.Raise();
                break;
            case (Indices.Poupee):
                indicePoupee_Disactivated.Raise();
                break;
            case (Indices.Tirroir):
                indiceTirroir_Disactivated.Raise();
                break;
            default:
                break;
        }
    }
}
