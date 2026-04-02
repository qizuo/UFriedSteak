using System.Net.Http.Headers;
using System.ComponentModel;
using System.Threading;
using System.Security.AccessControl;
using System.Globalization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Game.Common;
using System;
using DG.Tweening;
using GameFramework.DataTable;
using GameFramework.Resource;
// using UnityGameFramework.Runtime;
// using StarForce;
// using GameEntry = StarForce.GameEntry;
using UnityEngine.UI;

namespace Battle
{
    public class SceneMain : MonoBehaviour
    {
        // TODO: 临时用，以后改为加载
        // public GameObject monsterDeadEffectPrefab;
        // //普攻 暴击 秒杀
        // public GameObject monsterHitEffectPrefab;
        // public GameObject monsterCritEffPrefab;
        // public GameObject monsterDtKillEffPrefab;
        // public GameObject monsterBornEffectPrefab;
        // public GameObject heroUtlSkEffPrefab;
        // public GameObject monsterRebornEffectPrefab;
        
        // public Material bgMt1;
        // public Material bgMt2;
        // public Material bgMt3;

        // public float monsterDiePresentationInterval = .2f;
        // public Transform ground;
        // public GameObject borders;

        // public Transform unitParent;

        // public GameObject bgPanel;
        // public TouchPad touchPad;
        // public HPBarController hpBarController;
        // public Camera gameCamera;

        // public static SceneMain Instance => _instance ?? (_instance = FindObjectOfType<SceneMain>());

        // public event Action onHeroDied;
        // public event Action onFountainDied;
        // public event Action onMonstersAllDied;
        // public event Action<Hero> onHeroSpawn;
        // public event Action<Fountain> onFountainSpawn;
        // public event Action<Monster> onMonsterSpawn;
        // public event Action onBossCmAc;

        // public Action<Hero,Monster> onBossNearDie;

        // public Hero hero { get; private set; }
        // public Fountain fountain { get; private set; }
        // public List<Monster> monsters => _monsters;

        // public bool monstersAllDied => _monsters.All(m => m.dead);

        // // 当前是否最后一波怪
        // public bool isLastWave => currentWave == _battleWavesData.Count - 1;
        // public int currentWave { get; private set; }

        // public int reliveCount { get; private set; }
        // public int availableReliveCount { get;set; }

        // // 被广告或钻石复活过的次数
        // public int reliveCountByPayment { get; private set; }
        // public int availableReliveCountByPayment { get; private set; } = 1;

        // public StateMachine stageStateMachine { get; private set; }

        // public bool battleEnded { get; private set; }
        
        // //是否进行引导
        // public int novGuiCt = 0;

        // public TrivialObjectsManager trivialObjectsManager { get; private set; } = new TrivialObjectsManager();

        // // TODO: 配置选择buff，技能
        // private Dictionary<SceneBuffType, float[]> _presetSceneBuffs = new Dictionary<SceneBuffType, float[]>();
        // private Dictionary<HeroSkillType, float> _presetHeroSkills = new Dictionary<HeroSkillType, float>()
        // {
        //     [HeroSkillType.NormalAttack] = 0f
        // };

        // private List<Monster> _monsters = new List<Monster>();

        // public bool isPaused = false;
        // public bool isHeroHurt = false;
        
        // public bool isAutoGm = false;
        // //怪物全杀
        // public bool isMAllKill = false;
        // public int levelScore = 0;
        // public float skillFillFloat1 = 0;

        // private float _preservedTimeScale = 0f;

        // private float _pauseDuration;
        // private float _pauseElapsedTime;

        // private bool _touchPaused = false;

        // private Plane _groundPlane;

        // private static SceneMain _instance;

        // //地刺
        // public GameObject spikeObj1;
        // //移动电锯
        // public GameObject mtSawObj1;
        // //减速带
        // public GameObject shiftObj1;
        // //弹射带
        // public GameObject qukenObj1;
        // private Vector3 _originCameraPos;
        // private DG.Tweening.Tween _shakingTween;
        // private TweenCallback _onShakeComplete;

        // private StartGameParam _startGameParam;
        // private List<DRBattleWavesData> _battleWavesData = new List<DRBattleWavesData>();

        // private MonsterDiePresentationArranger _monsterDiePresentationArranger;

        // private Action<LivingUnit> _monsterDieActionExplode;
        // private Action<LivingUnit> _monsterDieActionFreeze;
        // private Action<LivingUnit> _monsterDieActionThunder;

        // public int curSelectLevel = 1;

        // // private string[] tipsTitleArr = new string[]
        // // {"引导",
        // // "引导",};
        // private string[] tipsContArr = new string[]
        // {"点击箭头所指击杀        吧!",
        // "连线击杀来获得combo吧",};
        
        // public bool judgeIsTimeInstar(int tm){
        //     if(tm<=starTms[curSelectLevel-1]){
        //         return true;
        //     }
        //     return false;
        // } 
        
        // public GameObject StartBornPresentation(LivingUnit unit,bool isRelif=false)
        // {
        //     GameObject go;
        //     if(isRelif){
        //         go = Instantiate(monsterRebornEffectPrefab);
        //     }else{
        //         go = Instantiate(monsterBornEffectPrefab);
        //     }
        //     if(unit!=null){
        //         go.transform.position = unit.cachedTransform.position;
        //         go.transform.SetPositionY(0.05f);
        //         go.transform.parent = unit.cachedTransform;
        //     }
        //     return go;
        // }


        // // 1 hero die 2 monster die
        // public void invokeEvent(int type){
        //     if(type==1){
        //         onHeroDied?.Invoke();
        //     }
        //     else if(type==2){
        //         onMonstersAllDied?.Invoke();
        //     }
        // }

        // public bool isPureGuide = false;
        // public bool isChgTask = false;
        // public int taskTicket = 0;
        // public int taskDiamond = 15;
        // public int[] taskMuls = new int[3]{2,3,4};
        // public int selectMulVal=0;

        // public bool judgeIsScoreStar(int score){
        //     if(score>=starScores[curSelectLevel-1]){
        //         return true;
        //     }
        //     return false;
        // } 

        // public bool isMonsterAlive(int idx){
        //     if(idx<0||idx>=_monsters.Count){
        //         return false;
        //     }
        //     if(_monsters[idx]==null){
        //         return false;
        //     }
        //     return !_monsters[idx].dead;
        // }

        // public bool judgeIsAllKillStar(int score){
        //     if(score/10==getTotalMonstersCt()){
        //         return true;
        //     }
        //     return false;
        // } 

        // void changeBgMat(int type){
        //     GameObject bg = bgPanel.transform.GetChild(0).GetChild(0).gameObject;
        //     MeshRenderer mr = bg.GetComponent<MeshRenderer>();
        //     if(type==1){

        //         mr.material = bgMt1;
        //     }
        //     else if(type==2)
        //     {
        //         mr.material = bgMt2;
        //     }
        //     else if(type==3){
        //         mr.material = bgMt3;
        //     }
        // }

        // private int[] starTms;
        // private int[] starScores;

        // //轻量提示
        // private GameObject stipsPanel;
        // //combo
        // private GameObject cbPanel;
        // public Action<int> comboChange;

        // public GameObject bossComeBgObj;
        // public GameObject bossComeObj;
        // public GameObject bossBigIconObj;
        // public GameObject nearSldObj;
        
        // public GameObject nearSldUpBkObj;
        // public GameObject nearSldDnBkObj;

        // public bool isGetHintHurt = false;
        // //h 27 w 11
        // public int[][] mtSawMatrix = null;
        // public int[] hinderCtArr = null;
        // public void clearMtsawMatrix(){
        //     for(int i=0;i<27;i++){
        //         for(int j=0;j<11;j++){
        //             mtSawMatrix[i][j]=0;
        //         }
        //     }
        // }

        // //类型之前的总数
        // public int preHinderCt(int idx){
        //     int ct=0;
        //     for(int i=idx;i>=0;i--){
        //         ct+=hinderCtArr[i];
        //     }
        //     return ct;
        // }

        // public void setGmSceVisible(bool flag){
        //     transform.parent.gameObject.SetActive(flag);
        // }


        // public List<Monster> newSpwnMons = null;
        // public GameObject staFtObj;
        // private void Awake(){

        //     stnHintColor = new Color(255/255f,0/255f,0/255f);
        //     mstHintColor = new Color(255/255f,0/255f,0/255f);
        //     barlHintColor = new Color(255/255f,0/255f,0/255f);

        
        //     newSpwnMons = new List<Monster>();

        //     // isPureGuide = true;
        //     starTms = new int[DataManage.totalLevel];
        //     starScores = new int[DataManage.totalLevel];
        //     for(int i=0;i<DataManage.totalLevel;i++){
        //         starTms[i]=150;
        //     }
        //     for(int i=0;i<DataManage.totalLevel;i++){
        //         starScores[i]=150;
        //     }

        //     mtSawMatrix = new int[27][];
        //     // Debug.Log("hehe1");
        //     hinderCtArr = new int[6]{0,0,0,0,0,0};
        //     for(int i=0;i<27;i++){
        //         mtSawMatrix[i] = new int[11];
        //         for(int j=0;j<11;j++){
        //             mtSawMatrix[i][j]=0;
        //         }
        //     }
        //     nearTips = new string[]{"猛戳         进行连击!!"};
        //     // bornMtEffArr = new GameObject[100];

        //     // loadCliBtnEffPrefabs();
        //     // loadScenePrefabs("bg",new Vector3(0,0,1),new Vector3(90,0,0),new Vector3(1,1,1));
        //     if(Screen.width==800&&Screen.height==1000){
        //         loadScenePrefabs("bg1",new Vector3(0.3f,0f,-21.2f),new Vector3(0,0,0),new Vector3(1.3f,1,0.9f));
        //     }
        //     else{
        //         loadScenePrefabs("bg1",new Vector3(0.3f,0f,-21.2f),new Vector3(0,0,0),new Vector3(1.1f,1,0.9f));
        //     }
        //     tipsPanel = touchPad.transform.parent.Find("tipsPanel").gameObject;
        //     tipsPanel.SetActive(false);
        //     stipsPanel = touchPad.transform.parent.Find("stipPanel").gameObject;
        //     stipsPanel.SetActive(false);
        //     cbPanel = touchPad.transform.parent.Find("cbPanel").gameObject;
        //     cbPanel.SetActive(false);
        //     comboChange=null;
        //     comboChange+=showCombo;


        //     bossComeObj = touchPad.transform.parent.Find("bossTips").gameObject;
        //     bossComeObj.SetActive(false);

        //     bossComeBgObj = touchPad.transform.parent.Find("bossTipBg").gameObject;
        //     bossComeBgObj.SetActive(false);
            

        //     bossBigIconObj = touchPad.transform.parent.Find("boss_icon").gameObject;
        //     bossBigIconObj.SetActive(false);
            
            
        //     nearSldObj = touchPad.transform.parent.Find("nearTips").gameObject;
        //     nearSldObj.SetActive(false);

        //     staFtObj = touchPad.transform.parent.Find("staFtImg").gameObject;
        //     staFtObj.SetActive(false);

        //     nearSldUpBkObj = touchPad.transform.parent.Find("upBk").gameObject;
        //     nearSldDnBkObj = touchPad.transform.parent.Find("dnBk").gameObject;
        //     hideNearSldTips();

        //     fitScreen();
        //     Button close = tipsPanel.GetComponent<Button>();                             
        //     close.onClick.AddListener(delegate(){
        //         tipsPanel.SetActive(false);
        //         Resume();
        //     });
        //     touchPad.gameObject.SetActive(false);

        //     initAutoPanel();
        // }

        // GameObject autoBtnObj;
        // GameObject autoPl;
        // void initAutoPanel(){
        //     autoBtnObj =  touchPad.transform.parent.Find("autoBtn").gameObject;
        //     autoPl = touchPad.transform.parent.Find("autoPl").gameObject;
        //     autoPl.SetActive(false); 
        //     isAutoGm = false;
        //     autoBtnObj.SetActive(false);
        //     Button autoBtn = autoBtnObj.GetComponent<Button>();
        //     autoBtn.onClick.RemoveAllListeners();
        //     autoBtn.onClick.AddListener(delegate(){
        //         autoPl.SetActive(true);
        //         isAutoGm = true;
        //     });
        //     Button cloAutoBtn = autoPl.transform.Find("mask").GetComponent<Button>();
        //     cloAutoBtn.onClick.RemoveAllListeners();
        //     cloAutoBtn.onClick.AddListener(delegate(){
        //         autoPl.SetActive(false);
        //         isAutoGm = false;
        //     });
        // }

        // public void cancelAutoGm(){
        //     isAutoGm = false;
        //     autoPl.SetActive(false);
        // }

        // void Start()
        // {
        //     curSelectLevel = 20;
        //     // 不显示边框墙，只让其Collider起作用
        //     foreach (var r in borders.GetComponentsInChildren<Renderer>())
        //     {
        //         r.enabled = false;
        //     }

