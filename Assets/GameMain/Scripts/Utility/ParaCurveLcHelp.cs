using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Battle
{
    public class ParaCurveLcHelp
    {
        public float verticalSpeed = 0.5f;

        private Vector3 _fromPos;
        private Vector3 _direction;
        private float _speed;
        private float _attack;

        private Vector3 _verticalOffset;

        private float _elapsedTime;
        private float _totalTime;

        public Transform _trans;

        private TweenCallback _cb;

        public int callTm = 0;


        public ParaCurveLcHelp(){
        }

        public void SetParams(Transform trans, Vector3 toPos, float flyTm,TweenCallback cb=null)
        {
            _trans = trans;
            Vector3 fromPos = trans.localPosition;
            callTm=0;
            _cb = cb;
            _fromPos = fromPos;
            _direction = (toPos - fromPos).normalized;
            _speed = (toPos - fromPos).magnitude /flyTm;
            _trans.localPosition = fromPos;
            // _trans.forward = _direction;

            _elapsedTime = 0f;
            _totalTime = flyTm;
            _verticalOffset = Vector3.up * verticalSpeed * _totalTime * .5f;

        }

        public void Update()
        {
            if (_elapsedTime<=_totalTime)
            {
                // Debug.Log("haah uuu");
                _elapsedTime += Time.deltaTime;

                var x = ((_elapsedTime / _totalTime) - .5f) * 2;
                var y = -15*Mathf.Pow(x, 2) + 15;

                // 一个抛物线
                var offset = _direction * _speed * _elapsedTime + _verticalOffset * y;

                var smooth = Mathf.Min(1f, Time.deltaTime / 0.15f);
                // _trans.LookAtLerp(_fromPos + offset, smooth);
                _trans.localPosition = _fromPos + offset;
                // Debug.Log("rrr.."+_trans.localPosition);
            }
            else{
                if(callTm==0){
                    if(_cb!=null){
                        _cb();
                    }
                    callTm+=1;
                }
            }
        }
    }
}