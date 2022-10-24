using System.Xml.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitSpawnInFightSystem : IEcsRunSystem {        
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<UnitsSpawnEventComponent>> _filter = default;
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<FillingUnitEvent> _fillingPool = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;


        public void Run (EcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                
                //var unit = GameObject.Instantiate(state.UnitStorage.GetGameObjectByID(eventBut.TypeUnit), eventBut.pos, Quaternion.identity);
                int emptyIndex = _state.Value.GetEmptyPlayerUnitsIndex();
                if(emptyIndex == -1)
                {
                    //_interfacePool.Value.Get(_state.Value.EntityInterface).CanvasController.CheckActiveColor();
                    Debug.Log("MEST NET");
                    return;
                }
                else
                {
                    ref var eventBut = ref _filter.Pools.Inc1.Get(entity);
                    if (!eventBut.IsReward)
                    {
                        _state.Value.Coins -= 300;
                        ref var interComp = ref _interfacePool.Value.Get(_state.Value.EntityInterface);
                        interComp.CanvasController.CoinsChanger(_state.Value.Coins);
                        //interComp.CanvasController.CheckActiveColor();
                    }
                    Debug.Log("MESTO EST " + emptyIndex);
                    
                    //заполняем новой значение в плеерЮнитс
                    _state.Value.PlayerUnits[emptyIndex] = eventBut.TypeUnit;
                    //записываю купленных юнитов в бою в дубликат PlayerUnits
                    _state.Value.AddToPoolPlayerUnitsID(eventBut.TypeUnit);
                    ref var fillingComp = ref _fillingPool.Value.Add(_world.Value.NewEntity());
                    fillingComp.Entity = _state.Value.UnitEntityes[emptyIndex];
                    fillingComp.Index = emptyIndex;
                    fillingComp.Exists = false;
                    fillingComp.Fight = true;
                    _interfacePool.Value.Get(_state.Value.EntityInterface).CanvasController.CheckActiveColor();
                }
            }
        }
    }
}