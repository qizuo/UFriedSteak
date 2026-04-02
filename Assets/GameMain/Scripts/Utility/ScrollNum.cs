using System.Runtime.CompilerServices;
using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollNum : MonoBehaviour {

    public int _start;
    public int _end;
    public int _current;
    public Text _text;
    public bool _isRunning = false;
    public bool _isScale = false;
    public bool _mulSym = false;
    public int scSpd = 41;
    //mulSym 加号
    public void startScroll(Text text,int start,int end,bool isScale=false,bool mulSym=false){
        _isRunning = true;
        _mulSym = mulSym;
        _start = start;
        _end = end;
        _text = text;
        _current = start;
       _text.text = _current+"";
    //    Debug.Log(start+" "+end+" "+_current);
       _isScale = isScale;
    }
    
    public void updateDelta() {
        // Debug.Log(_isRunning);
        if(_isRunning){
            // Debug.Log(_current);
            _current = _current+scSpd;
            string mul = "";
            if(_mulSym){
                mul = "+";
            }
            if(_current>_end){
                _isRunning = false;
                _text.text =mul+_end;
                _text.transform.localScale = new Vector3(1,1,1);
            }
            else{
                _text.text =mul+_current;
                if(_isScale){
                    _text.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
                }
            }
        }
    }    
    public void setNorScale(){
        _text.transform.localScale = new Vector3(1,1,1);
    }
}
