using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //populate these in the editor
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    [HideInInspector] public int enemyCount = 0; //keep track of how many enemies, not bosses, spawn in, and decrease this value by 1 when an enemy is killed from an outside script
    [HideInInspector] public int waveNumber = 1;
    [HideInInspector] public bool stopRunning = false; //if need to pause too? pause can lift from rwi

    private bool startAgain = false;

    // Start is called before the first frame update
    void Start()
    {
        //start the initial wave
        StartCoroutine(WaveIntroAnim());
        if (enemyPrefabs != null && bossPrefab != null)
        {
            StartCoroutine(Spawner(waveNumber));
        }
        else
        {
            Debug.Log("win!");
            //set an auto win condition here if the enemy and boss prefabs arent filled
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check each frame if can spawn again
        Check();
    }

    /// <summary>
    /// Keep track of when to spawn another wave.
    /// Paused until the spawner is done, all the boss enemies are dead, and the game isnt paused.
    /// </summary>
    void Check()
    {
        if (startAgain == true && enemyCount == 0 && stopRunning == false)
        {
            Debug.Log("checked and succeeded");
            StartCoroutine(WaveClearAnim()); //plays first cuz the first time this will be called is after the first wave is cleared
            waveNumber++;
            StartCoroutine(WaveIntroAnim());
            StartCoroutine(Spawner(waveNumber));
            startAgain = false;
        }
    }

    /// <summary>
    /// Set the spawn position of the enemy.
    /// </summary>
    /// <returns></returns>
    public Vector3 SpawnPosition()
    {
        Vector3 spawnPos = new(gameObject.transform.position.x, 0.6f, gameObject.transform.position.z);
        return spawnPos;
    }

    /// <summary>
    /// Play the intro animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaveIntroAnim()
    {
        //play the anim here of what wave number this is
        Debug.Log("intro anim");
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// Infinite enemy spawn routine.
    /// </summary>
    /// <param name="numToSpawn"></param>
    /// <returns></returns>
    IEnumerator Spawner(int numToSpawn)
    {
        //replace this with the intro anim, whose number changes based on the wave. maybe have that coroutine call this one. or have the intro anim here. or first call the intro anim, then the spawner
        yield return new WaitForSeconds(2);

        //make this into a specific number later, maybe some value specific to a sport. when beat boss you win
        if (numToSpawn % 5 == 0)
        {
            GameObject instantEnemy = Instantiate(bossPrefab, SpawnPosition(), bossPrefab.transform.rotation);
            instantEnemy.SetActive(true);
            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < numToSpawn; i++)
        {
            int randEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject instantEnemy = Instantiate(enemyPrefabs[randEnemyIndex], SpawnPosition(), enemyPrefabs[randEnemyIndex].transform.rotation); //maybe play spawn anim like the ball from rwi
            //use instantEnemy to activate the spawn anim
            enemyCount++; 
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        yield return new WaitForSeconds(2);

        startAgain = true;
    }

    /// <summary>
    /// Play the wave cleared animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaveClearAnim()
    {
        //play the anim here
        Debug.Log("clear anim");
        yield return new WaitForSeconds(1);
    }
}
