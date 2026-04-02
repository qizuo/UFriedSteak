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

public class CubHdItem : MonoBehaviour
{
    
    // -0.2 0.8
    //-0.3 1
    //-0.5 1.4
    float oriY = 0.3f;
    float delY = 0.15f;

    float[] posXs = new float[]{-3.3f,-3.3f,-3.3f};
    float[] sclXs = new float[]{300f,300f,300f};
    GameObject[] htObjs = new GameObject[2];
    void Awake(){

        htObjs[0] = transform.parent.Find("bot1").gameObject;
        htObjs[1] = transform.parent.Find("bot2").gameObject;
        // Debug.Log(htObjs[0]+" "+htObjs[1]);
    }
    bool isTri = false;
    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("hehele");
        if(isTri){
            return;
        }
        isTri = true;
        MainForm.onRotBarHitAc?.Invoke(_ht);
    }

    void setBotVis(int ht){
        for(int i=0;i<2;i++){
            htObjs[i].SetActive(false);
        }
        for(int i=0;i<ht;i++){
            htObjs[i].SetActive(true);
        }
    }

    int _ht = 1;
    public void setParams(int len,int height){
        if(len>3){
            len =3;
        }
        if(height>5){
            height-=5;
            transform.parent.eulerAngles = new Vector3(0,0,0);
        }
        else{
            transform.parent.eulerAngles = new Vector3(0,180,0);
        } 
        if(height>3){
            height=3;
        }  
        float ht = oriY+(height-1)*delY;
        _ht = height;
        transform.localPosition = new Vector3(posXs[len-1],ht,0f);
        transform.localScale = new Vector3(sclXs[len-1],300f,300f);

        setBotVis(height-1);
    }

    void Update(){
    }

}
