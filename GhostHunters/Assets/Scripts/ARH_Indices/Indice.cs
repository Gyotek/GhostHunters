using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indice : MonoBehaviour
{
    public int numberOfGhosting = 0;
    public bool isGhosted = false;
    private MeshRenderer meshRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material ghostedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ghost>())
        {
            numberOfGhosting++;
            Debug.Log("Object : " + gameObject.name + " triggered with : " + other.gameObject.name);

            if (!isGhosted)
            {
                isGhosted = true;
                meshRenderer.material = ghostedMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ghost>())
        {
            numberOfGhosting--;
            Debug.Log("Object : " + gameObject.name + " triggered with : " + other.gameObject.name);

            if (numberOfGhosting <= 0)
            {
                isGhosted = false;
                meshRenderer.material = defaultMaterial;
            }
        }
    }
}
