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

public class MapEdit : MonoBehaviour
{
    

    public Camera mainCme;
    public GameObject uiPt;
    Vector3 cmeOriPos = new Vector3(0,8,-20);
    
    Button leftBtn;
    Button rightBtn;
    Button upBtn;
    Button dnBtn;
    Button fordBtn;
    Button behdBtn;
    Button turnLftBtn;
    Button turnRgtBtn;
    public float cmeMoveSpd = 5;
    public float cmeTurnSpd = 15;

    Button itemBtn1;

    int[] chfHintNums;
    int[] rotHintNums;
    int[] patHintNums;
    int[] chiefNums;
    int[] guestsNums;
    int[] divPans;
    int[] divLights;
    int[] divCubes;
    int[] rotPtTps;

    int[][] dataStrs;

    Button saveBtn;

    int[][] seriMap;

    string mapPath = "";

    string getCurMapPath(int page){
        return "Assets/Resources/DataTables/BattleMapData"+page+".txt";
    }

    void initCacheData(){
        rotPtTps = new int[190];
        for(int i=0;i<190;i++){
            rotPtTps[i] = 1;
        }
        dataStrs = new int[19][];
        for(int i=0;i<19;i++){
            dataStrs[i] = new int[10];
        }
        chiefNums = new int[190];
        for(int i=0;i<190;i++){
            chiefNums[i]=-1;
        }

        chfHintNums = new int[190];
        for(int i=0;i<190;i++){
            chfHintNums[i]=0;
        }
        rotHintNums = new int[190];
        for(int i=0;i<190;i++){
            rotHintNums[i]=0;
        }
        patHintNums = new int[190];
        for(int i=0;i<190;i++){
            patHintNums[i]=0;
        }

        guestsNums = new int[190];
        for(int i=0;i<190;i++){
            guestsNums[i]=-1;
        }

        divCubes = new int[190];
        for(int i=0;i<190;i++){
            divCubes[i]=-1;
        }
        divLights = new int[190];
        for(int i=0;i<190;i++){
            divLights[i]=-1;
        }
        divPans = new int[190];
        for(int i=0;i<190;i++){
            divPans[i]=-1;
        }

        seriMap = new int[9][];
        seriMap[0] = chiefNums;
        seriMap[1] = guestsNums;
        seriMap[2] = divPans;
        seriMap[3] = divLights;
        seriMap[4] = divCubes;
        seriMap[5] = rotPtTps;

        seriMap[6] = rotHintNums;
        seriMap[7] = patHintNums;
        seriMap[8] = chfHintNums;
        
    }

    //10 直道
    //20 弯道 (1上右 2下右 3左下 4左上)
    //30 旋转平台
    //40 隔断灯(个位为编号 41 42)
    //50 隔断盘(个位为编号 51 52) 
    //60 捣乱的食客
    //70 厨师(个位为编号 71 72)
    //80 客人(个位为编号 81 82)
    //90 隔断块(个位为编号 91 92)

    //mapIdx 1开始
    void setSeriData(int itemIdx,int mapIdx,int val,bool isHint = false){
        int[] toSMap = new int[]{0,0,5,3,2,0,0,1,4,0};
        if(isHint){
            toSMap=null;
            toSMap = new int[]{0,0,6,0,7,0,8,0,0,0};
        }
        seriMap[toSMap[itemIdx-1]][mapIdx-1] = val;
    }

    int getSeriData(int itemIdx,int i,int j,bool isHint = false){
        int[] toSMap = new int[]{0,0,5,3,2,0,0,1,4,0};
          if(isHint){
            toSMap=null;
            toSMap = new int[]{0,0,6,0,7,0,8,0,0,0};
        }
        return seriMap[toSMap[itemIdx-1]][10*i+j];
    }

