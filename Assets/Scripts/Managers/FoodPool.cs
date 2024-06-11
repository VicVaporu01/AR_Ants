using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;
    private List<GameObject> foodPool;

    private int poolSize = 3;

    private void Start()
    {
        foodPool = new List<GameObject>();
        AddFoodsToPool();
    }

    private void AddFoodsToPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject food = Instantiate(foodPrefabs[i], transform);
            food.SetActive(false);

            foodPool.Add(food);
        }
    }

    public GameObject RequestFood(int foodToGive)
    {
        if (!foodPool[foodToGive].activeSelf)
        {
            foodPool[foodToGive].SetActive(true);
            return foodPool[foodToGive];
        }

        return null;
    }

    public void DeactivateAllFoods()
    {
        foreach (GameObject food in foodPool)
        {
            food.SetActive(false);
        }
    }
}