using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RewardStorage", menuName = "Configs/RewardStorage", order = 0)]
public class RewardStorage : ScriptableObject
{
    public Dictionary<int, Reward> Rewards;
    public void Init()
    {
        Rewards = new Dictionary<int, Reward>
        {
            [1] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [2] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [3] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [4] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [5] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [6] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [7] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [8] = new Reward
            {
                IsMoney = true,
                Money = 100
            },
            [9] = new Reward
            {
                IsMoney = false,
                Money = 1000
            }

        };
    }
    public bool GetIsMoneyByID(int id)
    {
        return Rewards[id].IsMoney;
    }
    public ulong GetMoneyByID(int id)
    {
        return Rewards[id].Money;
    }
}
