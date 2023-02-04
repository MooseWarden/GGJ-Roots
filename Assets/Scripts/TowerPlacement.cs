using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    //[HideInInspector]
    public GameObject towerToPlace;
    [HideInInspector] public bool placed = true;

    private Camera cam;
    private bool canPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 1f, 0.75f);
        GetComponent<MeshRenderer>().enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29.9f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Field"))
        {
            GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 0f, 0.75f);
            canPlace = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Field"))
        {
            GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 0f, 0.75f);
            canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Field"))
        {
            GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 1f, 0.75f);
            canPlace = true;
        }
    }

    /// <summary>
    /// Activate placing mode.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Placing()
    {
        placed = false;
        GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);

        while (placed == false)
        {
            if (Input.GetMouseButtonDown(0) == true && canPlace == true)// need a raycast to see if the mouse is over the field, from the spot?
            {
                GameObject instantTower = Instantiate(towerToPlace, transform.position, towerToPlace.transform.rotation);
                yield return new WaitForSeconds(0.1f);
                GetComponent<MeshRenderer>().enabled = false;
                placed = true;
            }
            yield return null;
        }
    }
}
