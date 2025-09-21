using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerDino;
    private PlayerMovement m_playerMovement;
    private DinoOxygen m_dinoOxygen;
    
    public Canvas MainMenu;
    public Canvas HowToPlay;
    public Canvas Credits;

    private void Start()
    {
        m_playerMovement = PlayerDino.GetComponent<PlayerMovement>();
        m_dinoOxygen = PlayerDino.GetComponent<DinoOxygen>();
    }
    public void OnPlayButtonClick()
    {
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
}
