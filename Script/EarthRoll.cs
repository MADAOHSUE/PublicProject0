using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthRoll : MonoBehaviour
{
    public GameObject Sphera;
    public GameObject BgImg;
    // Start is called before the first frame update
    private RectTransform bgRT;
    private float minX,minY,maxX,maxY;
    private float varX,varY,curX,curY;
    private float perTime;
    bool transPre;
    private float tarX;
    private float tarY;
    private Vector2 tarPos;
    private Vector2 srcPos;
    private bool isPre;
    private bool isspeed;
    private float srcLong;
    // Update is called once per frame
    private void Start()
    {
        bgRT = BgImg.GetComponent<RectTransform>();
        maxX = bgRT.transform.position.x;
        minX = maxX - 3500;
        minY = bgRT.transform.position.y;
        maxY = minY + 1000;
        curX = maxX;
        curY = minY;
        transPre = true;
        isPre = true;
        isspeed = true;
        srcLong = 1f;

    }
    private void checkPre()
    {
        isPre = true;
    }
    void Update()
    {
        //camRT = spheraCamera.GetComponent<RectTransform>();
        if (isPre) {
            if ((bgRT.transform.position.x > maxX - 30f)||(bgRT.transform.position.x > minX + 30f) || (bgRT.transform.position.y > maxY - 30f) || (bgRT.transform.position.y < minY +30f))
            {
                transPre = true;
            }
            if (transPre)
            {
                curX = bgRT.transform.position.x;
                curY = bgRT.transform.position.y;

                isPre = false;
                Invoke("checkPre",10f);
                Debug.Log("PP");
                    if (bgRT.transform.position.x > maxX - 30f)
                    {
                        tarX = Random.Range(minX, maxX - 1000f);
                        tarY = Random.Range(0f, 1f) > 0.5f ? minY : maxY;
                    }else
                    if (bgRT.transform.position.x > minX + 30f)
                    {
                        tarX = Random.Range(minX+1000f, maxX);
                        tarY = Random.Range(0f, 1f) > 0.5f ? minY : maxY;
                    }else
                    if (bgRT.transform.position.y > maxY - 30f)
                    {
                        tarY = Random.Range(minY, maxY - 300f);
                        tarX = Random.Range(0f, 1f) > 0.5f ? minX : maxX;
                    }else
                    if (bgRT.transform.position.y < minY + 30f)
                    {
                        tarY = Random.Range(minY+300f, maxY);
                        tarX = Random.Range(0f, 1f) > 0.5f ? minX : maxX;
                    }
                transPre = false;
                srcPos = new Vector2(curX, curY);
                tarPos = new Vector2(tarX, tarY);
                if (isspeed)
                {
                    srcLong = Mathf.Sqrt((tarPos.x - srcPos.x) * (tarPos.x - srcPos.x) + (tarPos.y - srcPos.y) * (tarPos.y - srcPos.y));
                    isspeed = false;
                }
                Debug.Log(tarPos.x+"  "+ tarPos.y);
            }
        }
        float lerp = Mathf.Sqrt((tarPos.x - srcPos.x) * (tarPos.x - srcPos.x) + (tarPos.y - srcPos.y) * (tarPos.y - srcPos.y));
        bgRT.transform.position = Vector2.Lerp(bgRT.transform.position, tarPos, 0.004f * srcLong/ lerp);

    }

    private void FixedUpdate()
    {
        //camRT.transform.Rotate();
    }
}