        //     Clear();
        //     _originCameraPos = gameCamera.transform.position;
        // }


        // public void showStips(string text){
        //     Text t = stipsPanel.transform.Find("Text").GetComponent<Text>();
        //     t.text = text;
        //     stipsPanel.SetActive(true);
        //     Invoke("hideStipsPanel",2);
        // }
        // public void hideStipsPanel(){
        //     stipsPanel.SetActive(false);
        // }

        // //开始战斗!
        // public void showStaFt(TweenCallback cb){
        //     Sequence tipsSeq = DOTween.Sequence();
        //     tipsSeq.AppendCallback(delegate(){
        //         staFtObj.SetActive(true);
        //         bossComeBgObj.SetActive(true);
        //         Animator atr = staFtObj.GetComponent<Animator>();
        //         atr.Play("bossTipsAinm_1");
        //     });
        //     tipsSeq.AppendInterval(1.5f);
        //     // tipsSeq.Append(bossComeObj.transform.DOLocalMoveX(Screen.width,0.5f).SetEase(Ease.OutSine));
        //     tipsSeq.AppendCallback(delegate(){
        //         staFtObj.SetActive(false);
        //         bossComeBgObj.SetActive(false);
        //         cb();
        //         tipsSeq.Kill(true);
        //         tipsSeq = null;
        //     });
        // }

        // public void showBossComing(TweenCallback cb){
        //     // bossComeObj.SetActive(true);
        //     Sequence tipsSeq = DOTween.Sequence();
        //     bossBigIconObj.transform.localScale = new Vector3(3,3,3);
        //     // bossComeObj.transform.SetLocalPositionX(-Screen.width);
        //     // bossComeObj.transform.localScale = new Vector3(1,1,1);
        //     // tipsSeq.Append(bossComeObj.transform.DOLocalMoveX(0,0.5f).SetEase(Ease.InSine));
        //     tipsSeq.AppendCallback(delegate(){
        //         bossComeObj.SetActive(true);
        //         bossComeBgObj.SetActive(true);
        //         Animator atr = bossComeObj.GetComponent<Animator>();
        //         atr.Play("bossTipsAinm_1");
        //     });
        //     tipsSeq.AppendCallback(delegate(){
        //         // bossBigIconObj.SetActive(true);
        //     });
        //     tipsSeq.Append(bossBigIconObj.transform.DOScale(new Vector3(1,1,1),0.3f).SetEase(Ease.OutSine));
        //     tipsSeq.AppendCallback(delegate(){
        //         onBossCmAc?.Invoke();    
        //     });
        //     tipsSeq.AppendInterval(1f);
        //     // tipsSeq.Append(bossComeObj.transform.DOLocalMoveX(Screen.width,0.5f).SetEase(Ease.OutSine));
        //     tipsSeq.AppendCallback(delegate(){
        //         bossComeObj.SetActive(false);
        //         bossBigIconObj.SetActive(false);
        //         bossComeBgObj.SetActive(false);
        //         cb();
        //         tipsSeq.Kill(true);
        //         tipsSeq = null;
        //     });
     
        //     // atr.
        // }

        // public string[] nearTips;
        // public void showNearSldTips(int strIdx,TweenCallback cb=null){
        //     Sequence tipsSeq = DOTween.Sequence();
        //     nearSldObj.transform.SetLocalPositionX(0);
        //     GameObject tObj = nearSldObj.transform.Find("Text").gameObject;
        //     Text cText = tObj.GetComponent<Text>();
        //     cText.text = nearTips[strIdx];
        //     nearSldObj.transform.SetPositionY(Screen.height/2);
        //     nearSldObj.SetActive(true);
        //     // tipsSeq.Append(nearSldUpBkObj.transform.DOLocalMoveY(Screen.height/2,0.5f).SetEase(Ease.OutSine));
        //     // tipsSeq.Join(nearSldDnBkObj.transform.DOLocalMoveY(-Screen.height/2,0.5f).SetEase(Ease.OutSine));
        //     tipsSeq.AppendInterval(1f);
        //     tipsSeq.Append(nearSldObj.transform.DOLocalMoveY(Screen.height/2-200,0.5f).SetEase(Ease.OutSine));
        //     tipsSeq.AppendCallback(delegate(){
        //         if(cb!=null){
        //             cb();
        //         }
        //         tipsSeq.Kill(true);
        //         tipsSeq = null;
        //     });
        // }

        // public void hideNearSldTips(){
        //     nearSldObj.SetActive(false);
        //     nearSldUpBkObj.transform.SetLocalPositionY(100+960);
        //     nearSldDnBkObj.transform.SetLocalPositionY(-100-960);
        // }   

        // public int comboCount=0;
        // public float comboAccTm = 0;
        // public void showCombo(int count){
        //     if(comboAccTm<1.5f){
        //     }
        //     else{
        //         count = 1;
        //     }
        //     Text t = cbPanel.transform.Find("Text").GetComponent<Text>();
        //     t.text = "x"+count;
        //     cbPanel.SetActive(true);
        //     comboCount = count;
        //     comboAccTm=0;

        //     Sequence seq = DOTween.Sequence();
        //     seq.AppendCallback(delegate(){
        //         cbPanel.transform.SetLocalScaleX(1.3f);
        //         cbPanel.transform.SetLocalScaleY(1.3f);
        //     });
        //     seq.AppendInterval(0.1f);
        //     seq.AppendCallback(delegate(){
        //         cbPanel.transform.SetLocalScaleX(1f);
        //         cbPanel.transform.SetLocalScaleY(1f);
        //         seq.Kill(true);
        //         seq=null;
        //     });
            
        //     Invoke("hideCombo",0.8f);            
        // }
        // public void hideCombo(){
        //     cbPanel.SetActive(false);
        // }

        // public void initbgPanel()
        // {
        //     float fitX = 0;
        //     float rate = (float)Screen.height/(float)Screen.width;
        //     if(rate>2f){
        //         fitX = 0.5f;
        //     }

        //     Vector3[] poss = new Vector3[]
        //         {new Vector3(-8.1f+fitX,0,16.14f),
        //         new Vector3(-7.22f+fitX,0,9.65f),
        //         new Vector3(-4.75f+fitX,0,-2.76f),
        //         new Vector3(7.67f-fitX,0,12.8f),
        //         new Vector3(4.74f-fitX,0,-1.45f),
        //         new Vector3(4.11f-fitX,0,-7.02f),}; 

        //     Vector3[] rots = new Vector3[]
        //         {new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),}; 
                
        //     Vector3[] scas = new Vector3[]
        //         {new Vector3(4f,4f,4f),
        //         new Vector3(5f,5f,5f),
        //         new Vector3(3f,3f,3f),
        //         new Vector3(5f,5f,5f),
        //         new Vector3(3f,3f,3f),
        //         new Vector3(2.5f,2.5f,2.5f),
        //         }; 

        //     for(int i=0;i<6;i++){
        //         loadScenePrefabs("stone",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //     {new Vector3(-7.92f+fitX,20.77f),
        //     new Vector3(-5.67f+fitX,0,6.12f),
        //     new Vector3(-4.59f+fitX,0,-0.45f),
        //     new Vector3(-3.88f+fitX,0,-5.54f),
        //     new Vector3(5.92f-fitX,0,7.68f),
        //     new Vector3(3.85f-fitX,0,-4.64f),
            
        //     }; 

        //     rots = new Vector3[]
        //         {new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,0),
        //         new Vector3(90,0,0),
        //         new Vector3(90,0,0),
        //         new Vector3(90,0,0),
        //         }; 

        //     scas = new Vector3[]
        //         {new Vector3(1.3f,1.3f,1.3f),
        //         new Vector3(1.3f,1.3f,1.3f),
        //         new Vector3(1.3f,1.3f,1.3f),
        //         new Vector3(1f,1f,1f),
        //         new Vector3(1.5f,1.5f,1.5f),
        //         new Vector3(1.2f,1.2f,1.2f),
        //         }; 
        //     //四棵外树
        //     for(int i=0;i<6;i++){
        //         loadScenePrefabs("tree2",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-5.23f+fitX,0,4.33f),
        //         new Vector3(-3.09f+fitX,0,-6.81f),
        //         new Vector3(7.59f-fitX,0,17.96f),
        //         new Vector3(5.45f-fitX,0,5.41f),
        //         new Vector3(4.64f-fitX,0,0.89f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(90,0,180),
        //         new Vector3(79,-180,0),
        //         new Vector3(90,0,0),
        //         new Vector3(90,0,0),
        //         new Vector3(101,0,-90),}; 

        //     scas = new Vector3[]
        //         {new Vector3(1.3f,1.3f,1.3f),
        //         new Vector3(1f,1f,1f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(1.5f,1.5f,1.5f),
        //         new Vector3(1.3f,1.3f,1.3f),}; 
        //     //五颗内树
        //     for(int i=0;i<5;i++){
        //         loadScenePrefabs("tree1",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-3.06f+fitX,0,0.87f),
        //         new Vector3(-2.9f+fitX,0,0.92f),
        //         new Vector3(3.45f-fitX,0,2.58f),
        //         new Vector3(3.7f-fitX,0,2.68f),
        //         new Vector3(2.7f-fitX,0,-4.8f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(90,164,10),
        //         new Vector3(90f,-156,0),
        //         new Vector3(90,164,10),
        //         new Vector3(90,-169,0),
        //         new Vector3(90,0,180),}; 

        //     scas = new Vector3[]
        //         {new Vector3(1f,1f,1f),
        //         new Vector3(1f,1f,1f),
        //         new Vector3(-1f,1f,1.3f),
        //         new Vector3(-1f,1f,1f),
        //         new Vector3(-1f,1f,1f),}; 
        //     //四颗草
        //     for(int i=0;i<4;i++){
        //         loadScenePrefabs("grass",poss[i],rots[i],scas[i]);
        //     }
        // }

        // public void initbgPanel1()
        // {
        //     Vector3[] poss = new Vector3[]
        //         {new Vector3(-7.7f,0,18.55f),
        //         new Vector3(-6.56f,0,12.27f),
        //         new Vector3(-4.27f,0,-2.44f),
        //         new Vector3(7.8f,0,18.8f),
        //         new Vector3(5.1f,0,2.6f),
        //         new Vector3(3.79f,0,-6.48f),}; 

        //     Vector3[] rots = new Vector3[]
        //         {new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),
        //         new Vector3(90,0,180),}; 

        //     Vector3[] scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(3f,3f,3f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(-2f,2f,2f),}; 

        //     for(int i=0;i<6;i++){
        //         loadScenePrefabs("stone1",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-5.3f,0,3.71f),
        //         new Vector3(6.1f,0,9.41f),
        //         new Vector3(4.14f,0,-2.04f),
        //         new Vector3(2.7f,0,0f),
        //         new Vector3(2.7f,0,-4.8f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(0,180,0),
        //         new Vector3(0,180,0),
        //         new Vector3(0,0,0),
        //         new Vector3(0,0,0),
        //         new Vector3(0,0,180),}; 

        //     scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(1f,1f,1f),
        //         new Vector3(-1f,1f,1f),}; 
        //     //三棵树
        //     for(int i=0;i<3;i++){
        //         loadScenePrefabs("tree3",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-8.44f,0,23f),
        //         new Vector3(-3.74f,0,-6.12f),
        //         new Vector3(5.42f,0,5.5f),
        //         new Vector3(-4.64f,0,0.72f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(90,0,-180),
        //         new Vector3(90,0,-180),
        //         new Vector3(90,0,-180),
        //         new Vector3(90,0,-180),}; 

        //     scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),}; 
        //     //三颗南瓜
        //     for(int i=0;i<4;i++){
        //         loadScenePrefabs("nangua",poss[i],rots[i],scas[i]);
        //     }
        // }

        // public void initbgPanel2()
        // {
        //     Vector3[] poss = new Vector3[]
        //         {new Vector3(-6.3f,0,15.8f),
        //         new Vector3(-5.59f,0,7.39f),
        //         new Vector3(-3.89f,0,-4.38f),
        //         new Vector3(6.7f,0,17.14f),
        //         new Vector3(4.54f,0,1.11f),
        //         new Vector3(4.03f,0,-6.61f),}; 

        //     Vector3[] rots = new Vector3[]
        //         {new Vector3(180,0,180),
        //         new Vector3(180,0,180),
        //         new Vector3(180,0,180),
        //         new Vector3(180,0,180),
        //         new Vector3(180,0,180),
        //         new Vector3(180,0,180),}; 

        //     Vector3[] scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(-2f,2f,2f),}; 

        //     for(int i=0;i<6;i++){
        //         loadScenePrefabs("tree4",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-4.71f,0,0f),
        //         new Vector3(-5.97f,0,10.93f),
        //         new Vector3(4.14f,0,-1.45f),
        //         new Vector3(-5.06f,0,3.24f),
        //         new Vector3(5.92f,0,10.37f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(0,180,0),
        //         new Vector3(0,180,0),
        //         new Vector3(0,0,0),
        //         new Vector3(0,180,0),
        //         new Vector3(0,180,0),}; 

