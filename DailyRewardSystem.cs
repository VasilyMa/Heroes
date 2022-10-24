using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace Client 
{
    sealed class DailyRewardSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<DailyRewardComponent>> _filter = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;
        readonly EcsPoolInject<FillingUnitEvent> _fillingPool = default;

        public void Run (EcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var dailyComp = ref _filter.Pools.Inc1.Get(entity);
                ref var interfaceComp = ref _interfacePool.Value.Get(_state.Value.EntityInterface);
                if (dailyComp.isHero)
                {
                    //to do
                    var emptyIndex = _state.Value.GetEmptyPlayerUnitsIndex();
                    if (emptyIndex > -1)
                    {
                        _state.Value.PlayerUnits[emptyIndex] = dailyComp.levelHero;

                        ref var fillingComp = ref _fillingPool.Value.Add(_world.Value.NewEntity());
                        fillingComp.Entity = _state.Value.UnitEntityes[emptyIndex];
                        fillingComp.Index = emptyIndex;
                        fillingComp.Exists = false;

                    }
                    else
                    {
                        var index = _state.Value.GetLowLevelByType(dailyComp.typeHero);
                        _state.Value.PlayerUnits[index] = dailyComp.levelHero;

                        ref var fillingComp = ref _fillingPool.Value.Add(_world.Value.NewEntity());
                        fillingComp.Entity = _state.Value.UnitEntityes[index];
                        fillingComp.Index = index;
                        fillingComp.Exists = true;
                        //_interfacePool.Value.Get(_state.Value.EntityInterface).CanvasController.CheckActiveColor();
                    }
                    _state.Value.Saves.SavePlayerUnits(_state.Value.PlayerUnits);
                }
                else 
                {
                    _state.Value.Coins += (ulong)dailyComp.money;
                    _state.Value.Saves.SaveCoin(_state.Value.Coins);
                    interfaceComp.CanvasController.CoinsChanger(_state.Value.Saves.AllCoin);
                    interfaceComp.CanvasController.PlayParticleCoins();
                    interfaceComp.CanvasController.CheckActiveColor();
                }
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}