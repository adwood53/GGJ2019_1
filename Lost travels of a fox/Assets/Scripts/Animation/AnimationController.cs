using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator m_anFox;

    enum FoxStates
    {
        Standing,
        Running,
        Crawling,
        Jumping,
        Landing
    }
    FoxStates m_fsFoxState;
    bool m_bStanding = false;
    bool m_bLanded = false;

    IEnumerator manageAnimator()
    {
        while (true)
        {
            switch (m_fsFoxState)
            {
                case FoxStates.Standing:
                    m_anFox.SetBool("Standing", true);
                    m_anFox.SetBool("Crawling", false);
                    m_anFox.SetBool("Running", false);
                    m_bStanding = true;
                    break;
                case FoxStates.Running:
                    m_anFox.SetBool("Running", true);
                    break;
                case FoxStates.Crawling:
                    m_anFox.SetBool("Standing", false);
                    m_anFox.SetBool("Crawling", true);
                    m_anFox.SetBool("Running", false);
                    m_bStanding = false;
                    break;
                case FoxStates.Jumping:
                    m_anFox.SetBool("Jumping", true);
                    m_anFox.SetBool("Landing", false);
                    m_bLanded = false;
                    StartCoroutine(setLandingTrue());
                    break;
                case FoxStates.Landing:
                    m_anFox.SetBool("Jumping", false);
                    m_anFox.SetBool("Landing", true);
                    Debug.Log("Landing State hit");
                    m_bLanded = true;
                    break;
            }
            yield return null;
        }
    }


    IEnumerator setLandingTrue()
    {
        float l_fTimer = 1f;
        while (l_fTimer > 0)
        {
            l_fTimer -= Time.deltaTime;
            yield return null;
        }

        m_anFox.SetBool("Jumping", false);
        m_anFox.SetBool("Landing", true);
        Debug.Log("Landing set");
    }

    // Use this for initialization
    void Start()
    {
        m_anFox = GetComponent<Animator>();
        m_fsFoxState = FoxStates.Crawling;
        StartCoroutine(manageAnimator());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            m_fsFoxState = FoxStates.Running;
            Debug.Log("Key Press");
        }
        else
        {
            if (m_bStanding)
                m_fsFoxState = FoxStates.Standing;
            else m_fsFoxState = FoxStates.Crawling;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (m_bStanding)
            {
                m_fsFoxState = FoxStates.Jumping;
            }
        }
        else
        {
            if (m_bLanded)
            {
                m_fsFoxState = FoxStates.Standing;
            }
        }
        if (Input.GetButtonDown("Stance"))
            if (m_bStanding)
                m_fsFoxState = FoxStates.Crawling;
            else m_fsFoxState = FoxStates.Standing;
    }
}
