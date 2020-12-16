using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;
public class AnimalLoad:MonoBehaviour
{
    public Image HuntImage;
    // Start is called before the first frame update
    public GameObject AnimalIcon;
    private string iconPath;
    private string huntImagePath;
    private object[] imgList;
    private Sprite huntSprite;
    public int mapID;
    private int animalID;
    public Text console;
    public Text AnimalName;
    public Text DiamandScore;
    public Text GoldScore;
    public Text SliverScore;
    public Text MaxDifficulty;
    public Text Level;
    public Text MaxWeight;
    public Text DrinkTime;
    public Text EatTime;
    public Text SleepTime;
    public Text Active;
    public Text Habaitat;
    private String AndroidPath;
    public Text Console;
    public Stream xml;
    public Text NormalColor;
    public GameObject RareColorObj;
    public Color srcColor;
    public Color tarColor;
    private string minDiamandLevel;
    public AudioSource AS;
    public BigScale bs;
    void Start()
    {
        mapID = 0;
        animalID = 0;
        if (PlayerPrefs.GetInt("map")>=0)
        {
            mapID = PlayerPrefs.GetInt("map");
        }
        
        huntImagePath = "HuntImage/map_" + mapID +"/";
        AudioClip AC;
        if (mapID != 8) AC= Resources.Load<AudioClip>("Audio/"+ mapID);
        else AC = Resources.Load<AudioClip>("Audio/main");
        AS.clip = AC;//+mapID);
        AS.Play();
        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            AS.enabled = false;
        }
        else
        {
            AS.enabled = true;
        }


        XLSX(); //init
        SetIcon();
    }

    void SetBgAndHuntImage(Sprite b)
    {
        HuntImage.sprite = b;
    }

    private void SetIcon()
    {

        iconPath = "AnimalIcon/animal_" + mapID.ToString();
        imgList = Resources.LoadAll(iconPath, typeof(Sprite));

       // Application.persistentDataPath


        foreach (Transform item in GameObject.Find("animalImage").transform)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < imgList.Length; i++)
        {
            GameObject animalIconInst = Instantiate(AnimalIcon);
            animalIconInst.transform.SetParent(GameObject.Find("animalImage").transform);
            animalIconInst.GetComponent<Image>().sprite = imgList[i] as Sprite;
            animalIconInst.transform.localScale = new Vector3(1,1,1);
            if (i != 0)
            {
                animalIconInst.GetComponent<Image>().color = new Color(1, 1, 1, 80f / 255f);
            }
        }
    }
    // Update is called once per frame
    public void RevOnClick(string str)
    {
        animalID = int.Parse(str);
        XLSX();
        foreach (Transform child in GameObject.Find("animalImage").transform)
        {
            child.GetComponent<Image>().color = new Color(1, 1, 1, 80f / 255f);
        }

    }

    void XLSX()
    {
        huntSprite = Resources.Load<Sprite>(huntImagePath + "hunt_"+ animalID.ToString());
        SetBgAndHuntImage(huntSprite);

        XmlDocument xml = new XmlDocument();
        TextAsset xmlText;
        if (PlayerPrefs.HasKey("lang"))
        {

            if (PlayerPrefs.GetInt("lang") == 1) xmlText = (TextAsset)Resources.Load("Xlsx/AnimalCHN");
            else xmlText = (TextAsset)Resources.Load("Xlsx/AnimalEng");
        }else xmlText = (TextAsset)Resources.Load("Xlsx/AnimalEng");


        if (xmlText)
        {
            xml.LoadXml(xmlText.text);
        }else
        {
            
        }
        foreach(XmlNode temp in xml.SelectSingleNode("animal_data").ChildNodes)
        {
            XmlElement xe = (XmlElement)temp;
            if(xe.GetAttribute("mapid").ToString() == mapID.ToString())
            {
                for (int i =0; i < temp.ChildNodes.Count; i++)
                {
                    if (i == animalID)
                    {
                        Debug.Log("i=" + i);
                        AnimalName.text = temp.ChildNodes[animalID].SelectSingleNode("name").InnerText;
                        DiamandScore.text = temp.ChildNodes[animalID].SelectSingleNode("diamond").InnerText;
                        GoldScore.text = temp.ChildNodes[animalID].SelectSingleNode("gold").InnerText;
                        SliverScore.text = temp.ChildNodes[animalID].SelectSingleNode("sliver").InnerText;
                        MaxDifficulty.text = temp.ChildNodes[animalID].SelectSingleNode("maxdifficulty").InnerText;
                        Level.text = temp.ChildNodes[animalID].SelectSingleNode("level").InnerText;
                        MaxWeight.text = temp.ChildNodes[animalID].SelectSingleNode("min_weight").InnerText;
                        EatTime.text = temp.ChildNodes[animalID].SelectSingleNode("drink").InnerText;
                        DrinkTime.text = temp.ChildNodes[animalID].SelectSingleNode("eat").InnerText;
                        SleepTime.text = temp.ChildNodes[animalID].SelectSingleNode("sleep").InnerText;
                        Active.text = temp.ChildNodes[animalID].SelectSingleNode("active").InnerText;
                        Habaitat.text = temp.ChildNodes[animalID].SelectSingleNode("habitat").InnerText;
                        minDiamandLevel = temp.ChildNodes[animalID].SelectSingleNode("minLevel").InnerText;
                        AnimalName.text += "#" + (animalID + 1);
                string colorT = temp.ChildNodes[animalID].SelectSingleNode("color").InnerText;
                        Debug.Log(colorT);

                        if (PlayerPrefs.GetInt("lang") == 1)
                        {
                            NormalColor.text = "普通颜色:\n";
                        }
                        else
                        {
                            NormalColor.text = "Normal Color:\n";
                        }
                        foreach (Transform item in GameObject.Find("RareColor").transform)
                        {
                            Destroy(item.gameObject);
                            
                        }
                        string[]str =  colorT.Split('	');
                        for(int j = 0; j < str.Length; j+=2)
                        {
                            if (str[j + 1] == "1")
                            {
                                NormalColor.text += str[j]+" ";
                            }else
                            {
                                GameObject rareColorInst = Instantiate(RareColorObj);
                                rareColorInst.transform.SetParent(GameObject.Find("RareColor").transform);
                                rareColorInst.GetComponent<Text>().text = str[j];
                                rareColorInst.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite >("Icon/"+str[j+1]);
                                float colorlerp = float.Parse(str[j + 1]) / 10f;
                                rareColorInst.transform.localScale = new Vector3(1, 1, 1);
                                rareColorInst.GetComponentInChildren<Image>().color = Color.Lerp(srcColor, tarColor, colorlerp);
                            }
                        }
                        break;
                    }
                }
            }
        }



        if (Level.text == "3" || Level.text == "2" || Level.text == "1") Level.color = Color.green;
        else if (Level.text == "4" || Level.text == "6" || Level.text == "5") Level.color = Color.yellow;
        else Level.color = Color.red;


        string[] difficulty;
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            difficulty = new string[] { " - 非常简单", " - 中等", " - 传奇" };
        }else difficulty = new string[] { " - Very Easy", " - Medium", " - Legendary" };


        MaxWeight.text += " kg";

        if (MaxDifficulty.text == "3") MaxDifficulty.text += difficulty[0];
        else if (MaxDifficulty.text == "5") MaxDifficulty.text += difficulty[1];
        else MaxDifficulty.text += difficulty[2];
        MaxDifficulty.text += "    ♤ " + minDiamandLevel;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

            SceneManager.LoadScene("SampleScene");
        }
    }
    public void clearConsole()
    {
        console.text = "console:";
    }
}