    void mapDataToScene(){
        for(int i=0;i<19;i++){
            for(int j=0;j<10;j++){
                if(dataStrs[i][j]>0){
                    int itemIdx = dataStrs[i][j]/10;
                    if(dataStrs[i][j]>99){
                        itemIdx = dataStrs[i][j]/100;
                    }
                    int exData = dataStrs[i][j]%10;
                    if(itemIdx==7||itemIdx==5){
                        exData = dataStrs[i][j]/10%10;
                    }
                    else if(itemIdx==3){
                        exData = dataStrs[i][j]-dataStrs[i][j]/100*100;
                    }
                    int mapIdx = (18-i)*10+j+1;
                    int tnType=0;
                    if(itemIdx==2||itemIdx==6||itemIdx==3){
                        tnType = exData;
                    }
                    createOneItem(mapIdx,itemIdx,tnType); 

                    if(itemIdx==7||itemIdx==5){
                        setSeriData(itemIdx,mapIdx,exData);
                        setSeriData(itemIdx,mapIdx,dataStrs[i][j]%10,true);
                    }
                    else if(itemIdx==4||itemIdx==8||itemIdx==9){
                        setSeriData(itemIdx,mapIdx,exData);
                    }
                    else if(itemIdx==3){
                        setSeriData(itemIdx,mapIdx,exData/10);
                        setSeriData(itemIdx,mapIdx,exData%10%2,true);
                    }
                }
            }
        }
    }

    string[] daTbTits;
    public int curPage=1;

    GameObject selcObj;
    void Awake(){

        initCacheData();
        mapPath = getCurMapPath(curPage);
        string[] readStrs = Util.readFileAll(mapPath,30);
        daTbTits = new string[12];
        for(int i=0;i<11;i++){
            daTbTits[i]=readStrs[i];
        }
        for(int i=12;i<31;i++){
            string[] linStr = readStrs[i-1].Split('\t');
            for(int j=0;j<10;j++){
                dataStrs[i-12][j] = Convert.ToInt32(linStr[j+2]);
            }
        }
        
        initMap();

        saveBtn = uiPt.transform.Find("btns/saveBtn").GetComponent<Button>();
        selcObj = uiPt.transform.Find("selcImg").gameObject;

        // mainCme = 
        leftBtn = uiPt.transform.Find("btns/left").GetComponent<Button>();
        rightBtn = uiPt.transform.Find("btns/right").GetComponent<Button>();
        fordBtn = uiPt.transform.Find("btns/ford").GetComponent<Button>();
        behdBtn = uiPt.transform.Find("btns/behd").GetComponent<Button>();

        upBtn = uiPt.transform.Find("btns/up").GetComponent<Button>();
        dnBtn = uiPt.transform.Find("btns/down").GetComponent<Button>();
        
        turnLftBtn = uiPt.transform.Find("btns/tLft").GetComponent<Button>();
        turnRgtBtn = uiPt.transform.Find("btns/tRgt").GetComponent<Button>();

        itemBtn1 = uiPt.transform.Find("btns/itemBtn1").GetComponent<Button>();

        rmAllBtn = uiPt.transform.Find("btns/rmAlBtn").GetComponent<Button>();

        operPl = uiPt.transform.Find("opPl").gameObject;
        operPl.SetActive(false);

        
        curItems = new GameObject[190];
        for(int i=0;i<190;i++){
            curItems[i]=null;
        }
        curItemCt=0;

        onRotPtCnObjs = new GameObject[10];
        onRtCnCt=0;

        initBtn();
        initMapGrid();
        mapDataToScene();
    }
    int curItemCt = 0;


    int calHitGridIndex(Vector3 inPos){
        for(int i=0;i<10;i++){
            for(int j=0;j<19;j++){
                if(inPos.x>mapGridPts[i][j].x&&inPos.x<=mapGridPts[i+1][j].x){
                    if(inPos.y>mapGridPts[i][j].y&&inPos.y<=mapGridPts[i][j+1].y){
                        return 10*j+i;
                    }
                }
            }
        }
        return -1;
    }



    float[] gridYs = new float[]{480,597,694,787,871,955,1026,1098,1163,1226,
                                1285,1344,1394,1447,1490,1534,1577,1618,1655,
                                1692,1727,1764,1795,1829,1857,1888,1914,};

