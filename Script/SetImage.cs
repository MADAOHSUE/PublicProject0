using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    public Text tst;
    // Start is called before the first frame update
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        Object[] imgList = Resources.LoadAll("Bg",typeof(Sprite));
        int k = Random.Range(0, imgList.Length);
        img.sprite = (Sprite)imgList[k] as Sprite;
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            tst.text = "摄制  " + imgList[k].name.Split('-')[1];
        }
        else
        {
            tst.text = "photo by " + imgList[k].name.Split('-')[1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