        //     scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(-2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(-2f,2f,2f),}; 
        //     //四棵草
        //     for(int i=0;i<5;i++){
        //         loadScenePrefabs("grass1",poss[i],rots[i],scas[i]);
        //     }
        //     poss = new Vector3[]
        //         {new Vector3(-6.04f,0,12.22f),
        //         new Vector3(-3.67f,0,-6.42f),
        //         new Vector3(5.35f,0,6.58f),
        //         new Vector3(3.91f,0,-3.55f),}; 

        //     rots = new Vector3[]
        //         {new Vector3(180,0,-180),
        //         new Vector3(180,0,-180),
        //         new Vector3(180,0,-180),
        //         new Vector3(180,0,-180),}; 

        //     scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),}; 
        //     //三颗南瓜
        //     for(int i=0;i<4;i++){
        //         loadScenePrefabs("treeZhg",poss[i],rots[i],scas[i]);
        //     }
        // }

        //  public void initbgPanel3()
        // {
        //     Vector3[] poss = new Vector3[]
        //         {new Vector3(-6.5f,0f,15.57f),
        //         new Vector3(-5f,0,5.08f),
        //         new Vector3(-3.03f,0,-2.28f),
        //         new Vector3(6.5f,0,13.83f),
        //         new Vector3(4.6f,0,3.42f),
        //         new Vector3(3.8f,0,-4.08f),}; 

        //     Vector3[] rots = new Vector3[]
        //         {new Vector3(-2.25f,-87.1f,0f),
        //         new Vector3(-13.7f,-162.43f,0),
        //         new Vector3(0f,65.34f,0f),
        //         new Vector3(0f,246.62f,0f),
        //         new Vector3(5.38f,-67.83f,0f),
        //         new Vector3(0f,283.45f,0f),}; 

        //     Vector3[] scas = new Vector3[]
        //         {new Vector3(2f,2f,2f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(1.5f,1.5f,1.5f),
        //         new Vector3(2f,2f,2f),
        //         new Vector3(1.5f,1.5f,1.5f),
        //         new Vector3(2f,2f,2f),}; 

        //     for(int i=0;i<6;i++){
        //         loadScenePrefabs("demo"+(i+1),poss[i],rots[i],scas[i]);
        //     }
           
        // } 

        // public GameObject bossSkItem = null;
        // public void clearScenePrefabs(){
        //     if(bgPanel.transform.childCount>1){
        //         for(int i=1;i<bgPanel.transform.childCount;i++){
        //             GameObject item = bgPanel.transform.GetChild(i).gameObject;
        //             Destroy(item);
        //             item = null;
        //         }
        //         changeBgMat(1);
        //     }
        // }

        // public GameObject cliBtnEff;
        // void loadCliBtnEffPrefabs(){
        //     GameEntry.Resource.LoadAsset(AssetUtility.GetCliBtnEffScenePrefab(), Constant.AssetPriority.ScenePrefab, new LoadAssetCallbacks(
        //     (assetName, asset, duration, userData) =>
        //     {
        //         cliBtnEff = GameObject.Instantiate(asset as UnityEngine.Object,bgPanel.transform) as GameObject;
        //         AssetUtility.toChangePlayerLayer(cliBtnEff,8);
        //         var trans = cliBtnEff.transform;
        //         trans.position =new Vector3(0f,1.85f,-3f);
        //         trans.rotation = Quaternion.Euler(90,0,0);
        //         // trans.localScale = new Vector3(0.2f,0.2f,0.2f);
        //         cliBtnEff.SetActive(false);
        //         Log.Info("Load CliBtnEff '{0}' OK.", assetName);
        //     },

        //     (assetName, status, errorMessage, userData) =>
        //     {
        //         Log.Error("Can not load CliBtnEff '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //     }));
        // }

        // // float mtsawLenToScale(){

        // // }

        // //dtpos 起始点 和 结束点的坐标
        // void mtsawMovement(GameObject mtSawObj,Vector4 dtPos){
        //     GameObject wdObj = mtSawObj.transform.GetChild(0).GetChild(0).gameObject;
        //     float deDisY = 0;
        //     float deDisX = 0;
        //     float moveDis = 3;
        //     if(dtPos.x==dtPos.z){
        //         mtSawObj.transform.localRotation = Quaternion.Euler(90,90,0);
        //         moveDis = Mathf.Abs(dtPos.y-dtPos.w)+1;
        //         wdObj.transform.SetLocalPositionX(-0.45f*moveDis+0.3f);
        //         wdObj.transform.SetLocalScaleX(0.3f*moveDis-0.2f);
        //     }
        //     else{
        //         mtSawObj.transform.localRotation = Quaternion.Euler(90,0,0);
        //         moveDis = Mathf.Abs(dtPos.z-dtPos.x)+1;
        //         wdObj.transform.SetLocalPositionX(0.15f*moveDis-0.35f);
        //         wdObj.transform.SetLocalScaleX(0.25f*moveDis-0.15f);
        //     }
        //     GameObject gearObj = mtSawObj.transform.GetChild(0).GetChild(1).gameObject;
        //     mtSawObj.transform.SetPositionX(dtPos.x);
        //     mtSawObj.transform.SetPositionY(0);
        //     mtSawObj.transform.SetPositionZ(dtPos.y);
        //     gearObj.transform.SetPositionX(dtPos.x+deDisX);
        //     gearObj.transform.SetPositionY(0);
        //     gearObj.transform.SetPositionZ(dtPos.y+deDisY);
         
        //     float oriX = gearObj.transform.position.x;
        //     Sequence msSeq = DOTween.Sequence();
        //     msSeq.AppendInterval(0.6f);
        //     msSeq.Append(gearObj.transform.DOMove(new Vector3(dtPos.z+deDisX,-0.32f,dtPos.w+deDisY),2));
        //     msSeq.AppendInterval(0.6f);
        //     msSeq.Append(gearObj.transform.DOMove(new Vector3(dtPos.x+deDisX,-0.32f,dtPos.y+deDisY),2));
        //     msSeq.SetLoops(-1);
        //     hinderSeqArr[hdrSeqLen]=msSeq;
        //     hdrSeqLen++;
        // }

        

        // float mtswAgAccTm = 0;
        // float mtswAgInTm = 0.2f;
        // float mtswCurRotateX = 0;
        // float mtswRotSpd = 10;

        // float autoAcAccTm = 0;
        // float autoAcInTm = 0.8f;
        // void Update()
        // {
        //     if(!MainForm.isCurMainForm){
        //         if(MainForm._totOlAwdTimer!=null){
        //             MainForm._totOlAwdTimer.UpdateDelta();
        //         }
        //         if(MainForm._olAwdTimer!=null){
        //             MainForm._olAwdTimer.UpdateDelta();
        //         }
        //     }
        //     comboAccTm+=Time.deltaTime;
        //     if(hinderCtArr[2]>0){
        //         for(int i=preHinderCt(1);i<preHinderCt(1)+hinderCtArr[2];i++){
        //             GameObject gearObj = curHinderArr[i].transform.GetChild(0).GetChild(1).gameObject;
        //             mtswCurRotateX+=mtswRotSpd;
        //             gearObj.transform.localRotation = Quaternion.Euler(0,mtswCurRotateX,0);
        //         }
        //         mtswAgAccTm=0;
        //     }

        //     if(isAutoGm){
        //         autoAcAccTm+=Time.deltaTime;
        //         if(autoAcAccTm>autoAcInTm){
        //             autoAcAccTm=0;
        //             if(!hero.dead){
        //                 Monster tgt = getOneMonsTarget();
        //                 hero.dashToMonster(tgt);
        //             }
        //         }
        //     }

        //     // if (isPaused)
        //     // {
        //     //     _pauseElapsedTime += Time.unscaledDeltaTime;
        //     //     if (_pauseElapsedTime >= _pauseDuration)
        //     //     {
        //     //         Resume();
        //     //     }
        //     // }
        //     // else if (_pauseElapsedTime < _pauseDuration + Mathf.Epsilon)
        //     // {
        //     //     Pause();
        //     // }
        //     // if(isJpHint){
        //     //     if(jpAlphaVal<0){
        //     //         jpAlphaVal = 0;
        //     //         jpChgDir = 1;
        //     //     }   
        //     //     if(jpAlphaVal>255){
        //     //         jpAlphaVal = 255;
        //     //         jpChgDir = 0;
        //     //     }
        //     //     if(jpChgDir==0){
        //     //         jpAlphaVal-=jpFadeSpd;
        //     //     }
        //     //     else{
        //     //         jpAlphaVal+=jpFadeSpd;
        //     //     }
        //     //     updateJpHint();
        //     // }
        // }

        // bool isJpHint = false;
        // float jpAlphaVal = 255;
        // float jpFadeSpd = 15;
        // int jpChgDir = 0;
        // void updateJpHint(){
        //     jpSpe.GetComponent<Renderer>().material.SetColor("_Color",new Color(230/255f,100/255f,100/255f,jpAlphaVal/255f));
        // }

        // public void startJpHint(Monster mons,TweenCallback cb=null){
        //     // Debug.Log("aaa");
        //     // isJpHint = true;
        //     Vector3 dir = (hero.transform.position - mons.transform.position).normalized;
        //     Vector3 ePos = hero.transform.position+dir;
        //     ParticleSystem ps = jpSpe.GetComponent<ParticleSystem>();
        //     if(mons.configData.Id==14){
        //         // ps.startColor = new Color(165/255f,65/255f,185/255f);
        //     }
        //     else if(mons.configData.Id==15){
        //         // ps.startColor = new Color(120/255f,80/255f,35/255f);
        //     }
    

        //     jpSpe.transform.position = ePos;
        //     jpSpe.SetActive(true);
        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(1f);
        //     wtSeq.AppendCallback(cb);
        //     wtSeq.AppendInterval(1f);
        //     wtSeq.AppendCallback(delegate(){
        //         wtSeq.Kill(true);
        //         wtSeq = null;
        //         // isJpHint = false;
        //         jpSpe.SetActive(false);
        //     });
        // }


        // public Color stnHintColor;
        // public Color mstHintColor; 
        // public Color barlHintColor;

        // public Color stnHintColor1 = new Color(110/255f,110/255f,110/255f);
        // public Color mstHintColor1 = new Color(165/255f,65/255f,185/255f);
        // public Color barlHintColor1 = new Color(120/255f,80/255f,35/255f);
        // //技能提示
        // public void stnSteelHint(int monId,Vector3 pos,float wtTm,int isSmall=0){
        //     jpSpe.transform.position = pos;
        //     if(isSmall==1){
        //         jpSpe.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        //     }
        //     else{
        //         jpSpe.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        //     }
        //     jpSpe.SetActive(true);
        //     ParticleSystem ps = jpSpe.GetComponent<ParticleSystem>();
        //     if(monId==14){
        //         ps.startColor = mstHintColor;
        //     }
        //     else if(monId==15){
        //         ps.startColor = barlHintColor;
        //     }
        //     else if(monId==13){
        //         ps.startColor = stnHintColor;
        //     }

        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(wtTm);
        //     wtSeq.AppendCallback(delegate(){
        //         wtSeq.Kill(true);
        //         wtSeq = null;
        //         jpSpe.SetActive(false);
        //         jpSpe.transform.localScale = new Vector3(1,1,1);
        //     });
        // }

        // public void masterPtsHint(Vector3[] poss,float wtTm){
        //     GameObject[] cache = new GameObject[poss.Length];
        //     for(int i=0;i<poss.Length;i++){
        //         GameObject hintObj = Instantiate(jpSpe);
        //         hintObj.transform.position = poss[i];
        //         hintObj.SetActive(true);
        //         hintObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        //         cache[i]=hintObj;
        //     }
        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(wtTm);
        //     wtSeq.AppendCallback(delegate(){
        //         wtSeq.Kill(true);
        //         wtSeq = null;
        //         for(int i=0;i<poss.Length;i++){
        //             Destroy(cache[i]);
        //         }
        //     });
        
        // }

        // void loadScenePrefabs(string prefab,Vector3 pos,Vector3 rotat,Vector3 scale){
        //     GameEntry.Resource.LoadAsset(AssetUtility.GetMainScenePrefab(prefab), Constant.AssetPriority.ScenePrefab, new LoadAssetCallbacks(
        //     (assetName, asset, duration, userData) =>
        //     {
        //         var go = GameObject.Instantiate(asset as UnityEngine.Object,bgPanel.transform) as GameObject;
        //         if(prefab=="shu1"||prefab=="shu2"){
        //             go.GetComponent<SceneUnit>().scaleCapsuleRange(scale[0]);
        //         }
        //         if(prefab=="bg1"){
        //             initSceneHinder();
        //             // spikeObj1 = go.transform.Find("spike1").gameObject;
        //             // mtSawObj1 = go.transform.Find("mtSaw1").gameObject;
        //             // shiftObj1 = go.transform.Find("shift1").gameObject;
        //             // qukenObj1 = go.transform.Find("quken1").gameObject;
        //             // mtsawMovement();
        //         }

