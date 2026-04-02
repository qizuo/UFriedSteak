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

//纸币
public class PpMoyItem : MonoBehaviour
{
    
      void Awake(){
        // Debug.Log(htObjs[0]+" "+htObjs[1]);
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
        MainForm.onHitPpMoyAc?.Invoke(other.transform);
        GameObject.Destroy(gameObject);
    }
    void Update(){
    }

}
