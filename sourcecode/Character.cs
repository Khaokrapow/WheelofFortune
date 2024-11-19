using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Character 
{
    public string nameChar;
    public Sprite imageCharacter;
    public EnumAbilityCode abilityCode;
    public int value;
    //public string description;
    public GameObject VFX;
    public AudioClip soundEffect;

}
