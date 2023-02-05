using System.Collections;
using UnityEngine;

/// <summary>
/// Base enemy behaviors.
/// </summary>
public class EnemyScript : MonoBehaviour
{
    //set in inspector
    public float speed;
    public float dmgRate;
    public int health;
    public int payout;

    [HideInInspector] public NodePointers startNodeNodeScript;
    [HideInInspector] public GameObject targetNode;
    [HideInInspector] public SpawnManager startNodeSpawnScript;
    [HideInInspector] public bool takeDmg = false;
    [HideInInspector] public int dmgMultiplier = 1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        startNodeNodeScript = GameObject.Find("Start Node").GetComponent<NodePointers>();
        targetNode = startNodeNodeScript.nextNodes[Random.Range(0, startNodeNodeScript.nextNodes.Length)];
        startNodeSpawnScript = GameObject.Find("Start Node").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (health > 0)
        {
            NodeWalk();
        }
    }

    /// <summary>
    /// Direct the enemy to the target node position.
    /// </summary>
    public void NodeWalk()
    {
        if (startNodeSpawnScript.stopRunning == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * speed);
        }
        else
        {
            //reach goal - play the enemy win anim if we have time to implement, they just jump up and down wherever they are on the field, or just confetti shoots out from them
        }
    }

    /// <summary>
    /// Damage the enemy.
    /// </summary>
    public IEnumerator GetHurt()
    {
        while (takeDmg)
        {
            health -= dmgMultiplier;

            if (health <= 0)
            {
                if (gameObject != null) //The object of type 'EnemyScript' has been destroyed but you are still trying to access it. - this sometimes still happens, dunno why
                {
                    StopAllCoroutines();
                    StartCoroutine(Die());
                }
                break;
            }

            yield return new WaitForSeconds(dmgRate);

            if (!takeDmg)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Destroy the enemy and decrement number of enemies.
    /// </summary>
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0f);
        startNodeSpawnScript.enemyCount--;
        Destroy(gameObject);
    }
}
