using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalScript : MonoBehaviour
{

    List<GameObject> m_goTabs = new List<GameObject>();
    Animator m_anJournal;
    public static bool m_bMenuActive = false;

    // Use this for initialization
    void Start()
    {
        m_goTabs.Add(GameObject.Find("Map Menu"));
        m_goTabs.Add(GameObject.Find("Diary Menu"));
        m_goTabs.Add(GameObject.Find("Settings Menu"));
        for (int iCount = 1; iCount < m_goTabs.Count; iCount++)
            m_goTabs[iCount].SetActive(false);
        m_anJournal = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (!m_bMenuActive)
            {
                m_anJournal.SetInteger("Enter", 1);
                m_bMenuActive = true;
            }
            else
            {
                m_anJournal.SetInteger("Enter", -1);
                m_bMenuActive = false;
            }
        }
    }


    public void tabSwitch(int p_iTab)
    {
        for (int iCount = 0; iCount < m_goTabs.Count; iCount++)
            m_goTabs[iCount].SetActive(false);
        m_goTabs[p_iTab].SetActive(true);
    }
}
