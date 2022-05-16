using System.Collections.Generic;
using UnityEngine;

public class FamilyTies : MonoBehaviour
{
    public Character mother = null;
    public Character father = null;
    public List<Character> children = new List<Character>();

    public FamilyTies() { }

    public FamilyTies(Character mother, Character father)
    {
        this.mother = mother;
        this.father = father;
    }
}