        //         AssetUtility.toChangePlayerLayer(go,8);
        //         var trans = go.transform;
        //         trans.localPosition = pos;
        //         trans.ResetLocalY();
        //         trans.localRotation = Quaternion.Euler(rotat.x,rotat.y,rotat.z);
        //         trans.localScale = scale;
        //         Log.Info("Load GetScenePrefab '{0}' OK.", assetName);
        //     },

        //     (assetName, status, errorMessage, userData) =>
        //     {
        //         Log.Error("Can not load GetScenePrefab '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //     }));
        // }


        // public void setGuideMeshVis(bool flag){
        //     Debug.Log("setGuideMeshVis.."+flag);
        //     GameObject arrObj = PauseForm.hpParent.Find("nGuiImg").gameObject;
        //     GameObject maskObj = PauseForm.hpParent.Find("mask").gameObject;
        //     maskObj.SetActive(flag);
        //     arrObj.SetActive(flag);
        // }

        // public void selectLevelScene(int level){
        //     // clearScenePrefabs();
        //     // Invoke("clearScenePrefabs",0.1f);
        //     // initbgPanel3();
        //     // GameObject bgObj1 = bgPanel.transform.GetChild(0).gameObject;
        //     if(level<=8){
        //         changeBgMat(1);    
        //     }
        //     else{
        //         changeBgMat(2);
        //     }
        //     // if(level%2==0){
        //     //     // initbgPanel();
        //     //     changeBgMat(1);
        //     //     bgObj1.transform.SetLocalScaleX(3.8f);
        //     // }
        //     // else if(level%2==1){
        //     //     // initbgPanel1();
        //     //     changeBgMat(2);
        //     //     bgObj1.transform.SetLocalScaleX(3.9f);
        //     // }
        //     // else{
        //     //     initbgPanel2();
        //     //     changeBgMat(3);
        //     // }
        // }


        // public void bossMoveHintEff(int monId,Vector3 toPos,Vector3 fromPos,float wtTm,float deX=0,float deZ=0){
        //     float powX = Mathf.Pow((toPos.x - fromPos.x),2);
        //     float powY = Mathf.Pow((toPos.z - fromPos.z),2);
        //     float length = Mathf.Sqrt(powX+powY);
        //     float angle = Mathf.Atan2((toPos.z-fromPos.z),(toPos.x-fromPos.x));
        //     float deg = angle*Mathf.Rad2Deg;
        //     if(powY==0){
        //         deg=90;
        //     }
        //     deg*=-1;
        //     Vector3 dir = (toPos-fromPos).normalized;
        //     GameObject bgObj1 = bgPanel.transform.GetChild(0).gameObject;
        //     GameObject hintObj = bgObj1.transform.Find("mvHint").gameObject;
        //     ParticleSystem ps = hintObj.GetComponent<ParticleSystem>();
        //     if(monId==15){
        //         ps.startColor = barlHintColor;
        //     }
        //     else if(monId==13){
        //         ps.startColor = stnHintColor;
        //     }


        //     hintObj.gameObject.SetActive(false);
        //     hintObj.transform.SetLocalScaleZ(length/5);
        //     // hintObj.transform.SetPositionX((toPos.x - fromPos.x)/2+deX*dir.x);
        //     // hintObj.transform.SetPositionZ((toPos.z - fromPos.z)/2+deZ*dir.z);
        //     hintObj.transform.position = fromPos;
        //     // hintObj.transform.localRotation = Quaternion.Euler(90,deg,0);
        //     Quaternion qtn= Quaternion.LookRotation(dir);
        //     hintObj.transform.localRotation = qtn;
            
        //     hintObj.gameObject.SetActive(true);
        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(wtTm);
        //     wtSeq.AppendCallback(delegate(){
        //         hintObj.gameObject.SetActive(false);
        //         wtSeq.Kill(true);
        //         wtSeq=null;
        //     });
        // }
        
        // public void bossMoveHintEff1(Vector3 toPos,Vector3 fromPos,float wtTm,float deX=0,float deZ=0){
        //     float powX = Mathf.Pow((toPos.x - fromPos.x),2);
        //     float powY = Mathf.Pow((toPos.z - fromPos.z),2);
        //     float length = Mathf.Sqrt(powX+powY);
        //     float angle = Mathf.Atan2((toPos.z-fromPos.z),(toPos.x-fromPos.x));
        //     float deg = angle*Mathf.Rad2Deg;
        //     if(powY==0){
        //         deg=90;
        //     }
        //     deg*=-1;
        //     deg+=90;
        //     Vector3 dir = (toPos-fromPos).normalized;
        //     GameObject bgObj1 = bgPanel.transform.GetChild(0).gameObject;
        //     GameObject hintObj = bgObj1.transform.Find("mvHint").gameObject;
        //     ParticleSystem ps = hintObj.GetComponent<ParticleSystem>();
        //     ps.startColor = stnHintColor;
            
        //     hintObj.gameObject.SetActive(false);
        //     hintObj.transform.SetLocalScaleZ(3);
        //     hintObj.transform.position = fromPos+2*dir;

        //     hintObj.transform.localRotation = Quaternion.Euler(0,deg,0);

        //     hintObj.gameObject.SetActive(true);
        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(wtTm);
        //     wtSeq.AppendCallback(delegate(){
        //         hintObj.gameObject.SetActive(false);
        //         wtSeq.Kill(true);
        //         wtSeq=null;
        //     });
        // }



        // // 1：普通方块 2:地刺 3：移动电锯4：减速带 5：加速带 6:可破碎方块
        // GameObject[] oriHinderArr=null;
        // GameObject hCube1;
        // GameObject hCube2;
        // Sequence[] hinderSeqArr = null;
        // int hdrSeqLen=0;
        // public bool isSpikeActive = false;
        // public int[] qukenDirArr = null;
        // void initSceneHinder(){
        //     GameObject bgObj1 = bgPanel.transform.GetChild(0).gameObject;
        //     oriHinderArr = new GameObject[7];
        //     GameObject cube1 = bgObj1.transform.Find("cube1").gameObject;
        //     oriHinderArr[0] = cube1;
        //     hCube1 = cube1;
        //     GameObject spike1 = bgObj1.transform.Find("spike1").gameObject;
        //     oriHinderArr[1] = spike1;
        //     GameObject mtSaw1 = bgObj1.transform.Find("mtSaw1").gameObject;
        //     oriHinderArr[2] = mtSaw1;
        //     GameObject shift1 = bgObj1.transform.Find("shift1").gameObject;
        //     oriHinderArr[3] = shift1;
        //     GameObject quken1 = bgObj1.transform.Find("quken1").gameObject;
        //     oriHinderArr[4] = quken1;
        //     GameObject melt1 = bgObj1.transform.Find("melt1").gameObject;
        //     oriHinderArr[5] = melt1;
        //     GameObject cube2 = bgObj1.transform.Find("cube2").gameObject;
        //     hCube2 = cube2;
            
         
        //     curHinderArr = new GameObject[600];
        //     hinderSeqArr = new Sequence[1000];
        //     hdrSeqLen = 0;
        //     qukenDirArr = new int[100];
        //     for(int i=0;i<100;i++){
        //         qukenDirArr[i]=0;
        //     }

        //     jpSpe = bgObj1.transform.Find("jpHint").gameObject;
        //     jpSpe.SetActive(false);
        // }   
        // //boss跳点
        // GameObject jpSpe;
        // void randomHcube(){
        //     if(UnityEngine.Random.Range(0,2)==0){
        //         oriHinderArr[0] = hCube1;
        //     }
        //     else{
        //         oriHinderArr[0] = hCube2;
        //     }
        // }

        
        // void doSpikeMovement(GameObject speObj,int spkIdx){
        //     //-0.03 0.01
        //     GameObject ciObj = speObj.transform.GetChild(0).GetChild(0).gameObject;
        //     ciObj.transform.SetLocalPositionY(-0.03f);
        //     Sequence spSeq = DOTween.Sequence();
        //     spSeq.Append(ciObj.transform.DOLocalMoveY(0.01f,0.3f));
        //     spSeq.AppendCallback(delegate(){
        //         isSpikeActive=true;
        //     });
        //     spSeq.AppendInterval(1f);
        //     spSeq.Append(ciObj.transform.DOLocalMoveY(-0.03f,0.3f));
        //     spSeq.AppendCallback(delegate(){
        //         isSpikeActive=false;
        //     });
        //     spSeq.AppendInterval(1f);
        //     spSeq.SetLoops(-1);
        //     hinderSeqArr[hdrSeqLen]=spSeq;
        //     hdrSeqLen++;

        // }

        // int curHinderLen = 0;     
        // public GameObject[] curHinderArr = null;
        // void loadSceneHinder(int level){
        //     clearCurSceneHinder();
        //     randomHcube();
        //     var drBattleSceneUnitData = GameEntry.DataTable.GetDataTable<DRBattleSceneUnitData>();
        //     int colIdx = 0;
        //     int dir = 0;
        //     int[] dirMap = new int[5]{0,3,1,0,2};
        //     for(int i=1;i<=drBattleSceneUnitData.Count;i++){
        //         var rowData = drBattleSceneUnitData.GetDataRow(i);
        //         if(rowData.LevelId==level){
        //             colIdx=0;
        //             while(rowData.GetUnitIdAt(colIdx)!=0)
        //             {
        //                 if(rowData.GetUnitIdAt(colIdx)==3){
        //                     int mX = (int)(rowData.GetYAt(colIdx)-18);
        //                     int mY = (int)(rowData.GetXAt(colIdx)+5);
        //                     mtSawMatrix[mX][mY]+=1;
        //                 }
        //                 else{
        //                     int hdType = rowData.GetUnitIdAt(colIdx); 
        //                     if(hdType>9){
        //                         hdType = rowData.GetUnitIdAt(colIdx)/10;
        //                         dir = rowData.GetUnitIdAt(colIdx)%10;
        //                     }
        //                     GameObject newHdr = Instantiate(oriHinderArr[hdType-1]);
        //                     newHdr.transform.parent = oriHinderArr[0].transform.parent;
        //                     newHdr.SetActive(true);
        //                     newHdr.transform.SetLocalPositionX(rowData.GetXAt(colIdx));
        //                     newHdr.transform.SetLocalPositionY(0);
        //                     newHdr.transform.SetLocalPositionZ(rowData.GetYAt(colIdx));
        //                     if(hdType==2){
        //                         doSpikeMovement(newHdr,hinderCtArr[1]);
        //                     }
        //                     else if(hdType==5){
        //                         newHdr.transform.localRotation = Quaternion.Euler(0,90*dirMap[dir],0);
        //                         qukenDirArr[hinderCtArr[hdType-1]]=dir;
        //                     }
        //                     curHinderArr[curHinderLen] = newHdr;
        //                     curHinderLen++;
        //                     hinderCtArr[hdType-1]++;
        //                 }
        //                 colIdx++;
        //             }
        //             if(rowData.GetUnitIdAt(0)==3){
        //                 Vector4[] lineInfos = genMtsawDeal();
        //                 int idx=0;
        //                 while(lineInfos[idx].x!=-1){
        //                     GameObject newHdr = Instantiate(oriHinderArr[rowData.GetUnitIdAt(0)-1]);
        //                     newHdr.transform.parent = oriHinderArr[0].transform.parent;
        //                     newHdr.SetActive(true);
        //                     Vector4 trPos = new Vector4();
        //                     trPos.x = lineInfos[idx].y-4.8f;
        //                     trPos.y = lineInfos[idx].x-6;
        //                     trPos.z = lineInfos[idx].w-4.8f;
        //                     trPos.w = lineInfos[idx].z-6;
        //                     mtsawMovement(newHdr,trPos);
        //                     curHinderArr[curHinderLen] = newHdr;
        //                     curHinderLen++;
        //                     idx++;
        //                     hinderCtArr[2]++;
        //                 }
        //             }
        //         }
        //     }
        // }


        // public Vector4[] genMtsawDeal(){
        //     //x,y,dir,len
        //     //dir 1 left 2 right 3 up 4 down
        //     Vector4[] res = new Vector4[20];
        //     for(int i=0;i<20;i++){
        //         res[i].x = -1;
        //     }
        //     //交叉+1
        //     for(int i=1;i<26;i++){
        //         for(int j=1;j<10;j++){
        //             if(mtSawMatrix[i][j]==1){
        //                 if(mtSawMatrix[i-1][j]==1&&mtSawMatrix[i+1][j]==1){
        //                     if(mtSawMatrix[i][j-1]==1&&mtSawMatrix[i][j+1]==1){
        //                         mtSawMatrix[i][j]+=1;
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     int resLen = 0;
        //     Vector2 getStaPos(){
        //         for(int i=0;i<27;i++){
        //             for(int j=0;j<11;j++){
        //                 if(mtSawMatrix[i][j]>0){
        //                     return new Vector2(i,j);
        //                 }
        //             }
        //         }
        //         return new Vector2(-1,-1);
        //     }
         
