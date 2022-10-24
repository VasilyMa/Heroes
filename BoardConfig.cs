using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "BoardConfig", menuName = "Configs/BoardConfig", order = 0)]
public class BoardConfig : ScriptableObject
{
    [Header("BoardPlace")]
    public GameObject BoardPlacePrefab;
}
