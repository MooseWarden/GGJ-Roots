using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject[] powerupPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    [HideInInspector] public bool stopRunning = false;

    private readonly float spawnRange = 9f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0 && stopRunning == false) //need to pause this until all the boss enemies are dead, minis and boss, or at least within the spawn method pause the enemy wave method and continue the power up spawner
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (enemiesToSpawn % 5 == 0)
        {
            Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        }
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int randEnemyIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randEnemyIndex], GenerateSpawnPosition(), enemyPrefabs[randEnemyIndex].transform.rotation);
            }
        }
    }

    //made public to be accessed by enemyboss script
    public void SpawnPowerup()
    {
        int randPowerupIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randPowerupIndex], GenerateSpawnPosition(), powerupPrefabs[randPowerupIndex].transform.rotation);
    }

    //made public to be accessed by enemyboss script
    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new(spawnPosX, 0, spawnPosZ); //this is how you simplify a new vector3 object declaration

        return randomPos;
    }
}