        //     while(getStaPos().x!=-1){
        //         Vector4 lineInfo = getLineInfo();
        //         // Debug.Log(lineInfo);
        //         res[resLen]=lineInfo;
        //         resLen++;
        //     }

        //     Vector4 getLineInfo(){
        //         Vector2 staPos = getStaPos();
        //         Vector2 curPos = new Vector2(staPos.x,staPos.y);
        //         mtSawMatrix[(int)staPos.x][(int)staPos.y]-=1;
        //         if(staPos.y-1>=0&&mtSawMatrix[(int)staPos.x][(int)staPos.y-1]>0){
        //             curPos.y-=1;
        //             while(mtSawMatrix[(int)curPos.x][(int)curPos.y]>0){
        //                 mtSawMatrix[(int)curPos.x][(int)curPos.y]-=1;
        //                 curPos.y-=1;
        //                 if(curPos.y==-1){
        //                     break;
        //                 }
        //             }
        //             curPos.y+=1;
        //         }
        //         else if(staPos.y+1<11&&mtSawMatrix[(int)staPos.x][(int)staPos.y+1]>0){
        //             curPos.y+=1;
        //             while(mtSawMatrix[(int)curPos.x][(int)curPos.y]>0){
        //                 mtSawMatrix[(int)curPos.x][(int)curPos.y]-=1;
        //                 curPos.y+=1;
        //                 if(curPos.y==11){
        //                     break;
        //                 }
        //             }
        //             curPos.y-=1;
        //         }
        //         else if(staPos.x+1>=0&&mtSawMatrix[(int)(staPos.x+1)][(int)staPos.y]>0){
        //             curPos.x+=1;
        //             while(mtSawMatrix[(int)curPos.x][(int)curPos.y]>0){
        //                 mtSawMatrix[(int)curPos.x][(int)curPos.y]-=1;
        //                 curPos.x+=1;
        //                 if(curPos.x==27){
        //                     break;
        //                 }
        //             }
        //             curPos.x-=1;
        //         }
        //         else{
        //             curPos.x-=1;
        //             while(mtSawMatrix[(int)curPos.x][(int)curPos.y]>0){
        //                 mtSawMatrix[(int)curPos.x][(int)curPos.y]-=1;
        //                 curPos.x-=1;
        //                 if(curPos.x==-1){
        //                     break;
        //                 }
        //             }
        //             curPos.x+=1;
        //         }
        //         return new Vector4(staPos.x,staPos.y,curPos.x,curPos.y);
        //     }
        //     return res;
        // }

        // public bool inHinderRange(Vector3 pos){
        //     //只绕过方块
        //     for(int i=0;i<hinderCtArr[0];i++)
        //     {
        //         if(pos.x>curHinderArr[i].transform.localPosition.x-1.5f){
        //             if(pos.x<curHinderArr[i].transform.localPosition.x+1.5f){
        //                 if(pos.z>curHinderArr[i].transform.localPosition.z-26.3f){
        //                     if(pos.z<curHinderArr[i].transform.localPosition.z-23.5f){
        //                         return true;
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     return false;
        // }

        // void clearCurSceneHinder(){
        //     clearMtsawMatrix();
        //     if(hinderCtArr[4]>0){
        //         for(int i=0;i<100;i++){
        //             qukenDirArr[i]=0;
        //         }
        //     }
        //     for(int i=0;i<6;i++){
        //         hinderCtArr[i]=0;
        //     }
        //     for(int i=0;i<curHinderLen;i++){
        //         Destroy(curHinderArr[i]);
        //         curHinderArr[i]=null;
        //     }
        //     curHinderLen = 0;

        //     for(int i=0;i<hdrSeqLen;i++){
        //         hinderSeqArr[i].Kill(true);
        //         hinderSeqArr[i] = null;
        //     }
        //     hdrSeqLen = 0;
        //     isSpikeActive = false;
        // }

        // public void StartGame(StartGameParam param)
        // {
        //     touchPad.gameObject.SetActive(true);
        //     _startGameParam = param;
        //     curSelectLevel = param.levelVal;
        //     isHeroHurt = false;
        //     levelScore = 0;
        //     isMAllKill = false;
        //     isGetHintHurt = false;
        //     autoBtnObj.SetActive(false);

        //     PauseForm.onRoleHpChange?.Invoke(1);
        //     PauseForm.skillFillChange?.Invoke(-10);

        //     atkCritPet = 0.1f;
        //     atkCritVal = 1f;
        //     skBdRate = 0;
        //     dogeRate =0;
            
        //     loadSceneHinder(curSelectLevel);

        //     comboCount=0;
        //     Monster.desNguiCt=0;
        //     currentWave = 0;
        //     _battleWavesData.Clear();

        //     var drBattleWavesData = GameEntry.DataTable.GetDataTable<DRBattleWavesData>();
        //     var drBattleLevelData = GameEntry.DataTable.GetDataTable<DRBattleLevelData>().GetDataRow(curSelectLevel);
        //     for (int i = 0; i < drBattleLevelData.WaveIdCount; i++)
        //     {
        //         var waveId = drBattleLevelData.GetWaveIdAt(i);
        //         if (waveId > 0)
        //         {
        //             _battleWavesData.Add(drBattleWavesData.GetDataRow(waveId));
        //         }
        //     }

        //     _presetSceneBuffs.Clear();
        //     _presetHeroSkills.Clear();

        //     if (param.presetSceneBuffs != null)
        //     {
        //         foreach (var b in param.presetSceneBuffs)
        //         {
        //             _presetSceneBuffs[b.Key] = b.Value;
        //         }
        //     }

        //     foreach (var s in param.presetHeroSkills)
        //     {
        //         _presetHeroSkills[s] = 0f;
        //     }

        //     reliveCountByPayment = 0;
        //     reliveCount = 0;

        //     availableReliveCount = _presetSceneBuffs.ContainsKey(SceneBuffType.Relive) ? Mathf.RoundToInt(_presetSceneBuffs[SceneBuffType.Relive][0]) : 0;

        //     stageStateMachine = new StateMachine();

        //     _groundPlane = new Plane(-ground.forward, ground.position);

        //     touchPad.onClick -= OnTouchClick;
        //     touchPad.onClick += OnTouchClick;

        //     ResumeTouch();

        //     _monsterDiePresentationArranger = new MonsterDiePresentationArranger(this);

        
        //     hpBarController.Init(this);
           

        //     battleEnded = false;
        //     // game start
        //     stageStateMachine.SetCurrentState(new SceneStagePrepare(this));
        // }

        // public int getTotalMonstersCt(){
        //     int ct = 0;
        //     for(int i=0;i<_battleWavesData.Count;i++){
        //         var waveData = _battleWavesData[i];
        //         for (int j = 0; j < waveData.MonsterIdCount;j++)
        //         {
        //             var monsterId = waveData.GetMonsterIdAt(j);
        //             if (monsterId > 0)
        //             {
        //                 ct++;
        //             }
        //         }
        //     }
        //     Debug.Log("getTotalMonstersCt+"+ct);
        //     return ct; 
        // }

        // //从0开始
        // public int getWaveMonCt(int wave){
        //     int ct=0;
        //     var waveData = _battleWavesData[wave];
        //     if(waveData==null){
        //         return ct;
        //     }
        //     for (int j = 0; j < waveData.MonsterIdCount;j++)
        //     {
        //         var monsterId = waveData.GetMonsterIdAt(j);
        //         if (monsterId > 0)
        //         {
        //             ct++;
        //         }
        //     }

        //     return ct;
        // }

        // //wave波之前的总个数
        // public int getBeforeWaveTotal(int wave){
        //     int ct=0;
        //     var waveDataT = _battleWavesData[wave];
        //     if(waveDataT==null){
        //         return ct;
        //     }
        //     if(wave==0){
        //         return 0;
        //     }
        //     for(int i=0;i<wave;i++){
        //         var waveData = _battleWavesData[i];
        //         for (int j = 0; j < waveData.MonsterIdCount;j++)
        //         {
        //             var monsterId = waveData.GetMonsterIdAt(j);
        //             if (monsterId > 0)
        //             {
        //                 ct++;
        //             }
        //         }
        //     }
        //     return ct;
        // }
        // public List<Monster> getCurwaveMonsters(){
        //     List<Monster> res = new List<Monster>();
        //     int befCt=getBeforeWaveTotal(currentWave);
        //     int waveCt = getWaveMonCt(currentWave);
        //     // Debug.Log("gcm.."+befCt+" "+waveCt+" "+_monsters.Count);
        //     if(befCt+waveCt<=_monsters.Count){
        //         for(int i=befCt;i<befCt+waveCt;i++){
        //             if(!_monsters[i].dead){
        //                 res.Add(_monsters[i]);
        //             }
        //         }
        //     }

        //     if(newSpwnMons!=null){
        //         for(int i=0;i<newSpwnMons.Count;i++){
        //             if(!newSpwnMons[i].dead){
        //                 res.Add(newSpwnMons[i]);    
        //             }
        //         }
        //     }

        //     return res;   
        // }

        // //有没有怪在周围
        // public int[] isMonsterInWay(Monster mons,Vector3 nextPos){
        //     //0 是否挡 出路12 l r
        //     int[] freePos = new int[]{0,1,1};
        //     List<Monster> mList = getCurwaveMonsters();
        //     for(int i=0;i<mList.Count;i++){
        //         if(!mList[i].dead&&mList[i]!=mons){
        //             if(Util.isTwoPositionClose(mons.transform.position,mList[i].transform.position,1f)){
        //                 freePos[0]=1;
        //                 Vector3 leftPos = new Vector3(mons.transform.position.x-0.7f,0,mons.transform.position.z);
        //                 Vector3 rightPos = new Vector3(mons.transform.position.x+0.7f,0,mons.transform.position.z);
        //                 if(Util.isTwoPositionClose(leftPos,mList[i].transform.position,1f)){
        //                     freePos[1]=0;
        //                 }
        //                 if(Util.isTwoPositionClose(rightPos,mList[i].transform.position,1f)){
        //                     freePos[2]=0;
        //                 }
        //             }
        //         }
        //     }
        //     // Debug.Log(freePos[0]+" "+freePos[1]+" "+freePos[2]);
        //     return freePos;
        // }

        // //选一个怪
        // public Monster getOneMonsTarget(){
        //     List<Monster> res = getCurwaveMonsters();
        //     List<Monster> aliRes = new List<Monster>();
        //     foreach (var mons in res)
        //     {
        //         if(!mons.dead){
        //             aliRes.Add(mons);
        //         }
        //     }
        //     if(aliRes.Count>0){
        //         return aliRes[UnityEngine.Random.Range(0,aliRes.Count)];
        //     }
        //     return null;
        // }

        // public void stopCurwaveMonsters(){
        //     List<Monster> res = getCurwaveMonsters();
        //     for(int i=0;i<res.Count;i++){
        //         res[i].moveComponent.moveSpeed = 0;
        //     }
        // }

        // //从前往后击退怪物
        // public void hitawayMonsters(){
        //     List<Monster> res = getCurwaveMonsters();
        //     // Debug.Log("hitawayMonsters.."+res.Count);
        //     //距离排序
        //     for(int i=0;i<res.Count-1;i++){
        //         for(int j=i+1;j<res.Count;j++){
        //             if(res[i].transform.position.z>res[j].transform.position.z){
        //                 Monster temp = res[i];
        //                 res[i] = res[j];
        //                 res[j] = temp;
        //             }
        //         }
        //     }
            
        //     // Debug.Log("hhmm.."+res.Count);
        //     Sequence hitSeq = DOTween.Sequence();
        //     for(int i=0;i<res.Count;i++){
        //         int temp = i;
        //         hitSeq.AppendCallback(delegate(){
        //             if(res[temp].isBoss){
        //                 return;
        //             }
        //             float dirX = UnityEngine.Random.Range(0f,1f);
        //             if(res[temp].transform.position.x<0){
        //                 dirX = dirX*-1;
        //             }
        //             else{
        //             }
        //             float dirZ = UnityEngine.Random.Range(0f,1f);
        //             Vector3 hitDis = new Vector3(8,0,8);
        //             Vector3 hitDir = new Vector3(dirX,0,dirZ);
        //             float disX = hitDis.x*hitDir.normalized.x;
        //             float disY = hitDis.y*hitDir.normalized.y;
        //             float disZ = hitDis.z*hitDir.normalized.z;
        //             Vector3 newPos = res[temp].transform.position+new Vector3(disX,disY,disZ);
        //             res[temp].transform.DOMove(newPos,0.5f).SetEase(Ease.OutSine);
        //         });
        //         hitSeq.AppendInterval(0.05f);
        //         hitSeq.AppendCallback(delegate(){
        //             // res[temp].DrainHP();
        //             res[temp].DecreaseHP(hero.attack*6);
        //         });
        //     }
        //     hitSeq.AppendCallback(delegate(){
        //         hitSeq.Kill(true);
        //         hitSeq = null;
        //     });
        // }

