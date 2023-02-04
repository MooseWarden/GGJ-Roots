using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HqManager : MonoBehaviour
{
    //populate in editor
    public GameObject playerTower1;
    public GameObject playerTower2;
    public int health = 100;

    private SpawnManager startNode;
    private TowerPlacement towerPlaceScript;

    // Start is called before the first frame update
    void Start()
    {
        //summon the place marker here?
        startNode = GameObject.Find("Start Node").GetComponent<SpawnManager>();
        towerPlaceScript = GameObject.Find("Placement Spot").GetComponent<TowerPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        //stick game menu functionalities here? make the system menus their own script

        //add in the monetary value check later
        if (Input.GetKeyDown(KeyCode.A) == true && towerPlaceScript.placed == true)
        {
            towerPlaceScript.towerToPlace = playerTower1;
            StartCoroutine(towerPlaceScript.Placing());
        }

        if (Input.GetKeyDown(KeyCode.S) == true && towerPlaceScript.placed == true)
        {
            towerPlaceScript.towerToPlace = playerTower2;
            StartCoroutine(towerPlaceScript.Placing());
        }
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
