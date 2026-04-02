using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdptio : MonoBehaviour {

     public   float standard_width = 1080;        //初始宽度
     public float standard_height = 1920;       //初始高度
     public float device_width = 0f;                //当前设备宽度
     public float device_height = 0f;               //当前设备高度
     public float adjustor = 0f;         //屏幕矫正比例
     private CanvasScaler sclaer;
     private int frame = 0;
     private double frameStartTime;
     private float fps;
     private float myTime = 0;
     private string message;
 
    void Start()
    {
		//获取设备宽
		device_width = Screen.width;
		device_height = Screen.height;

        if (device_width == 2001)
        {
            device_width = 2436;
            device_height = 1125;
        }
        sclaer = this.GetComponent<CanvasScaler>();
       // sclaer.referenceResolution = new Vector2(device_width, device_height);
        //计算宽高比例
        float standard_aspect = standard_width / standard_height;
        float device_aspect = device_width / device_height; 
        //计算矫正比例
        if (device_aspect < standard_aspect)
        {
            adjustor = standard_aspect / device_aspect;
        }
        //iphonexs Max 特殊判断
        if (Screen.height == 1242 && Screen.width == 2689)
        {
            adjustor = 0;
        }

        float hgRate = standard_height/device_height;
        if(hgRate>1){
            hgRate = 1/hgRate;
        }
        float wdhRate = standard_width/device_width;
        if(wdhRate>1){
            wdhRate = 1/wdhRate;
        }

        CanvasScaler canvasScalerTemp = transform.GetComponent<CanvasScaler>();
        if (adjustor == 0)
        {
            canvasScalerTemp.matchWidthOrHeight = 1f;
        }
        else
        {
            canvasScalerTemp.matchWidthOrHeight =0f;
        }

        transform.SetLocalScaleX(1);
        transform.SetLocalScaleY(1);
        RectTransform rtf = GetComponent<RectTransform>();
        rtf.sizeDelta = new Vector2(1080,1920);
        float sWidth = Screen.width;
        float sHeight = Screen.height;

        if(sWidth<standard_width){
            sWidth = standard_width;
        }
        if(sHeight<standard_height){
            sHeight = standard_height;
        }

        rtf.sizeDelta = new Vector2(sWidth,sHeight);
        if(device_aspect!=standard_aspect){
            if(device_height<standard_height||device_width<standard_width){
                if(device_aspect<standard_aspect){
                    transform.SetLocalScaleX(device_aspect/standard_aspect);
                    transform.SetLocalScaleY(device_aspect/standard_aspect);
                    rtf.sizeDelta = new Vector2(sWidth,standard_height/(device_aspect/standard_aspect));
                }
                else{
                    transform.SetLocalScaleX(1);
                    transform.SetLocalScaleY(1);
                    rtf.sizeDelta = new Vector2(standard_width/(standard_aspect/device_aspect),sHeight);
                }
            }
            else{
                if(adjustor>1){
                    transform.SetLocalScaleX(hgRate);
                    transform.SetLocalScaleY(hgRate);
                }
                else{
                    transform.SetLocalScaleX(wdhRate);
                    transform.SetLocalScaleY(wdhRate);
                }
            }
        }
        
    }

}
