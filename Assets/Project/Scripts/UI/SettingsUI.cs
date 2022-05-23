using System;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject[] settingsButtons;

    private void Awake()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        foreach (GameObject settingsButton in settingsButtons)
        {
            settingsButton.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        foreach (GameObject settingsButton in settingsButtons)
        {
            settingsButton.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
