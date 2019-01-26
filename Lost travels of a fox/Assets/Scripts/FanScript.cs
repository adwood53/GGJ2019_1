using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {

    private GameObject m_Object;
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
        m_Object = objectCol.gameObject;
        rb = m_Object.GetComponent<Rigidbody>();
    }


}
