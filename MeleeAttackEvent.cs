using UnityEngine;
namespace Client {
    struct MeleeAttackEvent {
        public int TargetEntity;
        public float Damage;
        public Vector3 Target;
        public int HolderEntity;
    }
}