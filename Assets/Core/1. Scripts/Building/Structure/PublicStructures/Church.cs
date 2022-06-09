using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : PublicStructure
{
    [SerializeField] float _range = 0;
    [SerializeField] StructureCircle _circle;

    protected override void Start()
    {
        base.Start();
        _circle.SetRange(_range);
        _circle.Initialize();
    }

    protected override void OnUpgrade()
    {
        switch (LVL)
        {
            case 2:
                _range += 4;
                _circle.SetRange(_range);
                break;

            case 3:
                _range += 3;
                _circle.SetRange(_range);
                break;

            default:
                break;
        }
    }

}
