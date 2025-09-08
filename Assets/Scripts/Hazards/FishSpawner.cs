using System;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public float Cooldown = 3f;
    public float m_currentCooldown;

    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentCooldown = Cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentCooldown -= Time.deltaTime;
        if(m_currentCooldown <= 0f)
        {
            SpawnFish();
            m_currentCooldown = Cooldown;
        }
    }

    private void SpawnFish()
    {
        GameObject newFish = Instantiate(fishPrefab, spawnPoint);
    }
}
