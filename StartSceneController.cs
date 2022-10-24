using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Client;
using UnityEngine.UI;
public class StartSceneController : MonoBehaviour
{
    private Saves _saves = new Saves();
    private float _currentTime = 0;
    private float _time;
    [SerializeField] private Text points;
    [SerializeField] private float _checkTime;
    [SerializeField] private float _minLoadTime;
    void Update()
    {
        ChangePoints();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(_saves.LoadSceneNumber());
    }
    private void ChangePoints()
    {
        if (_currentTime == 0 && points.text == "")
        {
            points.text = ".";
        }
        else if (_currentTime >= 1.5f && points.text == ".")
        {
            points.text = "..";
        }
        else if (_currentTime >= 2.5f && points.text == "..")
        {
            points.text = "...";
        }
        else if (_currentTime >= 3.5f && points.text == "...")
        {
            points.text = "";
        }

        _currentTime += Time.deltaTime * 3f;
        _time += Time.deltaTime;

        if (_currentTime >= 4.5f)
        {
            _currentTime = 0;
        }
        if(_time > _checkTime) LoadGame();

        // if (_time < _minLoadTime) return;

        // _checkTime += Time.deltaTime;
        // if (_checkTime <= 0 ) LoadGame(); //|| (/*_state.ADS.IsRewardedReady() && */Application.internetReachability != NetworkReachability.NotReachable)
    }
}
