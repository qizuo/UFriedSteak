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

public class LitHdItem : MonoBehaviour
{

    //0 加怒气 1 减怒气
    int _winType=0;
    int _scoreType=1;

    int width=1;

    GameObject effObj;

    public Material[] scoreMats;
    SpriteRenderer imgSrd;
    void Awake(){
        effObj = transform.parent.GetChild(3).gameObject;
        imgSrd = transform.Find("bgSp").GetComponent<SpriteRenderer>();
    }
    bool isTri = false;
    // 0.04 0.05
    private void OnTriggerEnter(Collider other)
    {  
        // Debug.Log("lit...tri.."+isTri);
        if(isTri){
            return;
        }
        isTri=true;
        // int score,int mul,int color
        
        if(_winType==1){
            int val = MainForm.angerConfig[0][_scoreType+3];
            MainForm.hitHinderAc?.Invoke(val,1,1);
        }
        else{
            int val = MainForm.angerConfig[0][_scoreType-1];
            MainForm.hitHinderAc?.Invoke(val,1,1);
        }
        GameObject.Destroy(transform.parent.gameObject);
    }

    void setBrdMat(Material mt){
        // transform.GetComponent<Renderer>().material = mt;
        setBoardShaderData(new Vector2(-0.63f,-1.08f),new Vector2(-0.18f,0.04f));
    }

    Vector3 symPos = new Vector3(-0.25f,0f,0f);
    Vector3 mulSymPos = new Vector3(-14.2f,0f,0f);
    Vector3 symRot = new Vector3(0,-180,0);
    Vector3 symScl = new Vector3(1f,1f,1f);
    Vector3 mulSymScl = new Vector3(0.8f,0.8f,0.8f);

    Vector3 numPos = new Vector3(0.15f,0f,0f);
    Vector3 numRot = new Vector3(0,180,0);
    Vector3 numScl = new Vector3(1f,1f,1f);

    float[] symPosXs = new float[]{-0.15f,-0.25f};
    
    // 1 2左 2右
    float[] numPosXs = new float[]{0f,0.02f,0.26f};


    
    //val wintype 0 +  1 -
    public void setScoreType(int val,int winType,int width){
        _winType = winType;
        _scoreType = val%10;
        setWidth(width);
        int shNum = _scoreType*10;
        if(winType==1){
            shNum*=-1;
            imgSrd.sprite = Resources.Load<Sprite>("img/lit_sub"+_scoreType);
        }
        else{
            imgSrd.sprite = Resources.Load<Sprite>("img/lit_add"+_scoreType);
        }
        showSpriteNum(shNum);
    }
  
    void showSpriteNum(int val,bool isMul=false){
        GameObject spObj1 = transform.Find("numPl/sp1").gameObject;
        GameObject spObj2 = transform.Find("numPl/sp2").gameObject;
        GameObject spObj3 = transform.Find("numPl/sp3").gameObject;
        SpriteRenderer srd1 = spObj1.GetComponent<SpriteRenderer>();
        SpriteRenderer srd2 = spObj2.GetComponent<SpriteRenderer>();
        SpriteRenderer srd3 = spObj3.GetComponent<SpriteRenderer>();
        Vector3 oriPos = new Vector3(0,0,-0.1f);
        if(isMul){
            spObj1.SetActive(false);
            spObj2.SetActive(true);
            spObj3.SetActive(true);
            oriPos.x = -0.15f;
            spObj2.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
            spObj2.transform.localPosition = oriPos;
            oriPos.x = 0.15f;
            spObj3.transform.localPosition = oriPos;
            srd2.sprite = Resources.Load<Sprite>("img/win_mul");
            srd3.sprite = Resources.Load<Sprite>("img/win_sc"+val);
        }
        else{
            spObj1.SetActive(true);
            oriPos.x = -0.25f;
            spObj1.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            spObj1.transform.localPosition = oriPos;
            spObj2.SetActive(true);
            oriPos.x = 0;
            spObj2.transform.localPosition = oriPos;
            spObj3.SetActive(true);
            oriPos.x = 0.25f;
            spObj3.transform.localPosition = oriPos;
            if(val>0){
                srd1.sprite = Resources.Load<Sprite>("img/win_add");
                srd2.sprite = Resources.Load<Sprite>("img/win_sc"+val/10);
                srd3.sprite = Resources.Load<Sprite>("img/win_sc"+val%10);
            }
            else{
                srd1.sprite = Resources.Load<Sprite>("img/lose_sub");
                srd2.sprite = Resources.Load<Sprite>("img/lose_sc"+(-val)/10);
                srd3.sprite = Resources.Load<Sprite>("img/lose_sc"+(-val)%10);
            }
        }
    }


    float bdOriSclX = 0.92f;
    float cylOriPosX = 0.46f;

    //min 2
    void setWidth(int val){
        if(val==0){
            return;
        }

        Vector3 newScale = transform.localScale;
        newScale.x = bdOriSclX*val;
        transform.localScale = newScale;
        
        Vector3 newPos = transform.localPosition;
        newPos.x = (val-1)*cylOriPosX;
        transform.localPosition = newPos;
        
        GameObject rGnObj = transform.parent.Find("gan2").gameObject;
        Vector3 rNewPos = rGnObj.transform.localPosition;
        rNewPos.x = cylOriPosX+2*(val-1)*cylOriPosX;
        rGnObj.transform.localPosition = rNewPos;

        for(int i=0;i<val-1;i++){
            GameObject newEffObj = GameObject.Instantiate(effObj);
            newEffObj.transform.SetParent(transform.parent);
            Vector3 pos = effObj.transform.localPosition;
            pos.x = 0.9f*(i+1);
            newEffObj.transform.localPosition = pos;
        }
        // effObj.transform.localPosition = new Vector3((val-1)*effDelPosX,0,0.24f);
        // effObj.transform.localScale = new Vector3(0.32f+(val-1)*effDelSclX,1,0.16f);

    }
    void setBoardShaderData(Vector2 tils,Vector2 offsets){
        transform.GetComponent<Renderer>().material.SetTextureScale("_MainTex",tils);
        transform.GetComponent<Renderer>().material.SetTextureOffset("_MainTex",offsets);
    }
    

    void Update(){
       
    }

  

}
