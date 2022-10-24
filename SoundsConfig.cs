using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SoundsConfig", menuName = "Configs/SoundsConfig", order = 0)]
public class SoundsConfig : ScriptableObject
{
    public AudioClip[] MeleeHitSounds;
    public AudioClip[] RangeHitSounds;
    public AudioClip[] ShotSounds;
    public AudioClip EnemyMeleeHitSound;
    public AudioClip EnemyRangeHitSound;
    public AudioClip EnemyShotSound;

    public AudioClip GetHitSound(string type, int index)
    {
        if(type == "Melee") return MeleeHitSounds[index - 1];
        else return RangeHitSounds[index - 1];
    }
    public AudioClip GetShotSound(int index)
    {
        return ShotSounds[index - 1];
    }

}
