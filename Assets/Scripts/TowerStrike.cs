using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStrike : TowerScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TowerFunction()
    {
        yield return new WaitForSeconds(1);
    }
}
