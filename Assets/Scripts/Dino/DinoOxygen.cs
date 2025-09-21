using System;
using UnityEngine;
using UnityEngine.UI;

public class DinoOxygen : MonoBehaviour
{
    public float MaxOxygen;
    private float m_CurrentDinoOxygen;

    [Range(0f, 1f)] public float value = 0f; // 0 = transparent, 1 = fully blue
    public Image blueOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        Color c = blueOverlay.color;
        c.a = Mathf.Clamp(0.8f - (m_CurrentDinoOxygen / MaxOxygen),0f,0.8f);
        blueOverlay.color = c;

        if (m_CurrentDinoOxygen <= 0f)
        {
            // TODO - Respawn.
            Debug.Log("No Oxygen");
        }
    }
}
