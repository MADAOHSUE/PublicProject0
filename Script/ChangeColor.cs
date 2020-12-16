using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    private Image img;
    private Color srcColor;
    private Color tarColor;
    void Start()
    {
        img = GetComponent<Image>();
        Change();
    }
    private void Change()
    {
        if(img.color == Color.white)
        {
            srcColor = Color.white;
            tarColor = Color.black;
        }
        else
        {
            tarColor = Color.white;
            srcColor = Color.black;
        }
    }
    // Update is called once per frame
    void Update()
    {
        img.color = Color.Lerp(srcColor, tarColor, 0.1f);
    }
}
