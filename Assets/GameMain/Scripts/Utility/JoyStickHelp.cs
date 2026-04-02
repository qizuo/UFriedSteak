using UnityEngine;
using UnityEngine.EventSystems;
 
public class JoyStickHelp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    /// <summary>
    /// 摇杆最大半径
    /// 以像素为单位
    /// </summary>
    private float JoyStickRadius = 10;
   
    /// <summary>
    /// 当前的Transform组件
    /// </summary>
    private RectTransform selfTransform;
    /// <summary>
    /// 当前摇杆的Transform组件
    /// </summary>
    public RectTransform joyTransform;
    /// <summary>
    /// 当前摇杆的背景
    /// </summary>
    public RectTransform joyBGTransform;
    /// <summary>
    /// 是否触摸了虚拟摇杆
    /// </summary>
    private bool isTouched = false;
    /// <summary>
    /// 虚拟摇杆的默认位置
    /// </summary>
    private Vector2 originPosition;
    /// <summary>
    /// 虚拟摇杆的移动方向
    /// </summary>
    private Vector2 touchedAxis;
    public Vector2 TouchedAxis
    {
        get
        {
            //if (touchedAxis.magnitude < JoyStickRadius)//数值随偏移量变化
            //    return touchedAxis.normalized / JoyStickRadius;
            return touchedAxis.normalized;
        }
    }
    /// <summary>
    /// 定义触摸开始事件委托
    /// </summary>
    public delegate void JoyStickTouchBegin(Vector2 vec);
    /// <summary>
    /// 定义触摸过程事件委托
    /// </summary>
    /// <param name="vec">虚拟摇杆的移动方向</param>
    public delegate void JoyStickTouchMove(Vector2 vec);
    /// <summary>
    /// 定义触摸结束事件委托
    /// </summary>
    public delegate void JoyStickTouchEnd();
    /// <summary>
    /// 注册触摸开始事件
    /// </summary>
    public event JoyStickTouchBegin OnJoyStickTouchBegin;
    /// <summary>
    /// 注册触摸过程事件
    /// </summary>
    public event JoyStickTouchMove OnJoyStickTouchMove;
    /// <summary>
    /// 注册触摸结束事件
    /// </summary>
    public event JoyStickTouchEnd OnJoyStickTouchEnd;
    void Start()
    {
        joyBGTransform.gameObject.SetActive(false);
        JoyStickRadius = joyBGTransform.sizeDelta.x * 0.5f;
        //初始化虚拟摇杆的默认方向
        selfTransform = this.GetComponent<RectTransform>();
        originPosition = joyTransform.anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 LocalPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(selfTransform,
                 eventData.position, eventData.pressEventCamera, out LocalPosition);
        joyBGTransform.localPosition = LocalPosition;
        // joyBGTransform.gameObject.SetActive(true);
 
        isTouched = true;
        touchedAxis = GetJoyStickAxis(eventData);
        if (this.OnJoyStickTouchBegin != null)
            this.OnJoyStickTouchBegin(TouchedAxis);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // joyBGTransform.gameObject.SetActive(false);
 
        isTouched = false;
        joyTransform.anchoredPosition = originPosition;
        touchedAxis = Vector2.zero;
        if (this.OnJoyStickTouchEnd != null)
            this.OnJoyStickTouchEnd();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        touchedAxis = GetJoyStickAxis(eventData);
        //if (this.OnJoyStickTouchMove != null)
        //    this.OnJoyStickTouchMove(TouchedAxis);
    }
    void Update()
    {
        //当虚拟摇杆无拖动
        //为了确保被控制物体可以继续移动
        //在这里手动触发OnJoyStickTouchMove事件
      
        if (isTouched && touchedAxis.magnitude >0)
        {
            if (this.OnJoyStickTouchMove != null)
                this.OnJoyStickTouchMove(TouchedAxis);
        }
        //松开虚拟摇杆后让虚拟摇杆回到默认位置
        if (!isTouched && joyTransform.anchoredPosition.magnitude > originPosition.magnitude)
            joyTransform.anchoredPosition = originPosition;
    }
    /// <summary>
    /// 返回虚拟摇杆的偏移量
    /// </summary>
    /// <returns>The joy stick axis.</returns>
    /// <param name="eventData">Event data.</param>
    private Vector2 GetJoyStickAxis(PointerEventData eventData)
    {
        //获取手指位置的世界坐标
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(joyTransform,
                 eventData.position, eventData.pressEventCamera, out worldPosition))
            joyTransform.position = worldPosition;
        //获取摇杆的偏移量
        Vector2 touchAxis = joyTransform.anchoredPosition - originPosition;
        //摇杆偏移量限制
        if (touchAxis.magnitude >= JoyStickRadius)
        {
            touchAxis = touchAxis.normalized * JoyStickRadius;
            joyTransform.anchoredPosition = touchAxis;
        }
        return touchAxis;
    }
}