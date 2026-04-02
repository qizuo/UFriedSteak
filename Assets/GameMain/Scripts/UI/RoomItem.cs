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
public class RoomItem : MonoBehaviour
{
    
    public Animator _animator;
    void Awake(){
        // Debug.Log(htObjs[0]+" "+htObjs[1]);
        _animator.enabled = false;
        _animator.speed =1;
        // _animator.Play("idle");
    }

    public void playIdle(){
        _animator.enabled = true;
    }

    public void playHitFly(){
        // _animator.enabled = true;
        // _animator.SetTrigger("toFly");
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
    void Update(){
    }

}
