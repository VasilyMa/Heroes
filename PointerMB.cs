using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
namespace Client
{
    public class PointerMB : MonoBehaviour
    {
        private GameState _state;
        private EcsWorld _world;
        private EcsPool<InterfaceComponent> _interfacePool;
        private EcsPool<LevelRawardComponent> _rewardPool;
        private CanvasController CanvasController;
        private ulong _addedCoins;
        private bool _test = false;

        public void Init(EcsWorld world, GameState state)
        {
            _world = world;
            _interfacePool = world.GetPool<InterfaceComponent>();
            _rewardPool = world.GetPool<LevelRawardComponent>();
            _state = state;
        }
        public void OnTriggerEnter(Collider other)
        {
            ref var interfaceComp = ref _interfacePool.Get(_state.EntityInterface);
            ref var rewardComp = ref _rewardPool.Get(_state.EntityLevelReward);
            CanvasController = interfaceComp.CanvasController;
            Debug.Log($"collision is {other.name}");
            switch (other.name)
            {
                case "ColliderA":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 0.25f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderB":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 0.4f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderC":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 0.75f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderD":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 0.5f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderE":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 1.25f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderF":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 1.75f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderG":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 1.5f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                case "ColliderH":
                    _addedCoins += ((ulong)(rewardComp.Value + (rewardComp.Value * 2f)));
                    interfaceComp.CanvasController.RewardText(_addedCoins);
                    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    break;
                //case "ColliderI":
                //    _addedCoins += (rewardComp.Value + (rewardComp.Value / 10));
                //    interfaceComp.CanvasController.RewardText(_addedCoins);
                //    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                //    break;
                //case "ColliderJ":
                //    _addedCoins += (rewardComp.Value + (rewardComp.Value / 11));
                //    interfaceComp.CanvasController.RewardText(_addedCoins);
                //    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                //    break;
                //case "ColliderK":
                //    _addedCoins += (rewardComp.Value + (rewardComp.Value / 12));;
                //    interfaceComp.CanvasController.RewardText(_addedCoins);
                //    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                //    break;
                //case "ColliderL":
                //    _addedCoins += (rewardComp.Value + (rewardComp.Value / 1));
                //    interfaceComp.CanvasController.RewardText(_addedCoins);
                //    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                //    break;
                //default:
                //    _addedCoins += 0;
                //    interfaceComp.CanvasController.GetSpin().transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                //    break;
            }
            if (!_test)
            {
                _test = true;
                interfaceComp.CanvasController.IsRewardMultiply(_addedCoins);
                gameObject.SetActive(false);
            }

        }
    }
}