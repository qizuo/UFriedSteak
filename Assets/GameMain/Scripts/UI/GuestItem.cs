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

public class GuestItem : MonoBehaviour
{
    
   
    public Animator _ani;
    Animator curAni;
    
    GameObject manObj;
    GameObject womanObj;

    GameObject hapEmoji;
    GameObject cryEmoji;
    void Awake(){
        // ballRanZ = UnityEngine.Random.Range(1,15);
        //[4,14)
        // manObj = transform.GetChild(0).gameObject;
        // womanObj = transform.GetChild(1).gameObject;
        
        // hapEmoji = transform.Find("emoji/item1").gameObject;
        // cryEmoji = transform.Find("emoji/item2").gameObject;
    }

    public void showEmoji(int type){
        // Debug.Log("shoemjj..."+type);
        // ParticleSystem ps;
        // if(type==1){
        //     hapEmoji.SetActive(true);
        //     cryEmoji.SetActive(false);
        //     ps = hapEmoji.GetComponent<ParticleSystem>();
        //     ps.Simulate(0f);
        //     ps.Play();
        //     ps.loop =true;
        // }
        // else{
        //     hapEmoji.SetActive(false);
        //     cryEmoji.SetActive(true);
        //     ps = cryEmoji.GetComponent<ParticleSystem>();
        //     ps.Simulate(0f);
        //     ps.Play();
        //     ps.loop =true;
        // }
    }

    public void stopEmoji(){
        // hapEmoji.SetActive(false);
        // cryEmoji.SetActive(false);
    }


    public void playWait(){
        _ani.SetTrigger("toWait");
    }

    
    public void playWalk(){
        _ani.SetTrigger("toWalk");
    } 
    public void playVict(){
        _ani.SetTrigger("toVict");
    }

    public void playAngry(){
        // curAni.SetTrigger("toAngry");

        // Sequence acSeq = DOTween.Sequence();
        // acSeq.AppendInterval(2.5f);
        // acSeq.AppendCallback(delegate(){
        //     showEmoji(2);
        //     playIdle();
        // });
        // acSeq.SetAutoKill();
    }

    public void playIdle(){
        curAni.SetTrigger("toWait");
    }

    public void switchItem(int type){
        manObj.SetActive(type==1);
        womanObj.SetActive(type==2);
    }

    //1 man 2 woman
    public void setType(int type){
        // if(type==1){
        //     curAni = _ani_m;
        // }
        // else{
        //     curAni = _ani_w;
        // }
        // switchItem(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("istri.."+other);

    }

    void Update(){
    }


}
