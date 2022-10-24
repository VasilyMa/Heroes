using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class InitTimer : IEcsInitSystem {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;
        public void Init (EcsSystems systems) {
            if (_state.Value.Saves.timerInter != 0)
            {
                ref var countdown = ref _world.Value.GetPool<CountdownComponent>().Add(_world.Value.NewEntity());
                countdown.currentAmount = _state.Value.Saves.timerInter;
            }
        }
    }
}