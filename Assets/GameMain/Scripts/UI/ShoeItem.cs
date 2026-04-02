using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarForce;
using Battle;
// using Umeng;
using DG.Tweening;

public class ShoeItem : MonoBehaviour
{
    
    public float oriHight = -0.04f;

    public int shitmIdx = 0;
    public int shoeId=1;
    public Animator[] _animators;
    GameObject staticItem;
    public int dymItemCt = 14;
    //手...棒球棒
    public int curState =1;
    GameObject emjDisObj;
    GameObject emjAngObj;
    void Awake(){
        _animators[curState-1].Play("run");
        emjDisObj = transform.Find("emjDis").gameObject;
        emjAngObj = transform.Find("emjAng").gameObject;
        emjDisObj.SetActive(false);
        emjAngObj.SetActive(false);
        switchState(1);
        emjPoss[0][1] = 1.8f+(Screen.height-1920)/420f*0.3f;
        emjPoss[1][1] = 1.73f+(Screen.height-1920)/420f*0.3f;
        if(Screen.width==800){
            emjPoss[0][1]+=0.9f;
            emjPoss[1][1]+=0.81f;

            emjPoss[0][0]+=0.2f;
            emjPoss[1][0]+=0.07f;
        }
        else if(Screen.height==1280){
            emjPoss[0][1]+=0.6f;
            emjPoss[1][1]+=0.57f;

            // emjPoss[1][0]-=0.05f;
        }
        else if(Screen.height==2340){
            emjPoss[0][1]-=0.2f;
            emjPoss[1][1]-=0.2f;
            // emjPoss[1][0]-=0.05f;
        }
        
    }

    //1-5
    public void switchState(int type){
        curState = type;
        for(int i=0;i<5;i++){
            GameObject ideObj = transform.Find("ly"+(i+1)).gameObject;
            ideObj.SetActive(false);
        }
        GameObject curObj = transform.Find("ly"+type).gameObject;
        curObj.SetActive(true);
    }

    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {
        // BoxCollider boxCo;
        // boxCo.po
    }


    public void updateEmjPos(float mnPosX){
        //0.08f 0.1f
        Vector3 newPos = emjDisObj.transform.localPosition;
        newPos.x=-mnPosX/1.2f*0.08f-0.45f;    
        newPos.y=-mnPosX/1.2f*0.1f+1.8f+(Screen.height-1920)/420f*0.3f;

        if(Screen.width==800){
            newPos.y+=0.9f;
            newPos.x=-mnPosX/1.2f*0.12f-0.25f;
        }
        else if(Screen.height==1280){
            newPos.y+=0.6f+mnPosX/1.2f*0.03f;
            newPos.x=mnPosX/1.2f*0.01f-0.45f;
        }
        else if(Screen.height==2340){
            newPos.y-=0.2f;
            // emjPoss[1][1]+=0.57f;
            // emjPoss[1][0]-=0.05f;
        }

        emjDisObj.transform.localPosition = newPos;
        emjAngObj.transform.localPosition = newPos;   
    }

    Vector3[] emjPoss = new Vector3[]{new Vector3(-0.45f,1.7f,0),new Vector3(-0.2f,1.63f,0)};
    Vector3[] emjScls = new Vector3[]{new Vector3(0.3f,0.3f,0.3f),new Vector3(0.15f,0.15f,0.15f)};

    // -0.24 0 0
    //  0.2 0.2 0.2
    float vdoDelX=0;
    public bool isVdoAdjX = false;
    public void switchEmjSte(int type){

        if(Screen.width==800&&type==2){
            vdoDelX=0;
        }
        emjDisObj.transform.localPosition = emjPoss[type-1];
        emjDisObj.transform.localScale = emjScls[type-1]; 
        emjAngObj.transform.localPosition = emjPoss[type-1];
        emjAngObj.transform.localScale = emjScls[type-1]; 
      
    }

    
    void Update(){
        if(bdAccTm>0){
            bdAccTm-=Time.deltaTime;
        }
        if(isVdoAdjX){
            float posX=0;
            if(Screen.width==800){
                vdoDelX+=0.0005f;
                posX = emjPoss[1][0]-vdoDelX;
            }
            else if(Screen.height==1280){
                vdoDelX+=0.0001f;
                posX = emjPoss[1][0]+vdoDelX;
            }
        
            Vector3 newPos = emjDisObj.transform.localPosition;
            newPos.x = posX;
            emjDisObj.transform.localPosition = newPos;
            emjAngObj.transform.localPosition = newPos;
        }
    }

