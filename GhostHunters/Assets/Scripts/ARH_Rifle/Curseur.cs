using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curseur : MonoBehaviour
{
    public static Curseur instance;
    private void Awake() { instance = this; }

    [SerializeField] Transform controllerTr;
    [SerializeField] Transform hitpointTr;
    public Vector3 hitPointPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.blue);
    
        //transform.rotation = Quaternion.Inverse(controllerTr.rotation);
        //transform.rotation = new Quaternion(-controllerTr.rotation.x, 0, 0, 0);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 1 << 9))
        {
            Debug.Log("hit something");
            Debug.DrawLine(transform.position, hit.point, Color.red);
            hitPointPosition = hit.point;

        }
    }
}
