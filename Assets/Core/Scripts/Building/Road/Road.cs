using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IDemolishable
{
    public void Demolish()
    {
        gameObject.transform.localScale /= 3f;
    }

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
