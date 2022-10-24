using System.Security.Cryptography;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;
using Client;
using System;
using System.IO;

namespace Client
{
    public class Saves
    {
        public int Sounds;
        public int Music;
        public int Vibration;
        public int LVL;
        public ulong AllCoin;
        public int SceneNumber;
        public string[] PlayerUnits;
        public int TutorialState;
        public List<string> OpenedHero;
        public int KeysCount;
        public int SundukCount;
        public float timerInter;


        public SaveSettings Save = new SaveSettings();
        public string path;
        //методы сохранений...
        #region
        public void SaveTimer(float value)
        {
            timerInter = value;
            Save.timerInter = timerInter;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveSounds(int value)
        {
            Sounds = value;
            Save.Sounds = Sounds;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }

        public void SaveMusic(int value)
        {
            Music = value;
            Save.Music = Music;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }

        public void SaveVibration(int value)
        {
            Vibration = value;
            Save.Vibration = Vibration;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }

        public void SaveLevel(int value)
        {
            LVL = value;
            Save.LVL = LVL;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveCoin(ulong value)
        {
            AllCoin = value;
            Save.AllCoin = AllCoin;
            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveSceneNumber(int value)
        {
            Save.SceneNumber = value;
            SceneNumber = value;

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SavePlayerUnits(string[] value)
        {
            Save.PlayerUnits = value;
            PlayerUnits = value;

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveTutorial(int value)
        {
            Save.TutorialState = value;
            TutorialState = value;

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveOpenedHero(string value)
        {
            Save.OpenedHero.Add(value);
            OpenedHero.Add(value);

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveKeysCount(int value)
        {
            Save.KeysCount = value;
            KeysCount = value;

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }
        public void SaveSundukCount(int value)
        {
            Save.SundukCount = value;
            SundukCount = value;

            File.WriteAllText(path, JsonUtility.ToJson(Save));
        }

        #endregion
        public void InitSave()
        {
            //в зависимости от того где запустили игру находим путь до json файла
#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "SaveSettings.json");
#else
            path = Path.Combine(Application.dataPath, "SaveSettings.json");
#endif
            //Если файл есть по заданому пути получаем значения из него
            if (File.Exists(path))
            {
                Save = JsonUtility.FromJson<SaveSettings>(File.ReadAllText(path));
                Sounds = Save.Sounds;
                Music = Save.Music;
                Vibration = Save.Vibration;
                LVL = Save.LVL;
                AllCoin = Save.AllCoin;
                SceneNumber = Save.SceneNumber;
                PlayerUnits = Save.PlayerUnits;
                TutorialState = Save.TutorialState;
                timerInter = Save.timerInter;
                //OpenedHero = Save.OpenedHero;
                OpenedHero = new List<string>();
                foreach(var item in Save.OpenedHero)
                {
                    OpenedHero.Add(item);
                }
                KeysCount = Save.KeysCount;
                SundukCount = Save.SundukCount;
            }
            //если нет, то записываем значения в SaveSettings и создаем файлик
            else
            {
                Sounds = 1;
                Music = 1;
                Vibration = 1;
                LVL = 1;
                AllCoin = 900;
                SceneNumber = 1;
                TutorialState = 1;
                CreatePlayerUnits();
                OpenedHero = new List<string>();
                OpenedHero.Add("1range");
                OpenedHero.Add("1melee");
                KeysCount = 0;
                SundukCount = 0;
                timerInter = 0;

                Save.Sounds = Sounds;
                Save.Music = Music;
                Save.Vibration = Vibration;
                Save.LVL = LVL;
                Save.AllCoin = AllCoin;
                Save.SceneNumber = SceneNumber;
                Save.PlayerUnits = PlayerUnits;
                Save.TutorialState = TutorialState;
                Save.OpenedHero = new List<string>();
                Save.timerInter = timerInter;
                foreach(var item in OpenedHero)
                {
                    Save.OpenedHero.Add(item);
                }
                Save.KeysCount = 0;
                Save.SundukCount = 0;
                File.WriteAllText(path, JsonUtility.ToJson(Save));
            }
        }
        public int LoadSceneNumber()
        {
            int sceneNumber = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "SaveSettings.json");
#else
            path = Path.Combine(Application.dataPath, "SaveSettings.json");
#endif
            if (File.Exists(path))
            {
                //File.Delete(path);
                Save = JsonUtility.FromJson<SaveSettings>(File.ReadAllText(path));
                sceneNumber = Save.SceneNumber;
            }
            else
            {
                sceneNumber = 1;
            }
            return sceneNumber;
        }

        public void CreatePlayerUnits()
        {
            PlayerUnits = new string[15];

            PlayerUnits[0] = "empty";
            PlayerUnits[1] = "empty";
            PlayerUnits[2] = "empty";
            PlayerUnits[3] = "empty";
            PlayerUnits[4] = "empty";
            PlayerUnits[5] = "empty";
            PlayerUnits[6] = "1melee";
            PlayerUnits[7] = "empty";
            PlayerUnits[8] = "1melee";
            PlayerUnits[9] = "empty";
            PlayerUnits[10] = "empty";
            PlayerUnits[11] = "empty";
            PlayerUnits[12] = "1range";
            PlayerUnits[13] = "empty";
            PlayerUnits[14] = "empty";
        }

        [Serializable]
        public class SaveSettings
        {
            public int Sounds;
            public int Music;
            public int Vibration;
            public int LVL;
            public ulong AllCoin;
            public int SceneNumber;
            public string[] PlayerUnits;
            public int TutorialState;
            public List<string> OpenedHero;
            public int KeysCount;
            public int SundukCount;
            public float timerInter;

        }
    }
}
