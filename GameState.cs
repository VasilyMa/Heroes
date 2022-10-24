using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using System;
using UnityEngine.SceneManagement;
//using Advertising;

namespace Client
{
    public class GameState
    {
        public EcsWorld World;
        public BoardConfig BoardConfig;
        public UnitStorage UnitStorage;
        public UltimateStorage UltimateStorage;
        public InterfaceConfig InterfaceConfig;
        public SoundsConfig SoundsConfig;
        public EnemyUnitStorage EnemyUnitStorage;
        public EffectsStorage EffectsStorage;
        public LevelConfig LevelsStorage;
        public RewardStorage RewardStorage;
        public int EntityBoard;
        public int EntityInput;
        public int EntityLevel;
        public int EntityMeleeUltimate;
        public int EntityRangeUltimate;
        public int EntityCamera;
        public int EntitySounds;
        public int EntityLevelReward;
        public ulong Coins;
        public int[] UnitEntityes;
        public string[] PlayerUnits;
        public string[] SavedPlayerUnits;
        public int EntityInterface;
        public int MoneyEntity;
        public UnitPlaceBounds[] UnitPlaceBounds;
        public Saves Saves = new Saves();
        public Biom CurrentBiom;
        //public IAdvertising ADS;
        public int[] SundukRandomArray;
        public int SundukCount;
        public int KeysCount;
        public float timerInter;
        public static bool isDailyReward;

        // enabledGroups
        public bool BeforeGroup, PlayGroup;

        public GameState(EcsWorld world, BoardConfig boardConfig, UnitStorage unitStorage, 
        UltimateStorage ultimateStorage, 
        InterfaceConfig interfaceConfig,
        EnemyUnitStorage enemyUnitStorage,
        SoundsConfig soundsConfig,
        EffectsStorage effectsStorage,
        LevelConfig levelConfig,
        //IAdvertising ads,
        RewardStorage rewardStorage)
        {
            World = world;
            BoardConfig = boardConfig;
            UnitStorage = unitStorage;
            UltimateStorage = ultimateStorage;
            InterfaceConfig = interfaceConfig;
            SoundsConfig = soundsConfig;
            EnemyUnitStorage = enemyUnitStorage;
            EffectsStorage = effectsStorage;
            LevelsStorage = levelConfig;
            Saves.InitSave();
            Coins = Saves.AllCoin;
            //ADS = ads;
            RewardStorage = rewardStorage;
            InitStorages();
            InitSundukRandomArray();

        }
        public void InitStorages()
        {
            UnitStorage.Init();
            EnemyUnitStorage.Init();
            UltimateStorage.Init();
            EffectsStorage.Init();
            PlayerUnits = new string[Saves.PlayerUnits.Length];
            for (int i = 0; i < PlayerUnits.Length;i++)
            {
                PlayerUnits[i] = Saves.PlayerUnits[i];
            }
            InitBiom();
            RewardStorage.Init();
            KeysCount = Saves.KeysCount;
        }
        private void InitSundukRandomArray()
        {
            SundukRandomArray = new int[9];
            for (int i = 0; i < SundukRandomArray.Length;i++)
            {
                SundukRandomArray[i] = i + 1;
            }
            var random = new System.Random();

            for (int j = SundukRandomArray.Length - 1; j >= 1;j--)
            {
                int k = random.Next(j + 1);
                var x = SundukRandomArray[k];
                SundukRandomArray[k] = SundukRandomArray[j];
                SundukRandomArray[j] = x;
            }

            SundukCount = Saves.SundukCount;
        }
        
