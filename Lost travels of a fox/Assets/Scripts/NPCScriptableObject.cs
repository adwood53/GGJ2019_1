using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPCScriptableObject : ScriptableObject
{
    public string npcName;
    public string[] dialogue; 

    public GameObject prefab;
    public GameObject speechBubble;

    public AudioClip dialogueSound;

    public void Print()
    {
        Debug.Log("NPC Name is : " + npcName + ": With a prefab of: " + prefab);
    }
}
