using UnityEngine;

/// <summary>
/// Damages enemy based on what category they are.
/// </summary>
public class TowerAttack : TowerScript
{
    //might need to make an object to simulate the towers throwing shit at the enemy? or just some generic pulse when enemies are in range?

    //If the enemy isnt taking dmg, start the gethurt coroutine, otherwise add onto the multiplier.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyScript tempEnemyScript = other.GetComponent<EnemyScript>();
                if (tempEnemyScript.takeDmg == false)
                {
                    tempEnemyScript.takeDmg = true;
                    StartCoroutine(tempEnemyScript.GetHurt());
                }
                else if (tempEnemyScript.takeDmg == true)
                {
                    tempEnemyScript.dmgMultiplier++;
                }

            }
            else if (other.CompareTag("Boss"))
            {
                BossScript tempBossScript = other.GetComponent<BossScript>();
                if (tempBossScript.takeDmg == false)
                {
                    tempBossScript.takeDmg = true;
                    StartCoroutine(tempBossScript.GetHurt());
                }
                else if (tempBossScript.takeDmg == true)
                {
                    tempBossScript.dmgMultiplier++;
                }

            }
        }
    }

    //Keep the takedmg bool true while the enemy stays in the collider.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyScript>().takeDmg = true;
            }
            else if (other.CompareTag("Boss"))
            {
                other.GetComponent<BossScript>().takeDmg = true;
            }
        }
    }

    //When the enemy exist, set takedmg to false and decrement the dmgmultiplier.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyScript>().takeDmg = false;
                other.GetComponent<EnemyScript>().dmgMultiplier--;
            }
            else if (other.CompareTag("Boss"))
            {
                other.GetComponent<BossScript>().takeDmg = false;
                other.GetComponent<BossScript>().dmgMultiplier--;
            }
        }
    }
}
