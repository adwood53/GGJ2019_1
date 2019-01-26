using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool playerInteract = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.E)) && (playerInteract == true))
        {
            Debug.Log("ENTER TEXT");

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             playerInteract = true;
             Debug.Log("Come and talke to me! Press 'E'");
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInteract = false;
            Debug.Log("Farewell traveller!");
        }
    }
}
