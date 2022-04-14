using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [Header("Game End Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameWinPanel;

    [Header("Crosshairs")]
    [SerializeField] GameObject unfocussedCrosshair;
    [SerializeField] GameObject focussedCrosshair;

    [Header("Next Level Name")]
    [SerializeField] string nextLevel = "MovementTest";

    void Start()
    {
        unfocussedCrosshair.SetActive(true);
        focussedCrosshair.SetActive(false);

        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        HideCrosshair();
        Time.timeScale = 0;
        gameWinPanel.SetActive(true);
    }

    public void ShowDeathScreen()
    {
        HideCrosshair();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    
    public void OnReplay_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevel);
    }

    public void SetCrosshairState(bool isFocussed)
    {
        unfocussedCrosshair.SetActive(!isFocussed);
        focussedCrosshair.SetActive(isFocussed);
    }

    private void HideCrosshair()
    {
        unfocussedCrosshair.SetActive(false);
        focussedCrosshair.SetActive(false);
    }

}
