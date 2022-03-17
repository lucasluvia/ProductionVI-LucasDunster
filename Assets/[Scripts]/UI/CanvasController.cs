using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    
    public void OnReplay_Pressed()
    {
        SceneManager.LoadScene("MovementTest");
    }

}
