using UnityEngine;
using Random = UnityEngine.Random;

public class PortraitGenerator : MonoBehaviour
{
    [SerializeField] private Sprite[] _femalePortraits;
    [SerializeField] private Sprite[] _malePortraits;

    public Sprite GetPortrait(Genders gender)
    {
        int index;

        if (gender == Genders.Female)
        {
            index = Random.Range(0, _femalePortraits.Length);
            Debug.Log("Female index " + index);
            return _femalePortraits[index];
        }
        else
        {
            index = Random.Range(0, _malePortraits.Length);
            Debug.Log("Male index " + index);
            return _malePortraits[index];
        }
    }
}
