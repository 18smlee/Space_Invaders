using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, Screen.height / 2 + 100, Screen.width - 10, 200));
        // Load the main scene
        // The scene needs to be added into build setting to be loaded!
        if (GUILayout.Button("New Game"))
        {
            SceneManager.LoadScene("GameplayScene");
        }
        if (GUILayout.Button("Back to Home"))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (GUILayout.Button("Exit"))
        {
            Application.Quit();
            Debug.Log("Application.Quit() only works in build,not in editor");
        }
        GUILayout.EndArea();
    }
}
