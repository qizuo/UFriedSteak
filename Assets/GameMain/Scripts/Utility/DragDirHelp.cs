using System;
/*************************************************
 * 项目名称：UGUI通用
 * 脚本创建人：
 * 脚本创建时间:
 * 脚本功能：UI图片拖拽功能（将脚本挂载在需要拖放的图片上）
 * ***********************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

//UI图片拖拽功能类
public class DragDirHelp : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    //翻滚  0 stop 1 leftup 2 rightup 3 up

    //移动 1 下 2 左下
    // dis
    public Action<int,float> directAc;
    public static bool isPanMv = false;
    public MainForm _mainObj;
    // private RectTransform rt;
    private void Awake() {
        // rt = GetComponent<RectTransform>();    
    }
    private Vector2 bgnPoint;    
    private Vector2 curPrePoint;
    
    float opAccTm = 0;
    //达到之后是移动
    float mvStaTm=0.5f;

    //开始拖拽触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!_mainObj.isGaming&&!_mainObj.guidePl.activeSelf){
            return;
        }
        _mainObj.isPanIdle = false;
        isPanMv = false;
        opAccTm=0;
        // Debug.Log("bbbb111..."+eventData.position.x);
        bgnPoint = new Vector2(eventData.position.x,eventData.position.y);
    }

    float intAccTm = 0;
    float intInTm = 0.08f;

    //拖拽过程中触发
    public void OnDrag(PointerEventData eventData)
    {
        if(!_mainObj.isGaming&&!_mainObj.guidePl.activeSelf){
            return;
        }
        intAccTm+=Time.deltaTime;
        // if(intAccTm>intInTm){
            // Debug.Log("ondra....");
        intAccTm=0;

        // Debug.Log("ondrag.."+curPrePoint+" "+eventData.position);
        if(Util.judgeTwoVec2Equ(curPrePoint,eventData.position)){
            directAc?.Invoke(0,0);
            return;
        }
        if(opAccTm>mvStaTm){
            isPanMv = true;
            if(_mainObj.guidePl.activeSelf){
                _mainObj.hidePanGuide();
                isMvGd = true;
            }
        }

        curPrePoint = eventData.position;
    }
    bool isMvGd = false;
    //结束拖拽触发
    public void OnEndDrag(PointerEventData eventData)
    {
        if(!_mainObj.isGaming&&!_mainObj.guidePl.activeSelf&&!isMvGd){
            return;
        }
        // Debug.Log(opAccTm+" "+mvStaTm);
        if(opAccTm<mvStaTm){
            float delX = eventData.position.x-bgnPoint.x;
            float delY = eventData.position.y-bgnPoint.y;
            // Debug.Log(delX+" "+delY);
            if(Mathf.Abs(delX)<100&&delY>0){
                directAc?.Invoke(3,delY);
            }
            else if(delY>0){
                float dis = Mathf.Sqrt(delX*delX+delY*delY);
                if(delX<0&&Mathf.Abs(delX+delY)<200){
                    directAc?.Invoke(1,dis);
                }
                else if(delX>0&&Mathf.Abs(delX-delY)<200){
                    directAc?.Invoke(2,dis);
                }
            }

            //飞1s
            // Sequence wtSeq = DOTween.Sequence();
            // wtSeq.AppendInterval(1);
            // wtSeq.AppendCallback(delegate(){
            //     directAc?.Invoke(0,0);
            // });
            // wtSeq.SetAutoKill();
            if(_mainObj.guidePl.activeSelf){
                _mainObj.hidePanGuide();
            }
        }
        else{
            // if(isMvGd){
            //     Sequence wtSeq =DOTween.Sequence();
            //     wtSeq.AppendInterval(1f);
            //     wtSeq.AppendCallback(delegate(){
            //         _mainObj.rollPanGuide(1);    
            //     });
            //     wtSeq.SetAutoKill();

            //     isMvGd = false;
            // }
        }

        _mainObj.isPanIdle = false;
        directAc?.Invoke(0,0);
        
        // if(eventData.position.x==bgnPoint.x){
        //     directAc?.Invoke(0);
        // }
        // else if(eventData.position.x<bgnPoint.x){
        //     directAc?.Invoke(-1);
        // }
        // else{
        //     directAc?.Invoke(1);
        // }
    }

    void Update(){
        opAccTm+=Time.deltaTime;
    }
}