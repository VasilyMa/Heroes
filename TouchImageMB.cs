using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using UnityEngine.UI;
namespace Client
{
    public class TouchImageMB : MonoBehaviour
    {
        private GameState _state;
        private Transform _transform;
        private Image _image;
        private Transform _textTransform;
        public Vector3[] _points;
        public int[] IndexPoints;
        private bool _moveTo = false;
        private bool _move = false;
        private float _t = 0f;
        private float _tScale = 0f;
        private float _textT = 0f;
        private LineRenderControll _lineRenderControll;
        private bool _draw = false;
        private bool _blink = false;
        private bool _upScale = true;
        private bool _textUpScale = true;

        public void Init(GameState state, LineRenderControll lines, Transform textTransform)
        {
            _state = state;
            _transform = gameObject.GetComponent<Transform>();
            _image = gameObject.GetComponent<Image>();
            _lineRenderControll = lines;
            _lineRenderControll.Init(state);
            _textTransform = textTransform;
        }
        private void Update()
        {
            if (_move) PointByPointMovement();
            if (_draw) Draw();
            if (_blink) Blink();
            BlinkText();
        }
        
        public void InitPoins(int pointCount, Vector3[] points, int[] indexPoints)
        {
            _points = new Vector3[pointCount];
            IndexPoints = new int[pointCount];
            for(int i = 0;i<_points.Length;i++)
            {
                _points[i] = points[i];
                IndexPoints[i] = indexPoints[i];
            }
        }
        public void StayAtPoint(Vector3 position)
        {
            _transform.position = position;
        }
        public void StartMove()
        {
            _move = true;
        }
        public void StopMove()
        {
            _move = false;
            _draw = false;
            _lineRenderControll.DeleteLine();
            _t = 0;
        }
        private void PointByPointMovement()
        {
            if (_moveTo)
            {
                _draw = true;
                if(_t==0)
                {
                    _lineRenderControll.AddNewLine(_points[0]);
                }
                
                _t += Time.deltaTime;
                if (_t >= 1.5f)
                {
                    _moveTo = false;
                    DeactivateImage();
                    _lineRenderControll.DeleteLine();
                    _t = 1;
                }
            }
            else
            {
                _draw = false;
                _t -= Time.deltaTime * 2f;
                if (_t <= 0)
                {
                    _moveTo = true;
                    ActivateImage();
                    _t = 0;
                }
            }
            float x = Mathf.Lerp(_points[0].x, _points[1].x, _t);
            float y = Mathf.Lerp(_points[0].y, _points[1].y, _t);
            _transform.position = new Vector3(x, y, 0);
        }
        public void ActivateImage()
        {
            _image.enabled = true;
        }
        public void DeactivateImage()
        {
            _image.enabled = false;
        }
        private void Draw()
        {
            _lineRenderControll.DrawLine(_transform.position);
        }
        private void Blink()
        {
            if (_upScale)
            {
                _tScale += Time.unscaledDeltaTime * 2f;
                if (_tScale >= 1)
                {
                    _upScale = false;
                }
            }
            else
            {
                _tScale -= Time.unscaledDeltaTime * 2f;
                if (_tScale <= 0)
                {
                    _upScale = true;
                }
            }
            float scale = Mathf.Lerp(1f, 0.8f, _tScale);

            _transform.localScale = new Vector3(scale, scale, scale);
        }
        private void BlinkText()
        {
            
            if (_textUpScale)
            {
                _textT += Time.unscaledDeltaTime * 2f;
                if (_textT >= 1)
                {
                    _textUpScale = false;
                }
            }
            else
            {
                _textT -= Time.unscaledDeltaTime * 2f;
                if (_textT <= 0)
                {
                    _textUpScale = true;
                }
            }
            float scale = Mathf.Lerp(1f, 0.8f, _textT);

            _textTransform.localScale = new Vector3(scale, scale, scale);
        }
        public void StartBlink()
        {
            _blink = true;
        }
        public void StopBlink()
        {
            _blink = false;
            _transform.localScale = new Vector3(1, 1, 1);
            _tScale = 0;
        }
        

    }
}
