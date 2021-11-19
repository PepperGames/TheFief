using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    public void Break(float percent);
    public void Fix(float percent);
}
