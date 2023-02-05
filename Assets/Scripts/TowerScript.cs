using UnityEngine;

/// <summary>
/// Base tower script.
/// </summary>
public class TowerScript : MonoBehaviour
{
    public int cost;

    private HqManager finance;

    //upon spawn, charge the hq for the cost of placement
    private void Start()
    {
        finance = GameObject.Find("End Node").GetComponent<HqManager>();
        Charge();
    }

    //when destroyed, refund the player
    private void OnDestroy()
    {
        Refund();
    }

    /// <summary>
    /// Subtract the cost of the tower from your cash pile.
    /// </summary>
    public void Charge()
    {
        finance.cash -= cost;
    }

    /// <summary>
    /// Add half the cost of the tower rounded up to your cash pile.
    /// </summary>
    public void Refund()
    {
        finance.cash += Mathf.RoundToInt(cost / 2);
    }
}
