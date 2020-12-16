using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class time : MonoBehaviour
{
    Text curTime;
    int minute;
    int hour;
    // Start is called before the first frame update
    void Start()
    {
        curTime = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //hour = DateTime.Now.Hour;
        //minute = DateTime.Now.Minute;
        //curTime.text = hour+":"+minute;
        curTime.text = DateTime.Now.ToString("t");
    }
}
