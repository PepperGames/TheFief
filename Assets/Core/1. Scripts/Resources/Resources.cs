using System;
using UnityEngine;

[Serializable]
public struct Resources
{
    [SerializeField] private float money, food, wood, stone, metal;

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

    public void Reset()
    {
        money = 0;
        food = 0;
        wood = 0;
        stone = 0;
        metal = 0;
    }

    public static Resources operator +(Resources a, Resources b)
    {
        return new Resources(a.Money + b.Money, a.Food + b.Food, a.Wood + b.Wood, a.Stone + b.Stone, a.Metal + b.Metal);
    }

    public static Resources operator -(Resources a, Resources b)
    {
        return new Resources(a.Money - b.Money, a.Food - b.Food, a.Wood - b.Wood, a.Stone - b.Stone, a.Metal - b.Metal);
    }

    public static bool operator >(Resources a, Resources b)
    {
        a -= b;
        if (a.Money < 0)
            return false;
        if (a.Food < 0)
            return false;
        if (a.Wood < 0)
            return false;
        if (a.Stone < 0)
            return false;
        if (a.Metal < 0)
            return false;


        return true;
    }

    public static bool operator <(Resources a, Resources b)
    {
        return b > a;
    }

    public static bool operator >(Resources a, float b)
    {
        Resources r = new Resources(b, b, b, b, b);

        return a > r;
    }

    public static bool operator <(Resources a, float b)
    {
        Resources r = new Resources(b, b, b, b, b);

        return a < r;
    }

    public static Resources operator *(Resources a, float b)
    {
        return new Resources(a.Money * b, a.Food * b, a.Wood * b, a.Stone * b, a.Metal * b);
    }

    public static Resources operator /(Resources a, float b)
    {
        return new Resources(a.Money / b, a.Food / b, a.Wood / b, a.Stone / b, a.Metal / b);
    }

    public static Resources operator *(float a, Resources b)
    {
        return b * a;
    }

    public static Resources operator /(float a, Resources b)
    {
        return b / a;
    } 
    
    public static Resources operator /(Resources a, Resources b)
    {
        return new Resources(a.Money / b.Money, a.Food / b.Food, a.Wood / b.Wood, a.Stone / b.Stone, a.Metal / b.Metal);
    }
}
