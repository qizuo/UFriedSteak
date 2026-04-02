
using System.Net;
using System.Net.NetworkInformation;
using System.Data;
using System.Collections.Generic;

using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using StarForce;
// using Umeng;

public class Util
{

    public static int[][] getTableData(int row,int col,string table){
        int[][] dataArr = new int[row][];
        // string dPath = AssetUtility.GetDataTableAsset(table,false);
        TextAsset cont = Resources.Load<TextAsset>("DataTables/"+table);
        for(int i=5;i<row+5;i++){
            int idx = i-5;
            string t1 = getRowString(i,cont.text);
            // Debug.Log(t1);
            string[] sRes = t1.Split('\t');
            // Debug.Log(sRes.Length);
            dataArr[idx] = new int[col];
            for(int j=0;j<col;j++){
                // Debug.Log("jajaja..."+sRes[j+2]);
                dataArr[idx][j]=Convert.ToInt32(sRes[j+2]);
            }
        }
        return dataArr;
    }

    public static int randomIdxVal(float[] rateArr){
        float[] totalRate = new float[rateArr.Length];
        totalRate[0] = rateArr[0];
        for(int i=1;i<rateArr.Length;i++){
            totalRate[i]=totalRate[i-1]+rateArr[i];
        }
        int val = UnityEngine.Random.Range(1,101);
        int res = 0;
        for(int i=0;i<rateArr.Length-1;i++){
            if(val<totalRate[i]*100){
                res = i;
                break;
            }
        }
        return  res;
    }

    //几种颜色
    public static int[] getTableColors(int row,int col,int[][] table){
        int[] colMap = new int[MainForm.colorTotCt];
        for(int i=0;i<MainForm.colorTotCt;i++){
            colMap[i]=0;
        }   

        for(int i=0;i<row;i++){
            for(int j=0;j<col;j++){
                colMap[table[i][j]]++;
            }
        }

        int ct=0;
        for(int i=0;i<MainForm.colorTotCt;i++){
            if(colMap[i]>0){
                ct++;
            }
        }
        int[] res = new int[ct];
        int cIdx=0;
        for(int i=0;i<MainForm.colorTotCt;i++){
            if(colMap[i]>0){
                res[cIdx]=i;
                cIdx++;
            }
        }
        return res;
    }

    //
    public static float[] getTableColorsRate(int row,int col,int[][] table,int[] color){
        // int ct = 0;
        int[] colCtArr = new int[color.Length];
        for(int i=0;i<color.Length;i++){
            colCtArr[i]=0;
        }
        float[] res = new float[MainForm.colorTotCt];

        for(int i=0;i<row;i++){
            for(int j=0;j<col;j++){
                for(int k=0;k<color.Length;k++){
                    if(table[i][j]==color[k]){
                        colCtArr[k]++;
                    }
                }
            }
        }
        for(int k=0;k<color.Length;k++){
            res[color[k]] = (float)colCtArr[k]/(row*col);
        }

        return res;
    }

    //type 颜色
    public static int getTableRowRan(int[] data,int len,int type){
        int newLen = 0;
        for(int i=0;i<len;i++){
            if(data[i]==type){
                newLen++;
            }
        }
        int[] newArr = new int[newLen];
        int nIdx=0;
        for(int i=0;i<len;i++){
            if(data[i]==type){
                newArr[nIdx]=i;
                nIdx++;
            }
        }
        int ranInt = UnityEngine.Random.Range(0,newLen);
        if(newLen==0){
            return -1;
        }
        return newArr[ranInt];
    }

    // row col min max
    // 1-row
    public static int[] getTableBorder(int row,int col,int[][] table){
        int min = 1;
        int max = row;

        bool allZ = true;
        for(int i=0;i<row;i++){
            for(int j=0;j<col;j++){
                if(table[i][j]==1){
                    allZ = false;
                }
            }
            if(allZ){
                min++;  
            }
        }
        if(min>row){
            min=row;
        }

        bool mAllZ = true;
        for(int i=row-1;i>=0;i--){
            for(int j=0;j<col;j++){
                if(table[i][j]==1){
                    mAllZ = false;
                }
            }
            if(mAllZ){
                max--;  
            }
        }
        if(max<1){
            max=1;
        }
        return new int[]{row,col,min,max};
    }


    public static string fileReadLineAt(int lineNumber, string path)
    {
        string res = "";
        FileInfo fi=new FileInfo(path);
        if(!fi.Exists) return res;
        FileStream fs=new FileStream(path, FileMode.Open);
        StreamReader sr=new StreamReader(fs);
        for(int i=0;i<lineNumber-1;i++)
        {
            sr.ReadLine();
        }
        res = sr.ReadLine();
        fs.Close();
        sr.Close();
        return res;
    }


    public static float formatAng(float ang){
        while(ang>360){
            ang-=360;
        }
        while(ang<0){
            ang+=360;
        }
        return ang;
    }

    public static string[] readFileAll(string path,int count){
        string[] res = new string[count];
        StreamReader sr = new StreamReader(path);
        for(int i=0;i<count;i++){
            res[i] = sr.ReadLine();
        }
        sr.Close();
        return res;
    }