        public void CopyPlayerUnits()
        {
            //Debug.Log("SCOPIROVALI");
            SavedPlayerUnits = new string[PlayerUnits.Length];
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                SavedPlayerUnits[i] = PlayerUnits[i];
            }
        }
        public void AddToPoolPlayerUnitsID(string id)
        {
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                if(SavedPlayerUnits[i] == "empty")
                {
                    Debug.Log("ZAPISALI " + i);
                    SavedPlayerUnits[i] = id;
                    return;
                }
            }
        }
        public int BoundsContain(Vector3 position)
        {
            // найти в какой месте находится точка position
            for (int i = 0; i < UnitPlaceBounds.Length; i++)
            {
                if (UnitPlaceBounds[i].Contains(position)) return i; // если нашли картику - вернуть ее индекс
            }
            return -1; // если не нашли - вернуть -1
        }
        public void CreateNextUnit(int index, string UnitID)
        {
            PlayerUnits[index] = UnitStorage.GetNextIDbyID(UnitID);
        }
        public int GetEmptyPlayerUnitsIndex()
        {
            for (int i = 0; i < PlayerUnits.Length;i++)
            {
                if(PlayerUnits[i] == "empty")
                {
                    return i;
                }
            }
            return -1;
        }
        //вернет id самого сильного юнита выбранного типа
        public string GetHighLevelByType(string type)
        {
            int level = -1;
            string value = string.Empty;
            for (int i = 0; i < PlayerUnits.Length;i++)
            {
                if(UnitStorage.GetUnitTypeByID(PlayerUnits[i]) == type)
                {
                    if(UnitStorage.GetLevelByID(PlayerUnits[i]) > level)
                    {
                        level = UnitStorage.GetLevelByID(PlayerUnits[i]);
                        value = PlayerUnits[i];
                    }
                }
            }
            return value;
        }
        public int GetLowLevelByType(string type)
        {
            int level = 10;
            string value = string.Empty;
            int index = -1;
            for (int i = 0; i < PlayerUnits.Length; i++)
            {
                if (UnitStorage.GetUnitTypeByID(PlayerUnits[i]) == type)
                {
                    if (UnitStorage.GetLevelByID(PlayerUnits[i]) < level)
                    {
                        level = UnitStorage.GetLevelByID(PlayerUnits[i]);
                        index = i;
                    }
                }
            }
            return index;
        }
        public void SetPlayerUnitEmptyValue(int index)
        {
            PlayerUnits[index] = "empty";
        }
        public bool UnitsInArrayMelee()
        {
            var exist = false;
            for (int i = 0; i < PlayerUnits.Length; i++)
            {
                if (PlayerUnits[i].Contains("1melee") || PlayerUnits[i].Contains("2melee") || PlayerUnits[i].Contains("3melee") || PlayerUnits[i].Contains("4melee") || PlayerUnits[i].Contains("5melee") || PlayerUnits[i].Contains("6melee") || PlayerUnits[i].Contains("7melee") || PlayerUnits[i].Contains("8melee") || PlayerUnits[i].Contains("9melee") || PlayerUnits[i].Contains("10melee"))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        public bool UnitsInArrayRange()
        {
           var exist = false;
            for (int i = 0; i < PlayerUnits.Length; i++)
            {
                if (PlayerUnits[i].Contains("1range") || PlayerUnits[i].Contains("2range") || PlayerUnits[i].Contains("3range") || PlayerUnits[i].Contains("4range") || PlayerUnits[i].Contains("5range") || PlayerUnits[i].Contains("6range") || PlayerUnits[i].Contains("7range") || PlayerUnits[i].Contains("8range") || PlayerUnits[i].Contains("9range") || PlayerUnits[i].Contains("10range"))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        public void InitBiom()
        {
            for (int b = 0; b < LevelsStorage.StartBiomLevels.Length; b++)
            {
                var biom = new Biom();
                biom.StartBiomLevel = LevelsStorage.StartBiomLevels[b];
                biom.BiomType = LevelsStorage.StartBiomTypes[b];
                biom.BiomSprite = LevelsStorage.BiomsSprites[b];
                if(b + 1< LevelsStorage.StartBiomLevels.Length)
                {
                    biom.NextBiomSprite = LevelsStorage.BiomsSprites[b + 1];
                }
                else
                {
                    biom.NextBiomSprite = LevelsStorage.BiomsSprites[0];
                }
                if (b == 0 || (b > 0 && b < LevelsStorage.StartBiomLevels.Length - 1))
                {
                    int length = LevelsStorage.StartBiomLevels[b + 1] - LevelsStorage.StartBiomLevels[b];
                    biom.BiomLevels = new List<int>();
                    for (int i = 0; i < length; i++) biom.BiomLevels.Add(LevelsStorage.StartBiomLevels[b] + i);
                }
                else if (b == LevelsStorage.StartBiomLevels.Length - 1)
                {
                    int length = (SceneManager.sceneCountInBuildSettings - 1) - (LevelsStorage.StartBiomLevels[b] - 1);
                    biom.BiomLevels = new List<int>();
                    for (int i = 0; i < length; i++) biom.BiomLevels.Add(LevelsStorage.StartBiomLevels[b] + i);
                }

                LevelsStorage.Bioms = new List<Biom>();
                LevelsStorage.Bioms.Add(biom);

                if (biom.BiomLevels.Contains(SceneManager.GetActiveScene().buildIndex))
                    CurrentBiom = biom;
            }
        }
        public bool CheckAnOpenedHero(string value)
        {
            if(!Saves.OpenedHero.Contains(value))
            {
                Saves.SaveOpenedHero(value);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetEmptyIndexInSavedPlayerUnits()
        {
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                if(SavedPlayerUnits[i] == "empty")
                {
                    return i;
                }
            }
            return -1;
        }
        public bool MoreMeleeUnitInSavedPlayerUnits()
        {
            int melee = 0;
            int range = 0;
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                if(SavedPlayerUnits[i] != "empty")
                {
                    if(UnitStorage.GetUnitTypeByID(SavedPlayerUnits[i]) == "Range") range++;
                    else melee++;
                }
            }
            if(melee>=range) return true;
            else return false;
        }
        public string GetHighLevelIDByTypeInSavedPlayerUnits(string type)
        {
            int level = -1;
            string value = string.Empty;
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                if(UnitStorage.GetUnitTypeByID(SavedPlayerUnits[i]) == type)
                {
                    if(UnitStorage.GetLevelByID(SavedPlayerUnits[i]) > level)
                    {
                        level = UnitStorage.GetLevelByID(SavedPlayerUnits[i]);
                        value = SavedPlayerUnits[i];
                    }
                }
            }
            return value;
        }
        public void MergeUnits()
        {
            for (int i = 0; i < SavedPlayerUnits.Length;i++)
            {
                for (int j = 0; j < SavedPlayerUnits.Length;j++)
                {
                    if(j != i && SavedPlayerUnits[j] != "empty" && SavedPlayerUnits[j]==SavedPlayerUnits[i])
                    {
                        //надосмержить
                        if(!UnitStorage.GetIsLastByID(SavedPlayerUnits[i]))
                        {
                            SavedPlayerUnits[i] = UnitStorage.GetNextIDbyID(SavedPlayerUnits[i]);
                            SavedPlayerUnits[j] = "empty";
                            Saves.SavePlayerUnits(SavedPlayerUnits);
                            return;
                        }
                    }
                }
            }
        }

    }
}
