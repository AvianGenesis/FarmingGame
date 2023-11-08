using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpotScript : MonoBehaviour
{
    public Sprite[] stages;
    public SpriteRenderer plantRenderer;
    public GameObject menu;

    private Outline olScript;
    private int stage;

    // Start is called before the first frame update
    void Start()
    {
        olScript = gameObject.GetComponent<Outline>();
        //olScript.enabled = true;
        //olScript.enabled = false;

        stage = 0;
    }

    public void Grow()
    {
        stage++;
        stage = stage % (stages.Length + 1);
        if(stage == 0)
        {
            plantRenderer.sprite = null;
        }
        else
        {
            plantRenderer.sprite = stages[stage - 1];
        }
        Debug.Log("Stage " + stage);
    }

    public void HighlightOn()
    {
        Debug.Log("HighlightOn()");
        if (!olScript.enabled)
        {
            olScript.enabled = true;
            menu.SetActive(true);
        }
    }

    public void HighlightOff()
    {
        Debug.Log("HighlightOff()");
        olScript.enabled = false;
        menu.SetActive(false);
    }
}
