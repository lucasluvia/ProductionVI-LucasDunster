using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameWinPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0;
        gameWinPanel.SetActive(true);
    }

    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    
    public void OnReplay_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MovementTest");
    }

}
