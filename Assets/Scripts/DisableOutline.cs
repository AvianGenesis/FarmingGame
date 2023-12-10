using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOutline : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(WaitHere());
    }

    IEnumerator WaitHere()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Outline>().enabled = false;
        Destroy(this);
    }
}
