using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public GameObject pausePanel;
    public GameObject losePanel;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Awake()
    {
        MakeInstance();
    }
    private void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void PauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitButton()
    {
        SceneManager.LoadScene(0);
        losePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        losePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OptionButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
}
