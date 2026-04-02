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

public class TurnWayItem : MonoBehaviour
{
    

    void Awake(){
        // _animator.Play("run");
        
    }
    bool isTri = false;
    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("tri.."+other);
        if(isTri){
            return;
        }
        isTri = true;
        string nameStr = transform.name;
        if(nameStr[0]=='l'){
            nameStr = nameStr.Substring(0,7);
        }
        else{
            nameStr = nameStr.Substring(0,8);
        }

        if(nameStr=="leftWay"){
            MainForm.turnWayAc?.Invoke(1);
        }
        else if(nameStr=="rightWay"){
            MainForm.turnWayAc?.Invoke(2);
        }
    }

    float triCd = 4;
    float triAccTm = 0;
    void Update(){
        if(isTri){
            triAccTm+=Time.deltaTime;
            if(triAccTm>triCd){
                triAccTm=0;
                isTri = false;
            }
        }
    }


}
