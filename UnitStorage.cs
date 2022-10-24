using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitStorage", menuName = "Configs/UnitStorage", order = 0)]
public class UnitStorage : ScriptableObject
{
    public GameObject[] MeleeUnitPrefabs;
    public GameObject[] RangeUnitPrefabs;
    public GameObject[] ArrowPoolPrefabs;
    public Sprite[] MeleeSprites;
    public Sprite[] RangeSprites;
    public Dictionary<string, Unit> Units;

    public void Init()
    {
        Units = new Dictionary<string, Unit>
        {

            ["empty"] = new Unit
            {
                //UnitPrefab = null
            },
            //MELEE
            ["1melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "2melee",
                Name = "Green Bully",
                IsLast = false,
                Level = 1,
                UnitPrefab = MeleeUnitPrefabs[0],
                Damage = 7,
                Health = 50,
                DistanceOfFight = 5,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[0]
            },
            ["2melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "3melee",
                Name = "Incredible Hunk",
                IsLast = false,
                Level = 2,
                UnitPrefab = MeleeUnitPrefabs[1],
                Damage = 14,
                Health = 113,
                DistanceOfFight = 5,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[1]
            },
            ["3melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "4melee",
                Name = "Angry Beast",
                IsLast = false,
                Level = 3,
                UnitPrefab = MeleeUnitPrefabs[2],
                Damage = 22,
                Health = 175,
                DistanceOfFight = 5,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[2]
            },
            ["4melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "5melee",
                Name = "Finite Gloves",
                IsLast = false,
                Level = 4,
                UnitPrefab = MeleeUnitPrefabs[3],
                Damage = 29,
                Health = 238,
                DistanceOfFight = 5,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[3]
            },
            ["5melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "6melee",
                Name = "Animale",
                IsLast = false,
                Level = 5,
                UnitPrefab = MeleeUnitPrefabs[4],
                Damage = 54,
                Health = 300,
                DistanceOfFight = 6,
                Speed = 10,
                Cooldown = 1.4f,
                UnitSprite = MeleeSprites[4]
            },
            ["6melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "7melee",
                Name = "Armored Animale",
                IsLast = false,
                Level = 6,
                UnitPrefab = MeleeUnitPrefabs[5],
                Damage = 65,
                Health = 363,
                DistanceOfFight = 6,
                Speed = 10,
                Cooldown = 1.4f,
                UnitSprite = MeleeSprites[5]
            },
            ["7melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "8melee",
                Name = "Jagg",
                IsLast = false,
                Level = 7,
                UnitPrefab = MeleeUnitPrefabs[6],
                Damage = 47,
                Health = 425,
                DistanceOfFight = 7,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = MeleeSprites[6]
            },
            ["8melee"] = new Unit
            {
                UnitType = UnitType.Melee,
                NextUnitID = "9melee",
                Name = "HunkBuster v.1",
                IsLast = false,
                Level = 8,
                UnitPrefab = MeleeUnitPrefabs[7],
                Damage = 54,
                Health = 488,
                DistanceOfFight = 8,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = MeleeSprites[7]
            },
            ["9melee"] = new Unit // dodelat'
            {
                UnitType = UnitType.Melee,
                NextUnitID = "10melee",
                Name = "HunkBuster v.2",
                IsLast = false,
                Level = 9,
                UnitPrefab = MeleeUnitPrefabs[8],
                Damage = 75,
                Health = 550,
                DistanceOfFight = 8,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[8]
            },
            ["10melee"] = new Unit // dodelat'
            {
                UnitType = UnitType.Melee,
                NextUnitID = string.Empty,
                Name = "Ancient Lord",
                IsLast = true,
                Level = 10,
                UnitPrefab = MeleeUnitPrefabs[9],
                Damage = 91,
                Health = 613,
                DistanceOfFight = 8,
                Speed = 10,
                Cooldown = 1f,
                UnitSprite = MeleeSprites[9]
            },

            //RANGE
            ["1range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "2range",
                Name = "Falcon Bow",
                IsLast = false,
                Level = 1,
                UnitPrefab = RangeUnitPrefabs[0],
                ArrowPrefab = ArrowPoolPrefabs[0],
                Damage = 10,
                Health = 35,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 1.5f,
                UnitSprite = RangeSprites[0]
            },
            ["2range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "3range",
                Name = "Thunderlight",
                IsLast = false,
                Level = 2,
                UnitPrefab = RangeUnitPrefabs[1],
                ArrowPrefab = ArrowPoolPrefabs[1],
                Damage = 46,
                Health = 53,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 3f,
                UnitSprite = RangeSprites[1]
            },
            ["3range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "4range",
                Name = "Holt I",
                IsLast = false,
                Level = 3,
                UnitPrefab = RangeUnitPrefabs[2],
                ArrowPrefab = ArrowPoolPrefabs[2],
                Damage = 50,
                Health = 70,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = RangeSprites[2]
            },
            ["4range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "5range",
                Name = "Shaker",
                IsLast = false,
                Level = 4,
                UnitPrefab = RangeUnitPrefabs[3],
                ArrowPrefab = ArrowPoolPrefabs[3],
                Damage = 103,
                Health = 88,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 3f,
                UnitSprite = RangeSprites[3]
            },
            ["5range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "6range",
                Name = "Laser Eye",
                IsLast = false,
                Level = 5,
                UnitPrefab = RangeUnitPrefabs[4],
                ArrowPrefab = ArrowPoolPrefabs[4],
                Damage = 139,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 3f,
                UnitSprite = RangeSprites[4]
            },
            ["6range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "7range",
                Name = "Holt III",
                IsLast = false,
                Level = 6,
                UnitPrefab = RangeUnitPrefabs[5],
                ArrowPrefab = ArrowPoolPrefabs[5],
                Damage = 113,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = RangeSprites[5]
            },
            ["7range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "8range",
                Name = "Captain Legend",
                IsLast = false,
                Level = 7,
                UnitPrefab = RangeUnitPrefabs[6],
                ArrowPrefab = ArrowPoolPrefabs[6],
                Damage = 107,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 1.5f,
                UnitSprite = RangeSprites[6]
            },
            ["8range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "9range",
                Name = "Mr. York",
                IsLast = false,
                Level = 8,
                UnitPrefab = RangeUnitPrefabs[7],
                ArrowPrefab = ArrowPoolPrefabs[7],
                Damage = 164,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = RangeSprites[7]
            },
            ["9range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = "10range",
                Name = "Past Master",
                IsLast = false,
                Level = 9,
                UnitPrefab = RangeUnitPrefabs[8],
                ArrowPrefab = ArrowPoolPrefabs[8],
                Damage = 184,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 2f,
                UnitSprite = RangeSprites[8]
            },
            ["10range"] = new Unit
            {
                UnitType = UnitType.Range,
                NextUnitID = string.Empty,
                Name = "Doom",
                IsLast = true,
                Level = 10,
                UnitPrefab = RangeUnitPrefabs[9],
                ArrowPrefab = ArrowPoolPrefabs[9],
                Damage = 168,
                Health = 105,
                DistanceOfFight = 50,
                Speed = 10,
                Cooldown = 1.5f,
                UnitSprite = RangeSprites[9]
            },
        };
    }
    public Sprite GetSpriteByID(string id)
    {
        return Units[id].UnitSprite;
    }
    public string GetNameByID(string id)
    {
        return Units[id].Name;
    }
    public int GetLevelByID(string id)
    {
        return Units[id].Level;
    }
    public string GetUnitTypeByID(string id)
    {
        return Units[id].UnitType.ToString();
    }
    public GameObject GetGameObjectByID(string id)
    {
        return Units[id].UnitPrefab;
    }
    public string GetNextIDbyID(string id)
    {
        return Units[id].NextUnitID;
    }
    public bool GetIsLastByID(string id)
    {
        return Units[id].IsLast;
    }
    public GameObject GetArrowObjectByID(string id)
    {
        return Units[id].ArrowPrefab;
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
}
