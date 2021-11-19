using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : BasicStructure
{
    private void Start()
    {
        StartCoroutine("WaitForImprove");
    }

    IEnumerator WaitForImprove()
    {
        yield return new WaitForSeconds(4f);
        Demolish();
    }
}
