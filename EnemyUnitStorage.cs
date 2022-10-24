using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

[CreateAssetMenu(fileName = "EnemyUnitStorage", menuName = "Configs/EnemyUnitStorage", order = 0)]
public class EnemyUnitStorage : ScriptableObject
{
    public GameObject[] MeleeUnitPrefabs;
    public GameObject[] RangeUnitPrefabs;
    public GameObject[] ArrowPoolPrefabs;
    public GameObject[] BulletPoolPrefab;
    public Dictionary<string, Unit> Units;

    public void Init()
    {
        Units = new Dictionary<string, Unit>
        {
            ["EnemyMeleeUnit"] = new Unit
            {
                Damage = 8,
                Health = 50,
                DistanceOfFight = 5,
                Speed = 10,
                Cooldown = 1.5f
            },

            ["EnemyRangeUnit"] = new Unit
            {
                Damage = 0.9f, //6.33 damage/per sec
                Health = 35,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 1.5f
            },
        };
    }
    public int GetSpeedByID(string id)
    {
        return Units[id].Speed;
    }
    public int GetDistanceByID(string id)
    {
        return Units[id].DistanceOfFight;
    }
    public float GetCoolDownByID(string id)
    {
        return Units[id].Cooldown;
    }
    public float GetDamageByID(string id)
    {
        return Units[id].Damage;
    }
    public int GetHealthByID(string id)
    {
        return Units[id].Health;
    }
    public GameObject GetBulletByID(int id)
    {
        return BulletPoolPrefab[id];
    }
}
