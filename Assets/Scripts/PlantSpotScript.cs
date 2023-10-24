using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpotScript : MonoBehaviour
{
    private Outline olScript;

    // Start is called before the first frame update
    void Start()
    {
        olScript = gameObject.GetComponent<Outline>();
        //olScript.enabled = true;
        //olScript.enabled = false;
    }

    public void HighlightOn()
    {
        Debug.Log("HighlightOn()");
        if (!olScript.enabled)
        {
            olScript.enabled = true;
        }
    }

    public void HighlightOff()
    {
        Debug.Log("HighlightOff()");
        olScript.enabled = false;
    }
}