    void initMapGrid(){
        mapGridPts = new Vector2[11][];
        for(int i=0;i<11;i++){
            mapGridPts[i] = new Vector2[27];
        }

        float xUnit = 78;
        float yUnit = 78;

        float xWidMax = 1075f;
        float xWidMin = 560f;
        float unitDx = (xWidMax-xWidMin)/26f;
        
        //x265 55
        //y 445

        //左边那根线
        for(int i=0;i<20;i++){
            mapGridPts[0][i] = new Vector2(156,427+i*yUnit);
        }

        for(int i=1;i<11;i++){
            for(int j=0;j<20;j++){
                mapGridPts[i][j] = new Vector2(156+xUnit*i,427+j*yUnit);
            }
        }

        // //左半块
        // for(int i=0;i<5;i++){
        //     for(int j=0;j<19;j++){
        //         float xLen = (xWidMax-j*unitDx)/10;
        //         float xPos = mapGridPts[5][j].x-xLen*(i+1);
        //         mapGridPts[4-i][j] = new Vector2(xPos,mapGridPts[5][j].y);
        //     }
        // }
        
        // //右半块
        // for(int i=0;i<5;i++){
        //     for(int j=0;j<19;j++){
        //         float xLen = (xWidMax-j*unitDx)/10;
        //         float xPos = mapGridPts[5][j].x+xLen*(i+1);
        //         mapGridPts[6+i][j] = new Vector2(xPos,mapGridPts[5][j].y);
        //     }
        // }

        // for(int i=0;i<19;i++){
        //     Debug.Log(i+" "+mapGridPts[0][i].y);
        // }
    }

