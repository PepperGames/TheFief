using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiAgent : MonoBehaviour
{
    public event Action<AiAgent, Action<bool>> OnReachedFinalPoint;

    public float speed = 0.2f;

    private List<Vector2> pathToGo = new List<Vector2>();
    private bool moveFlag = true;
    private int index = 0;
    private Vector2 endPosition;
    private float _idleTime = 0;

    public void Initialize(List<Vector2> path)
    {
        pathToGo = path;
        index = 1;
        if (pathToGo.Count > 1)
        {
            endPosition = pathToGo[index];
        }
        else
        {
            OnReachedFinalPoint?.Invoke(this, null);
        }
    }

    private void Update()
    {
        if (moveFlag)
        {
            PerformMovement();
        }
        else
        {
            Idle();
        }
    }

    private void PerformMovement()
    {
        if (pathToGo.Count > index)
        {
            float distanceToGo = MoveTheAgent();
            if (distanceToGo < 0.05f)
            {
                index++;
                if (index >= pathToGo.Count)
                {
                    moveFlag = false;
                    _idleTime = Random.Range(0.7f, 3f);
                    OnReachedFinalPoint?.Invoke(this, null);
                    return;
                }
                endPosition = pathToGo[index];
            }
        }
    }

    private void Idle()
    {
        _idleTime -= Time.deltaTime;
        if (_idleTime <= 0)
        {
            moveFlag = true;
        }
    }

    private float MoveTheAgent()
    {
        float step = speed * Time.deltaTime * InGameSpeed.Speed;

        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

        return Vector3.Distance(transform.position, endPosition);
    }
}
