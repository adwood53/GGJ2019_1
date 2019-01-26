using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour {

    public NPCScriptableObject npc;

    private string[] dialogue;
    private int currentDialogue = 0;

    private GameObject prefabModel;
    //public GameObject SpeechBubblePrefab;

    private Transform spawnPoint;
    [SerializeField] private bool triggered = false;
    
    //public Sprite speechBubbleSprite;
    // public AudioClip dialogueSoundLoad;

    // Use this for initialization
    void Start ()
    {
        // dialogueText = npc.dialogue;
        // dialogueSoundLoad = npc.dialogueSound;
        dialogue = npc.dialogue; 
        prefabModel = npc.prefab;
        Instantiate(prefabModel, transform);
        Debug.Log("BEAR SHOULD LOAD: " + npc.prefab);
        npc.Print();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (triggered)
        {
            TriggerDialogue();

            triggered = false;
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log(dialogue[currentDialogue]);
        //dialogue[currentDialogue];
        if (currentDialogue < dialogue.Length - 1)
        {
            currentDialogue++;
        }
        else
        {
            currentDialogue = 0;
        }

        // Print Dialogue[currentDialogue] 
        // if (currentDialogue < dialogue.size)
        //CurrentDialogue ++ 
        // else 
        //currentDialogue = 0 

        // If player interacts with npc
        // load / display dialogue 1 
        // if player presses next dialogue button
        // load through loop and display next dialogue
        // if dialogue finished 
        // trigger level event 

    }
}
