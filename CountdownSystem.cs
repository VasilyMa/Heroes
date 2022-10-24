using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class CountdownSystem : IEcsRunSystem {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<CountdownComponent>> _filterCountdown = default; 
        public void Run (EcsSystems systems) {
            foreach (var entity in _filterCountdown.Value)
            {
                ref var countdownComp = ref _filterCountdown.Pools.Inc1.Get(entity);
                if (countdownComp.currentAmount > 0)
                {
                    countdownComp.currentAmount -= Time.deltaTime;
                }
                else if (countdownComp.currentAmount <= 0)
                {
                    countdownComp.currentAmount = 0;
                    _state.Value.Saves.SaveTimer(countdownComp.currentAmount);
                    _filterCountdown.Pools.Inc1.Del(entity);
                    return;
                }
                _state.Value.Saves.SaveTimer(countdownComp.currentAmount);
            }
        }
    }
}