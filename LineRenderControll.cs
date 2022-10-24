using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Client;

public class LineRenderControll : MonoBehaviour
{
    private int _lineVertex = -1;
    [SerializeField] private GameObject _linePrefab;
    private RectTransform[] _linesRectTransform;
    private Image[] _linesImages;
    private float _delta = 0;
    public int LastNumber = -1;
    private GameState _gameState;
    private Canvas _canvas;

    public void Init(GameState state)
    {
        _canvas = gameObject.GetComponent<Canvas>();
        _gameState = state;
        _linesRectTransform = new RectTransform[1];
        _linesImages = new Image[1];
        _delta = _canvas.scaleFactor;
        var position = new Vector3(0, 0, 0);


        for (int i = 0; i < _linesRectTransform.Length; i++)
        {
            _linesRectTransform[i] = Instantiate(_linePrefab, position,
            new Quaternion(0, 0, 0, 0),
            gameObject.transform).GetComponent<RectTransform>();

            _linesImages[i] = _linesRectTransform[i].gameObject.GetComponent<Image>();
        }
    }
    

    public void DeleteLine()
    {
        _lineVertex = -1;
        LastNumber = -1;
        for (int i = 0; i < _linesRectTransform.Length; i++)
        {
            _linesRectTransform[i].sizeDelta = new Vector2(0, 60);
            _linesRectTransform[i].gameObject.SetActive(false);
        }
    }
    public void DrawLine(Vector2 eventPosition)
    {
        if (_lineVertex < _linesRectTransform.Length)
        {
            if (_lineVertex != -1 && _linesRectTransform[_lineVertex] != null)
            {
                float a = eventPosition.y - (_linesRectTransform[_lineVertex].transform.position.y);
                float b = eventPosition.x - (_linesRectTransform[_lineVertex].transform.position.x);
                float c = Mathf.Sqrt(a * a + b * b);
                float angle = 0;
                if (b != 0)
                {
                    angle = Mathf.Atan(a / b) * Mathf.Rad2Deg;
                }
                if (b < 0)
                {
                    angle -= 180;
                }
                _linesRectTransform[_lineVertex].sizeDelta = new Vector2(c / _delta, 60);
                _linesRectTransform[_lineVertex].eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }
    public void AddNewLine(Vector2 position)
    {
        _lineVertex++;
        if (_lineVertex < _linesRectTransform.Length)
        {
            _linesRectTransform[_lineVertex].transform.position = position;
            _linesRectTransform[_lineVertex].gameObject.SetActive(true);

        }
    }
    public void SetSecondLinePoint(Vector2 position)
    {
        if (_lineVertex != -1 && _lineVertex < _linesRectTransform.Length)
        {
            float a = (position.y) - (_linesRectTransform[_lineVertex].transform.position.y);
            float b = (position.x) - (_linesRectTransform[_lineVertex].transform.position.x);
            float c = Mathf.Sqrt(a * a + b * b);
            float angle = 0;
            if (b != 0)
            {
                angle = Mathf.Atan(a / b) * Mathf.Rad2Deg;
            }
            if (b < 0)
            {
                angle -= 180;
            }
            _linesRectTransform[_lineVertex].sizeDelta = new Vector2(c / _delta, 60);
            _linesRectTransform[_lineVertex].eulerAngles = new Vector3(0, 0, angle);
        }
    }


}