    //不能同时打开
    public static void writeFileAll(string path,string[] conts){
        StreamWriter sw = new StreamWriter(path);
        for(int i=0;i<conts.Length;i++){
            sw.WriteLine(conts[i]);
        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
    }


    // public static string 
    public static string getRowString(int lineNumber, string cont)
    {
        int curLine = 1;
        string res = "";
        for(int i=0;i<cont.Length;i++){
            string c = cont.Substring(i,1);
            if(c=="\n"){
                curLine++;
                if(curLine>lineNumber){
                    break;
                }
            }
            if(curLine==lineNumber){
                res+=c;
            }
        }
        return res;
    }


    // // type 1 曝光 2 点击 3 获奖
    // //基本 6个参数
    // //sub_scene,error_code,reward_type,reward_name,reward_num 
    // // 曝光 追加
    // // ad_state(0 不可播放 1可播放) (error_code(0无错误 1广告位冷却 2无广告填充 3其他))
    // //点击 追加
    // // ad_state,ad_session_id(时间戳),ad_source(穿山甲)(error_code(0无错误 1广告位冷却 2无广告填充 3其他))
    // // 获奖 追加
    // // ad_session_id,ad_source,ad_result(0失败 1完成),get_reward(0 未获得 1 获得),(error_code(0 成功))        
    // public static void triUmengEvent(int type,string key,string[] uParamVals){
    //     Dictionary<string,string> umParam = new Dictionary<string, string>();
    //     umParam["sub_scene"]=uParamVals[0];
    //     umParam["error_code"]=uParamVals[1];
    //     umParam["reward_type"]=uParamVals[2];
    //     umParam["reward_name"]=uParamVals[3];
    //     umParam["reward_num"]=uParamVals[4];

    //     if(type==1){
    //         umParam["ad_state"]=uParamVals[5];
    //     }
    //     else if(type==2){
    //         umParam["ad_state"]=uParamVals[5];
    //         umParam["ad_session_id"]=uParamVals[6];
    //         umParam["ad_source"]=uParamVals[7];
    //     }
    //     else if(type==3){
    //         umParam["ad_session_id"]=uParamVals[5];
    //         umParam["ad_source"]=uParamVals[6];
    //         umParam["ad_result"]=uParamVals[7];
    //         umParam["get_reward"]=uParamVals[8];
    //     }
    //     GA.Event(key,umParam);
    // }


    //返回90的倍数
    public static float calEndRotVal(float val){
        float res = val;
        if(val>=0){
            float ex = val%90;
            if(ex<45){
                res-=ex;
            }
            else{
                res+=(90-ex);
            }
        }
        else{
            float ex = -val%90;
            if(ex<45){
                res+=ex;
            }
            else{
                res-=(90-ex);
            }
        }
        return res;
    }

    public static bool judgeTwoVecEqu(Vector3 a,Vector3 b){
        if(a.x==b.x&&a.y==b.y&&a.z==b.z){
            return true;
        }
        return false;
    }

    public static bool judgeTwoVec2Equ(Vector2 a,Vector2 b){
        if(Mathf.Abs(a.x-b.x)<5f&& Mathf.Abs(a.y-b.y)<5f){
            return true;
        }
        return false;   
    }

    // local
    public static void tfSetNewPos(Transform item,Vector3 pos,int type=1){
        Vector3 res = new Vector3(pos.x,pos.y,pos.z);
        item.localPosition =res;
    }

    
    public static string GetNetDateTime()
    {
        WebRequest request = null;
        WebResponse response = null;
        WebHeaderCollection headerCollection = null;
        string datetime = string.Empty;
        try
        {
            request = WebRequest.Create("https://www.baidu.com");
            request.Timeout = 3000;
            request.Credentials = CredentialCache.DefaultCredentials;
            response = (WebResponse)request.GetResponse();
            headerCollection = response.Headers;
            foreach (var h in headerCollection.AllKeys)
            { if (h == "Date") { datetime = headerCollection[h]; } }
            return datetime;
        }
        catch (Exception) { return datetime; }
        finally
        {
            if (request != null)
            { request.Abort(); }
            if (response != null)
            { response.Close(); }
            if (headerCollection != null)
            { headerCollection.Clear(); }
        }
    }

    public static int GetNetDateTimeSec(){
        string datetime = GetNetDateTime();
        if(datetime==string.Empty){
            return -1;
        }
        TimeSpan ts = Convert.ToDateTime(datetime) - new DateTime(1970, 1, 1, 8, 0, 0, 0); 
        return Convert.ToInt32(ts.TotalSeconds);
    }


    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalMilliseconds).ToString();
    }      
    public static int GetTimeStampSec()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt32(ts.TotalSeconds);
    }     

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Numeric/deleteDB")]
    public static void deleteOldDBFile(){
        string filePath = Application.persistentDataPath+"/fruitTribe.db";
        Debug.Log(Application.persistentDataPath+"/fruitTribe.db");
        if(!File.Exists(filePath)){
            Debug.Log("file not exist");
            return ;
        }
        File.Delete(filePath);
        Debug.Log("delete finish");
    }
    [UnityEditor.MenuItem("Numeric/initPpf")]
    public static void initPpf(){
        // Debug.Log(Convert.ToInt64(Util.GetTimeStamp()));
        if(PlayerPrefs.HasKey("firstDone")){
            PlayerPrefs.DeleteKey("firstDone");
        }
        Debug.Log("initPpf finish");
    }
