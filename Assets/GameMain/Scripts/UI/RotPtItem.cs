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

public class RotPtItem : MonoBehaviour
{


    public Material flkMat;

    //1 直道 2 弯道
    public int _type=1;

    // public Animator _animator;
    MeshRenderer[] mrdArr;
    void Awake(){
        // _animator.enabled = false;
        mrdArr = new MeshRenderer[6];
        for(int i=0;i<3;i++){
            MeshRenderer mrd = transform.Find("strt/item"+(i+1)+"/geban4/up").GetComponent<MeshRenderer>(); 
            mrdArr[i]=mrd;
        }
        for(int i=0;i<3;i++){
            MeshRenderer mrd = transform.Find("turn/item"+(i+1)+"/geban4/up").GetComponent<MeshRenderer>(); 
            mrdArr[i+3]=mrd;
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        string mlNm="";
        if(other.name.Length>6){
            mlNm = other.name.Substring(0,6);
        }

        if(mlNm=="itemMl"){
        }
    }
    // public bool isRot = true;

    bool isMatFlk = false;

    float curMatAlp = 255;
    float matfadeSpd = -3;
    float matMaxAl = 255;
    float matMinAl = 100;

    public void playFlick(){
        isMatFlk = true;

        // string path = "";
        // if(_type==1){
        //     path = "strt/item";
        // }
        // else{
        //     path = "turn/item";
        // }

        // for(int i=0;i<3;i++){
        //     MeshRenderer mrd = transform.Find(path+(i+1)+"/geban4").GetComponent<MeshRenderer>(); 
        //     Material oriMat = mrd.material;

        //     Sequence flkSeq = DOTween.Sequence();
        //     flkSeq.AppendCallback(delegate(){
        //         mrd.material = flkMat;
        //     });
        //     flkSeq.AppendInterval(0.8f);
        //     flkSeq.AppendCallback(delegate(){
        //         mrd.material = oriMat;
        //     });
        //     flkSeq.AppendInterval(0.8f);
        //     flkSeq.SetLoops(6);
        //     flkSeq.SetAutoKill();
        // }
       
    }

    public void stopFlick(){
        isMatFlk = false;
        int staIdx = 0;
        int endIdx = 3;
        if(_type==2){
            staIdx+=3;
            endIdx+=3;
        }
        for(int i=staIdx;i<endIdx;i++){
            Color cur = mrdArr[i].material.color;
            cur.a = 1;
            mrdArr[i].material.color=cur;
        }
    }

     void switchShape(int type){
        GameObject sub1 = transform.Find("strt").gameObject;
        GameObject sub2 = transform.Find("turn").gameObject;
        sub1.SetActive(type==1);
        sub2.SetActive(type==2);
    }

    MainForm _mnFmobj;
    public void setType(int type,MainForm obj){
        _type = type;
        switchShape(type);
        _mnFmobj = obj;
    }

    void Update(){
        if(isMatFlk){
            int staIdx = 0;
            int endIdx = 3;
            if(_type==2){
                staIdx+=3;
                endIdx+=3;
            }

            for(int i=staIdx;i<endIdx;i++){
                Color cur = mrdArr[i].material.color;
                cur.a = curMatAlp/255f;
                mrdArr[i].material.color = cur;
                curMatAlp+=matfadeSpd;
                if(curMatAlp>matMaxAl){
                    matfadeSpd=-matfadeSpd;
                }
                if(curMatAlp<matMinAl){
                    matfadeSpd=-matfadeSpd;
                }
            }
        }
    }

}
