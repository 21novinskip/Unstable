using System;
using System.Security.Cryptography;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float spawnTime;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    private Transform spawnPoint;
    public GameObject carPrefab;

    private float spawnTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        spawnTimer += 0.1f;
        if (spawnTimer > spawnTime)
        {
            SpawnCar();
            spawnTimer = UnityEngine.Random.Range(0.0f, 2.5f);
        }
    }

    void SpawnCar()
    {
        int SPnum = UnityEngine.Random.Range(1, 5);
  
        if (SPnum == 1) { spawnPoint = spawnPoint1; }
        else if (SPnum == 2) { spawnPoint = spawnPoint2; }
        else if (SPnum == 3) { spawnPoint = spawnPoint3; }
        else if (SPnum == 4) { spawnPoint = spawnPoint4; }
        Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
    }
}
