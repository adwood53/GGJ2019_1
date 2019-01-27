using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour {

    public NPCScriptableObject npc;     // Reference to the npc scriptable object 
    public GameObject interactObject;

    private string[] dialogue;
    public int currentDialogue = 0;

    private GameObject prefabModel;
    private GameObject speechBubblePrefab;

    private Transform spawnPoint;
    private bool triggered = false;
    private bool active = false;
    private bool scored = false;

    private Text dialogueText;

    private Sprite speechBubbleSprite;
    private AudioClip dialogueSoundLoad;
    public GameObject dialogueOverlay;

    // Use this for initialization
    void Start ()
    {
        dialogueOverlay = GameObject.FindGameObjectWithTag("Dialogue Overlay");
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
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
        triggered = interactObject.GetComponent<Interaction>().playerInteract;

        if (triggered == false)
        {
            active = false;
        }

        if (triggered && !active)
        {
            Debug.Log("work");
            dialogueOverlay.SetActive(true);
            dialogueText.text = npc.dialogue[currentDialogue].ToString();
            // Play sound for npc - NEED TO ADD 
            TriggerDialogue();
            active = true;
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

                if (scored == false)
                {
                    scored = true;
                    GameObject.Find("Map Menu").GetComponent<LevelSwitcher>().score++;
                }
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

}
