using System;
using UnityEngine;
using UnityEngine.UI;

public class DinoOxygen : MonoBehaviour
{
    public float MaxOxygen;
    public float MaxOxygenIncrement;
    private float m_CurrentDinoOxygen;

    public Transform SpawnPoint;

    public float RespawnTimer;
    private float m_currentRespawnTimer;

    [Range(0f, 1f)] public float value = 0f; // 0 = transparent, 1 = fully blue
    public Image blueOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentRespawnTimer = RespawnTimer;
        ResetOxygen();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateOxygen();
    }

    public void ResetOxygen()
    {
        m_CurrentDinoOxygen = MaxOxygen;
    }

    private void UpdateOxygen()
    {
        m_CurrentDinoOxygen -= Time.deltaTime;

        if(m_CurrentDinoOxygen > 0f) {
            Color c = blueOverlay.color;
            c.a = Mathf.Clamp(0.8f - (m_CurrentDinoOxygen / MaxOxygen),0f,0.8f);
            blueOverlay.color = c;

            
        }else
        {
            // ShowAsdead
            m_currentRespawnTimer -= Time.deltaTime;
            if(m_currentRespawnTimer < 0f)
            {
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        m_currentRespawnTimer = RespawnTimer;
        gameObject.transform.position = SpawnPoint.position;
        // Play deathSound?
    }

    internal void IncreaseOxygen()
    {
        MaxOxygen += MaxOxygenIncrement;
    }
}
