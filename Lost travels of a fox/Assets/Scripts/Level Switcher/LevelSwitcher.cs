using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{

    [SerializeField]
    List<GameObject> m_lLevels = new List<GameObject>();

    Animator m_anJournal;
    GameObject m_goCurrentLevel;

    // Use this for initialization
    void Start()
    {
        m_anJournal = GameObject.Find("Journal").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeLevel(int p_iLevel)
    {
        m_anJournal.SetInteger("Enter", -1);
        JournalScript.m_bMenuActive = false;
        Destroy(m_goCurrentLevel);
        m_goCurrentLevel = m_lLevels[p_iLevel];
        
    }
}
