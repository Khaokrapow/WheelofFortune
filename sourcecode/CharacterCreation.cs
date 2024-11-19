using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterCreation : ScriptableObject
{
    public List<Character> characters;
    
    public int characterCount() 
    { 
        return characters.Count; 
    }
    public Character getCharacter(int index) 
    { 
        return characters[index];
    }

}
