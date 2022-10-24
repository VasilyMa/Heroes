using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UltimateStorage", menuName = "Configs/UltimateStorage", order = 0)]
public class UltimateStorage : ScriptableObject
{
    public Dictionary<string, Ultimate> Ultimates;
    public GameObject[] MeleeUltimatePrefabs;
    public GameObject[] MeleeUltimateAttachPrefabs;
    public Sprite[] MeleeUltimateSprites;
    public GameObject[] RangeUltimatePrefabs;
    public GameObject[] RangeUltimateAttachPrefabs;
    public Sprite[] RangeUltimateSprites;
    public Sprite[] RankUltimate;
    public void Init()
    {
        Ultimates = new Dictionary<string, Ultimate>
        {
            ["1melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[0],
                AttachPrefab = MeleeUltimateAttachPrefabs[0],
                Health = 25,
                Damage = 8,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[0],
                Rank = RankUltimate[0],
                UltimateTime = 4f,
                Color = Color.gray,
            },
            ["2melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[1],
                AttachPrefab = MeleeUltimateAttachPrefabs[1],
                Health = 56,
                Damage = 16,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[1],
                Rank = RankUltimate[1],
                UltimateTime = 4f,
                Color = Color.white,
            },
            ["3melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[2],
                AttachPrefab = MeleeUltimateAttachPrefabs[2],
                Health = 87,
                Damage = 24,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[2],
                Rank = RankUltimate[2],
                UltimateTime = 4f,
                Color = Color.green,
            },
            ["4melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[3],
                AttachPrefab = MeleeUltimateAttachPrefabs[3],
                Health = 119,
                Damage = 32,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[3],
                Rank = RankUltimate[3],
                UltimateTime = 4f,
                Color = Color.cyan,
            },
            ["5melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[4],
                AttachPrefab = MeleeUltimateAttachPrefabs[4],
                Health = 150,
                Damage = 40,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[4],
                Rank = RankUltimate[4],
                UltimateTime = 4f,
                Color = Color.blue,
            },
            ["6melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[5],
                AttachPrefab = MeleeUltimateAttachPrefabs[5],
                Health = 181,
                Damage = 48,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[5],
                Rank = RankUltimate[5],
                UltimateTime = 4f,
                Color = Color.magenta,
            },
            ["7melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[6],
                AttachPrefab = MeleeUltimateAttachPrefabs[6],
                Health = 212,
                Damage = 56,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[6],
                Rank = RankUltimate[6],
                UltimateTime = 4f,
                Color = Color.yellow,
            },
            ["8melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[7],
                AttachPrefab = MeleeUltimateAttachPrefabs[7],
                Health = 244,
                Damage = 64,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[7],
                Rank = RankUltimate[7],
                UltimateTime = 4f,
                Color = Color.red,
            },
            ["9melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[8],
                AttachPrefab = MeleeUltimateAttachPrefabs[8],
                Health = 275,
                Damage = 72,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[8],
                Rank = RankUltimate[8],
                UltimateTime = 4f,
                Color = Color.black,
            },
            ["10melee"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Melee,
                UltimatePrefab = MeleeUltimatePrefabs[9],
                AttachPrefab = MeleeUltimateAttachPrefabs[9],
                Health = 306,
                Damage = 80,
                Cooldown = 10f,
                Sprite = MeleeUltimateSprites[9],
                Rank = RankUltimate[9],
                UltimateTime = 4f,
                Color = Color.black,
            },

            //RANGE
            ["1range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[0],
                AttachPrefab = RangeUltimateAttachPrefabs[0],
                Damage = 10,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[0],
                Rank = RankUltimate[0],
                Color = Color.gray,
            },
            ["2range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[1],
                AttachPrefab = RangeUltimateAttachPrefabs[1],
                Damage = 23,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[1],
                Rank = RankUltimate[1],
                Color = Color.white,
            },
            ["3range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[2],
                AttachPrefab = RangeUltimateAttachPrefabs[2],
                Damage = 35,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[2],
                Rank = RankUltimate[2],
                Color = Color.green,
            },
            ["4range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[3],
                AttachPrefab = RangeUltimateAttachPrefabs[3],
                Damage = 48,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[3],
                Rank = RankUltimate[3],
                Color = Color.cyan,
            },
            ["5range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[4],
                AttachPrefab = RangeUltimateAttachPrefabs[4],
                Damage = 60,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[4],
                Rank = RankUltimate[4],
                Color = Color.blue,
            },
            ["6range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[5],
                AttachPrefab = RangeUltimateAttachPrefabs[5],
                Damage = 73,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[5],
                Rank = RankUltimate[5],
                Color = Color.magenta,
            },
            ["7range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[6],
                AttachPrefab = RangeUltimateAttachPrefabs[6],
                Damage = 85,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[6],
                Rank = RankUltimate[6],
                Color = Color.yellow,
            },
            ["8range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[7],
                AttachPrefab = RangeUltimateAttachPrefabs[7],
                Damage = 98,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[7],
                Rank = RankUltimate[7],
                Color = Color.red,
            },
            ["9range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[8],
                AttachPrefab = RangeUltimateAttachPrefabs[8],
                Damage = 110,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[8],
                Rank = RankUltimate[8],
                Color = Color.black,
            },
            ["10range"] = new Ultimate
            {
                UltimateType = Ultimate.UltimateTypes.Range,
                UltimatePrefab = RangeUltimatePrefabs[9],
                AttachPrefab = RangeUltimateAttachPrefabs[9],
                Damage = 123,
                Health = 0,
                Cooldown = 5f,
                Sprite = RangeUltimateSprites[9],
                Rank = RankUltimate[9],
                Color = Color.black,
            },
        };
    }   
    public int GetDamageByID(string id)
    {
        return Ultimates[id].Damage;
    }
    public int GetHealthByID(string id)
    {
        return Ultimates[id].Health;
    }
    public float GetCooldownByID(string id)
    {
        if (id == string.Empty)
            return 0;
        return Ultimates[id].Cooldown;
    }
    public Sprite GetSpriteByID(string id)
    {
        return Ultimates[id].Sprite;
    }
    public Sprite GetRankByID(string id)
    {
        return Ultimates[id].Rank;
    }
    public Color GetColorByID(string id)
    {
        return Ultimates[id].Color;
    }
    public GameObject GetPrefabByID(string id)
    {
        return Ultimates[id].UltimatePrefab;
    }
    public GameObject GetAttachByID(string id)
    {
        return Ultimates[id].AttachPrefab;
    }
    public float GetEffectTimeByID(string id)
    {
        return Ultimates[id].UltimateTime;
    }
}
