using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public LayerMask outlineMask;
    public LayerMask terrainMask;

    private bool psHitbox;
    private Transform highlight;
    private Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        psHitbox = false;
        Screen.SetResolution(320, 240, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(highlight != null)
        {
            //highlight.gameObject.GetComponent<PlantSpotScript>().HighlightOff();
            highlight = null;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, outlineMask))
        {
            if (psHitbox)
            {
                highlight = hit.transform;
                //highlight.gameObject.GetComponent<PlantSpotScript>().HighlightOn();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    highlight.gameObject.GetComponent<PlantSpotScript>().Grow();
                }
            }

            //Debug.Log("Raycast with " + hit.collider.name + " of mask " + hit.transform.gameObject.layer + " with normal " + hit.normal);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
        {
            terrain = hit.transform.gameObject.GetComponent<Terrain>();
            //Debug.Log("Raycast with " + hit.collider.name + " of mask " + hit.transform.gameObject.layer + " with normal " + hit.normal + " at point " + hit.point);
            if (hit.normal.Equals(Vector3.up))
            {
                //Debug.Log("Spot had good normal at terrain coordinate (" + WorldCoorToTerrCoor(hit.point).x + ", " + WorldCoorToTerrCoor(hit.point).z + ")");
            }
        }
    }

    private Vector3 WorldCoorToTerrCoor(Vector3 worldCoor)
    {
        Vector3 ret = new Vector3();
        Terrain ter = Terrain.activeTerrain;
        ret = worldCoor - ter.GetPosition();
        ret.x /= ter.terrainData.size.x;
        ret.y /= ter.terrainData.size.y;
        ret.z /= ter.terrainData.size.z;

        return ret;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered " + other.name + " of tag " + other.tag);
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
                //other.transform.parent.GetComponent<PlantSpotScript>().HighlightOn();
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
                //other.transform.parent.GetComponent<PlantSpotScript>().HighlightOff();
            }
        }
    }
}
