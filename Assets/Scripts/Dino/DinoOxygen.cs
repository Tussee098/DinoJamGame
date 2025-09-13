using System;
using UnityEngine;

public class DinoOxygen : MonoBehaviour
{
    public float MaxOxygen;
    private float m_CurrentDinoOxygen;
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
        if (m_CurrentDinoOxygen <= 0f)
        {
            Debug.Log("No Oxygen");
        }
    }
}
