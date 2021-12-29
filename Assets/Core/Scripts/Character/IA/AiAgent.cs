using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    public event Action<AiAgent> OnReachedFinalPoint;

    public float speed = 0.2f;

    List<Vector2> pathToGo = new List<Vector2>();
    bool moveFlag = false;
    int index = 0;
    Vector2 endPosition;

    public void Initialize(List<Vector2> path)
    {
        pathToGo = path;
        index = 1;
        moveFlag = true;
        if (pathToGo.Count > 1)
        {
            endPosition = pathToGo[index];
        }
        else
        {
            OnReachedFinalPoint?.Invoke(this);
        }
    }

    private void Update()
    {
        if (moveFlag)
        {
            PerformMovement();
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
                    OnReachedFinalPoint?.Invoke(this);
                    return;
                }
                endPosition = pathToGo[index];
            }
        }
    }

    private float MoveTheAgent()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

        return Vector3.Distance(transform.position, endPosition);
    }
}
