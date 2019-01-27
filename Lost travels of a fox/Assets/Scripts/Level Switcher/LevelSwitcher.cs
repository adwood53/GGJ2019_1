using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> m_lLevels = new List<GameObject>();
    public List<Vector3> m_vPositions = new List<Vector3>();

    public int score = 0;
    [SerializeField] int scoreNeeded;

    Animator m_anJournal;
    GameObject m_goCurrentLevel = null;
    GameObject m_goPlayer = null;
    GameObject m_goDialog = null;

    // Use this for initialization
    void Start()
    {
        m_anJournal = GameObject.Find("Journal").GetComponent<Animator>();
        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
        m_goDialog = GameObject.FindGameObjectWithTag("Dialogue Overlay");
    }

    // Update is called once per frame
    void Update()
    {
        if (score == scoreNeeded)
        {

        }
    }

    public void changeLevel(int p_iLevel)
    {
        m_goDialog.SetActive(true);
        Destroy(m_goCurrentLevel);
        m_goCurrentLevel = Instantiate(m_lLevels[p_iLevel]);
        m_anJournal.SetInteger("Enter", -1);
        JournalScript.m_bMenuActive = false;
        m_goPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_goPlayer.transform.position = m_vPositions[p_iLevel];

    }
}
