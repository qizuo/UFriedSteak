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

//房间
public class SmRoomItem : MonoBehaviour
{
    
    GameObject item1;
    GameObject item2;
    void Awake(){
        item1 = transform.GetChild(0).gameObject;
        item2 = transform.GetChild(1).gameObject;
    }

    int _type=1;

    
    float[] delZs = new float[]{4,0,8.7f,13.3f};

    int _angerType=1;
    public void setType(int type){
        _type = type;
        _angerType = type%10;
        item1.SetActive(_angerType==1);
        item2.SetActive(_angerType==2);
        int capType = type/10;
        Vector3 cPos = transform.localPosition;
        cPos.z+=delZs[capType-1];
        transform.localPosition = cPos;
    }

    


    private void OnTriggerStay(Collider other)
    {
        if(_angerType==1){
            MainForm.hitHinderAc?.Invoke(1,2,1);    
        }
        else{
            MainForm.hitHinderAc?.Invoke(-1,2,1);
        }
    }

    bool isTri = false;
    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        if(isTri){
            return;
        }
        isTri = true;
    }
    bool isETri = false;
    private void OnTriggerExit(Collider other)
    {
        // Debug.Log(other);
        if(isETri){
            return;
        }
        isETri = true;
        MainForm.hitHinderAc?.Invoke(0,2,1);
    }

    void Update(){
    }

}
