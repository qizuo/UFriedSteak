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

public class MealSurfItem : MonoBehaviour
{
    

    // 1 item2 2 item3
    public int sleMtTp = 1;
    public Material[] cookedMats;
    
    float[] surfCookAccTms;
    float[] segCookTms;
    public int curSuf = -1;
    void Awake(){
        // ballRanZ = UnityEngine.Random.Range(1,15);
        //[4,14)

        surfCookAccTms = new float[6];
        for(int i=0;i<6;i++){
            surfCookAccTms[i] = 0;
        }

        mtCols = new Color[4];
        mtCols[0] = new Color(240f/255f,105f/255f,95f/255f);
        mtCols[1] = new Color(180f/255f,95f/255f,75f/255f);
        mtCols[2] = new Color(125f/255f,75f/255f,30f/255f);
        mtCols[3] = new Color(25f/255f,25f/255f,25f/255f);
        
    }
    //总时间/4
    float cookTotalTm = 7;
    Color[] mtCols;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("meat on tri."+transform.name);
    }

    private void OnCollisionEnter(Collision other){
        // Debug.Log("meat on col."+transform.name+" ");
        cookOneSurf();
    }

    // 生肉是0
    MainForm _mainObj;
    Transform[] barTsfs;
    int _surfSum = 6;
    float _toOvTm = 0;
    public void initShowBar(MainForm mainObj,int surfCt,float[] cookTm,float toOvTm){
        _mainObj = mainObj;        
        _surfSum = surfCt;
        barTsfs = new Transform[6];
        cookTotalTm = cookTm[1]+toOvTm;
        _toOvTm = toOvTm;


        Transform barTsf1 = _mainObj.ckIfPlObj.transform.GetChild(0);
        barTsfs[0] = barTsf1;
        segCookTms = calSegCookTms(cookTm);

        setBarCkValTt(0);
        setBarCol(0);
        // for(int i=0;i<_surfSum-1;i++){
        //     GameObject newObj = GameObject.Instantiate(barTsf1.gameObject);
        //     newObj.transform.SetParent(barTsf1.parent);
        //     newObj.name = "cookPcePl"+(i+2);
        //     Vector3 pos = barTsf1.localPosition;
        //     pos.y+=-60*(i+1);
        //     newObj.transform.localPosition = pos;
        //     newObj.transform.localScale = new Vector3(1f,1f,1);
        //     barTsfs[i+1] = newObj.transform;
        // }

        // Text nameTt = barTsfs[0].Find("nameTt").GetComponent<Text>();
        // nameTt.text = "surf"+();
        // Image iconImg4 = barTsfs[i].Find("icon4").GetComponent<Image>();
        // iconImg4.color = new Color(1,1,1);
        
    }

    float[] calSegCookTms(float[] cookTm){
        if(_surfSum==6){
            return new float[3]{cookTm[0],cookTm[1],cookTotalTm};
        }
        else{
            return new float[3]{cookTm[0],cookTm[1],cookTotalTm};
        }
    }

    string[] doneNms = new string[]{"RARE","MEDIUM","WELL DONE"};
    //rare/medium/well done
    void setBarCkValTt(float val){
        // Debug.Log("setBarCkValTt.."+idx+" "+val);
        Text valTt = barTsfs[0].Find("valTt").GetComponent<Text>();
        val = Util.formatDeciNum(val,1);
        valTt.text = val+"/"+cookTotalTm;

        float rate = val/segCookTms[1];

        // Debug.Log("sb....."+val);
        if(val>segCookTms[1]+_toOvTm/2){
            _mainObj.overCookHintDeal();
        }
        else{
            _mainObj.stopCkHintDeal();
        }

        if(rate>1){
            rate=1;
        }

        Image contImg = barTsfs[0].Find("icon4").GetComponent<Image>();
        contImg.fillAmount = rate;

        //-235 185
        Transform ballTsf = barTsfs[0].Find("ball");
        Vector3 ballPos = ballTsf.localPosition;
        ballPos.x = -235+420*rate;
        ballTsf.localPosition = ballPos;

        if(checkIsFinish()){
            _mainObj.mkExtVegeDeal();            
        }
    }

    void setBarCol(int matIdx){
        if(matIdx>2){
            return;
        }
        Text steTt = barTsfs[0].Find("steTt").GetComponent<Text>();
        steTt.text = doneNms[matIdx];
        // contImg.color = mtCols[matIdx];
    }

    bool hasMvGd = false;
    void setMeatMatArr(int matIdx){
        if(_mainObj.curLevel==1&&!hasMvGd&&matIdx==1){
            hasMvGd = true;
            _mainObj.rollPanGuide(3);
        } 

        MeshRenderer mrd = transform.Find("item/meat").GetComponent<MeshRenderer>();
        Material[] res = mrd.materials;
        int idx = calPhysicalIdx();
        res[idx] = cookedMats[matIdx];
        mrd.materials = res;

        setBarCol(matIdx);
        // _mainObj.setCkInfoTt(idx,matIdx);
    }    

    int calPhysicalIdx(){
        if(_surfSum==6){
            int[] dataIdxToShape = new int[]{4,5,2,1,0,3};
            return dataIdxToShape[curSuf-1];
        }
        else{
            if(sleMtTp==2){
                return 2-curSuf;
            }
            return curSuf-1;
        }
    }

    public void cookOneSurf(){
        float minDis = 100;
        int res = 1;
        for(int i=1;i<=_surfSum;i++){
            Transform surObj = transform.Find("item/surf"+i);
            if(surObj.position.y<minDis){
                minDis = surObj.position.y;
                res = i;    
            }
        }
        curSuf = res;      
        // Debug.Log("cook surf."+curSuf);
    }

    void chooseSurfMat(){
        float cookTm = surfCookAccTms[curSuf-1];
        for(int i=0;i<3;i++){
            if(cookTm<segCookTms[i]){
                // Debug.Log("cc11."+i);
                setMeatMatArr(i);                   
                break;
            }
        }
        if(cookTm>=segCookTms[2]){
            // Debug.Log("cc11.3");
            setMeatMatArr(3);
        }
    }

    bool checkIsFinish(){
        int finCt = 0;
        for(int i=0;i<_surfSum;i++){
            if(surfCookAccTms[i]>=segCookTms[0]&&surfCookAccTms[i]<segCookTms[2]){
                finCt++;
            }
        }
        if(finCt==_surfSum){
            return true;    
        }
        return false;
    }

    void Update(){
        if(_mainObj.isGaming){
            // Debug.Log("uuu222.."+curSuf+"");
            if(curSuf>0){
                if(surfCookAccTms[curSuf-1]<cookTotalTm){
                    surfCookAccTms[curSuf-1]+=Time.deltaTime;
                    setBarCkValTt(surfCookAccTms[curSuf-1]);
                    chooseSurfMat();
                }
                else{
                    setBarCkValTt(segCookTms[2]);
                }
                // Debug.Log("uuu333..");
            }
        }
    }
}
