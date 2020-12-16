using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class photoManage : MonoBehaviour
{
    public Text Title;
    public Text Hunt;
    public Text Photo;
    public GameObject imgPrefab;
    public RectTransform gridHeight;
    public GridLayoutGroup grid;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            Title.text = "展览墙";
            Hunt.text = "战利品";
            Photo.text = "风景";
                ;
        }
        grid.cellSize = new Vector2(800, Screen.width *0.667f * 0.5f );
        LoadPhoto(1);
    }
    public void photoOnClick()
    {
        LoadPhoto(1);
    }
    public void huntOnClick()
    {
        
        LoadPhoto(0);
    }
    void LoadPhoto(int s)
    {

        Debug.Log(Screen.height + " "+ Screen.width);
        foreach (Transform item in GameObject.Find("Content").transform)
        {
            Destroy(item.gameObject);
        }
        Object[] imgList;
        if (s == 0) {
            imgList = Resources.LoadAll("HuntPhoto/", typeof(Sprite));// as Sprite[];
        }
        else
        {
            imgList = Resources.LoadAll("GoodPhoto/", typeof(Sprite));// as Sprite[];
        }
        gridHeight.sizeDelta = new Vector2(gridHeight.sizeDelta.x,(Screen.width * 0.45f +50)*imgList.Length);
        Debug.Log(imgList[0].name);
        
        foreach(var img in imgList)
        {
            GameObject photoInst = Instantiate(imgPrefab);
            photoInst.transform.SetParent(GameObject.Find("Content").transform);
            photoInst.GetComponent<Image>().sprite = img as Sprite;
            string[] str = img.name.Split('-');
            photoInst.transform.Find("Photoer").GetComponent<Text>().text = str[1];
            photoInst.transform.Find("PicName").GetComponent<Text>().text = str[0];
            photoInst.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void LoadPhoto()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
