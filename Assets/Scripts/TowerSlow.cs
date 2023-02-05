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
}
