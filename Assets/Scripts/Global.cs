using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Global : MonoBehaviour
{
    public int score;
    public int hiScore;
    public int numLives;
    public GameObject canvas;
    public GameObject heart;

    // Use this for initialization
    void Start()
    {
        numLives = 3;
        hiScore = PlayerPrefs.GetInt("hiScore");
        score = 0;

        float xSpace = 28.0f;
        for (int i = 0; i < numLives; i++) {
            var newHeart = Instantiate(heart);
            newHeart.transform.SetParent(canvas.transform, false);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector3(-217 + i * xSpace, -146, 0);
        }
        
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
        SceneManager.LoadScene("WinScene");
        if (score > hiScore) {
            updateHiScore();
        }
    }

    public void lose()
    {
        SceneManager.LoadScene("LoseScene");
        if (score > hiScore) {
            updateHiScore();
        }
    }

    public void loseLife()
    {
        numLives -= 1;
        // camera shake
        CameraShake camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        StartCoroutine(camShake.Shake(0.15f, 0.4f));
    }

    public void updateHiScore() {
        hiScore = score;
        PlayerPrefs.SetInt("hiScore", hiScore);
        PlayerPrefs.Save();
    }
}