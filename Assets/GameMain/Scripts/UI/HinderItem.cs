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

public class HinderItem : MonoBehaviour
{

    //1 钻戒 相框 游戏机 衬衫 
    int itemId = 1;

    int hasTri = 0;
    void Awake(){

    }
    int _type=1;
    public void setType(int type){
        _type = type;
    }

    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {
        // int score,int mul,int color
        int val = MainForm.angerConfig[_type+1][0];
        MainForm.hitHinderAc?.Invoke(val,1,1);
        GameObject.Destroy(gameObject);
    }

}
