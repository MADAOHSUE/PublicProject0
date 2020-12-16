using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource AS;
    public GameObject duihao;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Audio"))
        {
            OpenAudio();
        }
        else
        {
            if (PlayerPrefs.GetInt("Audio") == 0)
            {
                CloseAudio();

            }
            else
            {
                OpenAudio();
            }
        }
            
        
    }
    public void OnClick()
    {
            Debug.Log("has");
            if (PlayerPrefs.GetInt("Audio") == 0)
            {
            OpenAudio();
            }
            else
            {
            CloseAudio();
            }
        

    }
    void OpenAudio()
    {
        AS.enabled = true;
        PlayerPrefs.SetInt("Audio", 1);
        duihao.SetActive(false);
    }
    void CloseAudio()
    {
        AS.enabled = false;
        PlayerPrefs.SetInt("Audio", 0);
        duihao.SetActive(true);
    }
}