    GameObject operPl;
    int curSelGdIdx = 1;
    public void onClickTouchPl(){
        Debug.Log("onctp... "+Input.mousePosition);
        int idx = calHitGridIndex(Input.mousePosition);
        Debug.Log(idx);
        if(idx==-1){
            return;
        }
        GameObject utPt = transform.Find("gdPl/mapPlUnit"+(idx+1)).gameObject;
        if(utPt.transform.childCount==0){
            return;
        }

        Text numTt = operPl.transform.Find("numTt").GetComponent<Text>();   
        numTt.text = "地板编号:"+(idx+1);
        
        GameObject extPl = operPl.transform.Find("extPl").gameObject;
        extPl.SetActive(false);
        Text pryTt = extPl.transform.Find("pryTt").GetComponent<Text>();
    
        GameObject hidItem = utPt.transform.GetChild(0).gameObject;
        InputField numIf = extPl.transform.Find("inNum").GetComponent<InputField>();
        InputField hinIf = extPl.transform.Find("hinNum").GetComponent<InputField>();
        GameObject hinTtObj = extPl.transform.Find("hinTt").gameObject;
        GameObject coTtObj = extPl.transform.Find("coinTt").gameObject;
        coTtObj.SetActive(false);
        hinIf.gameObject.SetActive(false);
        hinTtObj.SetActive(false);
        numIf.onEndEdit.RemoveAllListeners();
        // Debug.Log("oc11.."+patHintNums[116]+" "+patHintNums[153]);
        if(hidItem.name=="item7(Clone)"){
            extPl.SetActive(true);
            pryTt.text = "厨师编号:";
            numIf.text = chiefNums[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                chiefNums[temI] = Convert.ToInt32(cont);
            });

            hinIf.gameObject.SetActive(true);
            hinTtObj.SetActive(true);
            hinIf.text = chfHintNums[idx]+"";
            hinIf.onEndEdit.RemoveAllListeners();
            hinIf.onEndEdit.AddListener(delegate(string cont){
                chfHintNums[temI] = Convert.ToInt32(cont);
            });

        }
        else if(hidItem.name=="item8(Clone)"){
            extPl.SetActive(true);
            pryTt.text = "客人编号:";
            numIf.text = guestsNums[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                guestsNums[temI] = Convert.ToInt32(cont);
            });
        }
        else if(hidItem.name=="item4(Clone)"){
            extPl.SetActive(true);
            pryTt.text = "隔断灯编号:";
            numIf.text = divLights[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                divLights[temI] = Convert.ToInt32(cont);
            });
        }
        else if(hidItem.name=="item5(Clone)"){
            extPl.SetActive(true);
            pryTt.text = "隔断盘编号:";
            numIf.text = divPans[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                divPans[temI] = Convert.ToInt32(cont);
            });

            hinIf.gameObject.SetActive(true);
            hinTtObj.SetActive(true);
            hinIf.text = patHintNums[idx]+"";
            
            hinIf.onEndEdit.RemoveAllListeners();
            hinIf.onEndEdit.AddListener(delegate(string cont){
                // Debug.Log("hh.."+cont+" "+temI);
                patHintNums[temI] = Convert.ToInt32(cont);
                // Debug.Log("oc33.."+patHintNums[116]+" "+patHintNums[153]);
            });
        }
        else if(hidItem.name=="item9(Clone)"){
            extPl.SetActive(true);
            pryTt.text = "隔断块编号:";
            numIf.text = divCubes[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                divCubes[temI] = Convert.ToInt32(cont);
            });
        }
        else if(hidItem.name=="item3(Clone)"){
            extPl.SetActive(true);
            coTtObj.SetActive(true);
            Text coinTt = coTtObj.GetComponent<Text>();
            if(utPt.transform.childCount==2){
                coinTt.text = "是否有金币:1";
            } 
            else{
                coinTt.text = "是否有金币:0";
            }

            pryTt.text = "转台类型:";
            numIf.text = rotPtTps[idx]+"";
            int temI = idx;
            numIf.onEndEdit.RemoveAllListeners();
            numIf.onEndEdit.AddListener(delegate(string cont){
                rotPtTps[temI] = Convert.ToInt32(cont);
                switchItem3Type(hidItem,rotPtTps[temI]);
            });

            hinIf.gameObject.SetActive(true);
            hinTtObj.SetActive(true);
            hinIf.text = rotHintNums[idx]+"";
            hinIf.onEndEdit.RemoveAllListeners();
            hinIf.onEndEdit.AddListener(delegate(string cont){
                rotHintNums[temI] = Convert.ToInt32(cont);
            });
        }
        // Debug.Log("oc22.."+patHintNums[116]+" "+patHintNums[153]);
        
        operPl.SetActive(true);
        curSelGdIdx = (idx+1);

    }

    //3 5 7...(10个)

    //10 直道
    //2x 弯道 (21上右 22下右 23左下 24左上)
    //30 旋转平台(31 直道 32 弯道)
    //40 隔断灯(个位为编号 41 42)
    //50 隔断盘(个位为编号 51 52) 
    //60 捣乱的食客
    //70 厨师(个位为编号 71 72)
    //80 客人(个位为编号 81 82)
    //90 隔断块(个位为编号 91 92)


    void writeToDataTable(){
        string[] conts = new string[30];
        for(int i=0;i<11;i++){
            conts[i] = daTbTits[i];
        }
        for(int i=11;i<30;i++){
            string lStr = "";
            lStr+="\t";
            lStr+=i-3;
            for(int j=0;j<10;j++){
                lStr+="\t";
                lStr+=dataStrs[i-11][j];
            }
            conts[i]=lStr;
        }
        Util.writeFileAll(mapPath,conts);
    }


    //胖子 下 1 左 2

    int getTurnItType(Vector3 eng){
        if(Mathf.Abs(eng.y)<Mathf.Epsilon){
            return 1;
        }
        else if(Mathf.Abs(eng.y-90)<Mathf.Epsilon){
            return 2;
        }
        else if(Mathf.Abs(eng.y-180)<Mathf.Epsilon){
            return 3;
        }
        else if(Mathf.Abs(eng.y-270)<Mathf.Epsilon){
            return 4;
        }
        return 1;
    }

    bool checkNearHasPat(int i,int j){
        if(i-1>=0&&dataStrs[i-1][j]/100==3){
            return true;
        }
        if(i+1<19&&dataStrs[i+1][j]/100==3){
            return true;
        }
        if(j-1>=0&&dataStrs[i][j-1]/100==3){
            return true;
        }
        if(j+1<10&&dataStrs[i][j+1]/100==3){
            return true;
        }
        return false;
    }
    
    void formatCoinDeal(){
        for(int i=0;i<19;i++){
            for(int j=0;j<10;j++){
                if(dataStrs[i][j]/10==2&&checkNearHasPat(i,j)){
                    dataStrs[i][j] = 26;
                }
            }
        }
    }

    Button rmAllBtn;
    void initBtn(){
        saveBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.AddListener(delegate(){
            for(int i=0;i<19;i++){
                for(int j=0;j<10;j++){
                    GameObject itemPt = transform.Find("gdPl/mapPlUnit"+(i*10+j+1)).gameObject;
                    
                    if(itemPt.transform.childCount==0){
                        dataStrs[18-i][j] = 0;
                        continue;
                    }
                    GameObject hitItem = itemPt.transform.GetChild(0).gameObject;
                    int itemIdx = Convert.ToInt32(hitItem.name.Substring(4,1));

                    if(itemIdx==6){
                        int itVal = itemIdx*10;
                        Vector3 enAngs = hitItem.transform.eulerAngles;
                        itVal+=getTurnItType(enAngs);
                        dataStrs[18-i][j] = itVal;
                    }
                    else if(itemIdx==2){    
                        int itVal = itemIdx*10;
                        dataStrs[18-i][j] = itVal;
                    }
                    else if(itemIdx==1){
                        dataStrs[18-i][j] = itemIdx*10;
                    }
                    else if(itemIdx==3){
                        int itVal = itemIdx*100;
                        int serNum = getSeriData(itemIdx,i,j);
                        int serNum1 = getSeriData(itemIdx,i,j,true);
                        
                        if(serNum==1){
                            float ang = Util.formatAng(hitItem.transform.eulerAngles.y);
                            if(ang==0||ang==180){
                                serNum=1;
                            }
                            else{
                                serNum=2;
                            }
                        }
                        else{
                            float ang = Util.formatAng(hitItem.transform.eulerAngles.y);
                            serNum = (int)ang/90+6;
                        }
                        int coinVal = 0;
                        if(itemPt.transform.childCount==2){
                            coinVal+=2;
                        }                        
                        dataStrs[18-i][j] = itVal+serNum*10+coinVal+serNum1;
                    }
                    else if(itemIdx==5||itemIdx==7){
                        int itVal = itemIdx*100;
                        int serNum1 = getSeriData(itemIdx,i,j); 
                        int serNum = getSeriData(itemIdx,i,j,true);
                        if(serNum==-1){
                            serNum=0;
                        }
                        dataStrs[18-i][j] = itVal+serNum1*10+serNum;
                    }
                    else{
                        int itVal = itemIdx*10;
                        int serNum = getSeriData(itemIdx,i,j);
                        if(serNum==-1){
                            serNum=0;
                        }
                        dataStrs[18-i][j] = itVal+serNum;
                    }
                }
            }

            formatCoinDeal();

            writeToDataTable();
            Debug.Log("保存完成！");
            // dataStrs
        });

        Button opRotBtn = operPl.transform.Find("rotBtn").GetComponent<Button>();
        opRotBtn.onClick.RemoveAllListeners();
        opRotBtn.onClick.AddListener(delegate(){
            GameObject cd = transform.Find("gdPl/mapPlUnit"+curSelGdIdx).GetChild(0).gameObject;
            if(cd.name!="item2(Clone)"&&cd.name!="item6(Clone)"&&cd.name!="item3(Clone)"){
                return;
            }
            Vector3 curEng = cd.transform.eulerAngles;
            curEng.y+=90;
            cd.transform.eulerAngles = curEng;
        });


        Button opCloRect = operPl.transform.Find("bg0").GetComponent<Button>();
        opCloRect.onClick.RemoveAllListeners();
        opCloRect.onClick.AddListener(delegate(){
            operPl.SetActive(false);
        });

        Button opCloBtn = operPl.transform.Find("cloBtn").GetComponent<Button>();
        opCloBtn.onClick.RemoveAllListeners();
        opCloBtn.onClick.AddListener(delegate(){
            operPl.SetActive(false);
        });

        Button opRmBtn = operPl.transform.Find("rmvBtn").GetComponent<Button>();
        opRmBtn.onClick.RemoveAllListeners();
        opRmBtn.onClick.AddListener(delegate(){
            int cdCt = transform.Find("gdPl/mapPlUnit"+curSelGdIdx).childCount;
            if(cdCt==1){
                GameObject cd = transform.Find("gdPl/mapPlUnit"+curSelGdIdx).GetChild(0).gameObject;
                GameObject.Destroy(cd);
                curItemCt--;
            }
            else if(cdCt==2){
                GameObject cd = transform.Find("gdPl/mapPlUnit"+curSelGdIdx).GetChild(1).gameObject;
                GameObject.Destroy(cd);
                onRtCnCt--;
            }
            operPl.SetActive(false);
        });
        

        rmAllBtn.onClick.RemoveAllListeners();
        rmAllBtn.onClick.AddListener(delegate(){
            for(int i=0;i<curItemCt;i++){
                GameObject.Destroy(curItems[i]);
                curItems[i]=null;
            }
            for(int i=0;i<onRtCnCt;i++){
                GameObject.Destroy(onRotPtCnObjs[i]);
                onRotPtCnObjs[i]=null;
            }
            curItemCt=0;
        });


        leftBtn.onClick.RemoveAllListeners();
        leftBtn.onClick.AddListener(delegate(){
            moveCmeHor(-1);
        });
        rightBtn.onClick.RemoveAllListeners();
        rightBtn.onClick.AddListener(delegate(){
            moveCmeHor(1);
        });

        fordBtn.onClick.RemoveAllListeners();
        fordBtn.onClick.AddListener(delegate(){
            moveCmeFBd(1);
        });
        behdBtn.onClick.RemoveAllListeners();
        behdBtn.onClick.AddListener(delegate(){
            moveCmeFBd(-1);
        });     

        upBtn.onClick.RemoveAllListeners();
        upBtn.onClick.AddListener(delegate(){
            moveCmeVer(1);
        });
        dnBtn.onClick.RemoveAllListeners();
        dnBtn.onClick.AddListener(delegate(){
            moveCmeVer(-1);
        });       

        turnLftBtn.onClick.RemoveAllListeners();
        turnLftBtn.onClick.AddListener(delegate(){
            turnCmeHor(-1);
        });
        turnRgtBtn.onClick.RemoveAllListeners();
        turnRgtBtn.onClick.AddListener(delegate(){
            turnCmeHor(1);
        });        
    }

    public void onPressDown(){
        Debug.Log("onpd..");
        Vector3 curPos = Input.mousePosition;
        curPos.x-=540;
        curPos.y-=970;
        selcObj.transform.localPosition = curPos;
        // Debug.Log(Input.mousePosition);
    }

    public void onItemDrag(){
        // Debug.Log("ondrg..");
        Vector3 curPos = Input.mousePosition;
        curPos.x-=540;
        curPos.y-=970;
        selcObj.transform.localPosition = curPos;
    }

    
    GameObject[] onRotPtCnObjs;
    int onRtCnCt=0;
    GameObject[] curItems;
    Vector2[][] mapGridPts;
    public void onItemUp(int itemIdx){
        int hitIdx = calHitGridIndex(Input.mousePosition);
        Debug.Log("onUp.."+hitIdx+" ");
        if(hitIdx==-1){
            return;
        }
        GameObject itePt = transform.Find("gdPl/mapPlUnit"+(hitIdx+1)).gameObject;
        bool isRotCn = false;
        if(itePt.transform.childCount==1){
            GameObject chd = itePt.transform.GetChild(0).gameObject;
            if(chd.name=="item3(Clone)"){
                isRotCn = true;
            }
            else{
                return;
            }
        }
        else if(itePt.transform.childCount==2){
            return;
        }

        createOneItem(hitIdx+1,itemIdx,0,isRotCn);
    }


    void switchItem3Type(GameObject item,int type){
        GameObject sub1 = item.transform.Find("strt").gameObject;
        GameObject sub2 = item.transform.Find("turn").gameObject;
        sub1.SetActive(type==1);
        sub2.SetActive(type==2);
    }

    //10 直道
    //2x 弯道 (21上右 22下右 23左下 24左上)
    //3xx 旋转平台(31 直道0度 32 直道180度 | 36 弯道0度 37 弯道90度 38 弯道180度 39 弯道270度) 末尾提示
    //40 隔断灯(个位为编号 41 42)
    //500 隔断盘(个位为编号 51 52) 末尾提示 
    //60 捣乱的食客
    //700 厨师(个位为编号 71 72) 末尾提示
    //80 客人(个位为编号 81 82)
    //90 隔断块(个位为编号 91 92)

    //都从1开始
    void createOneItem(int mapIdx,int itemIdx,int tnType=0,bool isRotCn = false){
        // Debug.Log(itemIdx);
        GameObject oriItem = (GameObject)Resources.Load(AssetUtility.getFloorBasePrefab(itemIdx)); 
        GameObject boxItem = GameObject.Instantiate(oriItem); 
        GameObject itePt = transform.Find("gdPl/mapPlUnit"+mapIdx).gameObject;
        boxItem.transform.SetParent(itePt.transform);
        boxItem.transform.localPosition = new Vector3(0.07f,1f,0);

        if(tnType>0){
            boxItem.transform.eulerAngles = new Vector3(0,(tnType-1)*90,0);
        }

        if(itemIdx>=3&&itemIdx<=5){
            boxItem.transform.localPosition = new Vector3(0f,0.95f,0f);
            boxItem.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            if(itemIdx==3){
                if(tnType<5){
                    switchItem3Type(boxItem,1);
                }
                else{
                    switchItem3Type(boxItem,2);
                }
                // Debug.Log(tnType);
                if(tnType==0){
                }
                else{
                    Debug.Log("xixi.."+tnType);
                    int dir = tnType/10;
                    int hasCn = tnType%10/2;
                    if(dir==1){
                        boxItem.transform.eulerAngles = new Vector3(0,0,0);
                    }
                    else if(dir==2){
                        boxItem.transform.eulerAngles = new Vector3(0,90,0);
                    }
                    else{
                        boxItem.transform.eulerAngles = new Vector3(0,90*(dir-6),0);
                    }

                    if(hasCn==1){
                        createOneItem(mapIdx,2);
                    }
                }
            }
            else if(itemIdx==5){
                boxItem.transform.localPosition = new Vector3(0f,1.1f,0f);
                boxItem.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
            }
            else if(itemIdx==4){
                boxItem.transform.localPosition = new Vector3(0.42f,1.1f,-3.25f);
                boxItem.transform.localScale = new Vector3(1f,1f,1f);
            }
        }
        else if(itemIdx==6){
            boxItem.transform.localPosition = new Vector3(0f,0.5f,0f);
            boxItem.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
        else if(itemIdx==7){
            boxItem.transform.localPosition = new Vector3(0f,0.6f,-0.2f);
            boxItem.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
        else if(itemIdx==8){
            boxItem.transform.localPosition = new Vector3(0f,0.5f,-0.1f);
            boxItem.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
        else if(itemIdx==9){
            boxItem.transform.localPosition = new Vector3(0.07f,1.8f,0);
            boxItem.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        }

        if(isRotCn){
            onRotPtCnObjs[onRtCnCt]=boxItem;
            onRtCnCt++;            
        }
        else{
            curItems[curItemCt] = boxItem;
            curItemCt++;
        }
    }

    // update 选颜色
    // 编号决定路径
    //3 旋转平台
    //4 隔断灯
    //5 隔断盘
    //6 捣乱的食客
    //7 厨师
    //8 客人
    //9 隔断块


    public Material[] mpUtMats;
    public Material[] gustMats;
    void initMap(){
        GameObject oriObj = transform.Find("gdPl/mapPlUnit1").gameObject;        
        Vector3 oriPos = oriObj.transform.localPosition;
        for(int i=0;i<19;i++){
            for(int j=0;j<10;j++){
                if(i==0&&j==0){
                }
                else{
                    GameObject newObj = GameObject.Instantiate(oriObj);
                    newObj.transform.SetParent(oriObj.transform.parent);
                    newObj.name = "mapPlUnit"+(10*i+j+1);
                    Vector3 newPos = new Vector3(-8.8f+j*2,oriPos.y,i*2-9.3f);
                    newObj.transform.localPosition = newPos;
                    newObj.transform.localScale = new Vector3(2,2f,2);
                    MeshRenderer mrd = newObj.GetComponent<MeshRenderer>();
                    mrd.material = mpUtMats[(i+j)%2];
                }
            }
        }
    }

    //ht
    //cube 1 lit 0.5 rot 1 wayMd 1.4 bana 0.2
    // 除了cube 缩放都是2

    void turnCmeHor(int dir){
        Vector3 newPos = mainCme.transform.eulerAngles;        
        newPos.y+=dir*cmeTurnSpd;
        mainCme.transform.eulerAngles = newPos;
    }

    //-1 1
    void moveCmeHor(int dir){
        Vector3 newPos = mainCme.transform.localPosition;        
        newPos.x+=dir*cmeMoveSpd;
        mainCme.transform.localPosition = newPos;
    }
    //-1 1
    void moveCmeVer(int dir){
        Vector3 newPos = mainCme.transform.localPosition;        
        newPos.y+=dir*cmeMoveSpd;
        mainCme.transform.localPosition = newPos;
    }
    //-1 1
    void moveCmeFBd(int dir){
        Vector3 newPos = mainCme.transform.localPosition;        
        newPos.z+=dir*cmeMoveSpd;
        mainCme.transform.localPosition = newPos;
    }

    void Update(){
     
    }


}
