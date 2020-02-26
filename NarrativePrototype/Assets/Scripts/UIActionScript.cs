using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActionScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("JakeTestScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void PauseButton()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeButton()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
