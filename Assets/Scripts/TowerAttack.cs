using UnityEngine;

/// <summary>
/// Damages enemy based on what category they are.
/// </summary>
public class TowerAttack : TowerScript
{
    //reach goal - might need to make an object to simulate the towers throwing shit at the enemy? or just some generic pulse when enemies are in range?

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
                    tempEnemyScript.InitGetHurt();
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
                    tempBossScript.InitGetHurt();
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

                //bandaid for the issue where occationally, the multiplier goes to the negatives (i think havent verified) and the enemy starts gaining health
                //usually happens when the enemy exits a tower cluster and shortly enters another one, weird combo of slow and attack towers maybe
                if (other.GetComponent<EnemyScript>().dmgMultiplier < 1)
                {
                    other.GetComponent<EnemyScript>().dmgMultiplier = 1;
                }
            }
            else if (other.CompareTag("Boss"))
            {
                other.GetComponent<BossScript>().takeDmg = false;
                other.GetComponent<BossScript>().dmgMultiplier--;

                //bandaid for the issue where occationally, the multiplier goes to the negatives (i think havent verified) and the enemy starts gaining health
                //usually happens when the enemy exits a tower cluster and shortly enters another one, weird combo of slow and attack towers maybe
                if (other.GetComponent<BossScript>().dmgMultiplier < 1)
                {
                    other.GetComponent<BossScript>().dmgMultiplier = 1;
                }
            }
        }
    }
}
