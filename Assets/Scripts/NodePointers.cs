using UnityEngine;

public class NodePointers : MonoBehaviour
{
    //store the potential next nodes here in editor
    public GameObject[] nextNodes;

    private void OnTriggerEnter(Collider other)
    {
        //set the next target node for the enemy/boss
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            other.GetComponent<EnemyScript>().targetNode = nextNodes[Random.Range(0, nextNodes.Length)];
        }
    }
}
