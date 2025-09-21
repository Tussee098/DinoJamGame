using Player;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerDino;
    private PlayerMovement m_playerMovement;
    private DinoOxygen m_dinoOxygen;

    public int BoatBoardsRemaining;
    
    public Canvas MainMenu;
    public Canvas HowToPlay;
    public Canvas Credits;

    public AudioSource TitleIntro;
    public AudioSource TitleLoop;
    public AudioSource DiveLoop;
    public AudioSource CreditLoop;

    private void Start()
    {
        BoatBoardsRemaining = GameObject.FindGameObjectsWithTag("BoatBoard").Length;
        m_playerMovement = PlayerDino.GetComponent<PlayerMovement>();
        m_dinoOxygen = PlayerDino.GetComponent<DinoOxygen>();
    }
    public void OnPlayButtonClick()
    {
        TitleLoop.Stop();
        TitleIntro.Stop();
        DiveLoop.Play();
        m_playerMovement.enabled = true;
        m_dinoOxygen.enabled = true;
        MainMenu.enabled = false;
    }
    public void OnCreditsButtonClick()
    {
        MainMenu.gameObject.SetActive(false);
        Credits.gameObject.SetActive(true);
    }
    public void OnBackToMainMenuClick()
    {
        MainMenu.gameObject.SetActive(true);
        HowToPlay.gameObject.SetActive(false);
        Credits.gameObject.SetActive(false);
        m_playerMovement.enabled = false;
    }
    public void OnHowtoPlayClick()
    {
        MainMenu.gameObject.SetActive(false);
        HowToPlay.gameObject.SetActive(true);
    }

    public void RepairBoat()
    {
        Debug.Log("Repair Boat");
        Debug.Log(BoatBoardsRemaining);
        BoatBoardsRemaining--;
    }
}
