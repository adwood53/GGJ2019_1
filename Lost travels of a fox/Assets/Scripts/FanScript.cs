using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {

    private GameObject g_Object;
    private Collider objectCol;
    private Rigidbody rb;
    [SerializeField] [Range(0,1)] private float power;

	// Use this for initialization
	
    void OnTriggerStay(Collider collisionStay)
    {
        rb.velocity += new Vector3(0f, power, 0f);
    }

    void OnTriggerEnter(Collider col)
    {
        objectCol = col;
        g_Object = objectCol.gameObject;
        rb = g_Object.GetComponent<Rigidbody>();
    }


}
