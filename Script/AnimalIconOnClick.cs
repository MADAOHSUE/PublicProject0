using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalIconOnClick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AnimalIconOnMouseClick()
    {
        string str = gameObject.GetComponent<Image>().sprite.name.Substring(0, 1);
        GameObject.Find("SetData").SendMessage("RevOnClick",str);
        Invoke("ChangeColor", 0.001f);
        
    }
    private void ChangeColor()
    {
        this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
