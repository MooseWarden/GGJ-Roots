using System.Collections;
using UnityEngine;

/// <summary>
/// Allows user to place their towers.
/// </summary>
public class TowerPlacement : MonoBehaviour
{
    [HideInInspector] public GameObject towerToPlace;
    [HideInInspector] public bool placed = true;

    private Camera cam;
    private bool canDo = true;
    private bool tearDown = false;
    private GameObject toDemolish;

    //private float mouseX;
    //private float mouseY;

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
        //this clamp method is messed up when the screen dimensions change, would need to find a different way to clamp the marker to the field physically. for now the ontriggers provide a bandaid
        /*
        //clamp the position of the spawn marker to the dimensions of the field via the mouse coordinates.
        if (Input.mousePosition.x < 8f)
        {
            mouseX = 8f;
        }
        else if (Input.mousePosition.x > 947f)
        {
            mouseX = 947;
        }
        else
        {
            mouseX = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < 143f)
        {
            mouseY = 143f;
        }
        else if(Input.mousePosition.y > 528f)
        {
            mouseY = 528f;
        }
        else
        {
            mouseY = Input.mousePosition.y;
        }

        transform.position = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 29.9f));
        */

        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29.9f));
    }

    //ontrigger methods serve to show you where you cannot place something
    private void OnTriggerEnter(Collider other)
    {
        if (tearDown == false)
        {
            if (other.CompareTag("Path") || other.CompareTag("HQ"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 0f, 0.75f);
                canDo = false;
            }
        }
        else if (tearDown == true)
        {
            if (other.CompareTag("Tower"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f, 0.75f);
                toDemolish = other.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (tearDown == false)
        {
            if (other.CompareTag("Path") || other.CompareTag("HQ"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 0f, 0.75f);
                canDo = false;
            }
        }
        else if (tearDown == true)
        {
            if (other.CompareTag("Tower"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f, 0.75f);
                toDemolish = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tearDown == false)
        {
            if (other.CompareTag("Path") || other.CompareTag("HQ"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 1f, 0.75f);
                canDo = true;
            }
        }
        else if (tearDown == true)
        {
            if (other.CompareTag("Tower"))
            {
                GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 0.75f);
                toDemolish = null;
            }
        }
    }

    /// <summary>
    /// Activate placing mode.
    /// </summary>
    public IEnumerator Placing()
    {
        placed = false;
        GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 1f, 0.75f);
        GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);

        while (placed == false)
        {
            //LMB for placing a tower, RMB for canceling the action
            if (Input.GetMouseButtonDown(0) == true && canDo == true) //stretch goal - need a raycast to see if the mouse is over the field, from the spot?
            {
                GameObject instantTower = Instantiate(towerToPlace, transform.position, towerToPlace.transform.rotation); //stretch goal - use instant tower for spawn fx play
                yield return new WaitForSeconds(0.1f);
                GetComponent<MeshRenderer>().enabled = false;
                placed = true;
            }
            else if (Input.GetMouseButtonDown(1) == true && canDo == true)
            {
                yield return new WaitForSeconds(0.1f);
                GetComponent<MeshRenderer>().enabled = false;
                placed = true;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Activate demolish mode.
    /// </summary>
    public IEnumerator Demolish()
    {
        placed = false;
        tearDown = true;
        GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 0.75f);
        GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);

        while (placed == false)
        {
            //LMB for demolishing a tower, RMB for canceling the action
            if (Input.GetMouseButtonDown(0) == true && toDemolish != null)
            {
                Destroy(toDemolish);
                yield return new WaitForSeconds(0.1f);
                GetComponent<MeshRenderer>().enabled = false;
                placed = true;
                tearDown = false;
            }
            else if (Input.GetMouseButtonDown(1) == true)
            {
                yield return new WaitForSeconds(0.1f);
                GetComponent<MeshRenderer>().enabled = false;
                placed = true;
                tearDown = false;
            }
            yield return null;
        }
    }
}
