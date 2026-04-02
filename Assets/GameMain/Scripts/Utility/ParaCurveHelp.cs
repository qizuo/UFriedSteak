using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Battle
{
    public class ParaCurveHelp
    {
        public float verticalSpeed = 15f;

        private Vector3 _fromPos;
        private Vector3 _direction;
        private float _speed;
        private float _attack;

        private Vector3 _verticalOffset;

        private float _elapsedTime;
        private float _totalTime;

        private Transform _trans;

        private SceneMain _sceneMain;
        private TweenCallback _cb;

        public int callTm = 0;

        public ParaCurveHelp(Transform transform){
            _trans = transform;
        }

        public void SetParams(Vector3 fromPos, Vector3 toPos, float flyTm,TweenCallback cb=null)
        {
            callTm=0;
            _cb = cb;
            _fromPos = fromPos;
            _direction = (toPos - fromPos).normalized;
            _speed = (toPos - fromPos).magnitude /flyTm;
            _trans.position = fromPos;
            _trans.forward = _direction;

            _elapsedTime = 0f;
            _totalTime = flyTm;

            _verticalOffset = Vector3.up * verticalSpeed * _totalTime * .5f;

        }

        public void Update()
        {
            // Debug.Log("rr.1."+_trans.position);
            // Debug.Log("rr.2."+_elapsedTime);
            // Debug.Log("rr.3."+_totalTime);
            if (_elapsedTime<=_totalTime)
            {
                _elapsedTime += Time.deltaTime;

                var x = ((_elapsedTime / _totalTime) - .5f) * 2;
                var y = -Mathf.Pow(x, 2) + 1;

                // 一个抛物线
                var offset = _direction * _speed * _elapsedTime + _verticalOffset * y;

                var smooth = Mathf.Min(1f, Time.deltaTime / 0.15f);
                // _trans.LookAtLerp(_fromPos + offset, smooth);
                _trans.position = _fromPos + offset;
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