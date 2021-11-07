using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resources
{
    [SerializeField] float money, food, wood, stone, metal;

    #region PROPERTIES

    public float Money
    {
        get => money;
        set
        {
            if (value >= 0)
            {
                money = value;
            }
            else
            {
                Debug.Log("negative value " + value);
            }
        }
    }

    public float Food
    {
        get => food;
        set
        {
            if (value >= 0)
            {
                food = value;
            }
            else
            {
                Debug.Log("negative value " + value);
            }
        }
    }

    public float Wood
    {
        get => wood;
        set
        {
            if (value >= 0)
            {
                wood = value;
            }
            else
            {
                Debug.Log("negative value " + value);
            }
        }
    }

    public float Stone
    {
        get => stone;
        set
        {
            if (value >= 0)
            {
                stone = value;
            }
            else
            {
                Debug.Log("negative value " + value);
            }
        }
    }

    public float Metal
    {
        get => metal;
        set
        {
            if (value >= 0)
            {
                metal = value;
            }
            else
            {
                Debug.Log("negative value " + value);
            }
        }
    }

    #endregion

    public Resources(float money, float food, float wood, float stone, float metal)
    {
        this.money = money;
        this.food = food;
        this.wood = wood;
        this.stone = stone;
        this.metal = metal;
    }

    public static Resources operator +(Resources a, Resources b)
    {
        return new Resources(a.Money + b.Money, a.Food + b.Food, a.Wood + b.Wood, a.Stone + b.Stone, a.Metal + b.Metal);
    }

    public static Resources operator -(Resources a, Resources b)
    {
        return new Resources(a.Money - b.Money, a.Food - b.Food, a.Wood - b.Wood, a.Stone - b.Stone, a.Metal - b.Metal);
    }

}
