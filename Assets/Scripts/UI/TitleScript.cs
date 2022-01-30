using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScript : MonoBehaviour
{
    private GUIStyle buttonStyle;
    // Use this for initialization

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void goToGamePlay() 
    {
        Debug.Log("HUHI");
        SceneManager.LoadScene("GameplayScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}