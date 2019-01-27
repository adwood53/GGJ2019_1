using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
    public int LeftTextPosX = -325;
    public int LeftTextPosY = -2000;
    public int RightTextPosX = 300;
    public int RightTextPosY = -2000;
    public int TopTextPosX = 0;
    public int TopTextPosY = -1800;
    public int ScrollSpeed = 1;
    public Text CreditsTextTop;
    public Text CreditsTextLeft;
    public Text CreditsTextRight;
    public int index = 0;


    // Use this for initialization
    void Start () {

        RectTransform LeftCreditsRT = CreditsTextLeft.GetComponent<RectTransform>();
        LeftCreditsRT.anchoredPosition = new Vector3(LeftTextPosX, LeftTextPosY, 0);


        RectTransform RightCreditsRT = CreditsTextRight.GetComponent<RectTransform>();
        RightCreditsRT.anchoredPosition = new Vector3(RightTextPosX, RightTextPosY, 0);


        RectTransform TopCreditsRT = CreditsTextTop.GetComponent<RectTransform>();
        TopCreditsRT.anchoredPosition = new Vector3(TopTextPosX, TopTextPosY, 0);
    }
	
	// Update is called once per frame
	void Update () {

        RightTextPosY += ScrollSpeed;
        LeftTextPosY += ScrollSpeed;
        TopTextPosY += ScrollSpeed;

        RectTransform LeftCreditsRT = CreditsTextLeft.GetComponent<RectTransform>();
        LeftCreditsRT.anchoredPosition = new Vector3(LeftCreditsRT.anchoredPosition.x, LeftTextPosY, 0);


        RectTransform RightCreditsRT = CreditsTextRight.GetComponent<RectTransform>();
        RightCreditsRT.anchoredPosition = new Vector3(RightCreditsRT.anchoredPosition.x, RightTextPosY, 0);

        RectTransform TopCreditsRT = CreditsTextTop.GetComponent<RectTransform>();
        TopCreditsRT.anchoredPosition = new Vector3(TopCreditsRT.anchoredPosition.x, TopTextPosY, 0);
        if (TopTextPosY == 1100)
        {
            SceneManager.LoadScene(index);
        }

    }
}
