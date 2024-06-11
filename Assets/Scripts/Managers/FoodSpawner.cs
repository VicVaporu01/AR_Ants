using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private FoodPool foodPool;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public GameObject activeFood;

    public void Start()
    {
        // activeFood = Instantiate(foodPrefab, transform);
        // activeFood.transform.localPosition = new Vector3(3, -0.5f, 1);
    }

    // public void SpawnFood()
    // {
    //     foodPool.DeactivateAllFoods();
    //     
    //     int randomFood = Random.Range(0, 3);
    //     int randomPosition = Random.Range(0, spawnPoints.Length);
    //
    //     activeFood = foodPool.RequestFood(randomFood);
    //     if (activeFood == null)
    //     {
    //         Debug.Log("Food pool devolvió null");
    //     }
    //     else
    //     {
    //         activeFood.transform.localPosition = spawnPoints[randomPosition].transform.localPosition;
    //     }
    // }

    public void SpawnFood()
    {
        int randomPosition = Random.Range(0, spawnPoints.Length);

        activeFood.transform.localPosition = spawnPoints[randomPosition].transform.localPosition;
    }

    public Vector3 RequestPosition()
    {
        int randomPosition = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomPosition].transform.localPosition;
    }
}