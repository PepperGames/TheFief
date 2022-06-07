using System;

public static class CharacterRounder
{
    public static string Round(float input, int digits)
    {
        return Math.Round(input, digits).ToString();
    }
}
