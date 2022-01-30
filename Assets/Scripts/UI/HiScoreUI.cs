using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreUI : MonoBehaviour
{
    Global globalObj;
    Text hiScoreText;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        hiScoreText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        string text = "High Score\n" + globalObj.hiScore;
        hiScoreText.text = text;
    }
}
