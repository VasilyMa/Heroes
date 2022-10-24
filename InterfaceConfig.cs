using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "InterfaceConfig", menuName = "Configs/InterfaceConfig", order = 0)]
public class InterfaceConfig : ScriptableObject
{
    public GameObject KeyGameObject;
    public GameObject AddedCoinGO;
    public Color ActiveButtonColor;
    public Color InactiveButtonColor;
    public Sprite CoinSprite;
    public GameObject RewardDaily;

}
