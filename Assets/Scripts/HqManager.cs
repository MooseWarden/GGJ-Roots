using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HqManager : MonoBehaviour
{
    public int health = 30; //hide later

    private SpawnManager startNode;

    // Start is called before the first frame update
    void Start()
    {
        startNode = GameObject.Find("Start Node").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //should i stick the tower setting functionalities and game menu functionalities here? make the system menus their own script
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);//maybe play an explosion when enemy hits the hq
            startNode.enemyCount--;
            health--;

            if (health <= 0)
            {
                GameOver();
            }
        }
        else if (other.CompareTag("Boss"))
        {
            StartCoroutine(other.GetComponent<BossScript>().BossAttack());
        }
    }

    /// <summary>
    /// End the game and signal to stop all processes.
    /// </summary>
    public void GameOver()
    {
        startNode.stopRunning = true;
    }
}
