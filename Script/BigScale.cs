using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigScale : MonoBehaviour
{
    private Vector2 firstTouch;
    private Vector2 lastTouch;
    float touchDistance;
    public RectTransform imageRect;
    float lastDistance;
    private Vector3 curImageScale;
    private bool isTwoTouch = false;
    private Vector2 srcLocalPosition;
    private Vector2 conLocalPosition;
    public Text console;
    [SerializeField, Header("UI Camera，需要将Canves Render Mode 设置为 ScreenSpace-Camera")]
    Camera pressEventCamera;
    private void Start()
    {
        srcLocalPosition = imageRect.localPosition; 
    }
    // Start is called before the first frame update
    private void LateUpdate()
    {
        TouchExtend();
    }

    void TouchExtend()
    {
        if (Input.touchCount > 1)//两指全按
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                firstTouch = Input.touches[0].position;
                lastTouch = Input.touches[1].position;
                touchDistance = Vector2.Distance(firstTouch, lastTouch);
                isTwoTouch = true;
                lastDistance = touchDistance;
            }
        
            if (isTwoTouch)
            {
                firstTouch = Input.touches[0].position;
                lastTouch = Input.touches[1].position;
                touchDistance = Vector2.Distance(firstTouch, lastTouch);
                curImageScale = new Vector3(imageRect.localScale.x, imageRect.localScale.y, 1);
                imageRect.localScale = new Vector3(Mathf.Clamp(touchDistance / lastDistance * curImageScale.x, 1, 2), Mathf.Clamp(touchDistance / lastDistance * curImageScale.y, 1, 2), 1);
                lastDistance = touchDistance;
            }
            if (Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                isTwoTouch = false;
                firstTouch = Vector3.zero;
                lastTouch = Vector3.zero;
            }
        }else if (Input.touchCount == 1)
        {

            Vector2 pos = Input.GetTouch(0).position;
            //Vector2 srcPos ;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                console.text += "  srcpos:" + srcLocalPosition;
                srcLocalPosition = Input.GetTouch(0).position;
                console.text += "  srcpos:" + srcLocalPosition;
                
                // RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRect, pos, pressEventCamera, out conLocalPosition);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 tarPos = Input.GetTouch(0).position;
                imageRect.localPosition = new Vector3(imageRect.localPosition.x + tarPos.x - srcLocalPosition.x,
                imageRect.localPosition.y + tarPos.y - srcLocalPosition.y, imageRect.localPosition.z);
                console.text += "  tarpos:" + tarPos;
                console.text += "  imGpos:" + imageRect.localPosition;
                srcLocalPosition = tarPos;

            }
        }
    }
    private void OnPointerDown(Vector2 position, Camera pressEventCamera)
    {

    }
    private void OnDrag(Vector2 position, Camera pressEventCamera)
    {
        
    }
}
