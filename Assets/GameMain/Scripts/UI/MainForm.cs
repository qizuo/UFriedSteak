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
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
// using com.adjust.sdk;
using UnityEngine.EventSystems;
// using Voodoo.Sauce.Internal;
// using Adjust;
public class MainForm : UGuiForm
{
    public Camera mainCamera;
    
    //主角
    GameObject mnRoleObj;
    
    public GameObject touchPadObj;
    

    //摄像机初始位置
    float oriCmPosX = 3;
    float curCmPosZ = -20.3f;
    float oriCmPosZ = -20.3f;
    
    float curRolePosZ = -16.5f;
    float oriRolePosZ = -16.5f;

    float hindStaPosZ = 7f;


    // int score,int mul,int color
    public static Action<int,int,int> hitHinderAc;

    
    GameObject tipsPlObj;

    public static Color[] colorMap;

    public static int[][] imgIntArr = null;
    public static int[] imgArrBdr = null;

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // var param = userData as Params;
    }


    GameObject egyPlObj;
    float curJumpEgyVal = 0;

    public static Action<int> onHitWayBdAc;

    Sequence hitBdSeq=null;


    //-1 左 0 中 1 右
    float bdPosType = 0;
    // -1 不能向左 1 不能向右
    int forbidDir = 0;
    void onHitWayBdCb(int posType){
        bdPosType = posType;
        Vector3 curPos = mnRoleObj.transform.localPosition;
        if(curPos.x<0){
            forbidDir=1;
        }
        else{
            forbidDir=-1;
        }
        if(hitBdSeq!=null){
            hitBdSeq.Kill(true);
            hitBdSeq=null;
        }
        hitBdSeq = DOTween.Sequence();
        hitBdSeq.AppendInterval(0.05f);
        hitBdSeq.AppendCallback(delegate(){
            forbidDir=0;
            hitBdSeq.Kill(true);
            hitBdSeq=null;
        });
    }

    ShoeItem sitmObj;
    GameEntry gmEyObj;

    //0 0.1 0.2
    float horMoveSpd = 0;
    float leftMaxDis = -1.25f;
    float rightMaxDis = 1.25f;    
    float curWyCtPos = 0f;

    float[] horMvSelcSpd = new float[]{0.02f,0.03f,0.04f};

    float loseLastTm = 6;
    float loseAccTm = 6;
    bool isLoseCtDn = false;

    void gameOver(){
        // Debug.Log("c..."+curAgrVal);
        sitmObj.playCry();
        moveSpdZ = 0;
        isLoseCtDn = true;
        isRunning = false;
        canHorMove = false;
        horMoveSpd = 0;
        loseAccTm = loseLastTm;
        finImgObj.SetActive(false);
        wayPceObj.SetActive(false);
        finPcePlObj.SetActive(false);
        loseGtTt.text = "+0";
        Invoke("showLosePl",2f);
    }

    void showLosePl(){
        // Debug.Log("gam over");
        // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,"sr01");
        losePlObj.SetActive(true);
        // levOnLoseTt.text = "level:"+curLevel;
    }

    public void gameWin(){
        Debug.Log("call.gamew...");
        // playGmSound(2,"setl_win");
        // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,"sr01");  
        // levOnMainTt.text = "level:"+curLevel;
        // int norWin = calFinWinMy();
        // norGnTt.text = "+"+norWin;
        // startWinPtrAc();
        gnMoreBtn.enabled = true;
        norGnBtn.enabled = true;
        winPlObj.SetActive(true);     
        Invoke("playWinExpEff",0.2f);
    }

    int[] mapRanHash = new int[5];
    int mapRanIdx = 0;

    int getNewLevelIdx(){
        int newlev = UnityEngine.Random.Range(1,31);
        while(checkInLevHash(newlev)){
            newlev = UnityEngine.Random.Range(1,31);
        }
        mapRanHash[mapRanIdx]=newlev;
        mapRanIdx++;
        if(mapRanIdx==5){
            mapRanIdx=0;
        }
        return newlev;
    }

    bool checkInLevHash(int lev){
        for(int i=0;i<5;i++){
            if(mapRanHash[i]==lev){
                return true;
            }
        }
        return false;
    }
    bool isOverMaxLev = false;
    void toNextLevel(){
        if(!isOverMaxLev){
            curLevel++;
            if(curLevel>30){
                curLevel=getNewLevelIdx();
                isOverMaxLev = true;
            }
        }
        else{
            curLevel=getNewLevelIdx();
        }
    }

    int calFinWinMy(float mul=1){
        int baseM = 100;
        // float mul = (getFinPow()+1)*0.1f+1;
        // Debug.Log("calf.."+baseM+" "+mul);
        return Mathf.FloorToInt(baseM*mul);
    }


    int finGainMyBase = 50;
    int curWinMyCt = 0;    
    bool canHorMove = true;

    void doMnHorMv(int dir){
        // if(!canHorMove){
        //     return;
        // }
        // // Debug.Log("doh..."+dir);
        // if(!isRunning){
        //     isRunning=true;
        //     moveSpdZ = cMoveSpdZ;
        //     // finImgObj.SetActive(true);
        //     wayPceObj.SetActive(true);
        //     wpceBarSpd = 3f*moveSpdZ*moveSpdRt/(finAddMapNum+1);
        //     if(finSeq!=null){
        //         finSeq.Kill(true);
        //         finSeq = null;
        //     }
        //     startPlObj.SetActive(false);
        // }
        // else{
        //     horMoveSpd = dir*horMvSelcSpd[1];
        // }
        // Debug.Log("domv..");
    }
    int pclCchCt = 1000;
    int plcIdx=0;

    // int curRedCt=0;

    void hitRedCb(){
        // curRedCt++;
        // redScoreTt.text = "红箱子数:"+curRedCt;
    }
    Button pushBtn;
    GameObject stairsObj;
    GameObject psBtnLtObj;

    
    int finFlyBoxCt = 1500;

    //ball -195 170
    //
    void setWayPceVal(float deltaVal=0){
        curWPceVal = curWPceVal+deltaVal;
        // Debug.Log(curWPceVal+" "+deltaVal);
        curWPceVal = Mathf.Clamp(curWPceVal,0,100);
        Vector3 blPos = wayPceBallObj.transform.localPosition;
        blPos.x = -198+376*curWPceVal/100;
        wayPceBallObj.transform.localPosition = blPos;

        RectTransform barRtf = wayPceBarObj.GetComponent<RectTransform>();
        barRtf.sizeDelta = new Vector2(30+360*curWPceVal/100,39);
    }
    float curWPceVal = 0;
    GameObject wayPceBallObj;
    GameObject wayPceBarObj;
    GameObject finPcePlObj;
    public GameObject uiPt;
    float wpceBarSpd = 1f;

    GameObject fingObj;    
    Sequence finSeq = null;

    float colAlMin = 200f;
    float colAlMax = 255f;
    float curAlVal = 255f;
    float colFdSpd = -3;
    bool isStaBrth = false;

    void staBrthAc(){
        startPlObj.SetActive(true);
        fingObj = startPlObj.transform.Find("slidePl/Text").gameObject;
        // Vector3 newPos = fingObj.transform.localPosition;
        // newPos.x = -160;
        // fingObj.transform.localPosition = newPos;
        // fingObj.transform.dofade
        isStaBrth = true;
        // finSeq = DOTween.Sequence();
        // finSeq.Append(fingObj.transform.DOLocalMoveX(260,1.5f).SetEase(Ease.OutSine));
        // finSeq.Append(fingObj.transform.DOLocalMoveX(-160,1.5f).SetEase(Ease.OutSine));
        // finSeq.SetLoops(-1);
    }
    GameObject startPlObj;
    GameObject menuPlObj;
    GameObject winPlObj;
    GameObject losePlObj;
    Button againBtn;
    Text gnMoreTt;
    Text norGnTt;

    Button norGnBtn;
    Button gnMoreBtn;
    GameObject winCtPlObj;

    Text levOnWyTt;
    public int curLevel = 1;
    Text levOnMainTt;
    Text levOnWinTt;
    Text levOnLoseTt;
    
    GameObject hpPlObj;

    Camera uiCme;

    GameObject finLnObj;

    Text loseGtTt;
    GameObject acFsPtObj;
    GameObject acFsFgrObj;
    Text hpUpTt;
    GameObject mlBtnPl;
    GameObject ndColPl;
    GameObject levPlObj;

    Button hintBtn;

    GameObject bgObj;
    void findItem(){
        uiCme = uiPt.transform.Find("Camera").GetComponent<Camera>();
        extCkPlObj = uiPt.transform.Find("extCkPl").gameObject; 
        ckIfPlObj = uiPt.transform.Find("ckIfPl").gameObject;

        upNdPlObj = uiPt.transform.Find("upNdPl").gameObject;

        guestNdImg = uiPt.transform.Find("needPl/contImg").GetComponent<Image>();
        pepPlObj = transform.Find("cookPl/pepPl").gameObject;
        eatRmObj = transform.Find("eatRm").gameObject;
        makeRmObj = transform.Find("makeRm").gameObject;

        makeBgObj = transform.Find("bg").gameObject;

        touchPlBtn = uiPt.transform.Find("touchPad").GetComponent<Button>();

        procePlObj = uiPt.transform.Find("procePl").gameObject;

        nextBtn = uiPt.transform.Find("nextBtn").GetComponent<Button>();
        needPlObj = uiPt.transform.Find("needPl").gameObject; 
        sauceObj = transform.Find("cookPl/sauceObj").gameObject;

        winPtrPlObj = uiPt.transform.Find("winPl/ptrPl").gameObject;
        winPlObj = uiPt.transform.Find("winPl").gameObject;
        winEffPlObj = winPlObj.transform.Find("effPl").gameObject;
        levOnWinTt = winPlObj.transform.Find("upPl/Text").GetComponent<Text>();
        losePlObj = uiPt.transform.Find("losePl").gameObject;
        levOnLoseTt = losePlObj.transform.Find("upPl/Text").GetComponent<Text>();
        gnMoreTt = winPlObj.transform.Find("moreBtn/Text2").GetComponent<Text>();
        norGnTt = winPlObj.transform.Find("ctPl/Text").GetComponent<Text>();
        loseGtTt = losePlObj.transform.Find("ctPl/Text").GetComponent<Text>();

        staGmBtn = uiPt.transform.Find("startPl/staBtn").GetComponent<Button>();

        bgObj = transform.Find("bg").gameObject;
        
        levPlObj = uiPt.transform.Find("levPl").gameObject;

        hintBtn = uiPt.transform.Find("hintBtn").GetComponent<Button>();

        gnMoreBtn = winPlObj.transform.Find("moreBtn").GetComponent<Button>();
        norGnBtn = winPlObj.transform.Find("getPl/norBtn").GetComponent<Button>();

        guidePl = uiPt.transform.Find("gdPl").gameObject;
        gdMvObj = guidePl.transform.Find("mvPl").gameObject;
        gdMvAni = gdMvObj.GetComponent<Animator>();
        
        guideTt = guidePl.transform.Find("gdTt").GetComponent<Text>();
        panMvGdImg = guidePl.transform.Find("icon").GetComponent<Image>();
        panRotGdImg = guidePl.transform.Find("arrIcn").GetComponent<Image>();

        // finLnObj = transform.Find("finLn").gameObject;

        winCtPlObj = winPlObj.transform.Find("ctPl").gameObject;

        menuPlObj = uiPt.transform.Find("menuPl").gameObject;
        setBtn = menuPlObj.transform.Find("setBtn").GetComponent<Button>();
        startPlObj = uiPt.transform.Find("startPl").gameObject;

        setPlObj = menuPlObj.transform.Find("setMask/setPl").gameObject;
        cloSetPlBtn = setPlObj.transform.Find("cloBtn").GetComponent<Button>();

        setSdBtn = setPlObj.transform.Find("sdBtn").GetComponent<Button>();
        setShkBtn = setPlObj.transform.Find("skeBtn").GetComponent<Button>();

        staGmRect = startPlObj.transform.Find("bg").GetComponent<Button>();

        //前进速度
        tipsPlObj = uiPt.transform.Find("tipsPanel").gameObject;
        // scoreTt = uiPt.transform.Find("scPl/scoreTt").GetComponent<Text>();

        againBtn = uiPt.transform.Find("losePl/rstBtn").GetComponent<Button>();
        
        prtyPlObj = uiPt.transform.Find("menuPl/prtyPl").gameObject;
        ppMoyTt = uiPt.transform.Find("menuPl/prtyPl/text").GetComponent<Text>();
        ppMoyTt.text = curPpMoyVal+"";
        upPpObj = ppMoyTt.transform.parent.Find("icon").gameObject;
    }

    GameObject prtyPlObj;

    public void setCkInfoTt(int idx,int cookIdx){
        Text infoTt = ckIfPlObj.transform.Find("infoTt"+(idx+1)).GetComponent<Text>();
        infoTt.text = "surf"+(idx+1)+":"+(cookIdx+1);
    }

    float curAgrVal =30f;
    void setCurAgrVal(){
        curAgrVal = Mathf.Clamp(curAgrVal,0,100);
        GameObject upObj = hpPlObj.transform.Find("up").gameObject;
        // RectTransform barRtf = upObj.GetComponent<RectTransform>();
        // barRtf.sizeDelta = new Vector2(95*curAgrVal/100,20);
        Image upImg =upObj.GetComponent<Image>();
        upImg.fillAmount = curAgrVal/100f;
    }


    float gusWtCd = 4f;
    float gusHpCd = 2f;
    float gusStaTm = 1.5f;
    float[] gusEmjAccTms;

    int curPpMoyVal = 100;
    int ppMoyAddVal = 10;
    public static Action<Transform> onHitPpMoyAc;
    GameObject upPpObj;
    void onHitPpMoyCb(Transform coin){
        if(isSetShk){
            Handheld.Vibrate();
        }
        // Debug.Log("onh.."+coin.GetHashCode()+" "+cnObjCt);
        int curCnIdx = 0;
        for(int i=0;i<cnObjCt;i++){
            // Debug.Log(i+" "+cnObjs[i].transform.GetHashCode()+" "+Util.GetTimeStamp());
            if(cnObjs[i].transform.GetHashCode()==coin.GetHashCode()){
                float posX = cnObjOriPoss[i].y;
                float posZ = cnObjOriPoss[i].x;
                int mapData = hindDataArr[(int)posZ][(int)posX];
                // Debug.Log(i+" "+posZ+" "+posX+" "+mapData+" "+Util.GetTimeStamp());
                if(mapData<99){
                    hindDataArr[(int)posZ][(int)posX]=10;
                }
                else{
                    hindDataArr[(int)posZ][(int)posX]-=2;                        
                }
                curCnIdx = i;
            }
        }
        GameObject cnTemp = cnObjs[curCnIdx];
        cnObjs[curCnIdx]=cnObjs[cnObjCt-1];
        cnObjs[cnObjCt-1] = cnTemp;

        Vector2 posTp = cnObjOriPoss[curCnIdx];
        cnObjOriPoss[curCnIdx] = cnObjOriPoss[cnObjCt-1];
        cnObjOriPoss[cnObjCt-1] = posTp;

        GameObject.Destroy(cnObjs[cnObjCt-1]);        
        cnObjs[cnObjCt-1]=null;
        cnObjCt--;


        playGmSound(2,"get_coin");
        // curWinMyCt++;

        Vector3 toPos = upPpObj.transform.position;
        Vector3 newPos = uiCme.WorldToScreenPoint(coin.position);
        // newPos.y+=100;
        newPos.x-=Screen.width/2-100;
        newPos.y-=Screen.height/2+500;

        flyPpMoy(uiPt,newPos,toPos,0.7f,delegate(){
            curPpMoyVal+=ppMoyAddVal;
            ppMoyTt.text = curPpMoyVal+"";
        });
    }
    Text ppMoyTt;

    void winPlBtnCb(){
        //广告获得
        gnMoreBtn.onClick.RemoveAllListeners();
        gnMoreBtn.onClick.AddListener(delegate(){
            isWinPtrMv = false;
            int curIdx = calCurPtrIdx();
            int curGnVal = calFinWinMy(finPtrMuls[curIdx]); 
            gnMoreBtn.enabled = false;
            norGnBtn.enabled = false;

            Sequence wtSeq = DOTween.Sequence();
            wtSeq.AppendInterval(3f);
            wtSeq.AppendCallback(delegate(){
                GameObject ppObj = gnMoreBtn.transform.Find("icon").gameObject;
                Vector3 toPos = upPpObj.transform.position;
                flyPpMoy(uiPt,ppObj.transform.position,toPos,0.7f,delegate(){
                    curPpMoyVal+=curGnVal;
                    ppMoyTt.text = curPpMoyVal+"";
                    Invoke("resetBtnCb",1.5f);
                });
                toNextLevel();
            });
            wtSeq.SetAutoKill();
            
        });

    }


    
    public static Action<Transform> hitCoinAc;

    void flyPpMoy(GameObject pt,Vector3 fromPos,Vector3 toPos,float flyTm,TweenCallback cb=null){
        GameObject newPpObj = GameObject.Instantiate(upPpObj);
        newPpObj.transform.SetParent(pt.transform);
        newPpObj.transform.localScale = new Vector3(1f,1f,1f);
        newPpObj.transform.localPosition = fromPos;

        Sequence flySeq = DOTween.Sequence();
        flySeq.Append(newPpObj.transform.DOMove(toPos,flyTm).SetEase(Ease.OutSine));
        flySeq.AppendCallback(delegate(){
            GameObject.Destroy(newPpObj);
            if(cb!=null){
                cb();
            }
        });
        flySeq.SetAutoKill();
    }

    //10个等级 越来越深

    //0 白 1 红 2 橙 3 黄 4 绿 5 青 6 蓝 7 紫 8 棕 9 粉 10 黑

    // 21 红7 22 蓝8 23 蓝3 24 棕5 25 绿7 26 粉6 27 黄3 28 绿4 29 绿8 30 棕8 
    // 31 棕 32 棕 33 绿 34 粉 35 红 36 红 
    void initColorMap(){
        colorMap = new Color[colorTotCt];
        colorMap[0] = new Color(200f/255f,200f/255f,200f/255f);
        colorMap[1] = new Color(255f/255f,0f,16f/255f);
        colorMap[2] = new Color(240f/255f,140/255f,0f);
        colorMap[3] = new Color(240f/255f,235f,0f);
        colorMap[4] = new Color(0f,240f/255f,0);
        colorMap[5] = new Color(0f,240f/255f,230f/255f);
        colorMap[6] = new Color(0f/255f,80f,240f/255f);
        colorMap[7] = new Color(180f/255f,0f,240f/255f);
        colorMap[8] = new Color(145f/255f,90f/255f,0f/255f);
        colorMap[9] = new Color(215f/255f,120f/255f,160f/255f);
        colorMap[10] = new Color(0f,0f,0f);
        
        colorMap[21] = new Color(255f/255f,0f/255f,16f/255f);
        colorMap[22] = new Color(63f/255f,148f/255f,241f/255f);
        colorMap[23] = new Color(168/255f,244f/255f,255f/255f);
        colorMap[24] = new Color(225f/255f,139f/255f,56f/255f);
        colorMap[25] = new Color(0f/255f,153f/255f,10f/255f);
        colorMap[26] = new Color(255/255f,130f/255f,222f/255f);
        colorMap[27] = new Color(255f/255f,218f/255f,88f/255f);
        colorMap[28] = new Color(106f/255f,211f/255f,119f/255f);
        colorMap[29] = new Color(0f/255f,139f/255f,9f/255f);
        colorMap[30] = new Color(148f/255f,92f/255f,54f/255f);

        colorMap[31] = new Color(217f/255f,137f/255f,58f/255f);
        colorMap[32] = new Color(246/255f,191f/255f,90f/255f);
        colorMap[33] = new Color(144f/255f,249f/255f,114f/255f);
        colorMap[34] = new Color(255f/255f,198f/255f,195f/255f);
        colorMap[35] = new Color(255f/255f,96f/255f,85f/255f);
        colorMap[36] = new Color(168f/255f,15f/255f,0f/255f);
    }

    public static int colorTotCt =40;
    
    // int covertColIdx(int idx){
    //     if(){

    //     }
    // }
    Button setBtn;

    Vector3 oriFinImgPos;
    GameObject boomPtObj;
    
    Color[] barColors = new Color[]{new Color(17/255f,236/255f,5/255f),
                    new Color(98/255f,159/255f,6/255f),new Color(171/255f,89/255f,7/255f),
                    new Color(224/255f,35/255f,6/255f),new Color(255/255f,7/255f,7/255f),};


    static AudioSource bgmAuSce;
    static AudioSource soundAuSce;
    void initSound(){
        bgmAuSce = transform.GetComponent<AudioSource>();
        soundAuSce = uiPt.GetComponent<AudioSource>();
    }

    // 1 bgm 2 sound
    public static void playGmSound(int type,string name){
        if(!isSetSound){
            return;
        }
        string path = "Sound/"+name;
        // Debug.Log("plssss.."+path);
        AudioClip ldClip = Resources.Load<AudioClip>(path);
        AudioSource playAuSce;
        if(type==1){
            playAuSce = bgmAuSce;
        }
        else{
            playAuSce = soundAuSce;
        }
        playAuSce.clip = ldClip;
        playAuSce.PlayOneShot(ldClip);
    }
    // 1 bgm 2 sound
    public static void stopGmSound(int type){
        // Debug.Log("plssss.."+path);
        // AudioSource playAuSce;
        // if(type==1){
        //     playAuSce = bgmAuSce;
        // }
        // else{
        //     playAuSce = soundAuSce;
        // }
        // playAuSce.Stop();
    }


    Button testBtn1;
    Button testBtn2;

    GameObject tuTbeObj;
    GameObject finRmPt;


    int[] toAcScoMap = new int[8]{0,7,6,5,4,3,2,1};

    void setFinItemsPos(){
        float posZ = 48+unitMapLen*finAddMapNum;
        tuTbeObj.transform.parent.localPosition = new Vector3(-0.8f,1.1f,posZ);
        tuTbeObj.transform.parent.eulerAngles = new Vector3(85f,162f,353f);
        tuTbeObj.transform.parent.localScale = new Vector3(0.1f,0.1f,0.1f);

        Vector3 lPos = finLnObj.transform.localPosition;
        lPos.z = 41.5f+finAddMapNum*unitMapLen;
        finLnObj.transform.localPosition = lPos;

        posZ = 55.4f+unitMapLen*finAddMapNum;
        finRmPt.transform.localPosition = new Vector3(0,-0.3f,posZ);
        
    }


    void onClickTnTbe(){
        // touchPlBtn.enabled = false;
        // if(tnGdFigSeq!=null){
        //     tnGdFigSeq.Kill(true);
        //     tnGdFigSeq=null;
        // } 
        // tuTbeRes = curRoomType+1;
        // if(UnityEngine.Random.Range(0,2)==0){
        //     tuTbeRes+=4;
        // }
        // turnTbeAction();
    }

    GameObject acFsObj;
    Image acFsImg;

    Button touchPlBtn;
    int tuTbeRes = 1;

    void addWayItem(GameObject item){
        while(wayItems[wayItemsIdx]!=null){
            wayItemsIdx++;
            if(wayItemsIdx==hItemCt*4*2){
                wayItemsIdx=0;
            }
        }
        wayItems[wayItemsIdx] = item;
    }

    GameObject[] wayItems = null;
    int wayItemsIdx = 0;
    public static int[][] angerConfig = null;
    int[][] tnTbesConfig = null;
    public GameObject guidePl;

    void fitUI(){
        if(Screen.height==1000&&Screen.width==800){
            extCkPlYs = new Vector2(-690,-1080);   
        }
        else{
            extCkPlYs = new Vector2(-790,-1080);
        }
    }


    Sequence gdFigSeq;
    Sequence gdCirSeq;
    void staGuideFing(){
        stopGuideFing();

        guidePl.SetActive(true);

        GameObject figrObj = guidePl.transform.Find("figr").gameObject;
        gdFigSeq = DOTween.Sequence();
        gdFigSeq.Append(figrObj.transform.DOScale(new Vector3(1.2f,1.2f,1.2f),1f));
        gdFigSeq.Append(figrObj.transform.DOScale(new Vector3(1,1,1),1f));
        gdFigSeq.SetLoops(-1);

        GameObject crleObj = guidePl.transform.Find("bg").gameObject;
        Image criImg = crleObj.GetComponent<Image>();
        gdCirSeq = DOTween.Sequence();
        gdCirSeq.Append(crleObj.transform.DOScale(new Vector3(0.7f,0.7f,0.7f),1f));
        gdCirSeq.Join(criImg.DOFade(0f,0.5f));
        gdCirSeq.AppendCallback(delegate(){
            crleObj.transform.localScale = new Vector3(0.5f,0.5f,1f);
            criImg.DOFade(1,-1);
        });
        gdCirSeq.SetLoops(-1);
    }


    void stopGuideFing(){
        // guidePl.SetActive(false);
        // if(gdFigSeq!=null){
        //     gdFigSeq.Kill(true);
        // }
        // if(gdCirSeq!=null){
        //     gdCirSeq.Kill(true);
        // }
        
        // GameObject figrObj = guidePl.transform.Find("figr").gameObject;
        // figrObj.transform.localScale = new Vector3(1,1,1);

        // GameObject crleObj = guidePl.transform.Find("bg").gameObject;
        // Image criImg = crleObj.GetComponent<Image>();
        // crleObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        // criImg.DOFade(1,-1);
    }




    // 位置 半径    
    Vector2[] calShapePosDis(){
        Vector2[] res = new Vector2[2];
        int posXMin = 9;
        int posXMax = 0;
        int posZMax = 0;
        int posZMin = 25;

        for(int i=0;i<hItemCt;i++){
            for(int j=0;j<runWayWd;j++){
                if(hindDataArr[i][j]>0){
                    if(i<posZMin){
                        posZMin = i;
                    }
                    if(i>posZMax){
                        posZMax = i;
                    }
                    if(j<posXMin){
                        posXMin = j;
                    }
                    if(j>posXMax){
                        posXMax = j;
                    }
                }
            }
        }

        int radX = (posXMax-posXMin)/2;
        int radZ = (posZMax-posZMin)/2;

        int ctrX = posXMin+radX;
        int ctrZ = posZMin+radZ;

        res[0] = new Vector2(ctrX,ctrZ);
        res[1] = new Vector2(radX,radZ);

        return res;
    }


    Vector3 mnCmeCtrPos = new Vector3(0.4f,45,-48f);
    int[][] smRoomConfig;
    Button testBtn;
    Button staGmRect;

    float adjCmeUtX = 2.5f;
    float adjCmeUtZ = 2.3f;
    void adjMnCmeLook(Vector2 spCtrPos1,int spRadisX,int spRadisZ){
        // Debug.Log(spCtrPos1);
        // Debug.Log(spRadisX+" "+spRadisZ);
        Vector2 spCtrPos = new Vector2(spCtrPos1.x+1,spCtrPos1.y+1);
        //横 竖
        // spCtrPos=new Vector2(8,4);
        // int spRadisX = 3;
        // int spRadisZ = 3;
        
        float adjZ = (26-2*spRadisZ)/2*adjCmeUtZ;
        //先底隆
        // Debug.Log(adjCmeUtZ*(26-(spCtrPos.y+spRadisZ)));
        mnCmeCtrPos.z+=adjCmeUtZ*(26-(spCtrPos.y+spRadisZ))+15;
        mnCmeCtrPos.z-=adjZ-20f;
        
        mnCmeCtrPos.x-=(5-spCtrPos.x)*adjCmeUtX;

        if(spRadisZ<=4&&spRadisX<=3){
            mnCmeCtrPos.y-=2;
            // mnCmeCtrPos.z+=8;
        }

        mainCamera.transform.localPosition = mnCmeCtrPos;
        mnCmeCtrPos = new Vector3(0.4f,45,-48f);
    }

    // DragDirHelp panddh;
    JoyStickHelp panJsh;
    DragDirHelp panDdh;
    Vector3 oriFingPos = new Vector3(0,0,0);
    GameObject gdMvObj=null;
    void Awake(){
        findItem();
        fitUI();
        setCurStepPt(1);

        panJsh = uiPt.transform.Find("touchPad").GetComponent<JoyStickHelp>();
        panJsh.enabled =false;

        panDdh = uiPt.transform.Find("touchPad").GetComponent<DragDirHelp>();
        panDdh._mainObj = this;
        panDdh.directAc+=movePanDeal;
        panDdh.enabled = false;

        
        staGmBtn.onClick.RemoveAllListeners();
        staGmBtn.onClick.AddListener(delegate(){
            startPlObj.SetActive(false);
            staGmBtn.enabled = false;
            startCookDeal();
            // createExtDeal();
            // extCkPlObj.SetActive(true);
        });

        setBtn.onClick.RemoveAllListeners();
        setBtn.onClick.AddListener(delegate(){
            // rollPanGuide(2);
        });

        hintBtn.onClick.RemoveAllListeners();
        hintBtn.onClick.AddListener(delegate(){
            // initUI();
        });
        //普通获得
        norGnBtn.onClick.RemoveAllListeners();
        norGnBtn.onClick.AddListener(delegate(){
            // initUI();
            // norGnBtn.enabled = false;
            // gnMoreBtn.enabled = false;
            // // Debug.Log("norggggg..");
            // GameObject ppObj = winCtPlObj.transform.Find("icon1").gameObject;
            // Vector3 toPos = upPpObj.transform.position;
            // int gainMy = calFinWinMy();
            // flyPpMoy(uiPt,ppObj.transform.position,toPos,0.7f,delegate(){
            //     curPpMoyVal+=gainMy;
            //     ppMoyTt.text = curPpMoyVal+"";
            //     Invoke("resetBtnCb",1.5f);
            // });
            // toNextLevel();
        });


        curLevel = DataManage.GetInt("curLevel");
        if(curLevel<=0){
            curLevel=1;
            DataManage.SetInt("curLevel",1);
        }

        gdMvAni.enabled =false;
        
        initUI(true);

    }

    //是否 要烤糊 提醒
    bool isCrHint = false;
    Sequence orCkSeq = null;
    public void overCookHintDeal(){
        Debug.Log("over.."+isCrHint);
        if(isCrHint){
            return;
        }
        isCrHint = true;

        orCkSeq = DOTween.Sequence();
        orCkSeq.Append(ckIfPlObj.transform.DOScale(new Vector3(1.2f,1.2f,1.2f),0.8f));
        orCkSeq.Append(ckIfPlObj.transform.DOScale(new Vector3(1f,1f,1f),0.8f));
        orCkSeq.SetLoops(-1);
    }
    
    public void stopCkHintDeal(){
        if(!isCrHint){
            return;
        }
        isCrHint = false;
        if(orCkSeq!=null){
            orCkSeq.Kill(true);
            orCkSeq = null;
        }  
        ckIfPlObj.transform.localScale = new Vector3(1,1,1); 
    }

    Animator gdMvAni;
    Text guideTt;
    Image panMvGdImg;
    Image panRotGdImg;
    void movePanGuide(){
        guideTt.text = "SLIP AND HOLD TO MOVE";
        panMvGdImg.gameObject.SetActive(true);
        panRotGdImg.gameObject.SetActive(false);
        guidePl.SetActive(true);
        gdMvAni.enabled = true;
    }

    void moveExtGuide(){
        guideTt.text = "DRAG TO PUT ITEM";
        panMvGdImg.gameObject.SetActive(false);
        panRotGdImg.gameObject.SetActive(false);
        guidePl.SetActive(true);

        if(makeGdSeq!=null){
            makeGdSeq.Kill(true);
            makeGdSeq = null;
        }

        gdMvObj.transform.localPosition = new Vector3(-345,-370,0);
        makeGdSeq = DOTween.Sequence();
        makeGdSeq.Append(gdMvObj.transform.DOLocalMove(new Vector3(0,210,0),2f));
        makeGdSeq.AppendCallback(delegate(){
            gdMvObj.transform.localPosition = new Vector3(-345,-370,0);
        });
        makeGdSeq.SetLoops(-1);
    }

    Sequence makeGdSeq = null;
    // 1 left 2 right 3 up
    public void rollPanGuide(int type){
        isGaming = false;
        guideTt.text = "SLIP ONCE FAST TO ROLL";
        guidePl.SetActive(true);
        panMvGdImg.gameObject.SetActive(false);
        panRotGdImg.gameObject.SetActive(true);

        if(makeGdSeq!=null){
            makeGdSeq.Kill(true);
            makeGdSeq = null;
        }
        Vector3 dirArr;
        Vector3 endPos;
        if(type==1){
            dirArr = new Vector3(0,0,-45);
            endPos = new Vector3(-300,300,0);
        }
        else if(type==2){
            dirArr = new Vector3(0,0,-135);
            endPos = new Vector3(300,300,0);
        }
        else{
            dirArr = new Vector3(0,0,-90);
            endPos = new Vector3(0,450,0);
        }
        gdMvObj.transform.localPosition = oriFingPos;
        panRotGdImg.transform.eulerAngles = dirArr;
        makeGdSeq = DOTween.Sequence();
        makeGdSeq.Append(gdMvObj.transform.DOLocalMove(endPos,2f));
        makeGdSeq.AppendCallback(delegate(){
            gdMvObj.transform.localPosition = new Vector3(0,0,0);
        });
        makeGdSeq.SetLoops(-1);
    }

    public void hidePanGuide(){
        isGaming = true;
        guidePl.SetActive(false);
        gdMvAni.enabled = false;
        if(makeGdSeq!=null){
            makeGdSeq.Kill(true);
            makeGdSeq = null;
        }
    }

    public Vector2 idleCenter = new Vector2(0,0);
    public GameObject ckIfPlObj;
    //是否在煎牛排
    public bool isGaming = true;
    public bool isPanIdle = false;
    public float meatFlyHt = 1.5f;

    // 1 up 2 down 3 front 4 behd 5 left 6 right 
    void throwMeatDeal(float power){
        float endHt = meatFlyHt*power;
        if(endHt<=0.2f){
            return;
        }
        isPanHorMv = true;
        meatSurfItem.curSuf = -1;
        Sequence upSeq = DOTween.Sequence();
        upSeq.Append(curMtObj.transform.DOLocalMoveY(endHt,0.5f).SetEase(Ease.OutSine));
        upSeq.Append(curMtObj.transform.DOLocalMoveY(0.2f,0.5f).SetEase(Ease.InSine));
        upSeq.AppendCallback(delegate(){
            Debug.Log("r1111...");
            isMtLkPan = true;
            mtLkAccTm=0;
            isPanHorMv = false;
            // msf.cookOneSurf();
            // int idx = msf.calBottomSurfIdx();
            // Debug.Log(idx);
        });
        upSeq.SetAutoKill();
    }

    //0.5s 卡死肉

    bool isMtLkPan = false;
    float mtLkAccTm = 0;
    float mtLkInTm = 0.1f;

    void staticMeatDeal(){
        Rigidbody rb = curMtObj.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,0);
    }

    // 1 up 2 down 3 front 4 behd 5 left 6 right 

    // x y z/1 2 3
    int[] calFormatAxis(int dnIdx){
        //-1
        if(dnIdx==1){
            return new int[]{0,-1,180};
        }
        else if(dnIdx==2){
            return new int[]{0,-1,0};
        }
        else if(dnIdx==3){
            return new int[]{-90,-1,0};
        }
        else if(dnIdx==4){
            return new int[]{90,-1,0};
        }
        else if(dnIdx==5){
            return new int[]{0,-1,90};
        }
        else{
            return new int[]{0,-1,-90};
        }
    }
    

    PlayerMoveController panPmc;
    GameObject curPanObj=null;
    GameObject curMtObj = null;
    int curPtBtnCt = 0;
    Vector3 oriPanPos = new Vector3(0,0,0);
    Vector3 oriPanRot = new Vector3(0,-90,0);
    MealSurfItem meatSurfItem;
    // 肉块 scl new Vector3(0.5f,0.5f,0.5f);
    // 肉排 new Vector3(0.8f,0.8f,0.8f);
    // 1 肉块 2 肉排
    int[] mtSurfCtArr = new int[]{6,2,2};
    Vector3[] mtSclArr = new Vector3[]{new Vector3(0.6f,0.6f,0.6f),new Vector3(0.8f,0.8f,0.8f),new Vector3(0.8f,0.8f,0.8f)};
    void createPanCook(){
        initCamera(2);

        upNdPlObj.SetActive(true);

        Transform hitItemPt = transform.Find("cookPl");
        if(curPanObj!=null){
            GameObject.Destroy(curPanObj);
        }

        GameObject oriItem = (GameObject)Resources.Load(AssetUtility.getFloorBasePrefab(5)); 
        GameObject boxItem = GameObject.Instantiate(oriItem);
        boxItem.transform.SetParent(hitItemPt);
        boxItem.transform.localPosition = oriPanPos;
        boxItem.transform.eulerAngles = oriPanRot;
        boxItem.transform.localScale = new Vector3(6,6,6);
        curPanObj = boxItem;
        initJoyStick();

        // if(curLevel==1){
        //     movePanGuide();    
        // }

        createNewMt();
    }

    void createNewMt(){
        if(curMtObj!=null){
            GameObject.Destroy(curMtObj);
        }
        GameObject oriItem1 = (GameObject)Resources.Load(AssetUtility.getFloorBasePrefab(guestNdArr[0])); 
        GameObject boxItem1 = GameObject.Instantiate(oriItem1);
        boxItem1.transform.SetParent(curPanObj.transform);
        boxItem1.transform.localPosition = new Vector3(0,0.5f,0);
        boxItem1.transform.eulerAngles = new Vector3(0,0,0);
        boxItem1.transform.localScale = mtSclArr[guestNdArr[0]-1];
        curMtObj = boxItem1;
        meatSurfItem = curMtObj.GetComponent<MealSurfItem>();
        
        if(curNdImgIdx<=6){
            meatSurfItem.initShowBar(this,mtSurfCtArr[guestNdArr[0]-1],beefPatleTms,toOverCkTm);    
        }
        else{
            meatSurfItem.initShowBar(this,mtSurfCtArr[guestNdArr[0]-1],beefSleTms,toOverCkTm);
        }
        Transform subMt = boxItem1.transform.Find("item");
        subMt.eulerAngles = new Vector3(0,0,0);
    }

    public float toOverCkTm = 20;
    public float[] beefPatleTms;
    public float[] beefSleTms;

    void initJoyStick(){
        panPmc = curPanObj.GetComponent<PlayerMoveController>();
        panJsh.joyBGTransform.gameObject.SetActive(false);
        panPmc.mvDataJoyStk = panJsh;
        panPmc.speedMovements = 12;
        panPmc.setMnFmObj(this);
    }

    bool isPanHorMv = false;
    bool isPanRot = false;
    int panMvDir = 0;

    //1 leftup 2 rightup 3 up
    float curSldLen = 0;
    void movePanDeal(int dir,float dis){
        // Debug.Log("mpd.."+dir+" "+dis);
        if(dir==0){
            rotPanDeal();
            return;
        }
        else{
            panMvDir = dir;
            curSldLen = Mathf.Clamp(dis,0,slideMaxLen);
            // Debug.Log(curSldLen);
        }
    }

    //翻锅序列
    void rollGdDeal(){
        // slipGdCt++;
        // if(slipGdCt==3){
        //     isGaming = true;
        //     hidePanGuide();
        // }
        // else if(slipGdCt>3){
        //     return;
        // }
        // if(guidePl.activeSelf){
        //     hidePanGuide();
        //     Sequence wtSeq =DOTween.Sequence();
        //     wtSeq.AppendInterval(1);
        //     wtSeq.AppendCallback(delegate(){
        //         rollPanGuide(slipGdCt+1);
        //     });
        //     wtSeq.SetAutoKill();
        // }
    }

    int slipGdCt = 0;

    public float slideMaxLen = 100;
    public float rotPanUpTm = 0.5f;
    public float rotPanDnTm = 0.3f;
    public float rotPanAng = 30;
    //翻锅
    void rotPanDeal(){
        if(isPanRot){
            return;                
        }
        if(panMvDir==0||curSldLen==0){
            return;
        }

        // rollGdDeal();

        Vector3 endEng = Vector3.zero;
        float thPowr = curSldLen/slideMaxLen;
        float upDis = 4;
        if(panMvDir<3){
            int sign = 1;
            if(panMvDir==2){
                sign=-1;
            }
            float endAng = rotPanAng*sign*thPowr;
            endEng = new Vector3(endAng,oriPanRot.y,oriPanRot.z);
            Debug.Log("endAng.."+endAng);  
        }
        else if(panMvDir==3){
            upDis*=thPowr;
            endEng = new Vector3(oriPanRot.x,oriPanRot.y,rotPanAng*thPowr);
        }
        isPanRot = true;
        isPanIdle = false;
        Quaternion oriQt = Quaternion.Euler(oriPanRot.x,oriPanRot.y,oriPanRot.z);
        Quaternion desQt = Quaternion.Euler(endEng.x,endEng.y,endEng.z);
        Sequence mvSeq = DOTween.Sequence();
        mvSeq.AppendCallback(delegate(){
            Sequence wtSeq = DOTween.Sequence();
            wtSeq.AppendInterval(0.2f);
            wtSeq.AppendCallback(delegate(){
                throwMeatDeal(thPowr);
            });
            wtSeq.SetAutoKill();
        });
        mvSeq.Append(curPanObj.transform.DOLocalRotateQuaternion(desQt,rotPanUpTm));
        mvSeq.Append(curPanObj.transform.DOLocalRotateQuaternion(oriQt,rotPanDnTm));
        mvSeq.AppendCallback(delegate(){
            isPanRot = false;
            // isPanIdle = true;
            panMvDir=0;
            curSldLen=0;
        });
        mvSeq.SetAutoKill();

        if(panMvDir==3){
            Sequence upMvSeq = DOTween.Sequence();
            upMvSeq.Append(curPanObj.transform.DOLocalMoveY(upDis,rotPanUpTm));
            upMvSeq.Append(curPanObj.transform.DOLocalMoveY(oriPanPos.y,rotPanDnTm));
            upMvSeq.SetAutoKill();
        }
    }

    float refBtnFlkInTm = 4f;
    float refBtnAccTm = 0;
    bool isRefBtnFlk = false;

    Sequence refBtnFkSeq;
    void refBtnStaFlk(){
        refBtnStopFlk();
        isRefBtnFlk = true;
        Image btnImg = hintBtn.gameObject.GetComponent<Image>();
        refBtnFkSeq = DOTween.Sequence();
        refBtnFkSeq.Append(btnImg.transform.DOScale(new Vector3(1,1,1),1));
        refBtnFkSeq.Append(btnImg.transform.DOScale(new Vector3(0.8f,0.8f,0.8f),1f));
        refBtnFkSeq.SetLoops(-1);
    }
    void refBtnStopFlk(){
        isRefBtnFlk = false;
        refBtnAccTm=0;
        if(refBtnFkSeq!=null){
            refBtnFkSeq.Kill(true);
            refBtnFkSeq = null;
        }
        Image btnImg = hintBtn.gameObject.GetComponent<Image>();
        btnImg.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
    }


    static bool isSetSound = true;
    static  bool isSetShk = true;
    
    Button setSdBtn;
    Button setShkBtn;

    GameObject setPlObj;
    Button cloSetPlBtn;
    //1 clo 2 show
    void setSetPlSte(int type){
        // 135 -131
        float fitDeX = 0;
        if(type==2){
            setBtn.gameObject.SetActive(false);
            Vector3 toPos = new Vector3(fitDeX,305,0);
            setPlObj.transform.localPosition = toPos;
            toPos.y = 0;
            Sequence dnSeq = DOTween.Sequence();
            dnSeq.Append(setPlObj.transform.DOLocalMove(toPos,0.5f));
            dnSeq.AppendCallback(delegate(){
                cloSetPlBtn.enabled = true;
            });
            dnSeq.SetAutoKill();
        }
        else{
            Vector3 toPos = new Vector3(fitDeX,0,0);
            setPlObj.transform.localPosition = toPos;
            toPos.y = 305;
            Sequence dnSeq = DOTween.Sequence();
            dnSeq.Append(setPlObj.transform.DOLocalMove(toPos,0.5f));
            dnSeq.AppendCallback(delegate(){
                setBtn.enabled = true;
                setBtn.gameObject.SetActive(true);
            });
            dnSeq.SetAutoKill();
        }
    }

    void hintFlickDeal(){
        float deX = 0;
        if(flickObj!=null){
            if(flickObj.name=="item5(Clone)"){
                MealItem mi = flickObj.GetComponent<MealItem>();
                mi.playFlick();
                deX=120;
            }
            else if(flickObj.name=="item3(Clone)"){
                RotPtItem rpi = flickObj.GetComponent<RotPtItem>();
                rpi.playFlick();
                deX=120;
            }
            else{
                // Debug.Log("fl....");
                MealItem mi = flickObj.GetComponent<MealItem>();
                mi.playFlick();

            }
            setGuideAndSta(deX);
        }
         
    }

    void setGuideAndSta(float deX = 0){
        // Vector2 pos1 = Camera.main.WorldToScreenPoint(flickObj.transform.position);
        // if(Screen.height==1000){
        //     pos1.y-=500;
        //     pos1.y*=1.5f;
        //     // Debug.Log("ss."+pos1);
        //     pos1.x-=430+deX;
        // }
        // else{
        //     pos1.y-=970+(Screen.height-1920)/2;
        //     pos1.x-=430+deX;
        // }
        // guidePl.transform.localPosition = pos1;
        // staGuideFing();
    }

    GameObject finImgObj;

    int finImgIdx =1;

    Quaternion cmBeginRot;
    Image desImg;

    int[] colorCtArr;
    //吃一个的率
    float[] colorSinRates;
    float[] totalColorRates;
    float[] curColorRates;
    int[] colCapArr;

    float winPtrRadius = 289f;

    bool isWinPtrMv = false;
    
    float curWinAng = 50;
    float maxWinAng = 125;
    float minWinAng = 55;
    float winPtrMvSpd = 1f;
    GameObject winPtrPlObj;
    int[] winMuls;
    //0开始
    int winStopMulIdx = 1;
    float endWinAng = 0;
    int ptrRunCt = 2;
    int ptrAccRunCt = 0;
    int[] winPtrAngArr;
    void startWinPtrAc(){
        isWinPtrMv = true;
        winPtrAngArr = new int[6];
        winPtrAngArr[0]=(int)minWinAng;
        float unit = (maxWinAng-minWinAng)/5;
        for(int i=1;i<5;i++){
            winPtrAngArr[i] = winPtrAngArr[i-1]+(int)unit;
        }
        endWinAng = UnityEngine.Random.Range(winPtrAngArr[winStopMulIdx],winPtrAngArr[winStopMulIdx+1]);
        ptrRunCt = UnityEngine.Random.Range(2,6);
        ptrAccRunCt = 0;
    }

    int calCurPtrIdx(){
        for(int i=0;i<5;i++){
            if(curWinAng>=winPtrAngArr[i]&&curWinAng<winPtrAngArr[i+1]){
                return i;
            }
        }
        return 0;
    }
    int[] finPtrMuls = new int[]{2,3,5,3,2};


    void doWinPtrMove(){
        GameObject ptrObj = winPtrPlObj.transform.Find("ptrImg").gameObject;

        curWinAng+=winPtrMvSpd;
        if(winPtrMvSpd>0&&curWinAng>maxWinAng){
            winPtrMvSpd = -winPtrMvSpd;
        }
        else if(winPtrMvSpd<0&&curWinAng<minWinAng){
            winPtrMvSpd = -winPtrMvSpd;
        }
        curWinAng+=winPtrMvSpd;

        // if(curWinAng==endWinAng){
        //     ptrAccRunCt++;
        //     if(ptrAccRunCt==ptrRunCt){
        //         isWinPtrMv = false;
        //         return;
        //     }
        // }

        int curIdx = calCurPtrIdx();
        int curGnVal = calFinWinMy(finPtrMuls[curIdx]);
        gnMoreTt.text = "+"+curGnVal;

        float xPos=0;
        float yPos=0;
        if(curWinAng<90){
            xPos = -winPtrRadius*Mathf.Cos(curWinAng*Mathf.Deg2Rad);
            yPos = -50-winPtrRadius+winPtrRadius*Mathf.Sin(curWinAng*Mathf.Deg2Rad);
        }
        else{
            float nowAng = curWinAng-90;
            xPos = winPtrRadius*Mathf.Sin(nowAng*Mathf.Deg2Rad);
            yPos = -50-winPtrRadius+winPtrRadius*Mathf.Cos(nowAng*Mathf.Deg2Rad);
        }
        Vector3 pos = new Vector3(xPos,yPos,0);
        // Vector3 cenPt = new Vector3(0,-50-winPtrRadius,0);
        ptrObj.transform.localPosition = pos;
        
        // Vector3 lookDir = (cenPt-pos).normalized;

        ptrObj.transform.eulerAngles = new Vector3(0,0,90-curWinAng);

    }

    GameObject winEffPlObj;
    //播放胜利烟花
    void playWinExpEff(){
        Sequence expSeq = DOTween.Sequence();
        for(int i=0;i<3;i++){
            GameObject effObj = winEffPlObj.transform.GetChild(i).gameObject;
            ParticleSystem ps = effObj.GetComponent<ParticleSystem>();
            expSeq.AppendCallback(delegate(){
                effObj.SetActive(true);
                ps.Play();
            });
            expSeq.AppendInterval(0.5f);
        }   
        expSeq.SetAutoKill();     
    }
    void hideWinExpEff(){
        for(int i=0;i<3;i++){
            GameObject effObj = winEffPlObj.transform.GetChild(i).gameObject;
            effObj.SetActive(false);
        }
    }   

    float colRtBigerRate = 2;

    void setCanHorMove(){
        canHorMove = true;
    }

    float curAcFsVal = 50;
    int curRoomType = 0;
    

    
    //当前模型
    int curAngType = 1;

    float[] angLevArr = new float[4];
    int calPectToLev(float val){
        if(val<angLevArr[0]){
            return 1;
        }
        else if(val<angLevArr[1]){
            return 2;
        }
        else if(val<angLevArr[2]){
            return 3;
        }
        else if(val<angLevArr[3]){
            return 4;
        }
        else{
            return 5;
        }
    }

    void setAcFsImg(){
        acFsImg.fillAmount = curAcFsVal/100f;
        int lev = calPectToLev(curAcFsVal);
        acFsImg.color = barColors[lev-1];
    }

    void setUpNdImg(){
        Image contImg = upNdPlObj.transform.Find("contImg").GetComponent<Image>();
        contImg.sprite = Resources.Load<Sprite>("img/cookMl"+curNdImgIdx);
    }

    void setGuestNdArr(){
        if(curNdImgIdx<=6){
            guestNdArr[0]=1;    
        }
        else if(curNdImgIdx<=12){
            guestNdArr[0]=2;
        }
        else{
            guestNdArr[0]=3;
        }
    }

    // 1 粒 2 片1 3 片2
    // 肉 西兰花 番茄 胡萝卜 胡椒
    int[] guestNdArr = new int[]{3,1,0,1,1};
    int [] curPutOnArr = new int[5]{2,-1,-1,-1,-1};
    string[] guestResNms = new string[]{"mItem1","mItem2","fItem1","fItem2"};
    GameObject guestObj = null;
    Image guestNdImg = null;
    GameObject upNdPlObj;

    GameObject makeBgObj;

    void startCookDeal(){
        levPlObj.SetActive(false);
        prtyPlObj.SetActive(false);
        setSceneType(2);
        nextBtn.gameObject.SetActive(false);
        needPlObj.SetActive(false);
        guestObj.SetActive(false);
        panJsh.enabled =true;
        panDdh.enabled = true;
        ckIfPlObj.SetActive(true);
        isGaming = true;
        // procePlObj.SetActive(true);
        setCurStepPt(1);

        createPanCook();
    }    

    int curNdImgIdx = 1;
    // 肉的形状
    // 6 6 6
    // 1 2 3
    // 熟度
    // 3 熟 3 半熟
    // medium/well done
    public bool isTest = false;
    public int testNdImgIdx = -1;
    void guestOrderDeal(bool isFirst = false){
        setSceneType(1);

        int ranGtIdx = UnityEngine.Random.Range(0,4);
        curNdImgIdx = UnityEngine.Random.Range(1,19);
        if(isTest){
            curNdImgIdx = testNdImgIdx;
        }

        guestNdImg.sprite = Resources.Load<Sprite>("img/cookMl"+curNdImgIdx);
        setUpNdImg();
        setGuestNdArr();

        string manRes = AssetUtility.getShoesPrefab(guestResNms[ranGtIdx]);
        void showOrder(){
            setOrderImg();   
            curPutOnArr[0]=guestNdArr[0];
            if(!isGaming){
                needPlObj.SetActive(true);
            }
            nextBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.AddListener(delegate(){
                startCookDeal();
            });
        }
        if(isFirst){
            guestObj = createOneObj(manRes,transform,new Vector3(0,-4,0f),new Vector3(0,180,0),new Vector3(16,16,16));    
            GuestItem gi = guestObj.GetComponent<GuestItem>();
            gi.playWait();
            showOrder();
        }
        else{
            guestObj = createOneObj(manRes,transform,new Vector3(14,-4,0f),new Vector3(0,270,0),new Vector3(16,16,16));
            GuestItem gi = guestObj.GetComponent<GuestItem>();
            Sequence mvSeq = DOTween.Sequence();
            mvSeq.Append(guestObj.transform.DOLocalMoveX(0,1f));
            mvSeq.AppendCallback(delegate(){
                gi.playWait();
            });
            mvSeq.Append(guestObj.transform.DOLocalRotate(new Vector3(0,180,0),0.5f));
            mvSeq.AppendCallback(delegate(){
                showOrder();
            });
            mvSeq.SetAutoKill();
        }
    }

    // 1 2
    GameObject procePlObj;    
    void setCurStepPt(int step){
        for(int i=0;i<step-1;i++){
            Transform ptTsf = procePlObj.transform.GetChild(i).GetChild(0);
            ptTsf.localScale = new Vector3(1,1,1);
        }
        Transform cPtTsf = procePlObj.transform.GetChild(step-1).GetChild(0);
        cPtTsf.localScale = new Vector3(0.6f,0.6f,1f);

        for(int i=step;i<2;i++){
            Transform ptTsf = procePlObj.transform.GetChild(i).GetChild(0);
            ptTsf.localScale = new Vector3(0,0,0);
        }
    }

    
    //位置 -790 -1080 
    // 上 下
    Vector2 extCkPlYs = new Vector2(-690,-1080);
    public void mkExtVegeDeal(){

        nextBtn.gameObject.SetActive(true);
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(delegate(){
            panDdh.enabled = false;
            panJsh.enabled = false;
            panJsh.joyBGTransform.gameObject.SetActive(false); 
            isGaming =false;
            setCurStepPt(2);
            nextBtn.gameObject.SetActive(false);
            Vector3 pos = curPanObj.transform.localPosition;
            pos.z=-25;
            Sequence mvSeq = DOTween.Sequence();
            mvSeq.Append(curPanObj.transform.DOLocalMove(pos,1));
            mvSeq.AppendCallback(delegate(){
                GameObject.Destroy(curPanObj);
                ckIfPlObj.SetActive(false);
           
                extCkBnSlipDeal(1);
                createExtDeal();
            });
            mvSeq.SetAutoKill();
        });
    }
    float extCkBnPlTm = 0.5f;
    
    // 1 弹出 2 收起
    void extCkBnSlipDeal(int type){
        Vector2 endYs = new Vector2(extCkPlYs.y,extCkPlYs.x);
        if(type==2){
            endYs = new Vector2(extCkPlYs.x,extCkPlYs.y);
        }
        else{

        }

        extCkPlObj.SetActive(true);
        Vector3 plPos = extCkPlObj.transform.localPosition;
        plPos.y = endYs[0];
        extCkPlObj.transform.localPosition = plPos;
        plPos.y = endYs[1];
        extCkPlObj.transform.DOLocalMove(plPos,extCkBnPlTm);
    }

    void setOrderImg(){
        for(int i=0;i<5;i++){
            Transform tsf = needPlObj.transform.Find(extNodeNms[i]);
            tsf.gameObject.SetActive(false);
            // if(guestNdArr[i]>0){
            //     tsf.gameObject.SetActive(true);
            // }
            // else{
            //     tsf.gameObject.SetActive(false);
            // }
        }
    }

    GameObject finAddBdObj;
    Button nextBtn;
    //1 dis 2 ang
    int curEmjType = 1;
    GameObject sauceObj;

    void clearPepObjs(){
        if(pepPatleObjs!=null){
            for(int i=0;i<totalPepCt;i++){
                if(pepPatleObjs[i]!=null){
                    GameObject.Destroy(pepPatleObjs[i]);
                    pepPatleObjs[i] = null;
                }
            }
        }
    }

    void clearSceneObj(){
        clearPepObjs();

        if(ckPanObj!=null){
            GameObject.Destroy(ckPanObj);  
            ckPanObj = null;
        }
        if(guestObj!=null){
            GameObject.Destroy(guestObj);  
            guestObj = null;
        }
    }

    void refreshCurDay(){
        levPlObj.SetActive(true);
        Text levTt = levPlObj.transform.Find("levTt").GetComponent<Text>();
        levTt.text = "DAY  "+curLevel;   
    }

    Button staGmBtn;
    //重来
    void initUI(bool isFirst = false){

        // TinySauce.OnGameStarted(levelNumber:curLevel+"");
        clearSceneObj();        

        prtyPlObj.SetActive(true);
        refreshCurDay();
        winPlObj.SetActive(false);
        // isGaming = true;
        nextBtn.gameObject.SetActive(false);
        ckIfPlObj.SetActive(false);
        extCkPlObj.SetActive(false);
        needPlObj.SetActive(false);
        // staBrthAc();
        for(int i=0;i<vegeCt+1;i++){
            curPutOnArr[i]=-1;
        }
        // createExtDeal();
        sauceObj.SetActive(false);
        procePlObj.SetActive(false);
        
        isVeBtnShow = false;
        setSceneType(1);
        
        pepPlObj.transform.localPosition = new Vector3(0,0,-2);
        // nextBtn.transform.localPosition = new Vector3(295,-610,0);
        upNdPlObj.SetActive(false);
        initCamera(1);
        initExtCkBtn();

        guidePl.SetActive(false);
        guestOrderDeal(isFirst);
        isMtOut = false;
        
    }

    void initCamera(int type){
        if(type==1){
            mainCamera.transform.localPosition = new Vector3(0,17,-60);    
            mainCamera.transform.eulerAngles = new Vector3(20,0,0);
        }
        else{
            mainCamera.transform.localPosition = new Vector3(0.6f,20.6f,-39.7f);
            mainCamera.transform.eulerAngles = new Vector3(50,0,0);
        }
        
    }
    GameObject needPlObj;
    string[] extCkNames = new string[]{"broccoli","tomato","carrot"};
    //ext_vege

    string[] extNodeNms = new string[]{"mtImg","bcliImg","tomtImg","tartImg","ppImg"};

    GameObject extCkPlObj;
    Transform[] extCkBtnTsfs;
    int[] hasPutExtArr = new int[]{0,0,0,0,0};
    Transform curMvExtTsf;
    Sequence ckBtnSeq = null;
    int vegeCt = 3;
    bool isVeBtnShow = false;

    void clearExtCkBtn(){
        int chdCt = extCkPlObj.transform.childCount;
        for(int i=1;i<chdCt;i++){
            GameObject.DestroyImmediate(extCkPlObj.transform.GetChild(1).gameObject);
        }
    }

    Vector3[] vegeScls = new Vector3[]{new Vector3(1f,1f,1f),new Vector3(1.5f,1.5f,1.5f),new Vector3(1f,1f,1f)};
    void initExtCkBtn(){
        clearExtCkBtn();

        Transform oriBtn = extCkPlObj.transform.GetChild(0);
        extCkBtnTsfs = new Transform[vegeCt];
        extCkBtnTsfs[0] = oriBtn;
        for(int i=0;i<vegeCt-1;i++){
            GameObject newBtn = GameObject.Instantiate(oriBtn.gameObject);
            newBtn.transform.SetParent(oriBtn.parent);
            newBtn.transform.localScale = new Vector3(1,1,1);
            extCkBtnTsfs[i+1] = newBtn.transform;
        }

        for(int i=0;i<vegeCt;i++){
            Image vCImg = extCkBtnTsfs[i].Find("img").GetComponent<Image>();   
            // nmTt.text = extCkNames[i];
            vCImg.sprite = Resources.Load<Sprite>("img/main_icon"+(31+i));

            EventTrigger eti = extCkBtnTsfs[i].GetComponent<EventTrigger>();
            eti.triggers.Clear();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.RemoveAllListeners();
            int tmI = i;
            entry.callback.AddListener(delegate(BaseEventData baseEventData){
                // if(judgeExtHasPutOn(tmI)){
                //     return;
                // }
                // Debug.Log("down");                 
                // Debug.Log(Input.mousePosition);
                string resNm = AssetUtility.getFloorBasePrefab(6+tmI);
                Transform panPt = transform.Find("cookPl");
                GameObject extObj = createOneObj(resNm,panPt,new Vector3(1,2,1),new Vector3(0,0,0),vegeScls[tmI]);
                curMvExtTsf = extObj.transform;
                curMvExtTsf.localPosition = covtMPosToLPos();

                setObjGavity(extObj.transform,1,false);        
            });
            eti.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Drag;
            entry.callback.RemoveAllListeners();
            entry.callback.AddListener(delegate(BaseEventData baseEventData){
                // if(judgeExtHasPutOn(tmI)){
                //     return;
                // }
                // Debug.Log(Input.mousePosition);
                curMvExtTsf.localPosition = covtMPosToLPos();                  
                // Debug.Log(curMvExtTsf.localPosition);          
            });
            eti.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.RemoveAllListeners();
            entry.callback.AddListener(delegate(BaseEventData baseEventData){
                if(curLevel==1&&tmI==0){
                    hidePanGuide();
                }
                // if(judgeExtHasPutOn(tmI)){
                //     return;
                // }
                if(judgeOutCkPan(curMvExtTsf.transform.localPosition)){
                    Vector3 pos = new Vector3(extAnsFPos.x,extAnsFPos.y,extAnsFPos.z);
                    if(tmI>2){
                        pos.y+=2;   
                    }
                    curMvExtTsf.transform.localPosition = pos;
                }
                curMvExtTsf.transform.SetParent(ckPanObj.transform);
                // Debug.Log("up");                
                
                if(ckBtnSeq!=null){
                    ckBtnSeq.Kill(true);
                    ckBtnSeq = null;
                }
                setObjGavity(curMvExtTsf,1,true);
                curPutOnArr[tmI+1] = 1;
                ckBtnSeq = DOTween.Sequence();
                ckBtnSeq.AppendInterval(1);
                ckBtnSeq.AppendCallback(delegate(){
                    if(checkHasPutOneVege()){
                        if(!isVeBtnShow){
                            isVeBtnShow = true;
                            nextBtn.gameObject.SetActive(true);
                            nextBtn.onClick.RemoveAllListeners();
                            nextBtn.onClick.AddListener(delegate(){
                                if(guestNdArr[4]>0){
                                    putPepperDeal();           
                                }
                                else{
                                    giveToGuesetDeal();
                                }
                            });
                        }
                    }
                    ckBtnSeq.Kill(true);
                    ckBtnSeq = null;
                });
            });
            eti.triggers.Add(entry);
        }
    }

    void initPepBtn(){
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(delegate(){
            giveToGuesetDeal();
        });

        EventTrigger eti = extCkBtnTsfs[0].GetComponent<EventTrigger>();
        eti.triggers.Clear();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.RemoveAllListeners();
        entry.callback.AddListener(delegate(BaseEventData baseEventData){
            isPeping = true;
            string resNm = AssetUtility.getFloorBasePrefab(9);
            Transform panPt = transform.Find("cookPl");
            GameObject extObj = createOneObj(resNm,panPt,new Vector3(1,4,1),new Vector3(0,0,0),new Vector3(1,1,1));
            curMvExtTsf = extObj.transform;
            curMvExtTsf.localPosition = covtMPosToLPos();

            setObjGavity(extObj.transform,1,false);        
        });
        eti.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.RemoveAllListeners();
        entry.callback.AddListener(delegate(BaseEventData baseEventData){
            curMvExtTsf.localPosition = covtMPosToLPos();                  
        });
        eti.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.RemoveAllListeners();
        entry.callback.AddListener(delegate(BaseEventData baseEventData){
            if(judgeOutCkPan(curMvExtTsf.transform.localPosition)){
                Vector3 pos = new Vector3(extAnsFPos.x,extAnsFPos.y,extAnsFPos.z);
                Debug.Log("aa..."+pos);
                pos.y+=2;   
                curMvExtTsf.transform.localPosition = pos;
            }
            curMvExtTsf.transform.SetParent(ckPanObj.transform);
            // Debug.Log("up");                
            
            if(ckBtnSeq!=null){
                ckBtnSeq.Kill(true);
                ckBtnSeq = null;
            }
            pepperSauceDeal(3);
            curPutOnArr[4] = 1;
        });
        eti.triggers.Add(entry);
    }

    bool isPeping = false;
    void putPepperDeal(){
        Debug.Log("pepper deal..");

        Sequence slipSeq = DOTween.Sequence();
        slipSeq.AppendCallback(delegate(){
            extCkBnSlipDeal(2);
        });            
        slipSeq.AppendInterval(extCkBnPlTm);
        slipSeq.AppendCallback(delegate(){
            isVeBtnShow = false;
            nextBtn.gameObject.SetActive(false);
            clearExtCkBtn();

            Transform oriBtn = extCkPlObj.transform.GetChild(0);
            extCkBtnTsfs[0] = oriBtn;

            Image ppImg = extCkBtnTsfs[0].Find("img").GetComponent<Image>();   
            ppImg.sprite = Resources.Load<Sprite>("img/main_icon34");
            // nmTt.text = "pepper";
            extCkBnSlipDeal(1);
        });     
        slipSeq.AppendInterval(extCkBnPlTm);
        slipSeq.AppendCallback(delegate(){
            initPepBtn();
        });
        slipSeq.AppendInterval(0.3f);
        slipSeq.AppendCallback(delegate(){
            if(!isPeping){
                nextBtn.gameObject.SetActive(true);
            }
        });
        slipSeq.SetAutoKill();
    }

    bool checkHasPutOneVege(){
        for(int i=1;i<=3;i++){
            if(curPutOnArr[i]>0){
                return true;
            }
        }   
        return false;     
    }

    void giveToGuesetDeal(){
        initCamera(1);
        nextBtn.gameObject.SetActive(false);
        setSceneType(1);
        procePlObj.SetActive(false);
        extCkBnSlipDeal(2);

        guestObj.SetActive(true);
        upNdPlObj.SetActive(false);
        needPlObj.SetActive(false);
        setSceneType(1);

        if(pepPatleObjs!=null){
            for(int i=0;i<totalPepCt;i++){
                if(pepPatleObjs[i]!=null){
                    Transform tsf = pepPatleObjs[i].transform.GetChild(0); 
                    Rigidbody rb = tsf.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    BoxCollider bci = tsf.GetComponent<BoxCollider>();
                    bci.isTrigger = true;
                }
            }
        }

        ckPanObj.transform.localPosition = new Vector3(0.3f,7.6f,-13f);

        ckPanObj.transform.localScale = new Vector3(4,4,4);

        if(guestNdArr[0]==1){
            pepPlObj.transform.localPosition = new Vector3(0.1f,-4.8f,-12.4f);
        }
        else{
            pepPlObj.transform.localPosition = new Vector3(0,-3.7f,-12.8f);
        }
        
    

        GuestItem gi = guestObj.GetComponent<GuestItem>();
        gi.playVict();
        Sequence wtSeq = DOTween.Sequence();
        wtSeq.AppendInterval(2.5f);
        wtSeq.AppendCallback(delegate(){
            ckPanObj.SetActive(false);
            clearPepObjs();
        });
        wtSeq.Append(guestObj.transform.DOLocalRotate(new Vector3(0,270,0),0.5f));
        wtSeq.AppendCallback(delegate(){
            gi.playWalk();
        });
        wtSeq.Append(guestObj.transform.DOLocalMoveX(-17,1f));
        wtSeq.AppendCallback(delegate(){
            clearSceneObj();
            guestOrderDeal();
        });
        wtSeq.AppendInterval(1);
        wtSeq.AppendCallback(delegate(){
            staGmBtn.enabled = true;
            startPlObj.SetActive(true);
            curLevel++;
            refreshCurDay();
            // TinySauce.OnGameFinished(true,curLevel);
            DataManage.SetInt("curLevel",curLevel);
            staGmBtn.onClick.RemoveAllListeners();
            staGmBtn.onClick.AddListener(delegate(){
                initUI();
                startPlObj.SetActive(false);
                staGmBtn.enabled = false;
                startCookDeal();
            });
            
        });
        wtSeq.SetAutoKill();
    }

    bool judgeExtHasPutOn(int idx){
        if(idx<=2){
            if(curPutOnArr[1]>-1){
                return true;
            }
        }
        else{
            if(curPutOnArr[2]>-1){
                return true;
            }
        }
        return false;
    }

    GameObject pepPlObj;
    int totalPepCt = 100;
    GameObject[] pepPatleObjs = null;
    void pepperSauceDeal(int idx,TweenCallback cb=null){
        if(idx==3){
            nextBtn.gameObject.SetActive(false);
            Vector3 pos = curMvExtTsf.localPosition;
            curMvExtTsf.localPosition = pos;
            Vector3 oriPos = new Vector3(pos.x,pos.y,pos.z);
            Vector3 upPos = new Vector3(pos.x,pos.y+2,pos.z-1);
            Quaternion oriRot = Quaternion.Euler(0,0,0);
            Quaternion upRot = Quaternion.Euler(-30,0,0);
            Sequence mvSeq = DOTween.Sequence();
            for(int i=0;i<3;i++){
                mvSeq.Append(curMvExtTsf.DOLocalMove(upPos,0.2f));
                mvSeq.Join(curMvExtTsf.DOLocalRotateQuaternion(upRot,0.2f));
                mvSeq.Append(curMvExtTsf.DOLocalMove(oriPos,0.2f));
                mvSeq.Join(curMvExtTsf.DOLocalRotateQuaternion(oriRot,0.2f));
            }
            mvSeq.AppendCallback(delegate(){
                GameObject.Destroy(curMvExtTsf.gameObject);
            });
            mvSeq.SetAutoKill();

            Sequence fallSeq = DOTween.Sequence();
            pepPatleObjs = new GameObject[totalPepCt];
            for(int i=0;i<totalPepCt;i++){
                string resName = AssetUtility.getFloorBasePrefab("ppPtle");
                float ranX = UnityEngine.Random.Range(0,15)-7;
                ranX/=10;
                float ranZ = UnityEngine.Random.Range(0,15)-7;
                ranZ/=10;
                Vector3 ppPos = new Vector3(pos.x+ranX,pos.y,pos.z+ranZ);
                int tmp =i;
                fallSeq.AppendCallback(delegate(){
                    GameObject item = createOneObj(resName,pepPlObj.transform,ppPos,new Vector3(0,0,0),new Vector3(0.15f,0.15f,0.15f));
                    pepPatleObjs[tmp] = item;
                });
                fallSeq.AppendInterval(0.02f);                             
            }
            fallSeq.AppendInterval(0.5f);
            fallSeq.AppendCallback(delegate(){
                nextBtn.gameObject.SetActive(true);
                isPeping = false;
                if(cb!=null){
                    cb();
                }
            });
            fallSeq.SetAutoKill();
        }
        else{
            Quaternion dnRot = Quaternion.Euler(0,0,60);
            curMvExtTsf.DOLocalRotateQuaternion(dnRot,0.7f);

            Sequence wtSeq = DOTween.Sequence();
            wtSeq.AppendInterval(0.3f);
            wtSeq.AppendCallback(delegate(){
                Vector3 pos = curMvExtTsf.localPosition;
                pos.x-=0.7f;
                pos.z+=0.3f;
                pos.y=-2;
                sauceObj.transform.localPosition = pos;
                sauceObj.SetActive(true);
            });
            wtSeq.AppendInterval(2.2f);
            wtSeq.AppendCallback(delegate(){
                GameObject.Destroy(curMvExtTsf.gameObject);
            });
            wtSeq.AppendInterval(1);
            wtSeq.AppendCallback(delegate(){
                if(cb!=null){
                    cb();
                }
            });
            wtSeq.SetAutoKill();
        }
    }

    Vector3 extAnsFPos = new Vector3(1,0,-5);
    // 3.3 / -7 -1
    bool judgeOutCkPan(Vector3 pos){
        if(pos.x<-3.3f||pos.x>3.3f){
            return true;
        }
        if(pos.z<-8||pos.z>-2){
            return true;
        }
        return false;
    }

    void setObjGavity(Transform item,int chdCt,bool flag){
        for(int i=0;i<chdCt;i++){
            Rigidbody rb = item.GetChild(i).GetComponent<Rigidbody>();
            rb.useGravity = flag;
        }
    }

    //100 1000/200 1300
    // -7 7/-14 2
    Vector3 covtMPosToLPos(){
        // Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(-7,0,-14);
        Vector3 mPos = Input.mousePosition;
    
        float delY = 0;
        if(Screen.width==800&&Screen.height==1000){
            delY = 300;
            mPos.y+=200;
        }

        if(mPos.x<100){
            mPos.x=100;
        }
        if(mPos.x>1000){
            mPos.x=1000;
        }
        if(mPos.y<200+delY){
            mPos.y=200+delY;
        }
        if(mPos.y>1300+delY){
            mPos.y=1300+delY;
        }

        pos.x+= 14*(mPos.x-100)/900f;
        pos.z+= 16*(mPos.y-200)/1100f;
        pos.y = 3;
        return pos;
    }

    GameObject createOneObj(string resNm,Transform pt,Vector3 pos,Vector3 eng,Vector3 scl){
        GameObject oriItem = (GameObject)Resources.Load(resNm); 
        GameObject boxItem = GameObject.Instantiate(oriItem);
        boxItem.transform.SetParent(pt);
        boxItem.transform.localPosition = pos;
        boxItem.transform.eulerAngles = eng;
        boxItem.transform.localScale = scl;   
        
        return boxItem;
    }

    GameObject ckPanObj = null;
    void createExtDeal(){
        if(curLevel==1){
            Sequence wtSeq = DOTween.Sequence();
            wtSeq.AppendInterval(1f);
            wtSeq.AppendCallback(delegate(){
                moveExtGuide();
            });
            wtSeq.SetAutoKill();
        }

        Transform panPt = transform.Find("cookPl");

        string resNm = AssetUtility.getFloorBasePrefab("putPan");
        ckPanObj = createOneObj(resNm,panPt,new Vector3(0f,-0.16f,-3),new Vector3(0,0,0),new Vector3(6,6,6));

        guestObj.transform.SetSiblingIndex(1);

        curMtObj.transform.SetParent(ckPanObj.transform);
        curMtObj.transform.localPosition = new Vector3(0f,0f,0f);
        curMtObj.transform.eulerAngles = new Vector3(0,0,0);
        meatSurfItem.enabled = false;
        
        Sequence wtSeq1 = DOTween.Sequence();
        wtSeq1.AppendInterval(0.25f);
        wtSeq1.AppendCallback(delegate(){
            curMtObj.transform.localPosition = new Vector3(0f,-0.3f,0f);
            isMtLkPan = true;
            mtLkAccTm = 0;
            isMtOut = false;
        });
        wtSeq1.SetAutoKill();

    }

    void initBtnSte(){
        // for(int i=0;i<4;i++){
        //     Button mlMvBtn = mlBtnPl.transform.Find("mlBtn"+(i+1)).GetComponent<Button>();
        //     mlMvBtn.enabled = false;
        //     Button panBtn = mlBtnPl.transform.Find("patCbBtn"+(i+1)).GetComponent<Button>();
        //     panBtn.enabled = false;
        //     partBtnObjs[i]=null;
        //     rotItems[i]=null;
        // }
    }

    void setBtnSteByMap(){
        for(int i=0;i<4;i++){
            Button rotBtn = mlBtnPl.transform.Find("rotPtBtn"+(i+1)).GetComponent<Button>();
            if(rotItems[i]!=null){
                rotBtn.enabled = true;
            }
            else{
                rotBtn.enabled = false;
            }

            Button panBtn = mlBtnPl.transform.Find("patCbBtn"+(i+1)).GetComponent<Button>();
            if(partBtnObjs[i]!=null){
                panBtn.enabled = true;
            }
            else{
                panBtn.enabled = false;
            }
        }
    }

    Vector3 ballPos = new Vector3(0,0.04f,0);
    Vector3 ballScl = new Vector3(0.9f,0.9f,0.9f);
    void initShoes(){
        GameObject oriItem = (GameObject)Resources.Load(AssetUtility.getShoesPrefab(1));
        GameObject shoeItem = GameObject.Instantiate(oriItem);
        shoeItem.transform.SetParent(mnRoleObj.transform);
        shoeItem.transform.localScale = ballScl;
        shoeItem.transform.localPosition = ballPos;
        // shoeItem.transform.eulerAngles = new Vector3(0,0,90);
        sitmObj = mnRoleObj.transform.GetChild(0).GetComponent<ShoeItem>();
    }


    void showTips(string val){
        Text conTt = tipsPlObj.transform.Find("text").GetComponent<Text>();
        conTt.text = val;
        tipsPlObj.SetActive(true);
        Invoke("hideTipsPanel",2f);
    }


    void hideTipsPanel(){
        tipsPlObj.SetActive(false);
    }

    //-0.4 0.04 0.4

    // x +0.2  z -0.2 y +0.124

    float putBoxOriY = 0.16f;
    float putBoxDelY = 0.16f;


    int calColorTotRate(){
        float toRate = 0;
        for(int i=0;i<11;i++){
            // Debug.Log("xixi.."+curColorRates[i]);
            toRate+=curColorRates[i];
        }
        int res = Mathf.CeilToInt(toRate*100);
        if(res>100){
            res = 100;
        }
        return res;
    }

    void setBoxPos(GameObject boxItem,int score){
        int delCtX = calDeltaPosCt(score,1);
        int delCtY = calDeltaPosCt(score,3);
        int delCtZ = calDeltaPosCt(score,2);
        
        float posX = putBoxOriX+delCtX*0.2f;
        float posY = putBoxOriY+delCtY*putBoxDelY;
        float posZ = putBoxOriZ-0.18f*delCtZ;
        
        boxItem.transform.localPosition = new Vector3(posX,posY,posZ);
    }

    Vector3 getBoxPos(int score){
        int delCtX = calDeltaPosCt(score,1);
        int delCtY = calDeltaPosCt(score,3);
        int delCtZ = calDeltaPosCt(score,2);
        
        float posX = putBoxOriX+delCtX*0.2f;
        float posY = putBoxOriY+delCtY*putBoxDelY;
        float posZ = putBoxOriZ-0.18f*delCtZ;
        
        return new Vector3(posX,posY,posZ);
    }


    float putBoxOriZ = 0.687f;
    float putBoxOriX = -0.4f;

    int curScore = 0;
    float hitLitCoolInTm = 0.5f;
    float hitLitCoolAccTm = 0;
    //itemType 1 单次出发 2 连续触发 score判断正负
    ParaCurveLcHelp[] pclObjCache;

    void pushToPlcCch(ParaCurveLcHelp itemObj){
        pclObjCache[plcIdx]=itemObj;
        plcIdx++;
        if(plcIdx==pclCchCt){
            plcIdx=0;
        }        
    }

    //streth 强度
    Vector3 calFallAround(Vector3 newPos,float streth=1){
        float dis = 0.5f*streth;
        if(newPos.x==putBoxOriX){
            newPos.x-=dis;
        }
        else if(newPos.x==-putBoxOriX){
            newPos.x+=dis;
        }
        else{
            if(newPos.z==putBoxOriZ){
                newPos.z+=dis;
            }   
            else{
                newPos.z-=dis;
            }
        }
        return newPos;
    }

    //前 左 右
    Vector3 calFallForward(Vector3 newPos,float streth=1){
        float dis = 0.5f*streth;

        if(newPos.x<0){
            newPos.x-=dis*UnityEngine.Random.Range(1,3);
        }
        else{
            newPos.x+=dis*UnityEngine.Random.Range(1,3);
        }
        newPos.z+=dis*UnityEngine.Random.Range(3,8);
        return newPos;
    }
    

    Vector3 callFallBeHind(Vector3 newPos){
        if(newPos.z==putBoxOriZ){
            newPos.z-=2f;
        }   
        else{
            newPos.z-=1f;
        }
        return newPos;
    }

    int randomColor(){
        float[] rateArr = new float[colCapArr.Length];
        for(int i=0;i<colCapArr.Length;i++){
            rateArr[i] = totalColorRates[colCapArr[i]];
        }
        int colIdx = Util.randomIdxVal(rateArr);
        return colCapArr[colIdx];
    }

    //dir 0 四周 1 向后
    void subCarBox(int ct,int color,int dir=0){
        // playGmSound(2,"boxfall");
        // int chdNum = mnRoleObj.transform.childCount-1;

        // bool ranCol =false;
        // if(color==-1){
        //     ranCol=true;
        // }
        // // Debug.Log("sss..."+ct+" "+chdNum+" "+curScore);
        // int lastIdx = chdNum-ct;
        // if(lastIdx<1){
        //     lastIdx = 1;
        // }
        // if(curScore>chdNum){
        //     curScore = chdNum;
        // }
        // for(int i=0;i<ct;i++){
        //     if(i<chdNum){
        //         if(ranCol){
        //             color = randomColor();
        //         }
        //         updateColorRates(color,-1);
        //         GameObject boxItem = mnRoleObj.transform.GetChild(curScore).gameObject;
        //         BoxCollider bci = boxItem.GetComponent<BoxCollider>();
        //         bci.enabled = false;
        //         curScore--;
        //         scoreTt.text = "分数:"+curScore;
        //         Vector3 newPos = boxItem.transform.localPosition;
        //         newPos.y=0.16f;
        //         if(dir==0){
        //             newPos = calFallAround(newPos,1);
        //         }
        //         else if(dir==1){
        //             newPos = callFallBeHind(newPos);
        //         }
        //         ParaCurveLcHelp pObj = new ParaCurveLcHelp();
        //         pushToPlcCch(pObj);
        //         pObj.verticalSpeed = 0.8f;
        //         int tmI = i;
        //         pObj.SetParams(boxItem.transform,newPos,0.5f,delegate(){
        //             GameObject.Destroy(boxItem);
        //             pObj=null;
        //         });
        //     }
        //     // Debug.Log(curScore+" "+chdNum);
        // }


        // Invoke("formatCarBox",0.2f);
    }


    //type 1 x  2 z 3 y 
    int calDeltaPosCt(int idx,int type){
        int res = 0;
        // if(type==3){
        //     res = (idx-1)/10;
        // }
        // else if(type==2){
        //     res = idx%10;
        //     if(res==0){
        //         res = 10;
        //     }
        //     if(res>5){
        //         res=1;
        //     }
        //     else{
        //         res=0;
        //     }
        // }
        // else{
        //     res = idx%5;
        //     if(res==0){
        //         res = 5;
        //     }
        //     res-=1;
        // }
        return res;
    }

    float addPointZ = -18;
    int runWayWd = 10;

    float[] rowPoss;
    

    int hItemCt = 26;
    int[][] hindDataArr;
    int[] segArr;

    void loadAngerConfig(){
        TextAsset txtAst =  (TextAsset)Resources.Load("DataTables/BattleAngerDatas");
        int staIdx = 5;
        for(int i=0;i<6;i++){
            string t1 = Util.getRowString(staIdx+i,txtAst.text);
            string[] sRes = t1.Split('\t');
            for(int j=0;j<8;j++){
                angerConfig[i][j] = Convert.ToInt32(sRes[j+2]);
                // Debug.Log(i+" "+j+" "+angerConfig[i][j]);
            }
        }
    }

    void loadTnTbeConfig(){
        TextAsset txtAst =  (TextAsset)Resources.Load("DataTables/BattleTnTbeDatas");
        int staIdx = 5;
        for(int i=0;i<8;i++){
            string t1 = Util.getRowString(staIdx+i,txtAst.text);
            string[] sRes = t1.Split('\t');
            tnTbesConfig[i] = new int[2];
            for(int j=0;j<2;j++){
                // Debug.Log(i+" "+j+" "+tnTbesConfig[i][j]);
                // Debug.Log(Util.GetTimeStamp()+" "+sRes[j+2]);
                tnTbesConfig[i][j] = Convert.ToInt32(sRes[j+2]);
                // Debug.Log(i+" "+Util.GetTimeStamp()+" "+j+" "+tnTbesConfig[i][j]);
            }
        }
    }
    void getLevMapData(){
        // VoodooLog.Log("test",Application.streamingAssetsPath);
        // string lvPath = AssetUtility.GetDataTableAsset("BattleLevelData",false);
        TextAsset txtAst =  (TextAsset)Resources.Load("DataTables/BattleLevelDatas");
        // Debug.Log(txtAst);
        int staIdx = 4+curLevel;
        // Debug.Log(txtAst.text);
        string t1 = Util.getRowString(staIdx,txtAst.text);
        // VoodooLog.Log("test",t1);
        // Debug.Log(t1);
        string[] sRes = t1.Split('\t');
        segArr = null;
        segArr = new int[5];
        finImgIdx = Convert.ToInt32(sRes[2]);
        for(int i=0;i<5;i++){
            // Debug.Log(i+" "+sRes[i+3]);
            segArr[i]=Convert.ToInt32(sRes[i+3]);
            if(i>0&&segArr[i]>0){
                finAddMapNum++;
            }
        }
    }


    Vector2[] mlStaPoss;
    int chfGutPrCt = 6;
    int partItemCt =5;
    int curChfGutPrCt = 0;
    void initHindData(){
        curChfGutPrCt=0;
        // Debug.Log("ihd..."+segArr[curAddMapCt]+" "+curAddMapCt);
        // string dPath = AssetUtility.GetDataTableAsset("BattleHindData"+segArr[curAddMapCt],false);

        // UnityEditor.AssetDatabase.Refresh();
        TextAsset cont = Resources.Load<TextAsset>("DataTables/BattleMapData"+curLevel);     


        int staIdx = 5;
        int endIdx = 5+hItemCt;
        for(int i=0;i<chfGutPrCt;i++){
            mlOriStaPoss[i] = new Vector2(-1,-1);
            mlOriEndPoss[i] = new Vector2(-1,-1);
        }

        for(int i=staIdx;i<endIdx;i++){
            int idx = i-5;
            string t1 = Util.getRowString(i,cont.text);
            // Debug.Log(t1);
            string[] sRes = t1.Split('\t');
            // Debug.Log(sRes.Length);
            hindDataArr[idx] = new int[runWayWd];
            //墙一列
            for(int j=0;j<runWayWd;j++){
                // Debug.Log("jajaja..."+j+" "+idx);
                // Debug.Log("jajaja..."+sRes[j+2]);
                // Debug.Log(hindDataArr[idx]);
                hindDataArr[idx][j]=Convert.ToInt32(sRes[j+2]);

                if(hindDataArr[idx][j]>99){
                    if(hindDataArr[idx][j]/100==7){
                        mlOriStaPoss[hindDataArr[idx][j]/10%10-1] = new Vector2(idx,j);
                        curChfGutPrCt++;
                    }
                }
                else{
                    if(hindDataArr[idx][j]/10==8){
                        mlOriEndPoss[hindDataArr[idx][j]%10-1] = new Vector2(idx,j);
                    }
                }
                // Debug.Log(hindDataArr[idx][j]);
            }
        }
        for(int i=0;i<chfGutPrCt;i++){
            if(mlOriEndPoss[i].x!=-1){
                mlOriEndPoss[i] = chfGuCvtToTbePos(mlOriEndPoss[i]);
            }
        }
    }


    //旋转平台四周填充
    void fillRotPlArdData(){
        for(int i=0;i<roItemIdx;i++){
            fillOneRotPtData(i);
        }
    }

    //面前的桌子
    Vector2 chfGuCvtToTbePos(Vector2 pos){
        int x = (int)pos.x;
        int y = (int)pos.y;
        
        if(x+1<hItemCt){
            if(checkCommWayPt(hindDataArr[x+1][y])){
                return new Vector2(x+1,y);
            }
        }
        if(x-1>=0){
            if(checkCommWayPt(hindDataArr[x-1][y])){
                return new Vector2(x-1,y);
            }
        }

        if(y+1<runWayWd){
            if(checkCommWayPt(hindDataArr[x][y+1])){
                return new Vector2(x,y+1);
            }
        }
        if(y-1>=0){
            if(checkCommWayPt(hindDataArr[x][y-1])){
                return new Vector2(x,y-1);
            }
        }
        return pos;
    }

    bool isChfHint(Vector2 pos){
        int x = (int)pos.x;
        int y = (int)pos.y;

        if(x+1<hItemCt){
            if((hindDataArr[x+1][y]/100==7)&&(hindDataArr[x+1][y]%10==1)){
                return true;
            }
        }
        if(x-1>=0){
            if((hindDataArr[x-1][y]/100==7)&&(hindDataArr[x-1][y]%10==1)){
                return true;
            }
        }

        if(y+1<runWayWd){
            if((hindDataArr[x][y+1]/100==7)&&(hindDataArr[x][y+1]%10==1)){
                return true;
            }
        }
        if(y-1>=0){
            if((hindDataArr[x][y-1]/100==7)&&(hindDataArr[x][y-1]%10==1)){
                return true;
            }
        }

        return false;
    }



    bool oneStepCanToEnd(int mlIdx,Vector2 oldPos,Vector2 newPos){
        if(mlIdx>-1){
            initChectToEnd(oldPos,mlIdx);
            checkCanToEnd(mlIdx,newPos);
            if(!mlCanMvToEnd){
                return false;
            }    
        }
        return true;
    }


    int calChfTbeDir(Vector2 pos,int mlIdx=-1,bool isGus = false){
        int x = (int)pos.x;
        int y = (int)pos.y;

        int[] ways = new int[4];
        for(int i=0;i<4;i++){
            ways[i]=0;
        }
        if(x+1<hItemCt){
            if(checkCommWayPt(hindDataArr[x+1][y],isGus)&&oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x+1,y))){
                ways[0]=1;
            }
            if(x+2<hItemCt){
                if(isGus){
                    if(isTurnPat(hindDataArr[x+2][y])){
                        ways[0]=1;
                    }
                }
                else{
                    if(checkCommWayPt(hindDataArr[x+1][y])){
                        if(isTurnPat(hindDataArr[x+2][y])){
                            if(oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x+2,y))){
                                ways[0]=1;
                            }
                        }
                    }
                }
            }
        }
        if(x-1>=0){
            if(checkCommWayPt(hindDataArr[x-1][y],isGus)&&oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x-1,y))){
                ways[2]=1;
            }
            if(x-2>=0){
                if(isGus){
                    if(isTurnPat(hindDataArr[x-2][y])){
                        ways[2]=1;
                    }
                }
                else{
                    if(checkCommWayPt(hindDataArr[x-1][y])){
                        if(isTurnPat(hindDataArr[x-2][y])){
                            if(oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x-2,y))){
                                ways[2]=1;
                            }
                        }
                    }
                }
            }
        }

        if(y+1<runWayWd){
            if(checkCommWayPt(hindDataArr[x][y+1],isGus)&&oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x,y+1))){
                ways[3]=1;
            }
            if(y+2<runWayWd){
                if(isGus){
                    if(isTurnPat(hindDataArr[x][y+2])){
                        ways[3]=1;
                    }
                }
                else{
                    if(checkCommWayPt(hindDataArr[x][y+1])){
                        if(isTurnPat(hindDataArr[x][y+2])){
                            if(oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x,y+2))){
                                ways[3]=1;
                            }
                        }
                    }
                }
            }
        }
        if(y-1>=0){
            if(checkCommWayPt(hindDataArr[x][y-1],isGus)&&oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x,y-1))){
                ways[1]=1;
            }
            if(y-2>=0){
                if(isGus){
                    if(isTurnPat(hindDataArr[x][y-2])){
                        ways[1]=1;
                    }
                }
                else{
                    if(checkCommWayPt(hindDataArr[x][y-1])){
                        if(isTurnPat(hindDataArr[x][y-2])){
                            if(oneStepCanToEnd(mlIdx,new Vector2(x,y),new Vector2(x,y-2))){
                                ways[1]=1;
                            }
                        }
                    }
                }
            }
        }
        int wayCt=0;
        for(int i=0;i<4;i++){
            if(ways[i]==1){
                wayCt++;
            }
        }
        // Debug.Log("cf..."+wayCt);
        if(wayCt>0){
            int[] ranArr = new int[wayCt];
            int rIdx=0;
            for(int i=0;i<4;i++){
                if(ways[i]==1){
                    ranArr[rIdx]=i+1;
                    rIdx++;
                }
            }
            return ranArr[UnityEngine.Random.Range(0,wayCt)];
        }
        return 0;
    }

    

    Vector2[] mlOriEndPoss;
    Vector2[] mlChfPoss;
    Vector2[] mlOriStaPoss;
    
    Vector2 isPatCetrNear(Vector2 pos){
        if(pos.x-1>=0&&isTurnPat(hindDataArr[(int)pos.x-1][(int)pos.y])){
            return new Vector2(pos.x-1,pos.y);
        } 
        if(pos.x+1<hItemCt&&isTurnPat(hindDataArr[(int)pos.x+1][(int)pos.y])){
            return new Vector2(pos.x+1,pos.y);
        } 
        if(pos.y-1>=0&&isTurnPat(hindDataArr[(int)pos.x][(int)pos.y-1])){
            return new Vector2(pos.x,pos.y-1);
        } 
        if(pos.y+1<runWayWd&&isTurnPat(hindDataArr[(int)pos.x][(int)pos.y+1])){
            return new Vector2(pos.x,pos.y+1);
        } 
        return new Vector2(-1,-1);
    }

    void chgRotCnParent(){
        for(int i=0;i<cnObjCt;i++){
            Vector3 cnObjPos = cnObjs[i].transform.localPosition;
            float posX = mapCoinPosToTbe(1,cnObjPos.x);
            float posZ = mapCoinPosToTbe(2,cnObjPos.z);
            Vector2 cnPos =new Vector2(posZ,posX);
            cnObjOriPoss[i] = cnPos;
            // Debug.Log("chg.pp."+cnObjOriPoss[i]);
            if(hindDataArr[(int)posZ][(int)posX]==26){
                Vector2 nerPtPos = isPatCetrNear(cnPos);
                for(int j=0;j<roItemIdx;j++){
                    if(checkVec2Equ(nerPtPos,rotItemPoss[j])){
                        cnObjs[i].transform.SetParent(rotItems[j].transform);           
                    }
                }
            }
        }
    }

    bool checkVec2Equ(Vector2 a,Vector2 b){
        if(a.x==b.x&&a.y==b.y){
            return true;
        }
        return false;
    }

    int[] hasGuiArr;


    int isNearDes(Vector2 pos,int type=8){
        if(pos.x-1>=0){
            if(hindDataArr[(int)pos.x-1][(int)pos.y]<100){
                if(hindDataArr[(int)pos.x-1][(int)pos.y]/10==type){
                    // Debug.Log("citg1....");
                    int colNum = hindDataArr[(int)pos.x-1][(int)pos.y]%10;
                    return 3+colNum*10;
                }
            }
        }
        if(pos.x+1<hItemCt){
            if(hindDataArr[(int)pos.x+1][(int)pos.y]<100){
                if(hindDataArr[(int)pos.x+1][(int)pos.y]/10==type){
                    // Debug.Log("citg2....");
                    int colNum = hindDataArr[(int)pos.x+1][(int)pos.y]%10;
                    return 1+colNum*10;
                }
            }
        }
        if(pos.y-1>=0){
            if(hindDataArr[(int)pos.x][(int)pos.y-1]<100){
                if(hindDataArr[(int)pos.x][(int)pos.y-1]/10==type){
                    // Debug.Log("citg3....");
                    int colNum = hindDataArr[(int)pos.x][(int)pos.y-1]%10;
                    return 2+colNum*10;
                }
            }
        }
        if(pos.y+1<runWayWd){
            if(hindDataArr[(int)pos.x][(int)pos.y+1]<100){
                if(hindDataArr[(int)pos.x][(int)pos.y+1]/10==type){
                    // Debug.Log("citg4....");
                    int colNum = hindDataArr[(int)pos.x][(int)pos.y+1]%10;
                    return 4+colNum*10;
                }
            }
        }     
        return -1;
    }

    int isPtPanDes(Vector2 pos){
        if(hindDataArr[(int)pos.x][(int)pos.y]<100){
            if(hindDataArr[(int)pos.x][(int)pos.y]/10==4){
                // Debug.Log("citg1....");
                int colNum = hindDataArr[(int)pos.x][(int)pos.y]%10;
                return 3+colNum*10;
            }
        }
        return -1;
    }

    bool checkIsToDes(int idx,int type=8){
        if(type==8&&!guetObjs[idx]){
            return false;
        }

        if(mlMvCurPosIdxs[idx]==0){
            return false;
        } 
        Vector2 pos = mlMvPathPoss[idx][mlMvCurPosIdxs[idx]];
        if(type==8){
            pos.x = mlCvtTbDaToPosDaRevs(2,pos.x);
            pos.y = mlCvtTbDaToPosDaRevs(1,pos.y);
        }
        else{
            pos.x = ptCvtTbDaToPosDaRevs(2,pos.x);
            pos.y = ptCvtTbDaToPosDaRevs(1,pos.y);
        }

       

        // Debug.Log("citg...."+pos);
        if(pos.x==-1){
            pos = mlEndPoss[idx];
        }
        
        int info=0;
        if(type==8){
            info = isNearDes(pos,type);
            if(info>0){
                int col = info/10;
                if(col==idx+1){
                    return true;
                }   
            }
        }
        else{
            info = isPtPanDes(pos);
            // Debug.Log(info+" "+idx);
            if(info>0){
                int col = info/10;
                if(col==idx-chfGutPrCt+1){
                    return true;
                }   
            }
        }
        return false;
    }

    Vector2 checkIsToFat(Vector2 pos){
        if(pos.x-1>=0){
            if(hindDataArr[(int)pos.x-1][(int)pos.y]<100){
                if(hindDataArr[(int)pos.x-1][(int)pos.y]/10==6){
                    return new Vector2(pos.x-1,pos.y);
                }
            }
        }
        if(pos.x+1<hItemCt){
            if(hindDataArr[(int)pos.x+1][(int)pos.y]<100){
                if(hindDataArr[(int)pos.x+1][(int)pos.y]/10==6){
                    return new Vector2(pos.x+1,pos.y);
                }
            }
        }
        if(pos.y-1>=0){
            if(hindDataArr[(int)pos.x][(int)pos.y-1]<100){
                if(hindDataArr[(int)pos.x][(int)pos.y-1]/10==6){
                    return new Vector2(pos.x,pos.y-1);
                }
            }
        }
        if(pos.y+1<runWayWd){
            if(hindDataArr[(int)pos.x][(int)pos.y+1]<100){
                if(hindDataArr[(int)pos.x][(int)pos.y+1]/10==6){
                    return new Vector2(pos.x,pos.y+1);
                }
            }
        }     
        return new Vector2(-1,-1);
    }


    int[] gusHasEatArr;
    public void onMlItemDestory(int idx){
        // Debug.Log("onml des.."+mlMvCurPosIdxs[idx-1]);
        // isMlMvs[idx-1]=false;

        // Vector2 toFatPos = checkIsFatNear(idx-1);
        // float wtTm = 2f;
        // // Debug.Log("omid..."+toFatPos);
        // GuestItem gi = guetObjs[idx-1].GetComponent<GuestItem>();
        // MealItem mi = mlMvObjs[idx-1].GetComponent<MealItem>();
        // Button taBtn = mlBtnPl.transform.Find("mlBtn"+idx).GetComponent<Button>();
        // taBtn.enabled = false;
        // if(checkIsToDes(idx-1)){
        //     // Debug.Log("ccc...");
        //     // gi.playEat();
        //     wtTm = 4f;
        //     gusHasEatArr[idx-1]=1;
        // }
        // else if(toFatPos.x!=-1){
        //     // Debug.Log("111");
        //     FatItem fi = fatObjs[(int)toFatPos.x][(int)toFatPos.y].GetComponent<FatItem>();
        //     fi.playEat();
        //     wtTm = 4f;
        //     gi.playAngry();
        //     panHasBkOs = true;
        // }
        // else{
        //     gi.playAngry();
        //     mi.playPanBreak();
        //     panHasBkOs = true;
        // }

        // Sequence wtSeq = DOTween.Sequence();
        // wtSeq.AppendInterval(wtTm);
        // wtSeq.AppendCallback(delegate(){
        //     curMvMlCt--;
        //     GameObject.Destroy(mlMvObjs[idx-1]); 
        //     mlMvObjs[idx-1]=null;
        //     if(checkIsWin()){
        //         gameWin();
        //     }
        //     else if(gusHasEatArr[idx-1]==1){

        //     }
        //     else{
        //         taBtn.enabled = true;
        //         GameObject colItem = ndColPl.transform.GetChild(idx+3).gameObject;
        //         Button mkBtn = colItem.transform.Find("btn").GetComponent<Button>();
        //         mkBtn.enabled = true;
        //         taBtn.onClick.RemoveAllListeners();
        //         taBtn.onClick.AddListener(delegate(){
        //             chfMkNewMlDeal(idx);
        //         });
        //         mkNwMlObjs[idx-1].SetActive(true);
        //     }
        // });
        // wtSeq.SetAutoKill();
    }   
    bool checkIsWin(){
        int ct=0;
        for(int i=0;i<chfGutPrCt;i++){
            // Debug.Log("ciw.."+i+" "+gusHasEatArr[i]);
            if(gusHasEatArr[i]==1){
                ct++;
            }
        }
        return ct==curChfGutPrCt;
    }

    void desPartItem(int idx){
        isMlMvs[idx-1]=false;
        int cIdx = idx - chfGutPrCt-1;
        //按下
        GameObject ptBtnUp = partBtnObjs[cIdx].transform.Find("anniu/up").gameObject;
        //下降 2->0.6
        GameObject ptCubObj = partCubeObjs[cIdx];

        MealItem mi = mlMvObjs[idx-1].GetComponent<MealItem>();
        mi.playPanBreak();

        if(checkIsToDes(idx-1,4)){
            Sequence mvSeq = DOTween.Sequence();
            mvSeq.AppendInterval(0.5f);
            mvSeq.AppendCallback(delegate(){
                playGmSound(2,"part_btn");
            });
            mvSeq.Append(ptBtnUp.transform.DOLocalMoveY(0,0.2f));
            mvSeq.Append(ptCubObj.transform.DOLocalMoveY(1.41f,0.2f));
            mvSeq.AppendCallback(delegate(){
                GameObject.Destroy(partBtnObjs[cIdx]);
                GameObject.Destroy(mlMvObjs[idx-1]);
                mlMvObjs[idx-1]=null;
                BoxCollider bci = ptCubObj.transform.GetChild(0).GetComponent<BoxCollider>(); 
                bci.enabled = false;
            });
            mvSeq.SetAutoKill();
        }        
        else{
            Sequence mvSeq = DOTween.Sequence();
            mvSeq.AppendInterval(0.5f);
            mvSeq.AppendCallback(delegate(){
                GameObject.Destroy(mlMvObjs[idx-1]);
                createPartPan((int)mlOriStaPoss[idx-1].x,(int)mlOriStaPoss[idx-1].y);
            });
            mvSeq.SetAutoKill();
        }     
    }


    public static Action<float> onRotBarHitAc;

    void onRotBarHitCb(float height){
        int boxCt = mnRoleObj.transform.childCount-1;
        int heiCt = 50+10*((int)height-1);
        // Debug.Log(boxCt+" "+heiCt);

        if(boxCt>heiCt){
            int fallCt = boxCt-heiCt;
            subCarBox(fallCt,-1,1);
        }
    }

    void resetBtnCb(){
        // initUI(true);
    }


    //蓝 红 绿 黄
    string[] finRmNms = new string[]{"bathRm","bookRm","slepRm","salonRm"};
    Vector3[] rmPoss = new Vector3[]{new Vector3(-7.2f,1.6f,-2.5f),new Vector3(0.1f,1.44f,-4.9f),new Vector3(0.14f,1.45f,-2.4f),new Vector3(8.57f,1.45f,-2.46f)};
    RoomItem finRItm;
    GameObject doorObj;

    float[] scoBdPosZ = new float[]{19.3f,20.3f,19.3f,19.3f};
    float[] scoBdPosY = new float[]{0.3f,0.3f,0.4f,0.5f};
    GameObject finScoBdObj;
    
    // 11 怒气浴室 12 普通浴室
    // 31 怒气卧室 32 普通卧室
    // 41 怒气客厅 42 普通客厅
    // 长度四个单位

    bool[] isMlMvs;
    Vector3[] mlMvCurPoss;
    int[] mlMvCurPosIdxs;
    GameObject[] mlMvObjs;

    // float[] mv

    ///1 横 2 竖
    float mlCvtTbDaToPosDa(int type,float pos){
        if(type==1){
            return 2.4f*pos-8.4f;
        }
        else{
            return -2.4f*pos+50f;
        }
    }

    float mlCvtTbDaToPosDaRevs(int type,float pos){
        if(type==1){
            return (pos+8.4f)/2.4f;
        }
        else{
            return (pos-50f)/-2.4f;
        }
    }

    float ptItCvtTbDaToPosDa(int type,float pos){
        if(type==1){
            return 2.4f*pos-10.68f;
        }
        else{
            return -2.4f*pos+50f;
        }
    }

    float ptCvtTbDaToPosDaRevs(int type,float pos){
        if(type==1){
            return (pos+10.68f)/2.4f;
        }
        else{
            return (pos-50f)/-2.4f;
        }
    }


    Vector2[] mlEndPoss;
    void mlInitMvRoute(int idx,bool isPart = false){
        // int chfTbeDir = calChfTbeDir(mlOriStaPoss[idx],idx);
        // // Debug.Log("mrrrr..."+mlMvDir);
        // mlMvDir = chfTbeDir;
        // Vector2 staPos = new Vector2(mlOriStaPoss[idx].x,mlOriStaPoss[idx].y);
        // if(!isPart){
        //     staPos = chfGuCvtToTbePos(staPos);
        // }
        // calWayPath2(staPos,idx);

        // for(int i=0;i<mlMvPathIdxs[idx];i++){
        //     if(!isPart){
        //         mlMvPathPoss[idx][i].x = mlCvtTbDaToPosDa(2,mlMvPathPoss[idx][i].x);
        //         mlMvPathPoss[idx][i].y = mlCvtTbDaToPosDa(1,mlMvPathPoss[idx][i].y);
        //     }
        //     else{
        //         float ptX = mlMvPathPoss[idx][i].y;
        //         float ptY = mlMvPathPoss[idx][i].x;
        //         mlMvPathPoss[idx][i].x = ptItCvtTbDaToPosDa(2,ptY);
        //         mlMvPathPoss[idx][i].y = ptItCvtTbDaToPosDa(1,ptX);
        //     }
        // }
    }

    GameObject flickObj = null;

    //转台也是路
    bool checkCommWayPt(int val,bool isGus=false){
        if(val>99){
            if(val/100==3){
                return true;
            }
            if(val/100==5){
                return true;
            }
        }
        else{
            if(val/10==1){
                return true;
            }
            if(val/10==2){
                return true;
            }
            if(val/10==9){
                if(isGus){
                    return true;
                }
                if(partCubeObjs[val%10-1]!=null){
                    float posY = partCubeObjs[val%10-1].transform.localPosition.y;
                    if(posY==3.3f){
                        return false;
                    }
                }
                return true;
            }
            if(val/10==4){
                return true;
            }
        }
        return false;
    }

    bool isTurnPat(int val){
        if(val>99){
            if(val/100==3){
                return true;
            }
        }
        return false;
    }

    // 1 down 2 left 3 up 4 right
    int[] calCanMvDir(Vector2 curPos,int mlIdx,bool isCheck = false){
        int[] res = new int[4]{0,0,0,0};
        if(curPos.x+1<hItemCt&&checkCommWayPt(hindDataArr[(int)curPos.x+1][(int)curPos.y])){
            if(mlHasVisArr[mlIdx][(int)curPos.x+1][(int)curPos.y]==0){
                if(isCheck){
                    if(!checkInVisPoss(mlIdx,(int)curPos.x+1,(int)curPos.y)){
                        res[0]=1;
                    }
                }
                else{
                    res[0]=1;    
                }
            }
            // Debug.Log(res[3]);
        }
        if(curPos.x-1>=0&&checkCommWayPt(hindDataArr[(int)curPos.x-1][(int)curPos.y])){
            if(mlHasVisArr[mlIdx][(int)curPos.x-1][(int)curPos.y]==0){
                if(isCheck){
                    if(!checkInVisPoss(mlIdx,(int)curPos.x-1,(int)curPos.y)){
                        res[2]=1;
                    }
                }
                else{
                    res[2]=1;    
                }
            }
            // Debug.Log(res[1]);
        }
        if(curPos.y+1<runWayWd&checkCommWayPt(hindDataArr[(int)curPos.x][(int)curPos.y+1])){
            if(mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y+1]==0){
                if(isCheck){
                    if(!checkInVisPoss(mlIdx,(int)curPos.x,(int)curPos.y+1)){
                        res[3]=1;
                    }
                }
                else{
                    res[3]=1;    
                }
            }
            // Debug.Log(res[2]);
        }
        if(curPos.y-1>=0&&checkCommWayPt(hindDataArr[(int)curPos.x][(int)curPos.y-1])){
            if(mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y-1]==0){
                if(isCheck){
                    if(!checkInVisPoss(mlIdx,(int)curPos.x,(int)curPos.y-1)){
                        res[1]=1;
                    }
                }
                else{
                    res[1]=1;    
                }

            }
            // Debug.Log(res[0]);
        }
        return res;
    }

    int isMvWayOnly(int[] dirs){
        int ct=0;
        for(int i=0;i<4;i++){
            if(dirs[i]>0){
                ct++;
            }
        }
        if(ct==1){
            for(int i=0;i<4;i++){
                if(dirs[i]>0){
                    return i;
                }
            }
        }
        return -1;
    }

    
    // 1 down 2 left 3 up 4 right
    Vector2 mlMvOneStp(int mlIdx,Vector2 curPos,int dir,bool isCheck = false){
        if(dir==0){
            curPos.x+=1;
        }
        else if(dir==1){
            curPos.y-=1;
        }
        else if(dir==2){
            curPos.x-=1;
        }
        else if(dir==3){
            curPos.y+=1;
        }

        //没走过，才能走
        if(mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y]==0){
            if(isCheck){
                if(!checkInVisPoss(mlIdx,(int)curPos.x,(int)curPos.y)){
                    return curPos;
                }
            }
            else{
                return curPos;
            }
        }
        return new Vector2(-1,-1);
    }


    //1 竖 2 横
    int cvtToDirIdx(int type,int val){
        if(type==1){
            if(val>0){
                return 0;
            }
            else{
                return 2;
            }
        }
        else{
            if(val>0){
                return 3;
            }
            else{
                return 1;
            }
        }
    }


    // 1 down 2 left 3 up 4 right
    void mlCloseOtrSta(int mlIdx,Vector2 curPos,int dir){
        if(dir==0){
            if((int)curPos.x-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x-1][(int)curPos.y]=1;
            }
            if((int)curPos.y+1<runWayWd){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y+1]=1;
            }
            if((int)curPos.x+1<hItemCt){
                mlHasVisArr[mlIdx][(int)curPos.x+1][(int)curPos.y]=1;
            }
        }
        else if(dir==1){

            if((int)curPos.y-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y-1]=1;
            }

            if((int)curPos.y+1<runWayWd){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y+1]=1;
            }
            if((int)curPos.x+1<hItemCt){
                mlHasVisArr[mlIdx][(int)curPos.x+1][(int)curPos.y]=1;
            }

        }
        else if(dir==2){
            if((int)curPos.y-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y-1]=1;
            }
            if((int)curPos.x+1<hItemCt){
                mlHasVisArr[mlIdx][(int)curPos.x+1][(int)curPos.y]=1;
            }

            if((int)curPos.x-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x-1][(int)curPos.y]=1;
            }
        }
        else{
            if((int)curPos.y-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y-1]=1;
            }
            if((int)curPos.y+1<runWayWd){
                mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y+1]=1;
            }
            if((int)curPos.x-1>=0){
                mlHasVisArr[mlIdx][(int)curPos.x-1][(int)curPos.y]=1;
            }
        }
    }


    bool checkIsNotWay(int val){
        if(val>99){
            if(val/100==7){
                return true;
            }
        }
        else{
            if(val==0||val/10==8||val/10==6){
                return true;
            }
        }
        return false;
    }


    bool checkIsEndPt(Vector2 pos,int mlIdx){
        if(pos.x==mlOriEndPoss[mlIdx].x&&pos.y==mlOriEndPoss[mlIdx].y){
            return true;
        }

        int wDyCt = 0;
        if(pos.x-1>=0){
            if(checkIsNotWay(hindDataArr[(int)pos.x-1][(int)pos.y])){
                wDyCt++;
            }
        }
        if(pos.x+1<hItemCt){
            if(checkIsNotWay(hindDataArr[(int)pos.x+1][(int)pos.y])){
                wDyCt++;
            }
        }
        if(pos.y-1>=0){
            if(checkIsNotWay(hindDataArr[(int)pos.x][(int)pos.y-1])){
                wDyCt++;
            }
        }
        if(pos.y+1<runWayWd){
            if(checkIsNotWay(hindDataArr[(int)pos.x][(int)pos.y+1])){
                wDyCt++;
            }
        }        
        if(wDyCt==3){
            return true;
        }
        return false;
    }


    Vector2 mlCalNextWayPt(Vector2 pos,int mlIdx,bool isFirst = false){
        isFirst = false;
        Vector2 onPtRes = mlIsOnlyPt(pos,mlIdx);
        if(onPtRes.x!=-1||onPtRes.x==-2){
            return onPtRes;
        }

        int ptsIdx = 0;
        Vector2[] poss = new Vector2[3];
        int[] dirs = new int[3];

        if(mlMvDir==4){
            if(isFirst){
                if(pos.y+1<runWayWd){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y+1);
                        dirs[ptsIdx]=4;   
                        ptsIdx++;
                    }
                }
            }
            else{
                if(pos.y+1<runWayWd){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y+1);
                        dirs[ptsIdx]=4;   
                        ptsIdx++;
                    }
                }
                if(pos.x+1<hItemCt){
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x+1,pos.y);
                        dirs[ptsIdx]=1;
                        ptsIdx++;
                    }
                }
                if(pos.x-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x-1,pos.y);
                        dirs[ptsIdx]=3;
                        ptsIdx++;
                    }
                }
            }
        }    
        else if(mlMvDir==3){
            if(isFirst){
                if(pos.x-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x-1,pos.y);
                        dirs[ptsIdx]=3;
                        ptsIdx++;
                    }
                }
            }
            else{   
                if(pos.y+1<runWayWd){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y+1);
                        dirs[ptsIdx]=4;
                        ptsIdx++;
                    }
                }
                if(pos.x-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x-1,pos.y);
                        dirs[ptsIdx]=3;
                        ptsIdx++;
                    }
                }
                if(pos.y-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y-1);
                        dirs[ptsIdx]=2;
                        ptsIdx++;
                    }
                }
            }
        }
        else if(mlMvDir==2){
            if(isFirst){
                if(pos.y-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y-1);
                        dirs[ptsIdx]=2;
                        ptsIdx++;
                    }
                }
            }
            else{
                if(pos.y-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y-1);
                        dirs[ptsIdx]=2;
                        ptsIdx++;
                    }
                }
                if(pos.x+1<hItemCt){
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])){
                            poss[ptsIdx]=new Vector2(pos.x+1,pos.y);
                            dirs[ptsIdx]=1;
                            ptsIdx++;
                        }
                    }
                if(pos.x-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x-1,pos.y);
                        dirs[ptsIdx]=3;
                        ptsIdx++;
                    }
                }
            }
        }
        else{
            if(isFirst){
                if(pos.x+1<hItemCt){
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x+1,pos.y);
                        dirs[ptsIdx]=1;
                        ptsIdx++;
                    }
                }
            }
            else{
                if(pos.y+1<runWayWd){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y+1);
                        dirs[ptsIdx]=4;
                        ptsIdx++;
                    }
                }
                if(pos.x+1<hItemCt){
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])){
                        poss[ptsIdx]=new Vector2(pos.x+1,pos.y);
                        dirs[ptsIdx]=1;
                        ptsIdx++;
                    }
                }
                if(pos.y-1>=0){
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])){
                        poss[ptsIdx]=new Vector2(pos.x,pos.y-1);
                        dirs[ptsIdx]=2;
                        ptsIdx++;
                    }
                }
            }
        }

        int canToEndCt=0;
        // 1 能到 2 有胖子
        Vector2[] wayInfos = new Vector2[ptsIdx];
        for(int i=0;i<ptsIdx;i++){
            wayInfos[i]=new Vector2(0,0);
        }
        // Debug.Log("hehe222 "+ptsIdx);
        for(int i=0;i<ptsIdx;i++){
            initChectToEnd(pos,mlIdx);
            // Debug.Log("rrr..."+poss[i]+" "+mlIdx);
            checkCanToEnd(mlIdx,poss[i]);
            // Debug.Log(mlCanMvToEnd+" "+mlWayHasFat);
            if(mlCanMvToEnd){
                wayInfos[i].x = 1;
            }
            else{
                canToEndCt++;
            }
            if(mlWayHasFat){
                wayInfos[i].y = 1;
            }
        }
        // for(int i=0;i<ptsIdx;i++){
        //     Debug.Log(wayInfos[i]);
        // }

        for(int i=0;i<ptsIdx;i++){
            if(wayInfos[i].x==1&&wayInfos[i].y==0){
                mlMvDir = dirs[i];
                return poss[i];
            }
        }
        for(int i=0;i<ptsIdx;i++){
            if(wayInfos[i].x==1){
                mlMvDir = dirs[i];
                return poss[i];
            }
        }


        if(canToEndCt==ptsIdx){
            int ranIdx = UnityEngine.Random.Range(0,ptsIdx);
            // Debug.Log("ranidx.."+ranIdx+" "+mlIdx+" "+ptsIdx);
            mlMvDir = dirs[ranIdx];
            return poss[ranIdx];
        }
        return new Vector2(-1,-1);
    }

    

    int mlCalConnPtCt(Vector2 pos){
        int ct=0;

       if(pos.x-1>=0){
            if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])){
                ct++;
            }
        }
        if(pos.x+1<hItemCt){
            if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])){
                ct++;
            }
        }
        if(pos.y-1>=0){
            if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])){
                ct++;
            }
        }
        if(pos.y+1<runWayWd){
            if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])){
                ct++;
            }
        }       
        return ct;
    }

    Vector2 doNearMove(Vector2 pos,int dir){
        if(dir==1){
            return new Vector2(pos.x+1,pos.y);
        }
        else if(dir==2){
            return new Vector2(pos.x,pos.y-1);
        }
        else if(dir==3){
            return new Vector2(pos.x-1,pos.y);
        }
        else{
            return new Vector2(pos.x,pos.y+1);
        }
    }


    Vector2 mlIsOnlyPt(Vector2 pos,int mlIdx){
        int cnCt = mlCalConnPtCt(pos);

        int[] ways = new int[4];
        for(int i=0;i<4;i++){
            ways[i]=0;
        }

        if(mlMvDir==4){
            if(cnCt==1){
                mlMvDir = calChfTbeDir(pos);
                return doNearMove(pos,mlMvDir);
            }
            else if(cnCt==2){
                if(pos.y+1<runWayWd){
                    Vector2 newPos = new Vector2(pos.x,pos.y+1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        ways[3]=1;
                        // return new Vector2(pos.x,pos.y+1);
                    }
                }       
                if(pos.x+1<hItemCt){
                    Vector2 newPos = new Vector2(pos.x+1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir = 1;
                        ways[0]=1;
                        // return new Vector2(pos.x+1,pos.y);
                    }
                }
                if(pos.x-1>=0){
                    Vector2 newPos = new Vector2(pos.x-1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=3;
                        // return new Vector2(pos.x-1,pos.y);
                        ways[2]=1;
                    }
                }
            }
        }
        else if(mlMvDir==3){
            if(cnCt==1){
                mlMvDir = calChfTbeDir(pos);
                return doNearMove(pos,mlMvDir);
            }
            else if(cnCt==2){
                if(pos.x-1>=0){
                    Vector2 newPos = new Vector2(pos.x-1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // return new Vector2(pos.x-1,pos.y);
                        ways[2]=1;
                    }
                }
                if(pos.y-1>=0){
                    Vector2 newPos = new Vector2(pos.x,pos.y-1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=2;
                        // return new Vector2(pos.x,pos.y-1);
                        ways[1]=1;
                    }
                }
                if(pos.y+1<runWayWd){
                    Vector2 newPos = new Vector2(pos.x,pos.y+1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=4;
                        // return new Vector2(pos.x,pos.y+1);
                        ways[3]=1;
                    }
                }       
            }
        }
        else if(mlMvDir==2){
            if(cnCt==1){
                mlMvDir = calChfTbeDir(pos);
                return doNearMove(pos,mlMvDir);
            }
            else if(cnCt==2){
                if(pos.y-1>=0){
                    Vector2 newPos = new Vector2(pos.x,pos.y-1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // return new Vector2(pos.x,pos.y-1);
                        ways[1]=1;
                    }
                }
                if(pos.x-1>=0){
                    Vector2 newPos = new Vector2(pos.x-1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x-1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=3;
                        // return new Vector2(pos.x-1,pos.y);
                        ways[2]=1;
                    }
                }
                if(pos.x+1<hItemCt){
                    Vector2 newPos = new Vector2(pos.x+1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=1;
                        // return new Vector2(pos.x+1,pos.y);
                        ways[0]=1;
                    }
                }       
            }
        }
        else {
            if(cnCt==1){
                mlMvDir = calChfTbeDir(pos);
                return doNearMove(pos,mlMvDir);   
            }
            else if(cnCt==2){
                if(pos.x+1<hItemCt){
                    Vector2 newPos = new Vector2(pos.x+1,pos.y);
                    if(checkCommWayPt(hindDataArr[(int)pos.x+1][(int)pos.y])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // return new Vector2(pos.x+1,pos.y);
                        ways[0]=1;
                    }
                }     
                if(pos.y-1>=0){
                    Vector2 newPos = new Vector2(pos.x,pos.y-1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y-1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=2;
                        // return new Vector2(pos.x,pos.y-1);
                        ways[1]=1;
                    }
                }
                if(pos.y+1<runWayWd){
                    Vector2 newPos = new Vector2(pos.x,pos.y+1);
                    if(checkCommWayPt(hindDataArr[(int)pos.x][(int)pos.y+1])&&oneStepCanToEnd(mlIdx,pos,newPos)){
                        // mlMvDir=4;
                        // return new Vector2(pos.x,pos.y+1);
                        ways[3]=1;
                    }
                }       
            }
        }

        int wayCt=0;
        for(int i=0;i<4;i++){
            if(ways[i]==1){
                wayCt++;
            }
        }
        if(wayCt>0){
            int[] ranArr = new int[wayCt];
            int rIdx=0;
            for(int i=0;i<4;i++){
                if(ways[i]==1){
                    ranArr[rIdx]=(i+1);
                    rIdx++;
                }
            }
            int dir = ranArr[UnityEngine.Random.Range(0,wayCt)];
            if(dir==1){
                mlMvDir=1;
                return new Vector2(pos.x+1,pos.y); 
            }   
            else if(dir==2){
                mlMvDir=2;
                return new Vector2(pos.x,pos.y-1); 
            }
            else if(dir==3){
                mlMvDir=3;
                return new Vector2(pos.x-1,pos.y); 
            }
            else{
                mlMvDir=4;
                return new Vector2(pos.x,pos.y+1); 
            }
        }

        //转台没路了
        if(cnCt==0){
            return new Vector2(-2,-2);
        }
        //多条路
        return new Vector2(-1,-1);
    }


    Vector2 mlCurPos;
    //dn left up right
    int mlMvDir = 4;
    void calWayPath2(Vector2 curPos,int mlIdx){
        // Debug.Log(curPos+" "+mlIdx+" "+mlOriEndPoss[mlIdx]);

        mlMvPathPoss[mlIdx] = new Vector2[80];
        mlMvPathIdxs[mlIdx]=0;

        mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]] = curPos;
        mlMvPathIdxs[mlIdx]++;

        mlCurPos = mlCalNextWayPt(curPos,mlIdx,true);
        if(mlCurPos.x==-2){
            mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]] = curPos;
            mlMvPathIdxs[mlIdx]++;
            return;
        }

        // Debug.Log("c222..."+mlIdx+" "+mlCurPos);


        if(checkIsEndPt(mlCurPos,mlIdx)){
            mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]] = mlCurPos;
            mlMvPathIdxs[mlIdx]++;
            return;
        }
        else{
            while(!checkIsEndPt(mlCurPos,mlIdx)){
                // if(mlIdx==1){
                //     Debug.Log("cal....2"+mlCurPos+" "+mlMvPathIdxs[mlIdx]+" "+mlIdx);
                // }
                mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]] = mlCurPos;
                mlMvPathIdxs[mlIdx]++;
                mlCurPos = mlCalNextWayPt(mlCurPos,mlIdx);
            }
            mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]] = mlCurPos;
            mlMvPathIdxs[mlIdx]++;
            // if(mlIdx==1){
            //     Debug.Log("cal....3"+mlCurPos+" "+mlMvPathIdxs[mlIdx]+" "+mlIdx);
            // }
            if(mlOriEndPoss[mlIdx].x==-1){
                mlEndPoss[mlIdx] = mlCurPos;
            }
        }
    }
    

    Vector2[][] mlMvPathPoss;    
    int[] mlMvPathIdxs;
    void mlMvStrtWay(int mlIdx,Vector2 curPos,bool isFirst = false,int dir=-1){
        // Debug.Log("ml.."+curPos+" "+mlIdx);
        // if(isFirst){
        //     initMlMvHah(mlIdx);
        //     mlMvPathPoss[mlIdx] = new Vector2[80];
        //     mlMvPathIdxs[mlIdx]=0;
        // }
        // checkCanToEnd(mlIdx,curPos,true);
        // // Debug.Log(mlCanMvToEnd);
        // if(!mlCanMvToEnd){
        //     return;
        // }
        // else{
        //     mlMvPathPoss[mlIdx][mlMvPathIdxs[mlIdx]]=curPos;
        //     mlMvPathIdxs[mlIdx]++;
        // }

        // if(dir!=-1){
        //     mlCloseOtrSta(mlIdx,curPos,dir);
        // }
        // int[] curCnDir = calCanMvDir(curPos,mlIdx);

        // mlHasVisArr[mlIdx][(int)curPos.x][(int)curPos.y]=1;

        // for(int i=0;i<4;i++){
        //     // Debug.Log(curCnDir[i]+" "+Util.GetTimeStamp()+" "+i);
        //     if(curCnDir[i]>0){
        //         Vector2 newPos = mlMvOneStp(mlIdx,curPos,i);
        //         if(newPos.x!=-1){
        //             mlMvStrtWay(mlIdx,newPos);
        //         }
        //     }
        // }
    }

    
    bool checkInVisPoss(int mlIdx,int x,int y){
        for(int i=0;i<mlVisPosIdxs[mlIdx];i++){
            if(mlVisPoss[mlIdx][i].x==x&&mlVisPoss[mlIdx][i].y==y){
                return true;
            }
        }
        return false;
    }

    Vector2[][] mlVisPoss;
    int[] mlVisPosIdxs;
    bool mlCanMvToEnd =false;

    void initChectToEnd(Vector2 pos,int mlIdx){
        mlCanMvToEnd =false;
        mlWayHasFat = false;
        mlVisPoss[mlIdx] = new Vector2[100];
        mlVisPosIdxs[mlIdx]=0;    

        mlVisPoss[mlIdx][mlVisPosIdxs[mlIdx]]=pos;
        mlVisPosIdxs[mlIdx]++;
    }

    bool mlWayHasFat = false;

    void checkCanToEnd(int mlIdx,Vector2 curPos,bool isfirst=false){
        if(curPos.x==mlOriEndPoss[mlIdx].x&&curPos.y==mlOriEndPoss[mlIdx].y){
            mlCanMvToEnd=true;
            return;
        }

        if(mlIdx==1){
            // Debug.Log("chc..."+curPos+" "+mlIdx+" "+mlOriEndPoss[mlIdx]);
        }
        // Debug.Log("chc..."+curPos+" "+mlIdx+" "+mlOriEndPoss[mlIdx]);
        if(isfirst){
            mlCanMvToEnd =false;
            mlWayHasFat = false;
            mlVisPoss[mlIdx] = new Vector2[100];
            mlVisPosIdxs[mlIdx]=0;
        }

        if(checkIsToFat(curPos).x!=-1){
            // Debug.Log("pppp.."+curPos);
            mlWayHasFat = true;
        }
        mlVisPoss[mlIdx][mlVisPosIdxs[mlIdx]]=curPos;
        mlVisPosIdxs[mlIdx]++;

        int[] curCnDir = calCanMvDir(curPos,mlIdx,true);
        for(int i=0;i<4;i++){
            // Debug.Log(curPos+" "+i+" "+curCnDir[i]+" "+Util.GetTimeStamp());
            if(curCnDir[i]>0){
                Vector2 newPos = mlMvOneStp(mlIdx,curPos,i,true);
                if(newPos.x==mlOriEndPoss[mlIdx].x&&newPos.y==mlOriEndPoss[mlIdx].y){
                    // Debug.Log("chc2...");
                    mlCanMvToEnd = true;
                }
                if(newPos.x!=-1){
                    checkCanToEnd(mlIdx,newPos);
                }
            }
        }
    }


    int[][][] mlHasVisArr;
    void initMlMvHah(int mlIdx){
        for(int i=0;i<hItemCt;i++){
            for(int j=0;j<runWayWd;j++){
                mlHasVisArr[mlIdx][i][j]=0;
                
            }
        }
    }

    void mtMvCtrler(){
        
    }

    void calToDstArr(){
        // only way 直接走
        // 1 down 2 left 3 up 4 right
        //sta (14,3) end (20,6)
        //分叉判断

        Vector2 staPos = new Vector2(14,3);
        Vector2 endPos = new Vector2(20,6);
        Vector2 curPos = new Vector2(14,3);

        int[] stepArr = new int[50];
        int curStp = 0;
        //下 正 右 正
        int yDir = Math.Sign(endPos.x-staPos.x);
        int xDir = Math.Sign(endPos.y-staPos.y);

        int[] totDirs = new int[2];
        totDirs[0] = cvtToDirIdx(1,yDir);
        totDirs[1] = cvtToDirIdx(2,xDir);

        for(int i=0;i<50;i++){
            stepArr[i]=-1;
        }

        // while(curPos.x!=endPos.x&&curPos.y!=endPos.y){
        //     int[] curCnDir = calCanMvDir(curPos);
        //     int isOyIdx =isMvWayOnly(curCnDir);
        //     if(isOyIdx>-1){
        //         curPos = mlMvOneStp(curPos,curCnDir[isOyIdx]);
        //         stepArr[curStp]=isOyIdx;
        //         curStp++;
        //     }
        //     else{
                               
        //     }
        // }
        // for(int i=0;i<50;i++){
        //     Debug.Log(Util.GetTimeStamp()+" "+stepArr[i]);
        // }
    }

    bool isRotPtIng = false;
    void changeRotItemEng(GameObject item,float val,int idx,bool isFirst = false){
        if(isRotPtIng){
            return;
        }
        isRotPtIng = true;
        if(flickObj!=null&&flickObj.name=="item3(Clone)"){
            RotPtItem rpi = item.GetComponent<RotPtItem>();
            rpi.stopFlick();
            stopGuideFing();
        }

        // Debug.Log("cr..."+idx);
        Vector3 enAng = item.transform.eulerAngles;
        enAng.y+=val;

        if(isFirst){
            item.transform.eulerAngles = enAng;
            isRotPtIng = false;
        }
        else{
            Sequence rotSeq = DOTween.Sequence();
            rotSeq.Append(item.transform.DOLocalRotate(enAng,0.3f));
            rotSeq.AppendCallback(delegate(){
                isRotPtIng = false;
                updHidArrByRotItem(idx);   
            });
            rotSeq.SetAutoKill();
        }
    }

    

    void updHidArrByRotItem(int idx,bool isFirst = false){
        if(isFirst){
            return;
        }
        float curAng = rotItems[idx].transform.eulerAngles.y;
        // Debug.Log("upd..."+curAng);
        curAng = Util.formatAng(curAng);

        rotItemTurnDeal(curAng,idx);
        refreshMapCoin();
    }

    void fillOneRotPtData(int idx){
        int val = hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y];

        int dir = val/10%10;
        //直
        if(rotItemTps[idx]==1){
            if(dir==1){
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
                }
            }
            else{
                if(hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
                }
            }
        }
        else{
            if(dir==6){
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
                }
            }
            else if(dir==7){
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
                }
            }
            else if(dir==8){
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
                }
            }
            else{
                if(hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]==0){
                    hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
                }
                if(hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]==0){
                    hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
                }
            }
        }
        
    }

    void rotItemTurnDeal(float curAng,int idx){

        int tempVal = hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y];
        hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y] = hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1];
        hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1] = hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y];
        hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y] = hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1];
        hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1] = tempVal;

        // if(Mathf.Abs(curAng)<0.1f){
            
        //     hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=0;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=0;
        // }
        // else if(Mathf.Abs(curAng-90)<0.1f){
        //     hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=0;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=0;
        // }
        // else if(Mathf.Abs(curAng-180)<0.1f){
        //     hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=0;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=0;
        // }
        // else{
        //     hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
        //     hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=0;
        //     hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=0;
        // }
    }

    void rotItemStrtDeal(float curAng,int idx){
        if(Mathf.Abs(curAng)<0.1f||Mathf.Abs(curAng-180)<0.1f){
            hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=10;
            hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=10;
            hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=0;
            hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=0;
        }
        else{
            hindDataArr[(int)rotItemPoss[idx].x+1][(int)rotItemPoss[idx].y]=10;
            hindDataArr[(int)rotItemPoss[idx].x-1][(int)rotItemPoss[idx].y]=10;
            hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y+1]=0;
            hindDataArr[(int)rotItemPoss[idx].x][(int)rotItemPoss[idx].y-1]=0;
        }
    }

    //10 直道
    //20 金币
    //3x 旋转平台(31 直线 32 直角)
    //40 隔断灯(个位为编号 41 42)
    //50 隔断盘(个位为编号 51 52) 
    //60 捣乱的食客 61 下 62 左
    //70 厨师(个位为编号 71 72)
    //80 客人(个位为编号 81 82)
    //90 隔断块(个位为编号 91 92)
    
    GameObject[] chiefObjs;
    GameObject[] guetObjs;
    float[] gusEmjCds;

    GameObject[][] fatObjs;
    

    Vector2[] rotItemPoss = new Vector2[10];
    GameObject[] rotItems = new GameObject[10];
    int[] rotItemTps = new int[10];
    int roItemIdx = 0;


    public void onClickRotItem(int idx){
        if(curMvMlCt>0){
            return;
        }
        playGmSound(2,"rotPlRot");
        GameObject boxItem = rotItems[idx];
        changeRotItemEng(boxItem,90,idx);
    }

    public void onStaPatCbMv(GameObject panItem,int index,int i,int ptIdx){
        // Debug.Log("ospc.."+index+" "+i+" "+ptIdx);
        // mlOriStaPoss[chfGutPrCt+ptIdx] = new Vector2(index,i);
        // mlStaPoss[chfGutPrCt+ptIdx] = new Vector2(mlOriStaPoss[chfGutPrCt+ptIdx].x,mlOriStaPoss[chfGutPrCt+ptIdx].y);

        // mlOriEndPoss[chfGutPrCt+ptIdx] = partBtnPoss[ptIdx];

        // mlMvObjs[chfGutPrCt+ptIdx] = panItem;
        // mlInitMvRoute(chfGutPrCt+ptIdx,true);
        // isMlMvs[chfGutPrCt+ptIdx]=true;

        // mlMvCurPoss[chfGutPrCt+ptIdx] = panItem.transform.localPosition;
        // mlMvCurPosIdxs[chfGutPrCt+ptIdx]=0;
    }   

    int curMvMlCt = 0;

    GameObject[] partBtnObjs = new GameObject[10];
    Vector2[] partBtnPoss = new Vector2[10];
    GameObject[] partCubeObjs = new GameObject[10];

    GameObject[] cnObjs = new GameObject[20];
    Vector2[] cnObjOriPoss = new Vector2[20];
    int cnObjCt = 0;


    void showHeadNdCol(GameObject item,int idx){
        GameObject colItem = ndColPl.transform.GetChild(idx-1).gameObject;
        colItem.SetActive(true);
        
        Vector2 btnPos = Camera.main.WorldToScreenPoint(item.transform.position);
        if(Screen.height==1000){
            btnPos.x-=540;
            btnPos.x+=(btnPos.x+350)/1.5f;
            btnPos.y-=330;
            btnPos.y+=(btnPos.y-150)/1.2f;
        }
        else if(Screen.width==720){
            btnPos.y*=1.5f;
            btnPos.y-=800+(Screen.height-1280)/1.5f;
            btnPos.x*=1.5f;
            btnPos.x-=570;
        }
        else{
            btnPos.x-=540;
            btnPos.y-=730+(Screen.height-1920)*0.5f;
        }
        colItem.transform.localPosition = btnPos;

        if(item.transform.localPosition.x>0){
            GameObject bgObj = colItem.transform.Find("bg").gameObject;
            bgObj.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }

        Image contImg = colItem.transform.Find("cont").GetComponent<Image>();
        contImg.color = nedCols[idx-1];
    }

    GameObject[] mkNwMlObjs;
    void setMakeNewMlPos(GameObject item,int idx){
        GameObject colItem = ndColPl.transform.GetChild(idx+3).gameObject;
        
        Vector2 btnPos = Camera.main.WorldToScreenPoint(item.transform.position);
        if(Screen.height==1000){
            btnPos.x-=360;
            btnPos.x*=1.5f;
            btnPos.y-=400;
            btnPos.y*=1.8f;
            // Debug.Log("ss.."+btnPos);
        }
        else if(Screen.width==720){
            btnPos.y*=1.5f;
            btnPos.y-=850+(Screen.height-1280)/1.5f;
            btnPos.x*=1.5f;
            btnPos.x-=470;
        }
        else{
            btnPos.x-=540;
            btnPos.y-=730+(Screen.height-1920)*0.5f;
        }

        colItem.transform.localPosition = btnPos;
    
        mkNwMlObjs[idx-1] = colItem;

        Button mkBtn = colItem.transform.Find("btn").GetComponent<Button>();
        mkBtn.onClick.RemoveAllListeners();
        mkBtn.onClick.AddListener(delegate(){
            chfMkNewMlDeal(idx);
        });
    }

    void chfMkNewMlDeal(int idx){
        // GameObject colItem = ndColPl.transform.GetChild(idx+3).gameObject;
        // Button mkBtn = colItem.transform.Find("btn").GetComponent<Button>();
        // mkBtn.enabled = false;

        // Button taBtn = mlBtnPl.transform.Find("mlBtn"+idx).GetComponent<Button>();
        // taBtn.enabled = false;   
        // createMeal(idx-1);
        // GuestItem gi = guetObjs[idx-1].GetComponent<GuestItem>();
        // gi.stopEmoji();

        // GameObject ndObj = ndColPl.transform.GetChild(idx-1).gameObject;
        // ndObj.SetActive(true);
        // colItem.SetActive(false);
    }

    //371 370 372 373
    //二进制高位:金币 低位：提示 

    void createOneCoin(int index,int i,bool isOnRot = false){
        if(!isOnRot){
            GameObject gdItem = createOneHind(1,index,i,new Vector3(2.4f*i-10.38f,1.35f,-2.4f*index+50.1f),new Vector3(1.2f,1.2f,1.2f));
            int isNrDir = isNearDes(new Vector2(index,i));
            if(isNrDir>0){
                showGusCover(gdItem,isNrDir);
            }
        }
        GameObject cnObj = createOneHind(2,index,i,new Vector3(2.4f*i-10.5f,3.1f,-2.4f*index+50.05f),new Vector3(0.8f,0.8f,0.8f));
        cnObjs[cnObjCt]=cnObj;
        cnObjCt++;   
    }

    float mapCoinPosToTbe(int type,float val){
        if(type==1){
            return (val+10.5f)/2.4f;
        }
        else{
            return (val-50.05f)/-2.4f;
        }
    }


    void refreshMapCoin(bool isFirst = false){
        Vector3 curEng = new Vector3(0,0,0);
        for(int i=0;i<cnObjCt;i++){
            if(i==0){
                curEng = cnObjs[i].transform.eulerAngles;
            }
            GameObject.Destroy(cnObjs[i]);
            cnObjs[i]=null;
        }
        cnObjCt=0;

        for(int i=0;i<hItemCt;i++){
            for(int j=0;j<runWayWd;j++){
                int data = hindDataArr[i][j];
                if(hindDataArr[i][j]/10==2){
                    // Debug.Log(i+" "+j);
                     if(data%10==6){
                        createOneCoin(i,j,true);
                    }
                    else{
                        createOneCoin(i,j,!isFirst);
                    }
                }
                else if(data/100==3&&data%10/2==1){
                    createOneCoin(i,j,true);
                }
            }
        }
        
        // Debug.Log("rf..."+cnObjCt);
        for(int i=0;i<cnObjCt;i++){
            cnObjs[i].transform.eulerAngles = curEng;
        }

        chgRotCnParent();
    }


    void createPartPan(int index,int i){
        // int data = hindDataArr[index][i];
        // GameObject panItem = createOneHind(5,index,i,new Vector3(2.4f*i-10.4f,2.8f,-2.4f*index+50f),new Vector3(2f,2f,2f));
        // GameObject btnObj = mlBtnPl.transform.Find("patCbBtn"+data/10%10).gameObject;
        // Vector2 btnPos = Camera.main.WorldToScreenPoint(panItem.transform.position);
        // if(Screen.height==1000){
        //     btnPos.x+=(btnPos.x-400)/1.5f;
        //     btnPos.x-=400;
        //     btnPos.y-=550;
        //     btnPos.y+=(btnPos.y+150)/1.8f;
        // }
        // else if(Screen.width==720){
        //     btnPos.y*=1.5f;
        //     btnPos.y-=950+(Screen.height-1280)/1.5f;
        //     btnPos.x*=1.5f;
        //     btnPos.x-=520;
        // }
        // else{
        //     btnPos.x-=540+(Screen.height-1920)*0.1f;
        //     btnPos.y-=950+(Screen.height-1920)*0.6f;
        // }
        
        // btnObj.transform.localPosition = btnPos;

        // if(data%10==1||curLevel==16){
        //     flickObj = panItem;
        // }

        // Button btn = btnObj.GetComponent<Button>();
        // btn.onClick.RemoveAllListeners();
        // btn.enabled = true;
        // int tmeI = i;
        // btn.onClick.AddListener(delegate(){
        //     btn.enabled = false;
        //     if(flickObj!=null&&flickObj.name=="item5(Clone)"){
        //         MealItem mi = panItem.GetComponent<MealItem>();
        //         mi.stopFlick();
        //         stopGuideFing();
        //     }
        //     playGmSound(2,"trans_ml");
        //     onStaPatCbMv(panItem,index,tmeI,data/10%10-1);
        // });
    }

    Material[] covSetMats;
    void showGusCover(GameObject item,int infoIdx){
        // GameObject covObj = item.transform.Find("zhuobu").gameObject;
        // covObj.SetActive(true);

        // int rotIdx = infoIdx%10;
        // int matIdx = infoIdx/10;

        // Vector3 eng = item.transform.eulerAngles;
        // eng.y = 90*(rotIdx-2);
        // item.transform.eulerAngles = eng;

        // MeshRenderer cvMrd = covObj.GetComponent<MeshRenderer>();
        // covSetMats[0] = covrColMats[matIdx-1];
        // cvMrd.materials = covSetMats;

        // Image img = ndImgObj.GetComponent<Image>();
        // img.color = nedCols[col-1];
        // Vector2 imgPos = Camera.main.WorldToScreenPoint(item.transform.position);
        // imgPos.x+=-550;
        // imgPos.y+=-740;
        // ndImgObj.transform.localPosition = imgPos;
    }

    GameObject createOneHind(int type,int index,int i,Vector3 pos,Vector3 scl){
        Transform hitItemPt = transform.Find("mapItems");
        GameObject oriItem = (GameObject)Resources.Load(AssetUtility.getFloorBasePrefab(type)); 
        GameObject boxItem = GameObject.Instantiate(oriItem);
        boxItem.transform.SetParent(hitItemPt);
        boxItem.transform.localPosition = pos;
        boxItem.transform.localScale = scl;
        return boxItem;
    }

    // public auto

    //踩香蕉
    public static Action banHitAc;
    //左右晃一下
    void shakeBox(){
        int boxCt = mnRoleObj.transform.childCount-1;
        float skOri = 0.02f;
        float skDel = 0.02f;
        float shTm = 0.1f;
        
        int subCt = boxCt-10;
        int hasFallCt = 0;
        if(boxCt>10){
            for(int i=10;i<mnRoleObj.transform.childCount;i++){
                // Debug.Log("ss.."+i+" "+mnRoleObj.transform.childCount);
                GameObject boxItem = mnRoleObj.transform.GetChild(i).gameObject;
                int ht = i/10;
                float skDis = skOri+(ht-1)*skDel;
                Sequence shSeq = DOTween.Sequence();
                int tmI = i;
                float oriPosX = boxItem.transform.localPosition.x;
                shSeq.Append(boxItem.transform.DOLocalMoveX(oriPosX-skDis,shTm));
                shSeq.AppendCallback(delegate(){
                    int ranVal = UnityEngine.Random.Range(0,2);
                    if(ranVal==1&&hasFallCt<subCt){
                        subCarBox(1,-1);
                        hasFallCt++;
                    }
                });
                shSeq.Append(boxItem.transform.DOLocalMoveX(oriPosX+skDis,shTm*2));
                shSeq.AppendCallback(delegate(){
                    if(hasFallCt<subCt){
                        subCarBox(1,-1);
                        hasFallCt++;
                    }
                });
                shSeq.Append(boxItem.transform.DOLocalMoveX(oriPosX,shTm));
                shSeq.SetAutoKill();
            }
        }   
    }



    //当前的前进位置
    float curAddPosZ = 0;

    //当前增加多少赛道了
    int curAddMapCt = 0;    


    GameObject curMapItem;


    // 1 匀速前进 2 最后起跳
    int actionStep = 1;
    int finJumpStep = 1;

    void clearJumpEgy(){
        // updateJumpEgyBar(-100);
    }

    ParaCurveLcHelp fnJpObj = null;

    //6级 0-5
    float finJumpLev = 0;

    float finStepOneAccTm=0;

    bool isFinCmaWith = false;
    
    void clearCmaWith(){
        isFinCmaWith = false;
    }


    void clearMapHind(){
        Transform itPt = transform.Find("mapItems");
        int itemCt = itPt.childCount;
        for(int i=0;i<itemCt;i++){
            GameObject.DestroyImmediate(itPt.GetChild(0).gameObject);
        }

        Transform mlPt = transform.Find("mealPl");
        int mlCt = mlPt.childCount;
        for(int i=0;i<mlCt;i++){
            GameObject.DestroyImmediate(mlPt.GetChild(0).gameObject);
        }
    }   

    float unitMapLen = 34f;    
    // int norFodStep = 1;    

    void normalForwardDeal(){
        // Debug.Log("nnn...."+finAddMapNum+" "+curAddMapCt);
        if(curAddMapCt==finAddMapNum){
            curAddMapCt++;
        }

        curAddPosZ+=Time.deltaTime;
        float curAddPointZ = unitMapLen*curAddMapCt+addPointZ;
        // Debug.Log("cc.."+curAddPosZ);
        //新增地图
        if(curAddMapCt<finAddMapNum&&mnRoleObj.transform.localPosition.z>curAddPointZ){
            curAddPosZ=0;
            curAddMapCt++;
            GameObject newMapItem = GameObject.Instantiate(curMapItem);
            newMapItem.transform.SetParent(curMapItem.transform.parent);
            newMapItem.name = "runWayPl"+curAddMapCt;
            // clearMapHind(newMapItem);

            Vector3 newMapPos = new Vector3(curMapItem.transform.localPosition.x,curMapItem.transform.localPosition.y,unitMapLen*curAddMapCt);
            // Debug.Log("nor..."+newMapPos);
            newMapItem.transform.localPosition = newMapPos;
            newMapItem.transform.rotation = curMapItem.transform.rotation;
            curMapItem = newMapItem;
            Vector3 ltPos = psBtnLtObj.transform.localPosition;
            ltPos.z+=unitMapLen;
            psBtnLtObj.transform.localPosition = ltPos;
            // hindStaPosZ = -17;

            if(curMapItem.transform.parent.childCount>6){
                GameObject oldMapItem = curMapItem.transform.parent.GetChild(5).gameObject;
                GameObject.DestroyImmediate(oldMapItem);
            }
            
        }

        //前进 准备转盘
        if(curAddMapCt<=finAddMapNum+1){
            // Debug.Log("222");
            //跑道 人物 匀速向前
            float cmEndZ = unitMapLen*curAddMapCt-13;
            curCmPosZ+=moveSpdZ*moveSpdRt;
            // if(curCmPosZ<cmEndZ){
            //     Vector3 cmNewPos = new Vector3(mainCamera.transform.localPosition.x,mainCamera.transform.localPosition.y,curCmPosZ);
            //     Util.tfSetNewPos(mainCamera.transform,cmNewPos);
            // }
            // else{
            // }
            Vector3 cmNewPos = new Vector3(mainCamera.transform.localPosition.x,mainCamera.transform.localPosition.y,curCmPosZ);
            Util.tfSetNewPos(mainCamera.transform,cmNewPos);

            curRolePosZ+=moveSpdZ*moveSpdRt;
            // Debug.Log(curRolePosZ+"  "+moveSpdZ);
            Vector3 newPos = new Vector3(mnRoleObj.transform.localPosition.x,0.1f,curRolePosZ);
            Util.tfSetNewPos(mnRoleObj.transform,newPos);    
            float endZ = unitMapLen*(finAddMapNum+1)-17;
            // Debug.Log("jaja.."+endZ+" "+curAddMapCt);
            float mnPosZ = mnRoleObj.transform.localPosition.z;
            if(mnPosZ>endZ){
                pPStartTurnPos = mnRoleObj.transform.localPosition;    
                actionStep++;
                isRunning =false;
                canHorMove = false;
                horMoveSpd = 0;
            }
        }
    }


    //矫正x
    void gotoFinRoomDeal(){
        moveSpdZ = 0.1f;

        curRolePosZ+=moveSpdZ;
        float mnPosX = mnRoleObj.transform.localPosition.x;
        if(!isOnFinWay){
            isOnFinWay = true;
            canHorMove = false;
            isRunning = false;
            horMoveSpd = 0;
            float pMnPosX = Mathf.Abs(mnPosX);
            if(pMnPosX>0.01f){
                finAdjSpdX =  mnPosX/(finWalkDis/moveSpdZ);
            }
        }      
        mnPosX-=finAdjSpdX;  

        // Debug.Log(curRolePosZ+"  "+moveSpdZ);
        Vector3 newPos = new Vector3(mnPosX,0.1f,curRolePosZ);
        Util.tfSetNewPos(mnRoleObj.transform,newPos);    


        curCmPosZ+=moveSpdZ;
        // Debug.Log("gfr.."+mainCamera.transform.localPosition);
        Vector3 cmNewPos = new Vector3(mnPosX,mainCamera.transform.localPosition.y,curCmPosZ);
        Util.tfSetNewPos(mainCamera.transform,cmNewPos);

        float endZ = unitMapLen*curAddMapCt-8f;
        float mnPosZ = mnRoleObj.transform.localPosition.z;
        if(mnPosZ>endZ){
            // pPStartTurnPos = mnRoleObj.transform.localPosition;    
            actionStep++;
        }

    }

    float[] drToMenDis = new float[]{1.4f,0.5f,1f,1.6f};
    bool isCallGAH = false;
    float finGoNerMSpd = 0.014f;
    void gotoMenAndHit(){
        // Sequence figSeq = DOTween.Sequence();
        // if(!isCallGAH){
        //     touchPlBtn.enabled = true;
        //     acFsPtObj.SetActive(true);
        //     isCallGAH = true;
        //     sitmObj.resetAni();
        //     moveSpdZ = finGoNerMSpd;
        //     figSeq.Append(acFsFgrObj.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.1f));
        //     figSeq.Append(acFsFgrObj.transform.DOScale(new Vector3(1,1,1),0.1f));
        //     figSeq.SetLoops(-1);
        // }
        // curCmPosZ+=moveSpdZ;
        // Vector3 cmNewPos = new Vector3(mainCamera.transform.localPosition.x,mainCamera.transform.localPosition.y,curCmPosZ);
        // Util.tfSetNewPos(mainCamera.transform,cmNewPos);

        // curRolePosZ+=moveSpdZ;
        // // Debug.Log(curRolePosZ+"  "+moveSpdZ);
        // Vector3 newPos = new Vector3(mnRoleObj.transform.localPosition.x,0.1f,curRolePosZ);
        // Util.tfSetNewPos(mnRoleObj.transform,newPos);    
        // float endZ = unitMapLen*curAddMapCt-6f+drToMenDis[curRoomType];
        // float mnPosZ = mnRoleObj.transform.localPosition.z;
        // if(mnPosZ>endZ){
        //     touchPlBtn.enabled = false;
        //     isCliAcFs = false;
        //     acFsFgrObj.transform.parent.gameObject.SetActive(false);
        //     figSeq.Kill(true);
        //     figSeq=null;

        //     actionStep++;
        //     GameObject rmManObj = finRItm.transform.Find("mens/male").gameObject;
        //     rmManObj.SetActive(false);
        //     sitmObj.stopRun();
        //     createMan();  
        //     Invoke("finHitMen",0.5f);
        // }
    }

    
    void playFinHitSd(){
        // if(curAngType==5){
        //     playGmSound(2,"bbar_hit");
        // }
        // else if(curAngType==3){
        //     playGmSound(2,"fist_hit");
        // }
        // else if(curAngType==1){
        //     playGmSound(2,"nor_hit");
        // }
        // else if(curAngType==4){
        //     playGmSound(2,"pot_hit");
        // }
    }

    float[] finHitSdWtTms = new float[]{1f,0.5f,1f,0.5f,0.8f};

    ParaCurveLcHelp finPch=null;
    bool isManFly = false;
    void finHitMen(){
        // Debug.Log("hitmen");
        Sequence hitSeq = DOTween.Sequence();
        hitSeq.AppendCallback(delegate(){
            sitmObj.playHit();
            hpPlObj.SetActive(false);
        });
        hitSeq.AppendInterval(finHitSdWtTms[curAngType-1]);
        hitSeq.AppendCallback(delegate(){
            Handheld.Vibrate();
            playFinHitSd();
        });
        hitSeq.AppendInterval(0.4f);
        hitSeq.AppendCallback(delegate(){
            manItem._animator.speed = 0.3f;
            manItem.playFly();
            Vector3 pos = manItem.transform.localPosition;
            float delZ = calFlyDelZ();
            Vector3 toPos = new Vector3(pos.x,pos.y,pos.z+delZ);
            // manItem.transform.DOLocalMove(pos,4);
            isManFly = true;
            finPch = new ParaCurveLcHelp();
            finPch.verticalSpeed = 0.02f;
            finPch.SetParams(manItem.transform,toPos,3);
        });
        hitSeq.AppendInterval(2.8f);
        hitSeq.AppendCallback(delegate(){
            // playGmSound(2,"fall_down");
        });
        hitSeq.AppendInterval(2.5f);
        hitSeq.AppendCallback(delegate(){
            settleDeal();
            finPch=null;
        });
        hitSeq.SetAutoKill();
    }

    //power 0-9
    int getFinPow(){
        int power = Mathf.FloorToInt(curAgrVal*0.2f+curAcFsVal*0.2f);
        power = Mathf.Clamp(power,0,39);
        // power = 39;
        // Debug.Log("getFinPow."+curAgrVal+" "+curAcFsVal+" "+power);
        return power;
    }

    //power 0-9
    float calFlyDelZ(){
        int power = getFinPow();
        Debug.Log("cal.."+power);
        float mul = 1;
        if(curRoomType==1||curRoomType==2){
            mul = 1.8f;
        }
        return 1.49f*power-creManPoss[curRoomType].z+scoBdPosZ[curRoomType]-drToMenDis[curRoomType]*mul-9f;
    }


    Vector3[] creManPoss = new Vector3[]{new Vector3(-0.09f,0.44f,-1.07f),new Vector3(0f,0.32f,-2.12f),
                                    new Vector3(-0.12f,0.46f,-1.8f),new Vector3(-0.08f,0.48f,-0.84f)};
    ManItem manItem=null;
    void createMan(){
        GameObject oriItem = (GameObject)Resources.Load(AssetUtility.getFloorBasePrefab(5)); 
        GameObject boxItem = GameObject.Instantiate(oriItem);
        int cdCt = finRmPt.transform.childCount;
        boxItem.transform.SetParent(finRmPt.transform);
        boxItem.transform.eulerAngles = new Vector3(0,180,0);
        boxItem.transform.localPosition = creManPoss[curRoomType];
        boxItem.transform.localScale = new Vector3(1f,1f,1f);
        manItem = boxItem.GetComponent<ManItem>();
    }


    public static Action<int> turnWayAc;
    void onTurnWayCb(int type){
        // Debug.Log("oncb..."+type);
        cmTurnDeal(type);
        Invoke("mnRoleTurn",0.2f);
    }

    
    int leftWayCt=1;
    int rightWayCt=1;

    int curTnWayIdx = 3;
    // 1 left 2 right

    string getTnPtPath(int dir,int ptIdx){
   
        string res = "";
        int ctIdx=1;
        if(dir==1){
            res+="leftWay";
            ctIdx = leftWayCt;
        }
        else{
            res+="rightWay";
            ctIdx = rightWayCt;
        }
        ctIdx = ctIdx%4;
        if(ctIdx==0){
            ctIdx=4;
        }
        res+=ctIdx+"/Waypoints/p"+ptIdx+"/Sphere";
        return res;
    }

    void calCurTnWyIdx(){
        float[] uns = new float[3];
        float left = curWyCtPos-rightMaxDis;
        float add = 2*rightMaxDis/4;
        uns[0]=left+add;
        for(int i=1;i<3;i++){
            uns[i] = uns[i-1]+add;
        }

        // curMoveDir

        for(int i=0;i<3;i++){
            if(curMoveDir==1||curMoveDir==3){
                float wPos = mnRoleObj.transform.position.x;
                if(wPos<uns[i]){
                    curTnWayIdx = i+1;
                    return;
                }
            }
            else{
                float wPos = mnRoleObj.transform.position.z;
                if(wPos<uns[i]){
                    curTnWayIdx = i+1;
                    return;
                }
            }
        }
        curTnWayIdx = 4;
    }

    bool isMnTurn = false;
    void mnRoleTurn(){
        finTurnCall = false;
        isMnTurn = true;
        curTnPtIdx=0;
        string wayPtStr;
        // Debug.Log("mnrrrrr..."+mnRoleTnTp);
        calCurTnWyIdx();

        for(int j=0;j<4;j++){
            for(int i=0;i<8;i++){
                string wyPtPh = getTnPtPath(mnRoleTnTp,j+1);
                // Debug.Log(wyPtPh);
                wayPointObjs[j][i] = transform.Find(wyPtPh+(i+1)).gameObject;
            }
        }

        if(mnRoleTnTp==1){
            leftWayCt++;
            goNextMoveDir(1);
        }
        else{
            goNextMoveDir(2);
            rightWayCt++;
        }
     
        turnTarget = wayPointObjs[curTnWayIdx-1][0];
        Vector3 curPos = mnRoleObj.transform.position;
        Vector3 newPos = turnTarget.transform.position;
        // Vector3 lookDir = (newPos-curPos).normalized;
        // mnRoleObj.transform.DORotateQuaternion(Quaternion.LookRotation(lookDir),0.5f);
        turnSpdX = calTurnSpd(curPos.x,newPos.x);
        turnSpdZ = calTurnSpd(curPos.z,newPos.z);
        // Debug.Log("mnrt..."+turnSpdX+" "+turnSpdZ+" "+newPos);
    }


    // 1 left 2 right
    void cmTurnDeal(int type){
        mnRoleTnTp = type;
        // Vector3 curEng = mainCamera.transform.eulerAngles;
        // curEng.y+=45;
        // mainCamera.transform.DORotate(curEng,5f);
        curCmTnPtIdx=0;
        cmTurnSpdX = 0f;
        cmTurnSpdZ = 0.02f;

        Vector3 cmCurPos = mainCamera.transform.position;
        string wayPtStr;
        if(type==1){
            float sdIdx = leftWayCt%4;
            if(sdIdx==0){
                sdIdx=4;
            }
            wayPtStr = "leftWay"+sdIdx+"/Waypoints/pCm/Sphere";
        }
        else{
            float sdIdx = rightWayCt%4;
            if(sdIdx==0){
                sdIdx=4;
            }
            wayPtStr = "rightWay"+sdIdx+"/Waypoints/pCm/Sphere";
        }
        for(int i=0;i<4;i++){
            wayCmPointPoss[i] = transform.Find(wayPtStr+(i+1)).position;
            float oriX = wayCmPointPoss[i].x;
            float oriY = wayCmPointPoss[i].y;
            float oriZ = wayCmPointPoss[i].z;
            wayCmPointPoss[i] = new Vector3(oriX,cmCurPos.y,oriZ);
        }
    }

    Vector3 pPStartTurnPos;

    //过终点线的距离
    float finWalkDis = 9f;
    float finAdjSpdX = 0;
    bool isOnFinWay = false;


    void setJumpMidBarVal(float val){
        // RectTransform barRtf = egyPlObj.transform.Find("midBar").GetComponent<RectTransform>();
        // barRtf.sizeDelta = new Vector2(430f*val/100f,47.6f);   
    }

    void setJumpBarVal(float val){
        // Debug.Log("sss..."+val);
        // RectTransform barRtf = egyPlObj.transform.Find("bar").GetComponent<RectTransform>();
        // barRtf.sizeDelta = new Vector2(430f*val/100f,47.6f);   
    }

    float lastPYAccTm = 0;
    float lastPYInTm = 0.2f;
    bool isRunning = false;
    float rfsBoxInTm =0.05f;

    void formatCarBox(){
        // Debug.Log("fffff.."+curScore);
        int curBoxCt = mnRoleObj.transform.childCount-1;
        if(curBoxCt>curScore){
            for(int i=0;i<curBoxCt-curScore;i++){
                GameObject.DestroyImmediate(mnRoleObj.transform.GetChild(curScore+1).gameObject);
            }
        }
        for(int i=1;i<=curScore;i++){
            if(i<mnRoleObj.transform.childCount){
                GameObject boxItem = mnRoleObj.transform.GetChild(i).gameObject;
                setBoxPos(boxItem,i);
            }
        }
    }

    void settleDeal(){
        if(curAgrVal<=0){
            gameOver();
        }
        else{
            gameWin();
        }
    }


    //1 ford 2 left 3 behd 4 right    
    float calCmDelAng(){
        float delAng=0;
        if(mnRoleTnTp==1){
            delAng = -270f;
            if(curMoveDir==2){
                delAng=0;
            }
            else if(curMoveDir==3){
                delAng=-90;
            }
            else if(curMoveDir==4){
                delAng=-180;
            }
        }
        else{
            delAng = 270f;
            if(curMoveDir==2){
                delAng=180;
            }
            else if(curMoveDir==3){
                delAng=90;
            }
            else if(curMoveDir==4){
                delAng=0;
            }
        }
        return delAng;
    }

    float covertCmAng(float val){
        if(mnRoleTnTp==1){
            while(val>0){
                val-=360;
            }
            while(val<-360){
                val+=360;
            }
        }
        return val;
    }


    Vector3 calCmTurnSrdPos(){
        Vector3 strdPos = mnRoleObj.transform.position;
        if(curMoveDir==1){
            strdPos.z-=4;
            strdPos.x = wayCmPointPoss[3].x;
        }
        else if(curMoveDir==2){
            strdPos.x+=4;
            strdPos.z = wayCmPointPoss[3].z;
        }
        else if(curMoveDir==3){
            strdPos.z+=4;
            strdPos.x = wayCmPointPoss[3].x;
        }
        else{
            strdPos.x-=4;
            strdPos.z = wayCmPointPoss[3].z;
        }
        strdPos.y = 2.81f;
        return strdPos;
    }

    //z -4 y 2.71
    void cmMoveToStrdPos(){
        Vector3 strdPos = calCmTurnSrdPos();

        Vector3 curCmPos = mainCamera.transform.position;
        Vector3 enag = mainCamera.transform.eulerAngles;
        // Debug.Log("rr."+strdPos.x+" "+curCmPos.x);
        if(Mathf.Abs(strdPos.x-curCmPos.x)>0.01f){
            cmToSrdSpdX = (strdPos.x-curCmPos.x)*0.05f;
            cmToSrdSpdZ = (strdPos.z-curCmPos.z)*0.05f;
            // Debug.Log("rr.."+cmToSrdSpdX+" "+cmToSrdSpdZ);
            curCmPos.x+=cmToSrdSpdX;
            curCmPos.z+=cmToSrdSpdZ;
            mainCamera.transform.localPosition = curCmPos;
        }
        else{
            curCmTnPtIdx=5;
        }

        float conVal = covertCmAng(enag.y);
        float delVal = calCmDelAng();
        if(mnRoleTnTp==1){
            // Debug.Log("tosp..."+enag.y+" "+delAng);
            if(conVal<=-90+delVal){
                curCmTnPtIdx=5;
            }
            if(enag.y<0&&curMoveDir==1){
                curCmTnPtIdx=5;
            }
        }
        else{
            if(conVal>=90+delVal){
                curCmTnPtIdx=5;
            }
            if(enag.y<1&&curMoveDir==1){
                curCmTnPtIdx=5;
            }
        }
    }
    
    
    Vector3 calCmTurnSpd(float adjSpdX,float adjSpdZ){
        Vector3 curPos = mainCamera.transform.localPosition;
        if(curMoveDir==1){
            curPos.x+=adjSpdX;
            curPos.z+=(moveSpdZ+adjSpdZ)*moveSpdRt;
        }
        else if(curMoveDir==2){
            curPos.x-=(moveSpdZ-adjSpdX)*moveSpdRt;
            curPos.z+=adjSpdZ;
        }
        else if(curMoveDir==3){
            curPos.x+=adjSpdX;
            curPos.z-=(moveSpdZ-adjSpdZ)*moveSpdRt;
        }
        else{
            curPos.x+=(moveSpdZ+adjSpdX)*moveSpdRt;
            curPos.z+=adjSpdZ;
        }
        return curPos;
    }

    void afterTurnCmMv(){
        Vector3 strdPos = calCmTurnSrdPos();

        Vector3 curPos = mainCamera.transform.localPosition;

        float adjSpdX = 0;
        if(Mathf.Abs(strdPos.x-curPos.x)>0.01f*moveSpdRt){
            adjSpdX = (strdPos.x-curPos.x)*0.05f;
        }

        float adjSpdZ = 0;
        if(Mathf.Abs(strdPos.z-curPos.z)>0.01f*moveSpdRt){
            adjSpdZ = (strdPos.z-curPos.z)*0.05f;
        }
        mainCamera.transform.localPosition = calCmTurnSpd(adjSpdX,adjSpdZ);
    }


    // bool 
    float cmToSrdSpdX = 0.1f;
    float cmToSrdSpdZ = 0.2f;


    //1 ford 2 left 3 behd 4 right
    Vector3 calAftTurnMnSpd(){
        Vector3 curPos = mnRoleObj.transform.position;
        if(curMoveDir==1){
            curPos.z+=moveSpdZ*moveSpdRt;
        }
        else if(curMoveDir==2){
            curPos.x-=moveSpdZ*moveSpdRt;
        }
        else if(curMoveDir==3){
            curPos.z-=moveSpdZ*moveSpdRt;
        }
        else{
            curPos.x+=moveSpdZ*moveSpdRt;
        }
        return curPos;
    }

    //1 ford 2 left 3 behd 4 right
    float calCurWayCntr(){
        if(curMoveDir==1||curMoveDir==3){
            return wayCmPointPoss[3].x;
        }
        else{
            return wayCmPointPoss[3].z;
        }
    }

    
    bool isChgTnWay = false;
    Vector3 chgTnWyPt;
    void adjCurWayIdx(int dir){
        if(turnTarget!=null&&curTnPtIdx<6){
            curTnWayIdx+=dir;
            if(curTnWayIdx<1){
                curTnWayIdx=1;
            }
            if(curTnWayIdx>4){
                curTnWayIdx=4;
            }
            isChgTnWay = true;
        
            Vector3 newPos = wayPointObjs[curTnWayIdx-1][curTnPtIdx+1].transform.position;
            chgTnWyPt = newPos;
            Vector3 curPos = mnRoleObj.transform.position;
            turnSpdX = calTurnSpd(curPos.x,newPos.x,0.5f);
            turnSpdZ = calTurnSpd(curPos.z,newPos.z,0.5f);
        }
    }


    bool judgeIsInWay(Vector3 pos){
        float lefMax = curWyCtPos-rightMaxDis*1.2f;
        float rightMax = curWyCtPos+rightMaxDis*1.2f;
        // Debug.Log(lefMax+" "+rightMax+" "+pos);
        if(curMoveDir==1||curMoveDir==3){
            // if(pos.z<lefMax||pos.z>rightMax){
            //     return false;
            // }
            // return true;
            if(curTnPtIdx<4){
                if(pos.z<lefMax||pos.z>rightMax){
                    return false;
                }
                return true;
            }
            else{
                float ct = calCurWayCntr();
                lefMax = ct-rightMaxDis*1.2f;
                rightMax = ct+rightMaxDis*1.2f;
                if(pos.x<lefMax||pos.x>rightMax){
                    return false;
                }
                return true;
            }
        }
        else{
            if(curTnPtIdx<4){
                // Debug.Log("jd333.."+lefMax+" "+rightMax+" "+pos);
                if(pos.x<lefMax||pos.x>rightMax){
                    return false;
                }
                return true;
            }
            else{
                float ct = calCurWayCntr();
                lefMax = ct-rightMaxDis*1.2f;
                rightMax = ct+rightMaxDis*1.2f;
                // Debug.Log("jd444.."+lefMax+" "+rightMax+" "+pos);
                if(pos.z<lefMax||pos.z>rightMax){
                    return false;
                }
                return true;
            }
        }
        return false;
    }

    void fordDeal(Vector3 curPos){
        mnRoleObj.transform.position = curPos;
        // Debug.Log("f11"+curPos+" "+turnSpdX+" "+turnSpdZ+" "+curTnPtIdx);
        if(isChgTnWay){
            if(Mathf.Abs(curPos.x-chgTnWyPt.x)<0.01f){
                isChgTnWay = false;
                curTnPtIdx+=1;
            }
        }
        else{
            Vector3 p1 = wayPointObjs[curTnWayIdx-1][curTnPtIdx].transform.position;
            // Debug.Log(" "+p1+" ");
            if(Mathf.Abs(curPos.x-p1.x)<0.01f){
                if(curTnPtIdx<7){
                    // Debug.Log("xixixi..");
                    Vector3 p2 = wayPointObjs[curTnWayIdx-1][curTnPtIdx+1].transform.position;
                    turnSpdX = calTurnSpd(p1.x,p2.x);
                    turnSpdZ = calTurnSpd(p1.z,p2.z);
                    // Debug.Log("rrr.."+p1+" "+p2+" "+turnSpdX+" "+turnSpdZ);
                    curTnPtIdx++;
                    Vector3 lookDir = (p2-p1).normalized;
                    mnRoleObj.transform.DORotateQuaternion(Quaternion.LookRotation(lookDir),0.5f);
                    // Debug.Log("xixi.."+curTnPtIdx);
                }
                else{
                    turnTarget=null;
                }
            }
        }
    }

    bool finTurnCall = false;
    float finPceSpd = 0.2f;
    float hasTurnAng = 0;

    float[] turnAcSpds = new float[]{0.08f,0.093f,0.102f,0.112f};
    float[] acEndAngs = new float[]{270,180,135,90};

    float formatAng(float ang){
        while(ang>360){
            ang-=360;
        }
        while(ang<0){
            ang+=360;
        }
        return ang;
    }

    float tuTbeSpd = 10f;
    float curTuSpd = 0;
    //1=360 8=45 7=90
    void turnTbeAction(){
        // Debug.Log("tuac.."+tuTbeRes);
        hasTurnAng = 0;
        curTuSpd = tuTbeSpd;
        isTnSlwDn = false;
        tnRanIdx = UnityEngine.Random.Range(0,4);
        // Debug.Log(tnRanIdx);
        float endAng = -45*tuTbeRes+405;
        preAng = endAng-acEndAngs[tnRanIdx];
        preAng = formatAng(preAng);
        isTnTbe = true;

    }

    void formatTnAng(){
        Quaternion cuEng = tuTbeObj.transform.localRotation;
        Vector3 ccuEng = cuEng.eulerAngles;
        ccuEng.y=10;
        Quaternion ori = Quaternion.Euler(ccuEng.x,ccuEng.y,ccuEng.z); 
        tuTbeObj.transform.localRotation = ori;
    }


    bool isTnSlwDn = false;
    int tnRanIdx=1;
    float preAng=20;
    bool isTnTbe = false;

    float finAdAngTm = 0.5f;
    float finAdAngAccTm = 0f;
    bool isFinAdAng = false;
    float finAngEndVal = 0;
    float finAdAngSpd = 1;
    void turnTnDeal(){
        if(isTnTbe){
            Quaternion cuEng = tuTbeObj.transform.localRotation;
            Vector3 ccEng = cuEng.eulerAngles;
            
            ccEng.y+=curTuSpd;
            hasTurnAng+=curTuSpd;
            // Debug.Log(hasTurnAng+" "+preAng+" "+tnRanIdx);
            if(Mathf.Abs(hasTurnAng-preAng)<6f&&hasTurnAng>=preAng){
                isTnSlwDn = true;
            }
            else if(preAng==0){
                isTnSlwDn = true;
            }
            if(isTnSlwDn){
                curTuSpd-=turnAcSpds[tnRanIdx];    
                if(curTuSpd<=0){
                    isTnTbe = false;
                    // mnRoleObj.SetActive(true);
                    // hpPlObj.SetActive(true);
                    int tnValType = tnTbesConfig[toAcScoMap[tuTbeRes-1]][0];
                    int tnValVal = tnTbesConfig[toAcScoMap[tuTbeRes-1]][1];

                    void backCmDeal(float waitTm){
                        isChgHpPos=true;
                        moveMainCam(cmStartTnPos,cmStartTnRot,delegate(){
                        // Debug.Log("xxx.."+mainCamera.transform.localPosition);
                            curCmPosZ=cmStartTnPos.z;
                            Sequence adAgSeq = DOTween.Sequence();
                            adAgSeq.AppendInterval(0.5f);
                            adAgSeq.AppendCallback(delegate(){
                                if(tnValType==1){
                                    finAngEndVal = curAgrVal+tnValVal;
                                    finAngEndVal = Mathf.Clamp(finAngEndVal,0,100);
                                    float delTa = finAngEndVal-curAgrVal;
                                    finAdAngSpd = delTa/(finAdAngTm/0.02f);
                                    // Debug.Log(curAgrVal+" "+delTa+" "+finAdAngSpd+" "+finAngEndVal);
                                    isFinAdAng = true;
                                    finAdAngAccTm=0;
                                }
                                else{
                                    finAngEndVal = curAgrVal;
                                }
                            });
                            adAgSeq.AppendInterval(1.1f+waitTm);
                            adAgSeq.AppendCallback(delegate(){
                                if(finAngEndVal==0){
                                    gameOver();
                                }
                                else{
                                    if(Screen.width==800){
                                        sitmObj.isVdoAdjX = true;
                                    }
                                    else if(Screen.height==1280){
                                        sitmObj.isVdoAdjX = true;
                                    }
                                    actionStep++;
                                    sitmObj.resetAni();
                                }
                            });
                            adAgSeq.SetAutoKill();
                        });
                    }
                    if(tnValType==2){
                        GameObject ppObj = tuTbeObj.transform.GetChild(0).gameObject;
                        Vector3 fromPos = ppObj.transform.position;
                        fromPos.y+=250;
                        Vector3 toPos = upPpObj.transform.position;

                        flyPpMoy(uiPt,fromPos,toPos,0.7f,delegate(){
                            curPpMoyVal+=tnValVal;
                            ppMoyTt.text = curPpMoyVal+"";
                            backCmDeal(-0.5f);
                        });
                    }
                    else{
                        backCmDeal(finAdAngTm);
                    }
                }
            }
            Quaternion newEgn = Quaternion.Euler(ccEng.x,ccEng.y,ccEng.z);
            tuTbeObj.transform.localRotation = newEgn;
        }
    }


    bool isAdjMtBd = false;
    void adjMtMidWayBd(){
        Vector3 curMnPos = mnRoleObj.transform.localPosition;
        float deX = getBdBoderDelt();
        if(forbidDir!=0&&Mathf.Abs(curMnPos.x-deX)<0.2f&&!isAdjMtBd){
            // Debug.Log("adjjjj....");
            int ranInt = UnityEngine.Random.Range(0,2);
            float posX = ranInt*0.4f-0.2f+deX;  
            curMnPos.x = posX;
            mnRoleObj.transform.DOLocalMove(curMnPos,0.1f);
            isAdjMtBd = true;
        }
    }

    float getBdBoderDelt(){
        float res=0;
        if(bdPosType==0){
            return res;
        }
        else if(bdPosType==-1){
            return res-rightMaxDis/2;
        }
        else{
            return res+rightMaxDis/2;
        }
    }


    float adjBdCoAccTm=0;
    float adjBdCoInTm=0.5f;

    float acFsSubSpd = 0.3f;
    float acFsAddSpd = 8f;
    //弹的距离
    bool isCliAcFs = false;
    bool isCliSubFs = false;
    float endAcFsVal = 0;
    float acFsJupSpd = 6;
    // bool 
    void onClickAddAcFs(){
        if(!isCliAcFs){
            isCliAcFs=true;
            endAcFsVal = curAcFsVal+acFsAddSpd+acFsAddSpd/2;
            if(endAcFsVal>100+acFsAddSpd/2){
                endAcFsVal=100+acFsAddSpd/2;
            }
        }        
    }

    void finAcFsDeal(){
        if(isCliAcFs){
            if(!isCliSubFs){
                curAcFsVal+=acFsJupSpd;
                setAcFsImg();
                if(curAcFsVal>=endAcFsVal){
                    isCliSubFs = true;
                    endAcFsVal-=acFsAddSpd/2;
                }
            }
            else{
                curAcFsVal+=-2*acFsJupSpd;
                setAcFsImg();
                if(curAcFsVal<=endAcFsVal){
                    isCliAcFs=false;
                    isCliSubFs =false;
                }
            }
        }
        if(curAcFsVal>0&&actionStep==5){
            curAcFsVal-=acFsSubSpd;
            setAcFsImg();
        }
    }




    bool isChgHpPos = false;

    Color[] nedCols;

    void initNedCol(){
        nedCols = new Color[4];
        nedCols[0] = new Color(255f/255f,0f/255f,0f/255f);
        nedCols[1] = new Color(0f/255f,255f/255f,0f/255f);
        nedCols[2] = new Color(0/255f,0f/255f,255f/255f);
        nedCols[3] = new Color(255f/255f,255f/255f,0f/255f);
    }


    //1 x  22 y正
    int getMlMovDir(Vector2 toPos,Vector3 frPos){
        if(Mathf.Abs(frPos.z-toPos.x)<0.01f){
            if(toPos.y>frPos.x){
                return 12;
            }
            else{
                return 11;
            }
            
        }
        else{
            if(toPos.x>frPos.z){
                return 22;
            }
            else{
                return 21;
            }
        }
    }

    bool isTwoVec2Equ(Vector3 pos1,Vector2 pos2){
        if((Mathf.Abs(pos1.x-pos2.y)<0.1f)&&(Mathf.Abs(pos1.z-pos2.x)<0.1f)){
            return true;
        }
        return false;
    }

    // void update

    

    void mlMvStepDeal(int i,bool isPtIt = false){
        if(mlMvObjs[i]!=null&&isMlMvs[i]){
            Vector2 desPos = mlMvPathPoss[i][mlMvCurPosIdxs[i]+1];
            if(!isPtIt&&checkIsFatNear(i).x!=-1){
                onMlItemDestory(i+1);
            }
            // Debug.Log(mlMvCurPoss[i]+" "+desPos+" "+mlMvCurPosIdxs[i]);
            // if(i==1){
            //     Debug.Log(mlMvCurPoss[i]+" "+desPos);
            // }
            int mvType = getMlMovDir(desPos,mlMvCurPoss[i]);
            if(mvType==11){
                mlMvCurPoss[i].x-=mlMoveSpd;
            }
            else if(mvType==12){
                mlMvCurPoss[i].x+=mlMoveSpd;
            }
            else if(mvType==21){
                mlMvCurPoss[i].z-=mlMoveSpd;
            }
            else{
                mlMvCurPoss[i].z+=mlMoveSpd;
            }

            Vector3 mvPos = mlMvObjs[i].transform.localPosition;
            mvPos.x = mlMvCurPoss[i].x;
            mvPos.z = mlMvCurPoss[i].z;
            mlMvObjs[i].transform.localPosition = mvPos;
            if(isTwoVec2Equ(mlMvCurPoss[i],desPos)){
                mlMvCurPosIdxs[i]++;
                if(mlMvCurPosIdxs[i]==mlMvPathIdxs[i]-1){
                    if(!isPtIt){
                        onMlItemDestory(i+1);
                    }
                    else{
                        desPartItem(i+1);
                    }
                    
                }
            }
        }
    }

    Vector2 checkIsFatNear(int mlIdx){
        int stepIdx = mlMvCurPosIdxs[mlIdx];
        if(stepIdx==0){
            return new Vector2(-1,-1);
        }
        Vector2 curPos = mlMvPathPoss[mlIdx][stepIdx];
        curPos.x = mlCvtTbDaToPosDaRevs(2,curPos.x);
        curPos.y = mlCvtTbDaToPosDaRevs(1,curPos.y);
        Vector2 isNeFat = checkIsToFat(curPos);
        // Debug.Log("cisfat.."+stepIdx+" "+curPos+" "+isNeFat);
        return isNeFat;
    }


    float mlMoveSpd = 0.2f;
    bool panHasBkOs = false;

    float panRadius = 0.5f;
    float panCurAng = 0;
    
    //盘子 待机
    void panIdleDeal(){
        if(curPanObj!=null&&isPanIdle){
            float posX = idleCenter.x+panRadius*Mathf.Cos(panCurAng*Mathf.Deg2Rad);
            float posY = idleCenter.y+panRadius*Mathf.Sin(panCurAng*Mathf.Deg2Rad);
            curPanObj.transform.localPosition = new Vector3(posX,0,posY);
            panCurAng+=2;
            if(panCurAng>360){
                panCurAng=0;
            }
        }
    }

    GameObject eatRmObj;
    GameObject makeRmObj;
    // type 0 1 2
    void setSceneType(int type){
        eatRmObj.SetActive(type==1);
        makeRmObj.SetActive(type==2);
        makeBgObj.SetActive(type==2);
    }
    public float meatAngSpd = 20f;
    public float flyHorSpd = 0.01f;
    bool isMtOut = false;
    void meatOutDeal(){
        if(isMtOut){
            return;
        }
        isMtOut = true;
        nextBtn.gameObject.SetActive(false);
        Sequence wtSeq = DOTween.Sequence();
        wtSeq.AppendInterval(0.5f);
        wtSeq.AppendCallback(delegate(){
            createNewMt();    
            isMtOut = false;
        });
        wtSeq.SetAutoKill();
    }

    //(0 90 0) 

    void panThrowMvDeal(){
        if(curPanObj!=null){
            Vector3 panObjPos = curPanObj.transform.position;
            Vector3 mtObjPos = curMtObj.transform.position;
            // Debug.Log(panObjPos+" "+mtObjPos);
            if(mtObjPos.y<panObjPos.y-1){
                meatOutDeal();
                meatSurfItem.curSuf=-1;
            }
            
            // Quaternion qt1 = curMtObj.transform.localRotation;
            // Vector3 eng1 = qt1.eulerAngles;
            // Debug.Log(eng1);

            if(isPanHorMv){
                Vector3 curPos = curMtObj.transform.localPosition;
                Vector3 curAng = curMtObj.transform.eulerAngles;
            
                float pwRt = curSldLen/slideMaxLen;
                if(panMvDir<3){
                    int sign = 1;
                    if(panMvDir==2){
                        sign=-1;
                    }
                    curPos.z+=sign*flyHorSpd*pwRt;

                    // Debug.Log(pwRt+" "+sign+" "+curSldLen);
                    Quaternion qt = curMtObj.transform.localRotation;
                    Vector3 eng = qt.eulerAngles;

                    float addAng = meatAngSpd*sign*pwRt;
                    eng.z+=addAng;
                    // curMtObj.transform.localRotation = Quaternion.Euler(eng);
                    curMtObj.transform.Rotate(curPanObj.transform.right,addAng,Space.World);

                    // Debug.Log("panthrow.."+curMtObj.transform.eulerAngles);
                }
                else if(panMvDir==3){
                    // curPos.x-=flyHorSpd*pwRt;
                    curMtObj.transform.Rotate(curPanObj.transform.forward,meatAngSpd*pwRt,Space.World);

                }
                curMtObj.transform.localPosition=curPos;
            }
            else{
                Vector3 eng = curMtObj.transform.eulerAngles;
                // Vector3.zero
                if(!Util.judgeTwoVecEqu(eng,Vector3.zero)){
                    // curMtObj.transform.eulerAngles = Vector3.zero;
                }
            }
        }
    }

    void corretPanPos(){
        if(curPanObj==null){
            return;
        }

        Vector3 lPos = curPanObj.transform.localPosition;
        if(lPos.x<-4f){
            lPos.x = -4f;
            panPmc.enabled =false;
            isPanIdle = false;
        }
        else if(lPos.x>4f){
            lPos.x=4f;
            panPmc.enabled =false;
            isPanIdle = false;
        }
        else{
            panPmc.enabled =true;
            // isPanIdle = true;
        }
        
        if(lPos.z>1.5f){
            lPos.z = 1.5f;
            panPmc.enabled =false;
            isPanIdle = false;
        }
        else if(lPos.z<-6){
            lPos.z=-6;
            panPmc.enabled =false;
            isPanIdle = false;
        }
        else{
            panPmc.enabled =true;
            // isPanIdle = true;
        }
        curPanObj.transform.localPosition = lPos;
    }

    void Update(){
        // if(curMvExtTsf!=null){
        //     Debug.Log(curMvExtTsf.transform.localPosition);
        // }

        if(isMtLkPan){
            mtLkAccTm+=Time.deltaTime;
            staticMeatDeal();
            if(mtLkAccTm>mtLkInTm){
                mtLkAccTm=0;
                isMtLkPan = false;
            }
        }

        corretPanPos();

        if(pepPatleObjs!=null){
            for(int i=0;i<totalPepCt;i++){
                if(pepPatleObjs[i]!=null){
                    Transform ppItem =pepPatleObjs[i].transform;
                    if(ppItem.localPosition.y<ckPanObj.transform.localPosition.y){
                        Vector3 pos = ppItem.localPosition;
                        pos.y = ckPanObj.transform.localPosition.y+5f;
                        ppItem.localPosition = pos;
                        setObjGavity(ppItem,1,false);
                    }
                }
            }
        }
        
        panThrowMvDeal();
        panIdleDeal();

        if(isStaBrth){
            Text tt = fingObj.GetComponent<Text>();
            tt.color = new Color(1f,1f,1f,curAlVal/255f);
            curAlVal+=colFdSpd;
            if(curAlVal<colAlMin){
                colFdSpd=-colFdSpd;
            }
            else if(curAlVal>colAlMax){
                colFdSpd=-colFdSpd;
            }
        }

        // if(isWinPtrMv){
        //     doWinPtrMove();   
        // }
    }

    Vector3 cmStartTnPos;
    Vector3 cmStartTnRot;
    bool isCallBgTn = false;
  
    Sequence tnGdFigSeq=null;

    float mvTnCmSpd = 0.5f;
    void moveMainCam(Vector3 pos,Vector3 rot,TweenCallback cb=null){
        Quaternion qRot = Quaternion.Euler(rot.x,rot.y,rot.z);
        Sequence mvSeq = DOTween.Sequence();
        mvSeq.Append(mainCamera.transform.DOLocalMove(pos,mvTnCmSpd).SetEase(Ease.OutSine));
        mvSeq.Join(mainCamera.transform.DORotateQuaternion(qRot,mvTnCmSpd).SetEase(Ease.OutSine));
        mvSeq.AppendCallback(delegate(){
            if(cb!=null){
                cb();
            }
        });
        mvSeq.SetAutoKill();
    }

    bool isCallRAc = false;
    void playRoomDeal(){
        if(isCallRAc){
            return;
        }
        sitmObj.isVdoAdjX = false;
        isCallRAc = true;
        sitmObj.stopRun();
        doorObj.GetComponent<Animator>().enabled = true;

        Sequence opSeq = DOTween.Sequence();
        opSeq.AppendInterval(0.5f);
        opSeq.AppendCallback(delegate(){
            doorObj.transform.parent.gameObject.SetActive(false);
        });
        opSeq.AppendInterval(0.2f);
        opSeq.AppendCallback(delegate(){
            finRItm.playIdle();    
        });
        opSeq.AppendInterval(1.5f);
        opSeq.AppendCallback(delegate(){
            actionStep++;
        });
        opSeq.SetAutoKill();
    }

    //1 left 2 right
    float getHorBdBoder(int type){
        float[] ress = new float[]{-0.2f,0.2f};
        if(bdPosType==0){
            return ress[type-1];
        }
        else if(bdPosType==-1){
            return ress[type-1]-rightMaxDis/2;
        }
        else{
            return ress[type-1]+rightMaxDis/2;
        }
    }

    float chgWayAccTm=0;
    float chgWayInTm = 0.2f;
    //1 ford 2 left 3 behd 4 right
    void mnHorMoveDeal(){
        if(isMnTurn){
            chgWayAccTm+=Time.deltaTime;
            if(chgWayAccTm>chgWayInTm){
                if(horMoveSpd>0){
                    adjCurWayIdx(1);
                }
                else{
                    adjCurWayIdx(-1);
                }
                chgWayAccTm=0;
            }
        }
        else{
            Vector3 curMnPos =mnRoleObj.transform.position; 
        
            if(curMoveDir==1||curMoveDir==3){
                float posX = curMnPos.x;
                // Debug.Log("jjj11.."+horMoveSpd);
                if(curMoveDir==1){
                    posX+=horMoveSpd;
                }
                else{
                    posX-=horMoveSpd;
                }
                if(posX>curWyCtPos+rightMaxDis||posX<curWyCtPos-rightMaxDis){
                    horMoveSpd = 0;
                }
                else{
                    //挡板
                    if((posX>getHorBdBoder(1)&&forbidDir==1)||(posX<getHorBdBoder(2)&&forbidDir==-1)){
                        horMoveSpd=0;
                    }
                    else{
                        curMnPos.x = posX;
                        mnRoleObj.transform.position = curMnPos;
                        sitmObj.updateEmjPos(mnRoleObj.transform.localPosition.x);
                    }
                }
            }
            else{
                float posZ = curMnPos.z;
                // Debug.Log("jjj22.."+horMoveSpd);
                if(curMoveDir==2){
                    posZ+=horMoveSpd;
                }
                else{
                    posZ-=horMoveSpd;
                }
                // Debug.Log(".."+posZ+" "+curWyCtPos+" "+rightMaxDis);
                if(posZ>curWyCtPos+rightMaxDis||posZ<curWyCtPos-rightMaxDis){
                    horMoveSpd = 0;
                }
                else{
                    curMnPos.z = posZ;
                    mnRoleObj.transform.position = curMnPos;
                }
            }
        }
     
    }

    bool isCallSettle = false;
    GameObject wayPceObj;


    //100制
    float curFinPceVal = 0;
    int curFinPceIntVal = 0;
    void setFinPceVal(){
        curFinPceVal = Mathf.Clamp(curFinPceVal,0,100);
        curFinPceIntVal = Mathf.FloorToInt(curFinPceVal);

        Image barImg =finPcePlObj.transform.Find("bar").GetComponent<Image>();
        barImg.fillAmount = (float)curFinPceIntVal/100;

        Text finTt = finPcePlObj.transform.Find("Text").GetComponent<Text>();
        finTt.text = curFinPceIntVal+"%";
    }

    void setLoseCtDnVal(){
        Text ctTt = losePlObj.transform.Find("tmPl/Text").GetComponent<Text>();
        int ctDnIntVal = Mathf.FloorToInt(loseAccTm);
        ctTt.text = ctDnIntVal+""; 
    }

    void calFinBoxCt(){
        float totVal = 0;
        for(int i=0;i<11;i++){
            totVal+=curColorRates[i];
        }
        finFallBoxCt = Mathf.FloorToInt(finFlyBoxCt*totVal); 
        totVal*=100;
        Debug.Log("finbox..."+totVal);
    }

    bool isCmeRot = false;    
    float rotRadius = 5f;
    float cmRotAccAng = 0;
    //界面配置
    //<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>
    //水平前进速度 
    float moveSpdZ = 0.2f;
    float cMoveSpdZ = 0.2f;

    [Range(0f,1.7f)]    
    float moveSpdRt = 1;
    //设置推箱的赛道
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    int finAddMapNum = 1;
    float addRadius = 0;

    float cmSpdY = -1.5f;
    void turnAwayCme(){
        // sitmObj.stopRun();
        // if(!isCmeRot){
        //     return;
        // }
        // cmRotAccAng+=cmRotAccSpd;
        // if(cmRotAccAng>=90&&addRadius==0){
        //     cmRotAccAng = 0;
        //     addRadius = rotRadius;
        // }
        // if(addRadius>0&&cmRotAccAng>=108){
        //     isCmeRot = false;
        //     actionStep++;
        // }

        // Vector3 newPos = new Vector3(cmBeginPos.x,cmBeginPos.y,cmBeginPos.z);
        // if(addRadius==0){
        //     newPos.x+=rotRadius*Mathf.Sin(cmRotAccAng*Mathf.Deg2Rad);
        //     newPos.z+=rotRadius-rotRadius*Mathf.Cos(cmRotAccAng*Mathf.Deg2Rad);
        // }
        // else{
        //     newPos.x+=rotRadius*Mathf.Cos(cmRotAccAng*Mathf.Deg2Rad);
        //     newPos.z+=rotRadius*Mathf.Sin(cmRotAccAng*Mathf.Deg2Rad)+rotRadius;
        // }

        // newPos.y+=cmSpdY;
        // Vector3 mnPos = mnRoleObj.transform.localPosition;
        // mnPos.x = 0;
        // Vector3 lookDir = (mnPos-mainCamera.transform.localPosition).normalized;
        // mnPos.y-=3;
        // Vector3 lookDir1 = (mnPos-mainCamera.transform.localPosition).normalized;

        // mainCamera.transform.localPosition = newPos;
        // mainCamera.transform.localRotation = Quaternion.LookRotation(lookDir);
        // Vector3 enag = mainCamera.transform.eulerAngles;
        // enag.y-=3f;
        // mainCamera.transform.eulerAngles = enag;

        // mainLight.transform.localRotation = Quaternion.LookRotation(lookDir1);
        // mainLight1.transform.localRotation = Quaternion.LookRotation(lookDir1);

    }

    float pPAccAng = 0;
    float pPAccSpd = 1.2f;
    float movRadius = 3.5f;
    
    float cmRadius = 5;

    float cmAccAng = 0;
    float cmAccSpd = 1.1f;
    float cmMovRadius = 2f;
    float cmWaitAccTm = 0;
    float cmWaitInTm = 0.6f;

    bool isCallTurn = false;
    GameObject turnTarget = null;
    float turnSpdX = 1;
    float turnSpdZ = 1;
    int curTnPtIdx = 0;
    GameObject[][] wayPointObjs;
    Vector3[] wayCmPointPoss;
    
    float cmTurnSpdX = 1;
    float cmTurnSpdZ = 1;
    int curCmTnPtIdx = -1;

    //1 left 2 right
    int mnRoleTnTp = 1;

    //1 ford 2 left 3 behd 4 right
    int curMoveDir = 1;
    
    // 1 left 2 right
    void goNextMoveDir(int dir){
        if(dir==1){
            curMoveDir+=1;
            if(curMoveDir==5){
                curMoveDir=1;
            }
        }
        else{
            curMoveDir-=1;
            if(curMoveDir==0){
                curMoveDir=4;
            }
        }
    }


    float calTurnCmSpd(float pos1,float pos2){
        return (pos2-pos1)*0.05f*moveSpdRt;
    }

    float calTurnSpd(float pos1,float pos2,float scl=1){
        return (pos2-pos1)*0.1f*scl;
    }



    void calPushBoxTmScl(int boxCt){
        // BallItem.lstTmScl = boxCt/10;
        // BallItem.lstTmScl = 10;
    }

    bool finJumpIsCall = false;
    Light mainLight;
    Light mainLight1;
    int finFallBoxCt = 300;
    float finRedRate = 0.2f;

    //0.25 0.5

    void calBallOffset(){
        // float curX = mnRoleObj.transform.localPosition.x;
        // float delX = rightMaxDis/4;
        // if(curX>=-rightMaxDis&&curX<-rightMaxDis+delX){
        //     BallItem.posOffX = -2;    
        // }
        // else if(curX>=-rightMaxDis+delX&&curX<-rightMaxDis+delX*3){
        //     BallItem.posOffX = -1;
        // }
        // else if(curX>=-delX&&curX<delX){
        //     BallItem.posOffX = 0;
        // }
        // else if(curX>=delX&&curX<3*delX){
        //     BallItem.posOffX = 1;
        // }
        // else if(curX>=rightMaxDis-delX&&curX<=rightMaxDis){
        //     BallItem.posOffX = 2;
        // }
    }

    //爆炸处理
    void expBoxDeal(){
        // GameObject oriTarget = (GameObject)Resources.Load("Prefabs/boomTarget");
        // GameObject boomTarget = GameObject.Instantiate(oriTarget);
        // Rigidbody rig = boomTarget.GetComponent<Rigidbody>();
        // // rig.useGravity = false;
        // boomTarget.transform.SetParent(mnRoleObj.transform);
        // boomTarget.transform.localPosition = new Vector3(0,0.5f,0.6f);
        // boomTarget.transform.localScale = new Vector3(0.8f,0.7f,0.5f);

        // Sequence seq = DOTween.Sequence();
        // seq.AppendInterval(0.1f);
        // seq.AppendCallback(delegate(){
        //     boomTarget.GetComponent<m_MeshExploder>().HandleExplode(68, 1.5f, 1f, true);
        // });
        // seq.AppendInterval(0.15f);
        // seq.AppendCallback(delegate(){
        //     int curBoxCt = boomPtObj.transform.childCount;
        //     GameObject[] hdObjs = new GameObject[curBoxCt];
        //     int hdObjCt = 0;
        //     for(int i=0;i<curBoxCt;i++){
        //         GameObject item = boomPtObj.transform.GetChild(i).gameObject;
        //         float posZ = item.transform.localPosition.z;
        //         if(posZ<57.5f){
        //             hdObjs[hdObjCt]=item;
        //             hdObjCt++;
        //         }
        //     }
        //     for(int i=0;i<hdObjCt;i++){
        //         GameObject.Destroy(hdObjs[i]);
        //     }
        //     hdObjs = null;
        // });
        // seq.AppendInterval(1.5f);
        // seq.AppendCallback(delegate(){
        //     int curBoxCt = boomPtObj.transform.childCount;
        //     for(int i=0;i<curBoxCt;i++){
        //         GameObject item = boomPtObj.transform.GetChild(0).gameObject;
        //         GameObject.DestroyImmediate(item);
        //     }
        // });
        // seq.SetAutoKill();
        
    }


    GameObject drPtObj;
   
    void toStartSettle(){
        GameObject ballObj = mnRoleObj.transform.GetChild(1).gameObject;
        GameObject.Destroy(ballObj);
        // Debug.Log("xixi");
        actionStep=4;
    }

    float cmPpToBoxTm = 2f;
    void startPush(){
        Vector3 newCmPos = mainCamera.transform.localPosition;
        newCmPos.z+=8;
        newCmPos.x+=0.15f;
        Sequence cmSeq = DOTween.Sequence();
        cmSeq.Append(mainCamera.transform.DOLocalMove(newCmPos,cmPpToBoxTm));
        cmSeq.AppendCallback(delegate(){
            // sitmObj.playPush();
            Invoke("pushBoxDeal",0.7f);
        });
        cmSeq.SetAutoKill();
    }

    void finalJumpDeal(){
        if(finJumpIsCall){
            return;
        }
        finJumpIsCall =true;
        pushBtn.gameObject.SetActive(true);
    }
}