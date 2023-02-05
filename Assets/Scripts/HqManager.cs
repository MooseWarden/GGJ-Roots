using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private TextMeshProUGUI pauseButtonText;

    [HideInInspector] public int cash = 100; //starting cash.
    [HideInInspector] public bool paused;
    [HideInInspector] public TextMeshProUGUI healthTracker;
    [HideInInspector] public TextMeshProUGUI cashTracker;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseButtonText = GameObject.Find("PauseButton").GetComponentInChildren<TextMeshProUGUI>();

        GameObject instantMarker = Instantiate(spawnMarker, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29.9f)), spawnMarker.transform.rotation);
        startNode = GameObject.Find("Start Node").GetComponent<SpawnManager>();
        towerPlaceScript = instantMarker.GetComponent<TowerPlacement>();

        healthTracker = GameObject.Find("Health Tracker").GetComponent<TextMeshProUGUI>();
        healthTracker.text = "HQ HP: " + health;
        cashTracker = GameObject.Find("Cash Tracker").GetComponent<TextMeshProUGUI>();
        cashTracker.text = "Cash Money: $" + cash;
        StartCoroutine(Accrue());
    }

    // Update is called once per frame
    void Update()
    {
        //stretch goal - maybe have the marker show up red when try placing a tower with no money
        if (Input.GetKeyDown(KeyCode.A) == true && towerPlaceScript.placed == true && cash >= playerTowerAttack.GetComponent<TowerAttack>().cost && paused == false)
        {
            towerPlaceScript.towerToPlace = playerTowerAttack;
            towerPlaceScript.InitPlacing();
        }

        if (Input.GetKeyDown(KeyCode.S) == true && towerPlaceScript.placed == true && cash >= playerTowerSlow.GetComponent<TowerSlow>().cost && paused == false)
        {
            towerPlaceScript.towerToPlace = playerTowerSlow;
            towerPlaceScript.InitPlacing();
        }

        if (Input.GetKeyDown(KeyCode.D) == true && towerPlaceScript.placed == true && paused == false)
        {
            towerPlaceScript.InitDemolish();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //stretch goal - maybe play an explosion when enemy hits the hq
            Destroy(other.gameObject);
            startNode.enemyCount--;
            health--;
            healthTracker.text = "HQ HP: " + health;

            if (health <= 0)
            {
                GameOver();
            }
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossScript>().InitBossAttack();
        }
    }

    /// <summary>
    /// End the game and signal to stop all processes.
    /// </summary>
    public void GameOver()
    {
        healthTracker.text = "GAME OVER";
        healthTracker.color = new Color(1f, 0f, 0f, 1f);
        startNode.stopRunning = true;
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Increase cash at a steady value.
    /// </summary>
    IEnumerator Accrue()
    {
        yield return new WaitForSeconds(1f);
        while (startNode.stopRunning == false)
        {
            //hard cap on cash just in case
            if (cash < 900)
            {
                cash += 5;
            }

            cashTracker.text = "Cash Money: $" + cash; //reach goal - gotta update the cash text in the other scripts that alter it too
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Return to the title.
    /// </summary>
    public void BackToTitle()
    {
        //just in case the player goes back if they paused
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene("TitleScreen");
    }

    /// <summary>
    /// Toggle pause state of game.
    /// </summary>
    public void TogglePauseGame() //stretch goal - check to make sure all systems are truly paused, as of now they seem to be fine
    {
        paused = !paused;

        if (paused == true && startNode.stopRunning == false)
        {
            pauseButtonText.text = "Resume";
        }
        else if (paused == false && startNode.stopRunning == false)
        {
            pauseButtonText.text = "Pause";
        }

        Time.timeScale = (!paused).GetHashCode();
    }
}