        // public bool isWaveHasBoss(int wave){
        //     if(wave<0){
        //         wave=0;
        //     }
        //     // Debug.Log(_battleWavesData.Count);
        //     if (wave < _battleWavesData.Count)
        //     {
        //         var waveData = _battleWavesData[wave];
        //         for (int i = 0; i < waveData.MonsterIdCount; i++)
        //         {
        //             var monsterId = waveData.GetMonsterIdAt(i);
        //             if (monsterId>=13&&monsterId<=15)
        //             {
        //                 return true;
        //             }
        //         }
        //     }
        //     return false;
        // }

        // public int getMaxWave(){
        //     return _battleWavesData.Count;
        // }


        // public void SpawnMonsters(int wave)
        // {
        //     MonsterStateAttack.isStnNextLock = false;
        //     isRstMstArea = false;
        //     Sequence wtSeq = DOTween.Sequence();
        //     wtSeq.AppendInterval(3.5f);
        //     wtSeq.AppendCallback(delegate(){
        //         isRstMstArea=true;
        //         wtSeq.Kill(true);
        //         wtSeq=null;
        //     });
        //     if(newSpwnMons!=null){
        //         newSpwnMons.Clear();
        //         newSpwnMons = new List<Monster>();
        //     }

        //     // Debug.Log("SpawnMonsters..."+wave);

        //     if(hero!=null){
        //         hero._invincible=0;
        //         hero.isRelife = false;
        //     }
        //     novGuiCt+=1;
        //     currentWave = wave;

        //     if (wave <= _battleWavesData.Count)
        //     {
        //         var waveData = _battleWavesData[wave];
        //         int mtCt = 0;
        //         for (int i = 0; i < waveData.MonsterIdCount; i++)
        //         {
        //             var monsterId = waveData.GetMonsterIdAt(i);
        //             if (monsterId > 0)
        //             {
        //                 mtCt++;
        //             }
        //         }

        //         bool hasBoss = false;
        //         void bornAction(int idx){
        //             Vector3 pos = new Vector3(waveData.GetXAt(idx),0,waveData.GetYAt(idx));
        //             var monsterId = waveData.GetMonsterIdAt(idx);
        //             if(monsterId>=13&&monsterId<=15){
        //                 if(hasBoss){
        //                     return;
        //                 }
        //                 hasBoss = true;
        //             }
        //             playBornPresentation(pos,0.5f);
        //             Sequence bornSeq = DOTween.Sequence();
        //             bornSeq.AppendCallback(delegate(){
        //                 SpawnMonster(monsterId, waveData.GetXAt(idx), waveData.GetYAt(idx),idx==mtCt-1,(idx==0&&wave==0));
        //             });
        //             bornSeq.AppendCallback(delegate(){
        //                 bornSeq.Kill(true);
        //                 bornSeq = null;
        //             });
        //         }

        //         int ranVal = UnityEngine.Random.Range(0,2);        
        //         if(ranVal==0&&mtCt<=10){
        //             Sequence mBorSeq = DOTween.Sequence();
        //             for(int i=0;i<mtCt;i++){       
        //                 int temp = i;
        //                 mBorSeq.AppendCallback(delegate(){
        //                     bornAction(temp);
        //                 }); 
        //                 mBorSeq.AppendInterval(0.1f);
        //             }
        //             mBorSeq.AppendCallback(delegate(){
        //                 mBorSeq.Kill(true);
        //                 mBorSeq = null;
        //             });
        //         }
        //         else{
        //             for(int i=0;i<mtCt;i++){
        //                 bornAction(i);
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogError($"SpawnMonsters() index out of bounds {wave} {_battleWavesData.Count}");
        //     }
        // }


        // // public GameObject[] bornMtEffArr;
        // // public int bornMtEffLen=0;
        // public void playBornPresentation(Vector3 pos,float playTm,TweenCallback cb=null)
        // {
        //     GameObject go = Instantiate(monsterBornEffectPrefab);
        //     go.transform.position = pos;
        //     go.transform.SetPositionY(0.05f);
        //     go.transform.parent = unitParent;
        //     // bornMtEffArr[bornMtEffLen] = go;
        //     // bornMtEffLen++;

        //     ParticleSystem ps = go.GetComponent<ParticleSystem>();
        //     ps.Play();
        //     ps.loop = true;
            
        //     Sequence bnSeq = DOTween.Sequence();
        //     bnSeq.AppendInterval(playTm);
        //     bnSeq.AppendCallback(delegate(){
        //         Destroy(go);
        //         go = null;
        //         if(cb!=null){
        //             cb();
        //         }
        //         bnSeq.Kill(true);
        //         bnSeq = null;
        //     });

        // }

        // //避开障碍
        // Vector3 adjustBornPoint(Vector3 pos){
        //     if(!inHinderRange(pos)){
        //         // Debug.Log("111");
        //         return pos;
        //     }
        //     float[] adjustPos(float radius){
        //         if(!inHinderRange(new Vector3(pos.x-radius,0,pos.z))){
        //             pos.x-=radius;
        //             return new float[]{1,pos.x,pos.z};
        //         }
        //         if(!inHinderRange(new Vector3(pos.x+radius,0,pos.z))){
        //             pos.x+=radius;
        //             return new float[]{1,pos.x,pos.z};
        //         }
        //         if(!inHinderRange(new Vector3(pos.x,0,pos.z-radius))){
        //             pos.z-=radius;
        //             return new float[]{1,pos.x,pos.z};
        //         }
        //         if(!inHinderRange(new Vector3(pos.x,0,pos.z+radius))){
        //             pos.z+=radius;
        //             return new float[]{1,pos.x,pos.z};
        //         }
        //         return new float[]{0,pos.x,pos.z};
        //     } 
        //     float oriradius = 0.5f;
        //     int ct = 0;
        //     float[] res = adjustPos(oriradius);
        //     while(res[0]==0){
        //         oriradius+=0.5f;
        //         ct++;
        //         if(ct==5){
        //             break;
        //         }
        //         res = adjustPos(oriradius);
        //         pos.x = res[1];
        //         pos.z = res[2];
        //     }   
        //     return pos;
        // }

        // public void SpawnMonster(int monsterId, float x, float y,bool fin=false,bool first = false,bool isNewSpwn=false)
        // {
        //     Vector3 pos = new Vector3(x,0,y);
        //     pos = adjustBornPoint(pos);
        //     x = pos.x;
        //     y = pos.z;
        //     var drBattleMonsterData = GameEntry.DataTable.GetDataTable<DRBattleMonsterData>().GetDataRow(monsterId);
        //     // Debug.Log(drBattleMonsterData.Prefab);
        //     GameEntry.Resource.LoadAsset(AssetUtility.GetMonsterPrefab(drBattleMonsterData.Prefab), Constant.AssetPriority.MonsterPrefab, new LoadAssetCallbacks(
        //         (assetName, asset, duration, userData) =>
        //         {
        //             // TODO: 后面用对象池优化
        //             var go = GameObject.Instantiate(asset as UnityEngine.Object, unitParent) as GameObject;
        //             var trans = go.transform;
        //             trans.position = new Vector3(x, 0, y);
        //             trans.ResetLocalY();

        //             trans.localRotation = Quaternion.identity;

        //             var monster = trans.GetComponent<Monster>();
        //             monster.InitializeMonster(this, drBattleMonsterData);
        //             if(newSpwnMons==null){
        //                 newSpwnMons = new List<Monster>();
        //             }
        //             if(isNewSpwn){
        //                 newSpwnMons.Add(monster);
        //             }

        //             // go.SetActive(false);
        //             // monster.bornEff = StartBornPresentation(monster);
        //             // Sequence wtSeq = DOTween.Sequence();
        //             // wtSeq.AppendInterval(1.5f);
        //             // wtSeq.AppendCallback(delegate(){
        //             //     go.SetActive(true);
        //             //     wtSeq.Kill(true);
        //             //     wtSeq=null;
        //             // });
        //             if(first){
        //                 monster.isFirst = true;
        //             }
        //             else{
        //                 monster.isFirst = false;
        //             }

        //             ApplySceneBuffToMonster(monster);

        //             monster.onHPChanged -= OnMonsterHPChanged;
        //             monster.onHPChanged += OnMonsterHPChanged;

        //             monster.onUnitDied -= OnMonsterDied;
        //             monster.onUnitDied += OnMonsterDied;

        //             if(!isNewSpwn){
        //                 _monsters.Add(monster);    
        //             }

        //             // Debug.Log(onMonsterSpawn+Util.GetTimeStamp());
        //             onMonsterSpawn?.Invoke(monster);

        //             Log.Info("Load GetMonsterPrefab '{0}' OK.", assetName);
        //             if(fin){
        //                 genGuideArr();
        //             }
        //         },

        //         (assetName, status, errorMessage, userData) =>
        //         {
        //             Log.Error("Can not load GetMonsterPrefab '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //         }));
        // }

        // public int waveThCt = 0;
        // public bool canMonsterClick = false;
        // public bool guiMonsFilter(int wave,int idx,int callCt=0){
        //     if(curSelectLevel!=1){
        //         return false;
        //     }
        //     if(wave==1){
        //         //第一波1只
        //         //第二波全部
        //         if(idx>=2){
        //             return true;
        //         }
        //     }
        //     else if(wave==2){
        //         if(canMonsterClick){
        //             return true;
        //         }
        //         waveThCt = callCt;
        //         //前波3只
        //         //第三波第一只
        //         if(callCt==0){
        //             if(idx==4){
        //                 return true;
        //             }
        //         }
        //         else if(callCt==1){
        //             //前波3只
        //             //第三波第5只
        //             if(idx==8){
        //                 return true;
        //             }
        //         }
        //     }
        //     return false;
        // }

        // //新手箭头
        // public void genGuideArr(int callCt=0){
        //     GameObject arrObj = PauseForm.hpParent.Find("nGuiImg").gameObject;
        //     arrObj.SetActive(false);
        //     int mCt= 0;
        //     foreach(var mt in monsters){
        //         mCt++;
        //         if(guiMonsFilter(currentWave,mCt,callCt)){
        //             GameObject arrNObj = GameObject.Instantiate(arrObj);
        //             arrNObj.transform.parent = arrObj.transform.parent;
        //             if(isPureGuide){
        //                 arrNObj.SetActive(false);
        //             }
        //             else{
        //                 arrNObj.SetActive(true);
        //             }
        //             mt.nguiItem = arrNObj;
        //             var nimgItem = arrNObj.GetComponent<NguiImgItem>();
        //             Camera cv = arrObj.transform.parent.GetComponent<Canvas>().worldCamera;
        //             nimgItem.Init(mt,gameCamera,cv);
        //         }
        //     }
        // }

        // public void fitScreen(){
        //     // staFtObj.transform.localScale = new Vector3(Screen.width/1080f,Screen.width/1080f,Screen.width/1080f);
        //     // bossComeObj.transform.localScale = new Vector3(Screen.width/1080f,Screen.width/1080f,Screen.width/1080f);  
            
        // }

        // private GameObject tipsPanel;
        // public void showExplain(bool flag,int type=0){
        //     tipsPanel.SetActive(flag);
        //     if(!flag){
        //     }
        //     else{
        //         Pause();
        //         // Text title = tipsPanel.transform.Find("pl/bg2/Text").GetComponent<Text>();
        //         // title.text = tipsTitleArr[type-1];
        //         Text cont = tipsPanel.transform.Find("pl/Text").GetComponent<Text>();
        //         Text mTypeTt = tipsPanel.transform.Find("pl/Text1").GetComponent<Text>();
        //         Image bgImg = tipsPanel.transform.Find("black").GetComponent<Image>();
        //         if(type==1){
        //             mTypeTt.gameObject.SetActive(true);
        //             mTypeTt.text = "怪物";
        //             bgImg.color = new Color(0,0,0,0);
        //         }
        //         else
        //         {
        //             mTypeTt.gameObject.SetActive(false);
        //             bgImg.color = new Color(0,0,0,204f/255f);
        //         }
        //         cont.text = tipsContArr[type-1];
                
        //     }
        // }
        

        // public Vector3 getHeroOriPos(){
        //     var drBattleLevelData = GameEntry.DataTable.GetDataTable<DRBattleLevelData>().GetDataRow(_startGameParam.levelId);
        //     Vector3 pos = new Vector3(drBattleLevelData.HeroX, 0, drBattleLevelData.HeroY);
        //     return pos;
        // }

