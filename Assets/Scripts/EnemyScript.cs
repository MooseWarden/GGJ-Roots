using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //set in inspector
    public float speed;
    public int health; //towers subtract 1 from here each hit

    [HideInInspector] public NodePointers startNodeNodeScript;
    [HideInInspector] public GameObject targetNode;
    [HideInInspector] public SpawnManager startNodeSpawnScript;

    // Start is called before the first frame update
    void Start()
    {
        startNodeNodeScript = GameObject.Find("Start Node").GetComponent<NodePointers>();
        targetNode = startNodeNodeScript.nextNodes[Random.Range(0, startNodeNodeScript.nextNodes.Length)];
        startNodeSpawnScript = GameObject.Find("Start Node").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeWalk();
    }

    /// <summary>
    /// Direct the enemy to the target node position.
    /// </summary>
    public void NodeWalk()
    {
        if(startNodeSpawnScript.stopRunning == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * speed);
        }
        else
        {
            //play the enemy win anim if we have time to implement
        }
    }
}
