using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSuit : MonoBehaviour
{
    // Start is called before the first frame update
    public Text temp;
    RectTransform RT;
    void Start()
    {
        RT = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float width = temp.preferredWidth;
        //width - RT.sizeDelta.x 
        RT.sizeDelta = new Vector2(width,RT.sizeDelta.y);
        RT.transform.position = new Vector3(RT.transform.position.x - (width - RT.sizeDelta.x) , RT.transform.position.y, RT.transform.position.z);
    }
}
