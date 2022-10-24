using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EffectStorage", menuName = "Configs/EffectsStorage", order = 0)]
public class EffectsStorage : ScriptableObject
{
    public GameObject[] EffectsPrefabs;
    public Dictionary<string, Effect> Effects;

    public void Init()
    {
        Effects = new Dictionary<string, Effect>
        {
            ["red"] = new Effect
            {
                EffectPrefab = EffectsPrefabs[0],
            },
            ["blue"] = new Effect
            {
                EffectPrefab= EffectsPrefabs[1],
            },
        };
    }
    public GameObject GetPrefabByID(string id)
    {
        return Effects[id].EffectPrefab;
    }
}
