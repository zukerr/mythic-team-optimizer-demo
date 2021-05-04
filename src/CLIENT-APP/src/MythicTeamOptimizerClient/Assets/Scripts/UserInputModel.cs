using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputModel
{
    public string DungeonName { get; set; }
    public string CharacterClass { get; set; }
    public string CharacterSpec { get; set; }

    public override string ToString()
    {
        return $"Dungeon: {DungeonName}, Class: {CharacterClass}, Spec: {CharacterSpec}";
    }
}
