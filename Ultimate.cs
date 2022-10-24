using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate
{
    public UltimateTypes UltimateType;
    public GameObject UltimatePrefab;
    public GameObject AttachPrefab;
    public int Damage;
    public int Health;
    public float Cooldown;
    public Sprite Sprite;
    public Sprite Rank;
    public float UltimateTime;
    public Color Color;
    public enum UltimateTypes
    {
        Melee,Range
    }
}
