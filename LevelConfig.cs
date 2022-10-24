using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    public Sprite[] BiomsSprites;
    public BiomType[] StartBiomTypes;
    public int[] StartBiomLevels;
    public List<Biom> Bioms;
    public Color CyrrentPointColor;
    public Color CompletePointColor;
    public Color AnCompletePointColor;
}
