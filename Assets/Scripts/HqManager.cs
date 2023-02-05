using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the key systems of the game.
/// </summary>
public class HqManager : MonoBehaviour
{
    //populate in editor
    public GameObject spawnMarker;
    public GameObject playerTowerAttack;
    public GameObject playerTowerSlow;
    public int health;

    private SpawnManager startNode;
    private TowerPlacement towerPlaceScript;
    
    [HideInInspector] public int cash = 100; //starting cash

    // Start is called before the first frame update
    void Start()
    {
        GameObject instantMarker = Instantiate(spawnMarker, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29.9f)), spawnMarker.transform.rotation);
        startNode = GameObject.Find("Start Node").GetComponent<SpawnManager>();
        towerPlaceScript = instantMarker.GetComponent<TowerPlacement>();
        StartCoroutine(Accrue());
    }

    // Update is called once per frame
    void Update()
    {
        //stick game menu functionalities here? make the system menus their own script
        //when pause, how do i pause all the coroutines? worst case, bandaid could be that i set their incrementing values to 0 until unpause

        //stretch goal - maybe have the marker show up red when try placing a tower with no money
        if (Input.GetKeyDown(KeyCode.A) == true && towerPlaceScript.placed == true && cash >= playerTowerAttack.GetComponent<TowerAttack>().cost)
        {
            towerPlaceScript.towerToPlace = playerTowerAttack;
            StartCoroutine(towerPlaceScript.Placing());
        }

        if (Input.GetKeyDown(KeyCode.S) == true && towerPlaceScript.placed == true && cash >= playerTowerSlow.GetComponent<TowerSlow>().cost)
        {
            towerPlaceScript.towerToPlace = playerTowerSlow;
            StartCoroutine(towerPlaceScript.Placing());
        }

        if (Input.GetKeyDown(KeyCode.D) == true && towerPlaceScript.placed == true)
        {
            StartCoroutine(towerPlaceScript.Demolish());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //streth goal - maybe play an explosion when enemy hits the hq
            Destroy(other.gameObject);
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

    /// <summary>
    /// Increase cash at a steady value.
    /// </summary>
    IEnumerator Accrue()
    {
        while (startNode.stopRunning == false)
        {
            cash += 5;
            //Debug.Log("cash: " + cash); //log to ui
            yield return new WaitForSeconds(1f);
        }
    }
}