#endif

    public static int getStrIdx(string[] strArr,string val){
        for(int i=0;i<strArr.Length;i++){
            if(strArr[i]==val){
                return i;
            }
        }
        return -1;
    }


        //判断版本号，是否是第一次安装
    // public static bool judgeFirstInstall(){
    //     string filePath="";
    //     if(!DataManage.isEncrypt){
    //         filePath = Application.persistentDataPath+"/fruitTribe.db";    
    //     }
    //     else{
    //         filePath = Application.persistentDataPath+"/fruitTribeEpt.db";
    //     }

    //     if(PlayerPrefs.HasKey("firstDone")){
    //     // Debug.Log("HasKey(firstDone");
    //         if(PlayerPrefs.GetFloat("firstDone")!=NumericalCalculator.versionId){
    //             return true;
    //         }
    //     }
    //     else{
    //         // Debug.Log("init ppf");
    //         return true;
    //     }
    //     return false;
    // }

    //二进制star 转化 star总数
    public static int getAcStar(int binStar){
        int res = 0;
        while(binStar>0){
            if(binStar%2==1){
                res++;
            }
            binStar/=2;
        }
        return res;
    }
    //二进制star 转化 位置数组
    public static int[] getAcStarPos(int binStar){
        int[] res = new int[3]{0,0,0};
        for(int i=0;i<3;i++){
            if(binStar%2==1){
                res[2-i]=1;
            }
            binStar/=2;
        }
        return res;
    }
    //数组 转化 bin总数
    public static int getArrAcStar(int[] starArr){
        int res = 0;
        int scale = 1;
        for(int i=0;i<3;i++){
            if(starArr[2-i]==1){
                res+=scale;
            }
            scale*=2;
        }
        return res;
    }

    public static string getPercentStr(float val){
        return val*100+"%";
    }

    public static int[] genRanInts(int num,int max,int staIdx=1){
        int[] res = new int[num]; 
        for(int i=0;i<num;i++){
            res[i]=-1;
        }
        bool notHasElem(int val){
            for(int i=0;i<num;i++){
                if(res[i]==val){
                    return false;
                }
            }
            return true;
        }
        int resIdx=0;
        while(true){
            if(resIdx>num-1){
                break;
            }
            float ranFlt = UnityEngine.Random.value;
            int ranInt = Mathf.FloorToInt(ranFlt*100)%max+staIdx;
            if(notHasElem(ranInt)){
                res[resIdx] = ranInt;
                resIdx++;
            }
        }
        return res;
    }

    public static bool isNumeric(string str)
    {
        if(str==""){
            return false;
        }
        char[] ch=new char[str.Length];
        ch=str.ToCharArray();
        for(int i=0;i<str.Length;i++)
        {
            if(ch[i]<48 || ch[i]>57)
                return false;
        }
        return true;
    }

    //0 no 1 left 2 right
    public static bool isTwoPositionClose(Vector3 a,Vector3 b,float dis){
        float powX = Mathf.Pow(a.x-b.x,2);
        float powZ = Mathf.Pow(a.z-b.z,2);
        if(powX+powZ<dis){
            return true;
        }
        return false;
    }

    public static int getStrNumCt(string str){
        int ct = 0;
        if(str==""){
            return ct;
        }
        char[] ch=new char[str.Length];
        ch=str.ToCharArray();
        for(int i=0;i<str.Length;i++)
        {
            if(ch[i]>=48&&ch[i]<=57)
            {
                ct = ct+1;
            }
        }
        return ct;
    }

    //type 0 normal 1 grey
    // public static void setBtnGray(Image btnImg,int type){
    //     if(type==0){
    //         btnImg.material = null;
    //     }
    //     else{
    //         btnImg.material = MainForm.greyMat;
    //     }
    // }


    //decCt 小数个数
    public static float formatDeciNum(float val,int decCt){
        float scl = decCt*10;
        return Mathf.RoundToInt(val*scl)/scl;
    }

    //保留一位小数
    public static string goldFormat(float val){
        if(val<1000f){
            return val+"";
        }
        else if(val<1000000f){
            return Mathf.RoundToInt(val/100f)/10f+"k";
        }
        else{
            return Mathf.RoundToInt(val/1000f)/1000f+"m";
        }
    }

    public static string intArrayToStr(int[] arr){
        string res = "";
        for(int i=0;i<arr.Length;i++){
            res = res + arr[i]+",";
        }
        return res;
    }

    public static int[] strToIntArray(string str){
        string[] res = str.Split(',');
        int ct = 0;
        while(res[ct].Length>0){
            ct++;
        }
        int[] ires = new int[ct];
        for(int i=0;i<ct;i++){
            ires[i] = Convert.ToInt32(res[i]);
        }
        return ires;
    }
}
