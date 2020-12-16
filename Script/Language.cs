using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Language : MonoBehaviour
{
    public Text text;
    public Text notice;
    // Start is called before the first frame update
    void Awake()
    {
        string systemLanguage;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass localeClass = new AndroidJavaClass("java/util/Locale");
            AndroidJavaObject defaultLocale = localeClass.CallStatic<AndroidJavaObject>("getDefault");
            AndroidJavaObject usLocale = localeClass.GetStatic<AndroidJavaObject>("US");
            systemLanguage = defaultLocale.Call<string>("getDisplayLanguage", usLocale);
        }
        else
        {
            systemLanguage = Application.systemLanguage.ToString();
        }
        if (systemLanguage == "Chinese") {
            text.text = "猎人手札";
            notice.text = "水果兵台制作\n感谢贴吧和猎人大佬们支持\nQQ群号: 1061627372";
            PlayerPrefs.SetInt("lang", 1);
        }else
        {
            text.text = "The Hunter's Letter";
            PlayerPrefs.SetInt("lang", 0);
        }
    }
}
