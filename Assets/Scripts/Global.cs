using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Global : MonoBehaviour
{
    public int score;
    public int numLives;
    // Use this for initialization
    void Start()
    {
        numLives = 3;
        score = 0;
    }
    void Update()
    {
        // player loses if they lose all of their lives
        if (numLives <= 0) {
            lose();
        }
    }

    public void win()
    {
        SceneManager.LoadScene("WinScene");;
    }

    public void lose()
    {
        SceneManager.LoadScene("LoseScene");;
    }

    public void loseLife()
    {
        numLives -= 1;
        Debug.Log("You now have " + numLives + " lives left");
    }
}