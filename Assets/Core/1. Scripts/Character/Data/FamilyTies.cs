using System;
using System.Collections.Generic;
using UnityEngine;

public class FamilyTies
{
    public Character mother = null;
    public Character father = null;
    public List<Character> children = new List<Character>();

    public int FamilySize
    {
        get
        {
            int result = 0;

            if (mother != null && mother.CharacterStatus == CharacterStatuses.Alive)
                result++; 

            if (father != null && father.CharacterStatus == CharacterStatuses.Alive)
                result++;

            result += children.Count;

            return result;
        }
    }

    public Action OnFamilyChange;
    public Action OnChildrenChange;
    public Action OnParentChange;

    public FamilyTies() { }

    public FamilyTies(Character mother, Character father)
    {
        this.mother = mother;
        this.father = father;

        OnParentChange?.Invoke();
        OnFamilyChange?.Invoke();
    }

    public void SetYourselfUpAsAChild(Character character)
    {
        if (mother != null)
        {
            mother.CharacterData.FamilyTies.AddChild(character);
        }
        if (father != null)
        {
            father.CharacterData.FamilyTies.AddChild(character);
        }
    }

    public void AddChild(Character child)
    {
        children.Add(child);
        Debug.Log("children.Count " + children.Count);

        OnChildrenChange?.Invoke();
        OnFamilyChange?.Invoke();
    }

    public bool IsChildren(Character character)
    {
        if (children.Contains(character))
        {
            Debug.Log("IsChildren");
            return true;
        }
        return false;
    }

    public bool IsParent(Character character)
    {
        if (mother == character || father == character)
        {
            Debug.Log("IsParent");
            return true;
        }
        return false;
    }

    /// <summary>
    /// кровный родственник
    /// </summary>
    public bool IsKinsman(Character character)
    {
        return IsChildren(character) || IsParent(character);
    }
}
