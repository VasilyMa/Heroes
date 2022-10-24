using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public UnitType UnitType;
    public string NextUnitID;
    public string Name;
    public bool IsLast;
    public int Level;
    public GameObject UnitPrefab;
    public GameObject ArrowPrefab;
    public int Speed;
    public float Damage;
    public int Health;
    public int DistanceOfFight;
    public float Cooldown;
    public Sprite UnitSprite;

}
public enum UnitType
{
    Melee,Range
}
