using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool playerInteract = false;
    private bool playerEntered = false;

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetButtonDown("Interact")) && (playerEntered == true))
        {
            playerInteract = true;
            Debug.Log("asd");
        }
        if ((Input.GetButtonUp("Interact")))
        {
            playerInteract = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
             Debug.Log("Come and talke to me! Press 'E'");
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = false;
            Debug.Log("Farewell traveller!");
            GetComponentInParent<NPCDisplay>().dialogueOverlay.SetActive(false);
            GetComponentInParent<NPCDisplay>().currentDialogue = 0;
        }
    }
}