    int curEmjType = 1;
    Sequence emjSeq;
    // 1 dis 2 ang
    public void playEmoji(int type,bool force = false){
        // Debug.Log("p.."+type+" "+curEmjType);
        if(type==curEmjType&&!force){
            return;
        }
        curEmjType = type;
        ParticleSystem ps;
        if(type==1){
            emjDisObj.SetActive(true);
            emjAngObj.SetActive(false);
            ps = emjDisObj.GetComponent<ParticleSystem>();
            
        }
        else{
            emjDisObj.SetActive(false);
            emjAngObj.SetActive(true);
            emjAngObj.transform.GetChild(0).gameObject.SetActive(true);
            ps = emjAngObj.GetComponent<ParticleSystem>();
        }
        if(emjSeq!=null){
            emjSeq.Kill(true);
            emjSeq=null;
        }
        emjSeq = DOTween.Sequence();
        emjSeq.AppendCallback(delegate(){
            ps.Simulate(0f); 
            ps.Play();
        });
        emjSeq.AppendInterval(0.8f);
        emjSeq.AppendCallback(delegate(){
            ps.Pause();
            if(type==2){
                emjAngObj.transform.GetChild(0).gameObject.SetActive(false);
            }
            emjSeq.Kill(true);
            emjSeq=null;
        });
        
    }


    float bdAccTm = 0;
    float bdInTm = 0.05f;    
    private void OnTriggerStay(Collider other){
        if(other.name=="itemBd(Clone)"){
            // Debug.Log("tir..bbb"+other.transform.localScale.x);
            if(bdAccTm<=0){
                int posType=0;
                if(other.transform.localPosition.x>0){
                    posType=1;
                }
                else if(other.transform.localPosition.x<0){
                    posType=-1;
                }

                MainForm.onHitWayBdAc?.Invoke(posType);
                bdAccTm=bdInTm;
            }
        }
    }

    public void playHit(){
        _animators[curState-1].enabled = true;
        _animators[curState-1].StopPlayback();
        _animators[curState-1].Play("hit",0,0);
        _animators[curState-1].Update(0);
    }

    public void playCry(){
        _animators[curState-1].enabled = true;
        _animators[curState-1].StopPlayback();
        _animators[curState-1].Play("cry",0,0);
        _animators[curState-1].Update(0);   
    }


    public void resetAni(bool isRst = false){
        // // Debug.Log("resani");
        // // _animator.stop
        // _animator.enabled = true;
        // _animator.StopPlayback();
        // _animator.Play("walk",0,0);
        // _animator.Update(0);
        // if(isRst){
        //     _animator.SetTrigger("toRun");
        // }
        _animators[curState-1].enabled = true;
        if(isRst){
            _animators[curState-1].StopPlayback();
            _animators[curState-1].Play("run",0,0);
            _animators[curState-1].Update(0);
        }
    }
    public bool canPlayAni = true;

    public void disAnimator(){
        _animators[curState-1].enabled = false;
    }
    

    public GameObject getStaticItem(){
        return staticItem;
    }

    public void stopRun(){
        _animators[curState-1].StopPlayback();
        _animators[curState-1].enabled = false;
    }

    //1 正常跳 2 静止
    public void switchShoeSte(int type){
        if(type==1){
            for(int i=0;i<dymItemCt;i++){
                GameObject item = transform.GetChild(i).gameObject;
                item.SetActive(true);   
            }
            staticItem.SetActive(false);
        }
        else{
            for(int i=0;i<dymItemCt;i++){
                GameObject item = transform.GetChild(i).gameObject;
                item.SetActive(false);   
            }
            staticItem.SetActive(true);
        }
    }

}
