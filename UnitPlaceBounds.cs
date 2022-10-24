using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public struct UnitPlaceBounds
    {
        public Vector3 p1;
        public Vector3 p2;
        public bool Contains(Vector3 p)
        {
            return (p.x >= p1.x && p.x <= p2.x) && (p.z >= p1.z && p.z <= p2.z);
        }
    }
}
