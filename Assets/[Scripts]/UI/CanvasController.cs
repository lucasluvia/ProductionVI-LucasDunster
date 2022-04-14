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

    [Header("Level Names")]
    [SerializeField] string nextLevel = "MovementTest";
    [SerializeField] string thisLevel = "MovementTest";

    [Header("Sound Effects")]
    [SerializeField] private AudioSource WinFX;
    [SerializeField] private AudioSource LoseFX;

    void Start()
    {
        unfocussedCrosshair.SetActive(true);
        focussedCrosshair.SetActive(false);

        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        WinFX.Play();
        HideCrosshair();
        Time.timeScale = 0;
        gameWinPanel.SetActive(true);
    }

    public void ShowDeathScreen()
    {
        LoseFX.Play();
        HideCrosshair();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    
    public void OnNext_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevel);
    }
    
    public void OnReplay_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(thisLevel);
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
