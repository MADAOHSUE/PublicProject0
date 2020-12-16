using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetMapName : MonoBehaviour
{
    public Text score;
    public Text level;
    public Text maxD;
    public Text maxW;
    public Text active;
    public Text habit;
    private Text theText;
    public Image img;
    public Text map;
    public Text rareColor;
    public Text author;
    private string[] str = { "Hirschfelden", "Layton Lakes", "Medved Taiga",
        "Fernando", "Vurhonga Savanna", "Cuatro Colinas" , "Yukon","Silver Ridge","蒂阿拉罗瓦国家公园"};
    private string[] strCHN = { "赫希费尔登狩猎保护区", "莱顿湖区", "梅德韦泰嘉国家公园",
        "费南多自然公园", "乌尔霍加热带稀树草原", "夸特罗科利纳斯野生动物保护区" , "育空河谷自然保护区","银岭峰","蒂阿拉罗瓦国家公园"};
    // Start is called before the first frame update
    void Start()
    {
        Object[] imgList = Resources.LoadAll("Bg", typeof(Sprite));
        int k = Random.Range(0, imgList.Length);
        img.sprite = (Sprite)imgList[k] as Sprite; ;
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            author.text = imgList[k].name.Split('-')[1]+ "  摄制";
        }else
        {
            author.text = "photo by " + imgList[k].name.Split('-')[1];
        }
            

        theText = gameObject.GetComponent<Text>();
        if (PlayerPrefs.GetInt("lang" )== 1){
            theText.text = strCHN[PlayerPrefs.GetInt("map")];
        }else theText.text = str[PlayerPrefs.GetInt("map")];
        if (PlayerPrefs.HasKey("lang"))
        {
            if (PlayerPrefs.GetInt("lang") == 1)
            {
                score.text = "分数";
                level.text = "等级";
                level.fontStyle = FontStyle.Bold;
                maxD.text = "最高狩猎难度";
                maxW.text = "最大重量区间";
                active.text = "活动时间";
                habit.text = "栖息地偏好";
                map.text = "地图";
                rareColor.text = "稀有色:";

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
