using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMoblie : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform rt;
    void Start()
    {
        float weight = 1980;
        float height = 1080;
        float rateW = (float)Screen.width / weight; //0.32 1.1
        float rateH = (float)Screen.height / height;/// 30.44 0.9
        Debug.Log("rateW :" + rateW + "rateH :" + rateH);
        rt = gameObject.GetComponent<RectTransform>();
        if (rateW > rateH)
        {
            rt.sizeDelta = new Vector2(weight * rateW, height * rateW);

        }
        else
        {
            rt.sizeDelta = new Vector2(weight * rateH, height * rateH);
        }
    }
}
