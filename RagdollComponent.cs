using UnityEngine;
using System.Collections.Generic;

namespace Client
{
    struct RagdollComponent
    {
        public List<Collider> AllColliders;
        public List<Rigidbody> AllRigidbodys;
    }
}