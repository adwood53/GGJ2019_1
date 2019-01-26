using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      /*  var playerObject = GameObject.Find("Player");
        var NPCObject = GameObject.Find("Interactable");
        Vector3 playerPosition = playerObject.transform.position;
        Vector3 NPCPosition = NPCObject.transform.position;
        float dist = Vector3.Distance(playerPosition, NPCPosition);

        if (dist <= 5 && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TEXT");
        }*/

    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
