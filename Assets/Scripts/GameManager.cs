using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score = 0;

    public Text scoreText;
    public Text highScoreText;
    public Text startText;

    public AudioSource increaseSound;
    public AudioSource fallSound;
    public AudioSource backgroundMusic;

    public Sprite OffSprite;
    public Sprite OnSprite;
    public Button but;

    public void StartGame()
    {
        gameStarted = true;
        startText.fontSize = 30;
        startText.text = "Press 'Space' to turn!";
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        increaseSound.Play();
        scoreText.text = score.ToString();

        if (score == 2)
        {
            startText.text = "";
        }

        if(score > PlayerPrefs.GetInt("highscore"))
        {
            highScoreText.text = " High Score: " + score.ToString();
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    private void Update()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore");

        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {
            StartGame();
        }
    }

    public void OnMutePressed()
    {
        ChangeImage();
    }

    public void ChangeImage()
    {
        checkBackground();

        if (but.image.sprite == OnSprite)
        {
            but.image.sprite = OffSprite;
            increaseSound.mute = true;
            fallSound.mute = true;
            backgroundMusic.mute = true;
        }
        else
        {
            but.image.sprite = OnSprite;
            increaseSound.mute = false;
            fallSound.mute = false;
            backgroundMusic.mute = false;
        }
    }

    public void checkBackground()
    {
        if(backgroundMusic == null)
        {
            // get the new game object and set background music to its audio source
            backgroundMusic = GameObject.Find("backgroundMusic").GetComponent<AudioSource>();
        }
    }

}