        // public bool isHeroWideAtk = false;
        // //默认暴击率
        // public float atkCritPet = 0.1f;
        // //默认暴击值
        // public float atkCritVal = 2f;

        // public float skBdRate = 0;
        // public float dogeRate = 0;

        // //buff 暴击值
        // public float getCritBuffVal(){
        //     _presetSceneBuffs.TryGetValue(SceneBuffType.IncreaseCritValue, out var param);
        //     if(param==null){
        //         return atkCritVal;
        //     }
        //     return atkCritVal+param[0];
        // } 
        // //buff 暴击率
        // public float getCritBuffPet(){
        //     _presetSceneBuffs.TryGetValue(SceneBuffType.IncreaseCrit, out var param);
        //     if(param==null){
        //         return atkCritPet;
        //     }
        //     return atkCritPet+param[0];
        // } 

        // public float getSuckBloodBuffVal(){
        //     _presetSceneBuffs.TryGetValue(SceneBuffType.SuckBlood, out var param);
        //     if(param==null){
        //         return 0;
        //     }
        //     return param[0];
        // } 

        // public float getAddCoinBuffVal(){
        //     _presetSceneBuffs.TryGetValue(SceneBuffType.IncreaseGold, out var param);
        //     if(param==null){
        //         return 0;
        //     }
        //     return param[0];
        // } 
        // //是否无伤障碍
        // public float getCrossBuff(){
        //     _presetSceneBuffs.TryGetValue(SceneBuffType.CrossField, out var param);
        //     if(param==null){
        //         return 0;
        //     }
        //     return param[0];
        // } 

        // public bool isRstMstArea = false;

        // public void SpawnHero(bool relif=false)
        // {
        //     var drBattleHeroData = GameEntry.DataTable.GetDataTable<DRBattleHeroData>().GetDataRow(MainForm.hrSkinIdx);
        //     // Debug.Log("sss,"+MainForm.hrSkinIdx+" "+AssetUtility.GetHeroPrefab(drBattleHeroData.Prefab));
        //     GameEntry.Resource.LoadAsset(AssetUtility.GetHeroPrefab(drBattleHeroData.Prefab), Constant.AssetPriority.HeroPrefab, new LoadAssetCallbacks(
        //         (assetName, asset, duration, userData) =>
        //         {
        //             if (hero)
        //                 Destroy(hero.gameObject);

        //             var drBattleWeaponData = GameEntry.DataTable.GetDataTable<DRBattleWeaponData>().GetDataRow(_startGameParam.weaponId);
        //             var drBattleLevelData = GameEntry.DataTable.GetDataTable<DRBattleLevelData>().GetDataRow(curSelectLevel);

        //             var go = GameObject.Instantiate(asset as UnityEngine.Object, unitParent) as GameObject;
                
        //             var trans = go.transform;

        //             // Debug.Log("sphr..."+_startGameParam.levelId);
        //             // Debug.Log("sphr..."+drBattleLevelData.HeroX+" "+drBattleLevelData.HeroY);
        //             trans.position = new Vector3(drBattleLevelData.HeroX, 0, drBattleLevelData.HeroY);
        //             trans.ResetLocalY();

        //             var hpBase = NumericalCalculator.CalcHero(drBattleHeroData.HP, _startGameParam.heroLevel,1);
        //             // Debug.Log("hpbase1.."+hpBase);

        //             var attackBase = NumericalCalculator.CalcHero(drBattleHeroData.Attack, _startGameParam.heroLevel,3);
        //             // Debug.Log("SpawnHero.."+attackBase);
        //                 // + NumericalCalculator.CalcWeapon(drBattleWeaponData.Attack, _startGameParam.weaponLevel);

        //             // Debug.Log("kkk.."+drBattleHeroData.Defence);
        //             var defenceBase = NumericalCalculator.CalcHero(drBattleHeroData.Defence, _startGameParam.heroLevel,2);
        //                 // + NumericalCalculator.CalcWeapon(drBattleWeaponData.Defence, _startGameParam.weaponLevel);
        //             // Debug.Log("kkk.."+defenceBase);
        //             ApplySceneBuffToHeroBaseAttribute(ref hpBase, SceneBuffType.IncreaseHP);
                    
        //             float oldAtk = attackBase;
        //             ApplySceneBuffToHeroBaseAttribute(ref attackBase, SceneBuffType.IncreaseAttack);
        //             // Debug.Log("SpawnHero1.."+attackBase);
                    
        //             // TODO: 暴击等
        //             hero = trans.GetComponent<Hero>();
        //             hero.isRelife = relif;
        //             if(relif){
        //                 PauseForm.onRoleHpChange.Invoke(1);
        //                 List<Monster> curMons = getCurwaveMonsters();
        //                 foreach(var mons in curMons){
        //                     mons.isLockTarget = false;
        //                 }
        //             }

        //             if(attackBase>oldAtk){
        //                 hero.showBuffEff(3);
        //             }
        //             StartBornPresentation(hero,relif);
        //             // Debug.Log("rr.."+hpBase);
        //             hero.InitializeUnitAttributes(this, hpBase, attackBase, defenceBase, drBattleHeroData.MoveSpeed);
        //             // Debug.Log("rr.."+hero.HP);
        //             hero.InitializeHero(_presetHeroSkills);

        //             ApplySceneBuffToHero(hero);

        //             hero.onUnitDied -= OnHeroDied;
        //             hero.onUnitDied += OnHeroDied;

        //             hero.onHitMonster -= OnHeroHitMonster;
        //             hero.onHitMonster += OnHeroHitMonster;

        //             onHeroSpawn?.Invoke(hero);

        //             Log.Info("Load GetHeroPrefab '{0}' OK.", assetName);
        //         },

        //         (assetName, status, errorMessage, userData) =>
        //         {
        //             Log.Error("Can not load GetHeroPrefab '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //         }));
        // }

        // public void SpawnFountain()
        // {
        //     var drBattleLevelData = GameEntry.DataTable.GetDataTable<DRBattleLevelData>().GetDataRow(_startGameParam.levelId);
        //     var drBattleFountainData = GameEntry.DataTable.GetDataTable<DRBattleFountainData>().GetDataRow(drBattleLevelData.FountainId);

        //     GameEntry.Resource.LoadAsset(AssetUtility.GetFountainPrefab(drBattleFountainData.Prefab), Constant.AssetPriority.FountainPrefab, new LoadAssetCallbacks(
        //         (assetName, asset, duration, userData) =>
        //         {
        //             if (fountain)
        //                 Destroy(fountain.gameObject);

        //             var go = GameObject.Instantiate(asset as UnityEngine.Object, unitParent) as GameObject;
        //             var trans = go.transform;
        //             go.SetActive(false);
        //             trans.position = new Vector3(drBattleLevelData.FountainX, 0, drBattleLevelData.FountainY);
        //             trans.ResetLocalY();

        //             float hp = NumericalCalculator.CalcHero(drBattleFountainData.HP,curSelectLevel,1);
        //             float def = NumericalCalculator.CalcHero(drBattleFountainData.Defence,curSelectLevel,2);
        //             float atk = NumericalCalculator.CalcHero(drBattleFountainData.Attack,curSelectLevel,3);    
    
        //             fountain = trans.GetComponent<Fountain>();
        //             fountain.InitializeUnitAttributes(this, hp,atk,def, drBattleFountainData.MoveSpeed);
        //             fountain.InitializeFountain();

        //             fountain.onUnitDied -= OnFountainDied;
        //             fountain.onUnitDied += OnFountainDied;

        //             onFountainSpawn?.Invoke(fountain);

        //             Log.Info("Load GetFountainPrefab '{0}' OK.", assetName);
        //         },

        //         (assetName, status, errorMessage, userData) =>
        //         {
        //             Log.Error("Can not load GetFountainPrefab '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //         }));
        // }

        // public void MonstersVictory()
        // {
        //     foreach (var m in _monsters)
        //     {
        //         if (!m.dead)
        //         {
        //             m.Victory();
        //         }
        //     }
        // }

        // public void OnEnterStageEnd()
        // {
        //     battleEnded = true;
        //     PauseTouch();
        //     MonstersVictory();
        // }

        // public void ReliveHeroAndFountain()
        // {
        //     battleEnded = false;

        //     // if (reliveCount < availableReliveCount)
        //     //     reliveCount++;
        //     // else
        //     //     reliveCountByPayment++;

        //     trivialObjectsManager.Clear();

        //     Action<Hero> heroReliveAction = null;
        //     onHeroSpawn += heroReliveAction = (hero) =>
        //     {
        //         onHeroSpawn -= heroReliveAction;
        //         // hero.buffManager.AddBuff(BuffType.Invincible, 5f, null);
        //         hero.buffManager.AddBuff(BuffType.ModifyAttack, Mathf.Infinity, new[] { .3f });
        //         hero.buffManager.AddBuff(BuffType.ModifyHPMax, Mathf.Infinity, new[] { .2f });
        //     };

        //     if (hero)
        //     {
        //         // Destroy(hero.gameObject);
        //         // hero =null;
        //         hero.gameObject.SetActive(false);
        //     }
        //     SpawnHero(true);

        //     if (fountain)
        //     {
        //         // Destroy(fountain.gameObject);
        //         // fountain = null;
        //         fountain.gameObject.SetActive(false);
        //     }
        //     SpawnFountain();

        //     foreach (var m in _monsters)
        //     {
        //         if (!m.dead)
        //         {
        //             m.ContinueFromVictory();
        //             m.stoic = false;
        //             m.CancelInvincible(true);
        //             m.isInvinc=false;
        //         }
        //     }

        //     ResumeTouch();
        // }

        // public void RestartGame()
        // {
        //     Clear();
        //     StartGame(_startGameParam);
        // }

        // public void clearMonsters(){
        //     foreach (var monster in _monsters)
        //     {
        //         if(monster.nguiItem){
        //             Destroy(monster.nguiItem);
        //             monster.nguiItem = null;
        //         }
        //         Destroy(monster.gameObject);
        //     }
        //     _monsters.Clear();
        // }

        // /// <summary>
        // /// 清理战场
        // /// </summary>
        // public void Clear()
        // {
        //     isHeroHurt = false;
        //     levelScore = 0;
        //     isMAllKill = false;
            
        //     comboCount=0;
        //     Monster.desNguiCt=0;
        //     currentWave = 0;
        //     canMonsterClick = false;

        //     Resume();
        //     PauseTouch();

        //     hpBarController.Clear();
        //     clearCurSceneHinder();

        //     if (stageStateMachine != null)
        //     {
        //         stageStateMachine.Clear();
        //         stageStateMachine = null;
        //     }

        //     if(bossSkItem!=null){
        //         Destroy(bossSkItem);
        //         bossSkItem = null;
        //     }
        //     autoBtnObj.SetActive(false);

        //     if (hero)
        //     {
        //         Destroy(hero.gameObject);
        //         hero = null;
        //     }
        //     if (fountain)
        //     {
        //         Destroy(fountain.gameObject);
        //         fountain = null;
        //     }

        //     newSpwnMons.Clear();
        //     newSpwnMons = null;

        //     foreach (var monster in _monsters)
        //     {
        //         if(monster.nguiItem){
        //             Destroy(monster.nguiItem);
        //             monster.nguiItem = null;
        //         }
        //         Destroy(monster.gameObject);
        //     }
        //     _monsters.Clear();
        //     novGuiCt=0;
        //     hpBarController.nGuiObj.SetActive(false);

        //     trivialObjectsManager.Clear();

        //     onHeroDied = null;
        //     onFountainDied = null;
        //     onMonstersAllDied = null;
        //     onHeroSpawn = null;
        //     onFountainSpawn = null;
        //     onMonsterSpawn = null;

        //     _battleWavesData.Clear();

        //     if (_monsterDiePresentationArranger != null)
        //     {
        //         _monsterDiePresentationArranger.Clear();
        //         _monsterDiePresentationArranger = null;
        //     }

        //     _monsterDieActionExplode = null;
        //     _monsterDieActionFreeze = null;
        //     _monsterDieActionThunder = null;

        //     battleEnded = true;
        // }

        // public void Pause()
        // {
        //     Debug.Log("scmain pause1");
        //     if (!isPaused)
        //     {
        //         Debug.Log("scmain pause2");
        //         _preservedTimeScale = Time.timeScale;
        //         Time.timeScale = 0f;
        //         isPaused = true;
        //         touchPad.gameObject.SetActive(false);
        //     }
        // }

        // public void Resume()
        // {
        //     if (isPaused)
        //     {
        //         touchPad.gameObject.SetActive(true);
        //         Time.timeScale = _preservedTimeScale;
        //         isPaused = false;
        //     }
        // }

        // public void RequestPause(float pauseTime)
        // {
        //     if (isPaused)
        //     {
        //         _pauseDuration = pauseTime;
        //         _pauseElapsedTime = 0f;
        //     }
        //     else
        //     {
        //         _pauseDuration += pauseTime;
        //     }
        // }

