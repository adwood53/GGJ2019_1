using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour {

    public NPCScriptableObject npc;     // Reference to the npc scriptable object 

    private string[] dialogue;
    private int currentDialogue = 0;

    private GameObject prefabModel;
    private GameObject speechBubblePrefab;

    private Transform spawnPoint;
    [SerializeField] private bool triggered = false;

    [SerializeField] private Text dialogueText;

    public Sprite speechBubbleSprite;
    private AudioClip dialogueSoundLoad;
    
    private GameObject dialogueOverlay;

    // Use this for initialization
    void Start ()
    {
        dialogueOverlay = GameObject.FindGameObjectWithTag("Dialogue Overlay");
        dialogueOverlay.SetActive(false);

        dialogueSoundLoad = npc.dialogueSound;
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
            dialogueOverlay.SetActive(true);
            dialogueText.text = npc.dialogue[currentDialogue].ToString();
            // Play sound for npc - NEED TO ADD 
            TriggerDialogue();
            triggered = false;
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log(dialogue[currentDialogue]);
        //dialogue[currentDialogue];
        if (currentDialogue <= dialogue.Length - 1)
        {
            if (npc.dialogue[currentDialogue] == "")
            {
                dialogueOverlay.SetActive(false);
                currentDialogue = 0;
            }
            else
            {
                currentDialogue++;
            }
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

    public void SpeechOverlay()
    {
        
    }
}
