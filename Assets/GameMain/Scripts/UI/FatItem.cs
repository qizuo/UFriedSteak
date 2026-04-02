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

public class FatItem : MonoBehaviour
{
    
   
    public Animator _animator;
    void Awake(){
        // ballRanZ = UnityEngine.Random.Range(1,15);
        //[4,14)
    }

    public void playEat(){
        MainForm.playGmSound(2,"eat");
        // Debug.Log("eat...");
        // _animator.StopPlayback();
        // _animator.Play("make",0,0);
        // _animator.Update(0);
        _animator.SetTrigger("toEat");
        Invoke("playIdle",2.5f);
    }

    public void playHit(){
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("eat"))
        {
            return;
        } 
        _animator.SetTrigger("toHit");
        Invoke("playIdle",1.3f);
    }

    public void playIdle(){
        _animator.SetTrigger("toWait");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("istri.."+other);

    }

    float hitAccTm = 0;
    float hitInTm = 4;
    void Update(){
        hitAccTm+=Time.deltaTime;
        if(hitAccTm>=hitInTm){
            hitAccTm=0;
            playHit();
        }
    }
}
