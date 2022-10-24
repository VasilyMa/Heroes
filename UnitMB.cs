using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client
{
    public class UnitMB : MonoBehaviour
    {
        public int Entity;
        public Transform _parent;
        public Transform _transform;
        public string ID;
        public void Init(int entity, Transform parent, Transform transform, string id)
        {
            Entity = entity;
            _parent = parent;
            _transform = transform;
            ID = id;
        }
        public void SetNewParent(Transform newParent)
        {
            _parent = newParent;
        }
        public void TeleportToParent()
        {
            _transform.SetParent(_parent);
            _transform.localPosition = new Vector3(0,2.2f,0);
        }
        public void ParentNull()
        {
            _transform.SetParent(null);
        }

        
    }
}
