using UnityEngine;

/// <summary>
/// Slows down enemies based on what category they are.
/// </summary>
public class TowerSlow : TowerScript
{
    //these stack, no need for a multiplier value
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().speed /= 2;
        }
        else if (other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossScript>().speed /= 1.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().speed *= 2;
        }
        else if (other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossScript>().speed *= 1.5f;
        }
    }

    /*
    private void OnDestroy()
    {
        //reach goal - make sure to reset the enemy speed if destroy a tower while enemy is in range, if destoryed the speed stays altered
        //see if the attack tower needs an ondestroy method too
        //make sure to override this, make the towerscript base ondestroy overridable, and call the base in here as well
    }
    */
}
