using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PictureZoom : MonoBehaviour
{
    #region Zoom

    /// <summary>
    /// 上一帧两指间距离
    /// </summary>
    private float lastDistance = 0;

    /// <summary>
    /// 当前两个手指之间的距离
    /// </summary>
    private float twoTouchDistance = 0;

    /// <summary>
    /// 得到图片
    /// </summary>
    public RectTransform image;

    /// <summary>
    /// 第一根手指按下的坐标
    /// </summary>
    Vector2 firstTouch = Vector3.zero;

    /// <summary>
    /// 第二根手指按下的坐标
    /// </summary>
    Vector2 secondTouch = Vector3.zero;
    /// <summary>
    /// 是否有两只手指按下
    /// </summary>
    private bool isTwoTouch = false;
    [SerializeField, Header("缩放")]
    float minExtend = 0.5f;
    [SerializeField, Header("扩大")]
    float maxExtend = 10.0f;
    #endregion

    #region Move

    // 鼠标起点
    private Vector2 originalLocalPointerPosition;
    // 面板起点  
    private Vector3 originalPanelLocalPosition;
    // 当前面板  
    [SerializeField]
    private RectTransform panelRectTransform;
    // 父节点,这个最好是UI父节点，因为它的矩形大小刚好是屏幕大小
    [SerializeField]
    private RectTransform parentRectTransform;
    private static int siblingIndex = 0;

    #endregion

    [SerializeField, Header("UI Camera，需要将Canves Render Mode 设置为 ScreenSpace-Camera")]
    Camera pressEventCamera;
    [SerializeField]
    Image images;
    [SerializeField]
    bool isScrollWheel = false;
    [SerializeField]
    private float mouseExtendScale = 0.0f;
    [SerializeField, Header("鼠标缩放计算")]
    bool scrollScaleCal = false;
    [SerializeField, Range(1, 10)]
    int scaleFactor = 3;

    void Awake()
    {
        panelRectTransform = transform as RectTransform;//.parent as RectTransform;
        parentRectTransform = panelRectTransform.parent as RectTransform;
    }

    // Use this for initialization
    void Start()
    {
    }

    private void LateUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseExtend();
#elif UNITY_IOS || UNITY_ANDROID
        TouchExtend();
#endif
    }

    void MouseExtend()
    {
        if (isScrollWheel)
        {
            float scrollValue = Input.GetAxis("Mouse ScrollWheel");

            if (scrollValue < 0)
            {
                if (mouseExtendScale > 0)
                {
                    mouseExtendScale = 0;
                }
                mouseExtendScale += scrollValue * scaleFactor;
                scrollScaleCal = true;
                Debug.Log("Zoom Out:\t" + mouseExtendScale);
            }
            //-Zoom In-//
            if (scrollValue > 0)
            {
                if (mouseExtendScale < 0)
                {
                    mouseExtendScale = 0;
                }

                mouseExtendScale += scrollValue * scaleFactor;
                scrollScaleCal = true;

                Debug.Log("Zoom In:\t" + scrollValue);
            }

            PictureScale();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePostion = Input.mousePosition;
            OnPointerDown(mousePostion, pressEventCamera);
        }

        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePostion = Input.mousePosition;
            OnDrag(mousePostion, pressEventCamera);
        }

    }

    /// <summary>
    /// 触摸屏放大缩小
    /// </summary>
    void TouchExtend()
    {
        //如果有两个及以上的手指按下
        if (Input.touchCount > 1)
        {
            //当第二根手指按下的时候
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                isTwoTouch = true;
                //获取第一根手指的位置
                firstTouch = Input.touches[0].position;
                //获取第二根手指的位置
                secondTouch = Input.touches[1].position;

                lastDistance = Vector2.Distance(firstTouch, secondTouch);
            }

            //如果有两根手指按下
            if (isTwoTouch)
            {
                //每一帧都得到两个手指的坐标以及距离
                firstTouch = Input.touches[0].position;
                secondTouch = Input.touches[1].position;

                twoTouchDistance = Vector2.Distance(firstTouch, secondTouch);

                //当前图片的缩放
                Vector3 curImageScale = new Vector3(image.localScale.x, image.localScale.y, 1);
                //两根手指上一帧和这帧之间的距离差
                //因为100个像素代表单位1，把距离差除以100看缩放几倍
                float changeScaleDistance = (twoTouchDistance - lastDistance) / 100;
                //因为缩放 Scale 是一个Vector3，所以这个代表缩放的Vector3的值就是缩放的倍数
                Vector3 changeScale = new Vector3(changeScaleDistance, changeScaleDistance, 0);
                //图片的缩放等于当前的缩放加上 修改的缩放
                image.localScale = curImageScale + changeScale;
                //控制缩放级别
                image.localScale = new Vector3(Mathf.Clamp(image.localScale.x, minExtend, maxExtend), Mathf.Clamp(image.localScale.y, minExtend, maxExtend), 1);
                //这一帧结束后，当前的距离就会变成上一帧的距离了
                lastDistance = twoTouchDistance;
            }

            //当第二根手指结束时（抬起）
            if (Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                isTwoTouch = false;
                firstTouch = Vector3.zero;
                secondTouch = Vector3.zero;
            }
        }
        else if (Input.touchCount == 1)
        {
            Vector2 pos = Input.GetTouch(0).position;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                
                OnPointerDown(pos, pressEventCamera);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                
                OnDrag(pos, pressEventCamera);
            }
        }
    }
    /// <summary>
    /// 图片缩放核心脚本 Windows 平台放大缩小
    /// </summary>
    /// <param name="scaleDistance">缩放距离</param>
    private void PictureScale()
    {
        if (scrollScaleCal)
        {
            //当前图片的缩放
            Vector3 curImageScale = new Vector3(image.localScale.x, image.localScale.y, 1);
            //两根手指上一帧和这帧之间的距离差
            //因为100个像素代表单位1，把距离差除以100看缩放几倍
            float changeScaleDistance = mouseExtendScale / 100;
            //因为缩放 Scale 是一个Vector3，所以这个代表缩放的Vector3的值就是缩放的倍数
            Vector3 changeScale = new Vector3(changeScaleDistance, changeScaleDistance, 0);
            //图片的缩放等于当前的缩放加上 修改的缩放
            image.localScale = curImageScale + changeScale;
            //控制缩放级别
            image.localScale = new Vector3(Mathf.Clamp(image.localScale.x, minExtend, maxExtend), Mathf.Clamp(image.localScale.y, minExtend, maxExtend), 1);

            scrollScaleCal = false;

        }

    }

    /// <summary>
    /// 鼠标、手指摁下获取的位置
    /// </summary>
    /// <param name="position"></param>
    /// <param name="pressEventCamera"></param>
    public void OnPointerDown(Vector2 position, Camera pressEventCamera)
    {
        siblingIndex++;
        panelRectTransform.transform.SetSiblingIndex(siblingIndex);

        originalPanelLocalPosition = panelRectTransform.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, position, pressEventCamera, out originalLocalPointerPosition);
    }
    /// <summary>
    /// 拖拽功能
    /// </summary>
    /// <param name="position"></param>
    /// <param name="pressEventCamera"></param>
    public void OnDrag(Vector2 position, Camera pressEventCamera)
    {
        if (panelRectTransform == null || parentRectTransform == null)
            return;

        Vector2 localPointerPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, position, pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }

        ClampToWindow();
    }

    /// <summary>
    /// Clamp panel to area of parent
    /// </summary>
    void ClampToWindow()
    {
        Vector3 pos = panelRectTransform.localPosition;

        Vector3 minPosition = parentRectTransform.rect.min - panelRectTransform.rect.min;
        Vector3 maxPosition = parentRectTransform.rect.max - panelRectTransform.rect.max;

        pos.x = Mathf.Clamp(panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);

        panelRectTransform.localPosition = pos;
    }

}