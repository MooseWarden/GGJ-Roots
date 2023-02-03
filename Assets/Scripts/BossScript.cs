using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{
    private HqManager endNode;

    // Start is called before the first frame update
    void Start()
    {
        startNodeNodeScript = GameObject.Find("Start Node").GetComponent<NodePointers>();
        targetNode = startNodeNodeScript.nextNodes[Random.Range(0, startNodeNodeScript.nextNodes.Length)];
        startNodeSpawnScript = GameObject.Find("Start Node").GetComponent<SpawnManager>();

        endNode = GameObject.Find("End Node").GetComponent<HqManager>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeWalk();
    }

    /// <summary>
    /// Initiate boss's final endless attack against the base. Continues until boss is destroyed.
    /// </summary>
    /// <returns></returns>
    public IEnumerator BossAttack()
    {
        while (startNodeSpawnScript.stopRunning == false)
        {
            endNode.health--; //do an explosion anim too each time

            if (endNode.health <= 0)
            {
                endNode.GameOver();
            }

            yield return new WaitForSeconds(1.25f);
        }
    }
}
