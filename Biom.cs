using UnityEngine;
using System.Collections.Generic;

public class Biom
{
    public List<int> BiomLevels; // ���������� ������� �����
    public int StartBiomLevel; // ��������� ������� �����
    public BiomType BiomType; // ��� �����
    public Sprite BiomSprite; // �������� ������������ ����
    public Sprite NextBiomSprite;
    //public Sprite BlurSprite; // ����������� �������� ��� ��������
}

public enum BiomType
{
    Street = 1, Suburb = 2, Village = 3, Skyscrapper = 4, Mars = 5
}
