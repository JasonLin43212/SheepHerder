using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{

    public GameObject PauseScreen;
    public GameObject PauseButton;

    bool GamePaused;


    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void PauseGame()
    {
        GamePaused = true;
        PlayersScore.pause = true;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        GamePaused = false;
        PlayersScore.pause = false;
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");

    }
}