using UnityEngine;
namespace Client {
    struct RangeAttackEvent {
        public GameObject Target;
        public int Entity;
        public Transform Transform;
        public Transform TransformFirePoint;
        public float Damage;
        public HealthBarMB HealthBar;
        public int HolderEntity;
    }
}