        // public void quitGame(){
        //     #if UNITY_EDITOR
        //         UnityEditor.EditorApplication.isPlaying = false;
        //     #else
        //         Application.Quit();
        //     #endif
        // }

        // public void PauseTouch()
        // {
        //     touchPad.enabled = false;
        //     _touchPaused = true;
        // }

        // public void ResumeTouch()
        // {
        //     touchPad.enabled = true;
        //     _touchPaused = false;
        // }

        // // 1 四周晃 2 上下晃
        // public void ShakeCamera(int type = 1)
        // {
        //     // gameCamera.GetComponent<CameraShake>().DoShake();
        //     if (_shakingTween != null) _shakingTween.Kill();
        //     if(type==1){
        //         // _shakingTween = gameCamera.DOShakePosition(.1f,.3f, 5, 90,false)
        //         // .OnComplete(_onShakeComplete ?? (_onShakeComplete = () =>{
        //         //     // gameCamera.transform.DOMove(_originCameraPos, .1f)  
        //         // } 
        //         // ));
        //         // float oriX = 0.25f;
        //         // float deX = 0.5f;
        //         // if(UnityEngine.Random.Range(0,2)==1){
        //         //     deX = -0.5f;                    
        //         // }
        //         // Sequence skSeq = DOTween.Sequence();
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveX(oriX+deX,0.08f).SetEase(Ease.InSine));
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveX(oriX,0.03f).SetEase(Ease.OutSine));
        //         // skSeq.AppendCallback(delegate(){
        //         //     skSeq.Kill(true);
        //         //     skSeq = null;
        //         // });

        //         // float oriY = 18;
        //         // Sequence skSeq = DOTween.Sequence();
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveY(oriY+0.4f,0.08f).SetEase(Ease.InSine));
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveY(oriY,0.03f).SetEase(Ease.OutSine));
        //         // skSeq.AppendCallback(delegate(){
        //         //     skSeq.Kill(true);
        //         //     skSeq = null;
        //         // });
        //         // PauseForm.shakeBySceAc?.Invoke(0.4f);
        //     }
        //     else if(type==2){
        //         // float oriY = 18;
        //         // Sequence skSeq = DOTween.Sequence();
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveY(oriY+0.8f,0.08f).SetEase(Ease.InSine));
        //         // skSeq.Append(gameCamera.transform.DOLocalMoveY(oriY,0.03f).SetEase(Ease.OutSine));
        //         // skSeq.AppendCallback(delegate(){
        //         //     skSeq.Kill(true);
        //         //     skSeq = null;
        //         // });
        //         // PauseForm.shakeBySceAc?.Invoke(0.8f);
        //     }

        // }


        // bool isGuiCanClick(Vector2 touch){
        //     GameObject obj = hpBarController.nGuiObj;
        //     float deltaX = 0;
        //     float deltaY = 0;
        //     if(currentWave==0){
        //     }
        //     else if(currentWave==2){
        //         int mCt= 0;
        //         foreach(var mt in monsters){
        //             mCt++;
        //             // Debug.Log("rrrbbb+"+currentWave+mCt+waveThCt);
        //             if(guiMonsFilter(currentWave,mCt,waveThCt)){
        //                 obj = mt.nguiItem;
        //                 deltaX = -55;
        //                 deltaY = -10;
        //                 // Debug.Log("rrrbbb+"+obj);
        //             }
        //         }
        //     }
        //     if(obj==null){
        //         return true;
        //     }
        //     if(MonsterStateMoveForward.callTm==1||MonsterStateMoveForward.callTm==3){
        //         return true;
        //     }
        //     Vector3 pos = obj.transform.position;
        //     pos.x-=50;
        //     pos.y+=50;
        //     Debug.Log("rrrbbb+"+pos+" "+ touch+" "+obj.transform.position);
        //     // 70*70;
        //     float x2 = Mathf.Pow(touch.x-Mathf.Abs(pos.x+deltaX),2);
        //     float y2 = Mathf.Pow(touch.y-Mathf.Abs(pos.y+deltaY),2);
        //     float radis = Mathf.Pow(120,2);
        //     if(x2+y2<=radis) return true;
        //     Debug.Log("xxx");
        //     return false;
        // }


        // public Button.ButtonClickedEvent skillBtnBce1;
        // public bool skillBtnBceEnable = true;
        // bool isInSkillBtnRange(Vector2 touch){
        //     float deltaX = 0;
        //     float deltaY = 0;
        //     // Screen.width
        //     float pX = 110/720f*Screen.width;
        //     float adjX =Screen.height/Screen.width-1.7f;
        //     if(adjX<0.1f){
        //         adjX = 0;
        //     }
        //     float pY = 200/1280f*Screen.height-Mathf.Abs(adjX)*200;
        //     if(Screen.width==800&&Screen.height==1000){
        //         pY+=80;
        //     }
        //     Vector3 pos = new Vector3(pX,pY,0);
        //     Debug.Log("rrrbbb222+"+pos+" "+ touch+" ");
        //     // 70*70;
        //     float x2 = Mathf.Pow(touch.x-Mathf.Abs(pos.x+deltaX),2);
        //     float y2 = Mathf.Pow(touch.y-Mathf.Abs(pos.y+deltaY),2);
        //     float range = 65/720f*Screen.width;
        //     float radis = Mathf.Pow(range,2);
        //     if(x2+y2<=radis) 
        //     {
        //         if(skillBtnBceEnable){
        //             skillBtnBce1?.Invoke();
        //         }
        //         return true;
        //     }
        //     return false;
        // }

        // void OnTouchClick(Vector2 touch)
        // {
        //     if (!_touchPaused)
        //     {
        //         showExplain(false);
        //         if (ScreenPosToGroundPos(touch, out var groundPos))
        //         {
        //             if(isInSkillBtnRange(touch)){
        //                 return;
        //             }

        //             if(curSelectLevel==1){
        //                 if(currentWave==0){
        //                     Debug.Log("0000");
        //                     if(isGuiCanClick(touch)){
        //                         // Debug.Log("1111");
        //                         if (hero) hero.Dash(groundPos);
        //                     }
        //                 }
        //                 else if(currentWave==2){
        //                     if(isGuiCanClick(touch)){
        //                         if (hero) hero.Dash(groundPos);
        //                         // Debug.Log("2222");
        //                     }
        //                 }
        //                 else{
        //                     // Debug.Log("3333");
        //                     if (hero) hero.Dash(groundPos);
        //                 }
        //             }
        //             else{
        //                 if (hero) hero.Dash(groundPos);
        //             }
        //             // Debug.Log("4444");
        //         }
        //         else
        //         {
        //             Debug.LogError("屏幕坐标转换出错 " + touch);
        //         }
        //     }
        // }




        // void OnHeroDied(LivingUnit unit)
        // {
        //     onHeroDied?.Invoke();
        // }

        // void OnFountainDied(LivingUnit unit)
        // {
        //     onFountainDied?.Invoke();
        // }

        // // TODO: 监听用于计分
        // void OnMonsterDied(LivingUnit unit)
        // {
        //     var startPos = unit.cachedTransform.position;

        //     GameEntry.Resource.LoadAsset(AssetUtility.GetFlySpiritPrefab("FlySpirit"), Constant.AssetPriority.FlySpiritPrefab, new LoadAssetCallbacks(
        //         (assetName, asset, duration, userData) =>
        //         {
        //             if (hero) // 
        //             {
        //                 var go = GameObject.Instantiate(asset as UnityEngine.Object) as GameObject;
        //                 var flySpirit = go.GetComponent<FlySpirit>();
        //                 flySpirit.Fly(startPos, hero.cachedTransform,delegate(){
        //                     hero.playAccEngyEff();
        //                 });
        //             }

        //             Log.Info("Load FlySpirit '{0}' OK.", assetName);
        //         },

        //         (assetName, status, errorMessage, userData) =>
        //         {
        //             Log.Error("Can not load FlySpirit '{0}' from '{1}' with error message '{2}'.", assetName, assetName, errorMessage);
        //         }));

        //     if (monstersAllDied)
        //     {
        //         onMonstersAllDied?.Invoke();
        //     }
        //     else
        //     {
        //         if (_monsters.Where(m => !m.configData.CantTouch).All(m => m.dead))
        //         {
        //             var first = _monsters.FirstOrDefault(m => m.configData.CantTouch && !m.dead);
        //             if (first != null) first.DrainHP();
        //         }
        //     }
        // }

        // // TODO: 监听用于计分
        // void OnMonsterHPChanged(LivingUnit unit, float oldValue, float newValue)
        // {

        // }

        // public int hitType=1;
        // //type 1 普攻 2 暴击 3 秒杀
        // void OnHeroHitMonster(Monster monster, Vector3 hitPoint,int type)
        // {
        //     // TODO: 如果以后不通用的话在各自逻辑调用
        //     GameObject go;
        //     if(hitType==1){
        //         go = Instantiate(monsterHitEffectPrefab);
        //     }
        //     else if(hitType==2){
        //         go = Instantiate(monsterCritEffPrefab);
        //     }
        //     else{
        //         go = Instantiate(monsterDtKillEffPrefab);
        //     }

        //     go.transform.position = monster.cachedTransform.position;
        //     go.transform.SetPositionY(1);
        //     if(monster.configData.Id<13||monster.configData.Id>15){
        //         ShakeCamera();
        //     }
        //     // RequestPause(.05f);
        // }

        // private bool ScreenPosToGroundPos(Vector2 screenPos, out Vector3 groundPos)
        // {
        //     var ray = gameCamera.ScreenPointToRay(screenPos);
        //     var raycast = _groundPlane.Raycast(ray, out var enter);
        //     groundPos = ray.GetPoint(enter);
        //     return raycast;
        // }

        // private void ApplySceneBuffToHeroBaseAttribute(ref float attribute, SceneBuffType sceneBuffType)
        // {
        //     if (_presetSceneBuffs.TryGetValue(sceneBuffType, out var param))
        //         attribute += attribute * param[0];
        // }

        
        // private void ApplySceneBuffToHero(Hero hero)
        // {
        //     float[] param;
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.KillDirectly, out param))
        //     {
        //         hero.killDirectlyAllow = true;
        //         hero.killDirectlyProbability = param[0];
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.SuckBlood, out param))
        //     {
        //         hero.suckBlood = true;
        //         hero.suckBloodValue = param[0];
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Fury, out param))
        //     {
        //         hero.furyAllow = true;
        //         hero.furyThreshold = param[0];
        //         hero.furyValue = param[1];
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.HardShell, out param))
        //     {
        //         hero.beInvincibleWhenHurt = true;
        //         hero.invincibleTimeWhenHurt = param[0];
        //         hero.hurtInvCd = param[1];
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.BeatBack, out param))
        //     {
        //         hero.attackFollowHP = true;
        //         hero.attackFollowHPLowerThreshold = param[0];
        //         hero.attackFollowHPUpperThreshold = param[1];
        //         hero.attackFollowHPFactor = param[2];
        //     }

        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Recover, out param))
        //     {
        //         hero.buffManager.AddBuff(BuffType.Heal, Mathf.Infinity, new[] { param[0], param[1] });
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Invincible, out param))
        //     {
        //         hero.buffManager.AddBuff(BuffType.InvincibleInterval, Mathf.Infinity, new[] { param[0], param[1] });
        //     }

        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Shield, out param))
        //     {
        //         hero.AddShield(hero.HPMax*param[0]);
        //     }

        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.LongWeapon, out param))
        //     {
        //         hero.ExtendAttackRange(param[0]);
        //     }
        // }

        // public float[] getBuffVal(SceneBuffType type){
        //     float[] param = new float[]{0};
        //     _presetSceneBuffs.TryGetValue(type, out param);
        //     return param;
        // }

        // private void ApplySceneBuffToMonster(Monster monster)
        // {
        //     float[] param;
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Explode, out param))
        //     {
        //         monster.onUnitDied += _monsterDieActionExplode ?? (_monsterDieActionExplode = MonsterDieActionExplode.GetAction(param));
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Freeze, out param))
        //     {
        //         monster.onUnitDied += _monsterDieActionFreeze ?? (_monsterDieActionFreeze = MonsterDieActionFreeze.GetAction(param));
        //     }
        //     if (_presetSceneBuffs.TryGetValue(SceneBuffType.Thunder, out param))
        //     {
        //         monster.onUnitDied += _monsterDieActionThunder ?? (_monsterDieActionThunder = MonsterDieActionThunder.GetAction(param));
        //     }
        // }

        // public class StartGameParam
        // {
        //     public int levelVal;
        //     public int heroId;
        //     public int heroLevel;
        //     public int weaponId;
        //     public int weaponLevel;
        //     public int levelId;
        //     public Dictionary<SceneBuffType, float[]> presetSceneBuffs;
        //     public List<HeroSkillType> presetHeroSkills;
        // }
    }
}