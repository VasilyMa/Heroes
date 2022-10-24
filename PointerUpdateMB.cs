using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
namespace Client
{
    public class PointerUpdateMB : MonoBehaviour
    {
        private GameState _state;
        private EcsWorld _world;
        private EcsPool<InterfaceComponent> _interfacePool;
        private EcsPool<LevelRawardComponent> _rewardPool;
        private ulong amountReward; 
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
            //Debug.Log($"collision is {other.name}");
            switch (other.name)
            {
                case "ColliderA":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 0.25f))));
                    break;
                case "ColliderB":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 0.4f))));
                    break;
                case "ColliderC":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 0.75f))));
                    break;
                case "ColliderD":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 0.5f))));
                    break;
                case "ColliderE":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 1.25f))));
                    break;
                case "ColliderF":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 1.75f))));
                    break;
                case "ColliderG":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 1.5f))));
                    break;
                case "ColliderH":
                    interfaceComp.CanvasController.RewardText(amountReward = 
                        ((ulong)(_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value * 2f))));
                    break;
                //case "ColliderI":
                //    interfaceComp.CanvasController.RewardText(amountReward = 
                //        (_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value / 10)));
                //    break;
                //case "ColliderJ":
                //    interfaceComp.CanvasController.RewardText(amountReward = 
                //        (_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value / 11)));
                //    break;
                //case "ColliderK":
                //    interfaceComp.CanvasController.RewardText(amountReward = 
                //        (_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value / 12)));
                //    break;
                //case "ColliderL":
                //    interfaceComp.CanvasController.RewardText(amountReward = 
                //        (_rewardPool.Get(_state.EntityLevelReward).Value + (_rewardPool.Get(_state.EntityLevelReward).Value / 1)));
                //    break;
                default:
                    _state.Coins += 0;
                    break;
            }
        }
    }
}