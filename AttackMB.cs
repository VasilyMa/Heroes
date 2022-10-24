using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
namespace Client
{
    public class AttackMB : MonoBehaviour
    {
        public enum UnitTypes { Range, Melee }
        public UnitTypes UnitType;

        [SerializeField] private GameObject _rayParticle;
        private AudioSource _audioSource;
        [SerializeField] private Transform _firePoint;
        private GameState _state;
        private EcsWorld _world;
        private EcsPool<RangeAttackEvent> _rangePool;
        private EcsPool<MeleeAttackEvent> _meleePool;

        private GameObject _target;
        private int _entity;
        private int _holderEntity;

        private Transform _transform;
        private float _damage;
        private HealthBarMB _healthBar;
        public void Init(EcsWorld world, GameState state)
        {
            _rangePool = world.GetPool<RangeAttackEvent>();
            _meleePool = world.GetPool<MeleeAttackEvent>();
            _world = world;
            _state = state;
            
        }
        public void SetRayParticle(GameObject rayParticle)
        {
            _rayParticle = rayParticle;
        }
        public GameObject GetRayParticle()
        {
            return _rayParticle;
        }
        public void SetAudioSource()
        {
            _audioSource = _rayParticle.GetComponent<AudioSource>();
        }
        public void InitArrow(GameObject target, int entity, Transform transform, float damage, int holderEntity)
        {
            _target = target;
            _entity = entity;
            _transform = transform;
            _damage = damage;
            _healthBar = target.GetComponent<HealthBarMB>();
            _holderEntity = holderEntity;
        }
        public void InitMeleeStrike(int entity, float damage, GameObject target, int holderEntity)
        {
            _entity = entity;
            _damage = damage;
            _target = target;
            _healthBar = target.GetComponent<HealthBarMB>();
            _holderEntity = holderEntity;
        }
        public void InitRayStrike(int entity, float damage, GameObject target, int holderEntity)
        {
            _entity = entity;
            _damage = damage;
            _target = target;
            _healthBar = target.GetComponent<HealthBarMB>();
            _holderEntity = holderEntity;
        }
        public void RangeDamage()
        {
            if (_target != null)
            {
                ref var rangeComp = ref _rangePool.Add(_world.NewEntity());
                rangeComp.Damage = _damage;
                rangeComp.Entity = _entity;
                rangeComp.Transform = _transform;
                rangeComp.TransformFirePoint = _firePoint;
                rangeComp.Target = _target;
                rangeComp.HolderEntity = _holderEntity;
            }
        }
        public void MeleeDamage()
        {
            if (_target != null)
            {
                ref var meleeComp = ref _meleePool.Add(_world.NewEntity());
                meleeComp.TargetEntity = _entity;
                meleeComp.Damage = _damage;
                meleeComp.Target = _target.transform.position;
                meleeComp.HolderEntity = _holderEntity;
            }
        }
        public void RayDamage()
        {
            if (_target != null)
            {
                ref var meleeComp = ref _meleePool.Add(_world.NewEntity());
                meleeComp.TargetEntity = _entity;
                meleeComp.Damage = _damage;
                meleeComp.Target = _target.transform.position;
                meleeComp.HolderEntity = _holderEntity;
                EnableRay();
            }
        }

        public void EnableRay()
        {
            if(_state.Saves.Sounds == 0)
            _audioSource.volume = 0;
            _rayParticle.SetActive(true);
        }
        public void DisableRay()
        {
            _rayParticle.SetActive(false);
        }
    }
}
