using UnityEngine;

namespace Client
{
    struct ViewComponent
    {
        public GameObject GameObject;
        public Transform Transform;
        public Rigidbody Rigidbody;
        public Collider Collider;
        public Animator Animator;
        public AttackMB AttackMB;
        public ParticleSystem[] BuffParticles;
        public Transform ParticleHolder;
        public HealthBarMB HealthBarMB;
        public AudioSource[] AudioSource;
        public GameObject ExplosionParticleSystem;
        public GameObject[] RangeUltimateParticleSystem;
        public ParticleSystem HitParticle;
        public ParticleSystem RayParticle;
        public ParticleSystem[] RayParticleChildren;
    }
}