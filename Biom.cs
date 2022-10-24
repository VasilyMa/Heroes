using UnityEngine;
using System.Collections.Generic;

public class Biom
{
    public List<int> BiomLevels; // количество уровней биома
    public int StartBiomLevel; // стартовый уровень биома
    public BiomType BiomType; // тип биома
    public Sprite BiomSprite; // картинка отображающая биом
    public Sprite NextBiomSprite;
    //public Sprite BlurSprite; // заблюренная картинка для магазина
}

public enum BiomType
{
    Street = 1, Suburb = 2, Village = 3, Skyscrapper = 4, Mars = 5
}
