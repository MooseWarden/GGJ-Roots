using System.Collections;
using UnityEngine;

/// <summary>
/// Base boss behaviors.
/// </summary>
public class BossScript : EnemyScript
{
    private HqManager endNode;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        endNode = GameObject.Find("End Node").GetComponent<HqManager>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Initiate boss's final endless attack against the base. Continues until boss is destroyed.
    /// </summary>
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
