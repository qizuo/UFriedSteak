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

public class MealItem : MonoBehaviour
{

    public Material[] flickMats;
    
    MeshRenderer[] mrdArr;
    void Awake(){
        // ballRanZ = UnityEngine.Random.Range(1,15);
        //[4,14)
        // bkObj1 = transform.Find("panzi/item1").gameObject;
        // bkObj2 = transform.Find("panzi/item2").gameObject;
        // bkObj3 = transform.Find("panzi/item3").gameObject;
        
        // mrdArr = new MeshRenderer[3];
        // mrdArr[0] = bkObj1.GetComponent<MeshRenderer>();
        // mrdArr[1] = bkObj2.GetComponent<MeshRenderer>();
        // mrdArr[2] = bkObj3.GetComponent<MeshRenderer>();
    }

    int _type=1;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("istri.."+other.name);
        string mlNm="";
        if(other.name.Length>6){
            mlNm = other.name.Substring(0,6);
        }
        if(mlNm=="itemMl"){
            _mnFmobj.onMlItemDestory(_type);
        }
        if(other.name=="item9"){
            _mnFmobj.onMlItemDestory(_type);
        }

        if(other.name=="item2(Clone)"){
            // Debug.Log("hit coin");
            MainForm.hitCoinAc?.Invoke(other.transform);
        }
        
    }

    float curMatAlp = 255;
    float matfadeSpd = -3;
    float matMaxAl = 255;
    float matMinAl = 100;

    bool isMatFlk = false;

    public void playFlick(){
        isMatFlk = true;
    }
    public void stopFlick(){
        isMatFlk = false;
        for(int i=0;i<3;i++){
            Color cur = mrdArr[i].material.color;
            cur.a = 1;
            mrdArr[i].material.color=cur;
        }
    }


    GameObject bkObj1;
    GameObject bkObj2;
    GameObject bkObj3;
    public void playPanBreak(){
        MainForm.playGmSound(2,"pan_bk");
        // Debug.Log("play pan break");
        // bkObj1.transform.DOLocalMoveZ(-3.5f,0.5f);
        // bkObj2.transform.DOLocalMoveX(19.7f,0.5f);
        // Vector3 newPos = new Vector3(19.1f,-0.057f,-2.7f);
        // bkObj3.transform.DOLocalMove(newPos,0.5f);
    }


    void choiceItem(){
        GameObject mlObj1 = transform.Find("item1").gameObject;
        GameObject mlObj2 = transform.Find("item2").gameObject;
        GameObject mlObj3 = transform.Find("item3").gameObject;
        GameObject mlObj4 = transform.Find("item4").gameObject;   

        
        mlObj1.SetActive(false);
        mlObj2.SetActive(false);
        mlObj3.SetActive(false);
        mlObj4.SetActive(false);

        if(curMlType==1){
            mlObj1.SetActive(true);
            curMlObj = mlObj1;
        }        
        else if(curMlType==2){
            mlObj2.SetActive(true);
            curMlObj = mlObj2;
        }
        else if(curMlType==3){
            mlObj3.SetActive(true);
            curMlObj = mlObj3;
        }
        else{
            mlObj4.SetActive(true);
            curMlObj = mlObj4;
        }
    }

    
    GameObject curMlObj;
    int curMlType=1;
    MainForm _mnFmobj;
    Material _oriMat;
    public void setType(int type,MainForm obj,Material oriMat){
        curMlType = UnityEngine.Random.Range(1,5);
        choiceItem();

        // MeshRenderer mrd1 = bkObj1.GetComponent<MeshRenderer>();
        // MeshRenderer mrd2 = bkObj2.GetComponent<MeshRenderer>();
        // MeshRenderer mrd3 = bkObj3.GetComponent<MeshRenderer>();

        // mrd1.material = oriMat;
        // mrd2.material = oriMat;
        // mrd3.material = oriMat;

        // _type = type+1;
        // _mnFmobj = obj;
        // _oriMat = oriMat;
    }

    void Update(){
        if(isMatFlk){
            for(int i=0;i<3;i++){
                Color cur = mrdArr[i].material.color;
                cur.a = curMatAlp/255f;
                mrdArr[i].material.color = cur;

                curMatAlp+=matfadeSpd;
                if(curMatAlp>matMaxAl){
                    matfadeSpd=-matfadeSpd;
                }
                if(curMatAlp<matMinAl){
                    matfadeSpd=-matfadeSpd;
                }
            }
        }
    }

}
