using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapBrowser : MonoBehaviour
{
    // Start is called before the first frame update
    public Image img;
    public GameObject imgIn;

    void Start()
    {
        
    }
    public void onclick()
    {
        if (img.enabled == true)
        {
            img.enabled = false;
            img.GetComponentInParent<BigScale>().enabled = false;
            imgIn.SetActive(false);
        }
        else
        {
            img.enabled = true;
            imgIn.SetActive(true);
            if (PlayerPrefs.GetInt("lang") == 1)
            {
                img.sprite = Resources.Load<Sprite>("MapP/" + PlayerPrefs.GetInt("map"));
            }else img.sprite = Resources.Load<Sprite>("Map/" + PlayerPrefs.GetInt("map"));
            img.GetComponentInParent<BigScale>().enabled = true;
        }
    }
    // Update is called once per frame
}
