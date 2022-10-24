using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public class HealthBarMB : MonoBehaviour
    {
        private EcsWorld _world;
        private GameState _state;
        [SerializeField] private Slider _slider;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _image;
        [SerializeField] private float _curHp;
        [SerializeField] private float _maxHP;
        [SerializeField] private GameObject _healthBar;
        //[SerializeField] private GameObject _camera;
        private EcsPool<CameraComponent> _cameraPool = null;
        public void Init(EcsWorld world, GameState state)
        {
            _world = world;
            //_camera = GameObject.FindGameObjectWithTag("MainCamera");
            _cameraPool = _world.GetPool<CameraComponent>();
            _state = state;
        }

        public void SetHealth(float health)
        {
            _slider.value = health;
            _curHp = health;
        }
        public void SetMaxHealth(float health)
        {
            _slider.maxValue = health;
            _slider.value = health;
            _maxHP = health;
            _image.color = _gradient.Evaluate(1f);
        }
        public void UpdateHealthBar(float damage)
        {
            _curHp = damage;
            _slider.value = _curHp;
            _image.color = _gradient.Evaluate(_slider.normalizedValue);
            if (_slider.value <= 0)
                _healthBar.SetActive(false);
        }
        public void UpdateHeal(float heal)
        {
            _curHp = heal;
            _slider.value = _curHp;
            _image.color = _gradient.Evaluate(_slider.normalizedValue);
        }
        void Update()
        {
            CameraFollow();
        }
        public void CameraFollow()
        {
            ref var cameraComp = ref _cameraPool.Get(_state.EntityCamera);
            _healthBar.transform.LookAt(_healthBar.transform.position + cameraComp.CameraTransform.forward);
        }
    }
}
