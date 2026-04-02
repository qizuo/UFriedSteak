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

public class ManItem : MonoBehaviour
{


    public Animator _animator;
    void Awake(){
        _animator.enabled = false;
    }

    public void playFly(){
        _animator.enabled = true;
    }
   
    bool isTri = false;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("ontri..");
        if(isTri){
            return;
        }
        isTri =true;
    }
    // public bool isRot = true;

    void Update(){
     
    }

}
