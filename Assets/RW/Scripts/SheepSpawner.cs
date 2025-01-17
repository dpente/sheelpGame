//Script that controls the 3 spawn locations, spawn interval.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    public float spawnSpeedUpInterval;
    public float spawnSpeedIncrease;

    private List<GameObject> sheepList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
        spawnSpeedUp();
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }
        sheepList.Clear();
    }

    private void spawnSpeedUp()
    {
        if(timeBetweenSpawns != spawnSpeedIncrease)
        {
            if (Mathf.RoundToInt(Time.timeSinceLevelLoad) % spawnSpeedUpInterval == 0)
            {
                timeBetweenSpawns = timeBetweenSpawns - spawnSpeedIncrease;
            }
        }
    }
}
