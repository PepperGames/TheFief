using UnityEngine;

public static class MatrixEstateToPrestige
{
    //по строкам(вниз) i
    //по столбцам(вправо) j

    static float[,] _matrix = new float[4, 4]
    {
        { 1f, 0.9f, 0.8f, 0.7f },
        { 0.9f, 1f, 0.9f,  0.8f },
        { 0.8f, 0.9f, 1f,  0.9f },
        { 0.7f, 0.8f, 0.9f,  1f }
    };

    public static float GetCoefficient(Estates characterEstate, Estates structureEstate)
    {
        float result = _matrix[(int)characterEstate, (int)structureEstate];
        return result;
    }
}
