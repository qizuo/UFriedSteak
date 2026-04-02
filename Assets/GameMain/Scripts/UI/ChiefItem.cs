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

public class ChiefItem : MonoBehaviour
{
    
   
    public Animator _animator;
    void Awake(){
        // ballRanZ = UnityEngine.Random.Range(1,15);
        //[4,14)
        // mealBdObj = transform.Find("brd").gameObject;
        // mealBdObj.SetActive(kktrue);
    }

    public void playMake(){
        // Debug.Log("make...");
        // _animator.StopPlayback();
        // _animator.Play("make",0,0);
        // _animator.Update(0);
        // Invoke("playMkSd",1);
        // _animator.SetTrigger("toMake");
        // mealBdObj.SetActive(true);
        // Invoke("playIdle",1.6f);
        // // Invoke("hideBrd",2.3f);
    }

    void playMkSd(){
        MainForm.playGmSound(2,"mk_ml");
    }

    void hideBrd(){
        // mealBdObj.SetActive(false);
    }

    public void playIdle(){
        _animator.SetTrigger("toWait");
    }


    private void OnCollisionEnter(Collision other){
        // Debug.Log("meat on col."+transform.name+" ");
        Debug.Log(other);
    }    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        // Debug.Log("istri.."+other);

    }

    void Update(){
        // Debug.Log("uu.");
    }

}
