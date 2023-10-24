using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public LayerMask rayMask;

    private bool psHitbox;
    private Transform highlight;

    // Start is called before the first frame update
    void Start()
    {
        psHitbox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(highlight != null)
        {
            highlight.gameObject.GetComponent<PlantSpotScript>().HighlightOff();
            highlight = null;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask))
        {
            highlight = hit.transform;
            highlight.gameObject.GetComponent<PlantSpotScript>().HighlightOn();

            Debug.Log("Raycast with " + hit.collider.name + " of mask " + hit.transform.gameObject.layer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered " + other.name);
        if(other.tag == "PlantSpotTrigger")
        {
            psHitbox = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlantSpotTrigger")
        {
            Debug.Log("Inside plant trigger");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5, 8))
            {
                other.transform.parent.GetComponent<PlantSpotScript>().HighlightOn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlantSpotTrigger")
        {
            psHitbox = false;
            Debug.Log("Exit plant trigger");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5, 8))
            {
                other.transform.parent.GetComponent<PlantSpotScript>().HighlightOff();
            }
        }
    }
}
