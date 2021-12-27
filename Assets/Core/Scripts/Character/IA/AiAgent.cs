using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    public event Action OnDeath;

    public float speed = 0.2f;
    public float rotationSpeed = 10f;

    List<Vector2> pathToGo = new List<Vector2>();
    bool moveFlag = false;
    int index = 0;
    Vector2 endPosition;

    public void Initialize(List<Vector2> path)
    {
        pathToGo = path;
        index = 1;
        moveFlag = true;
        endPosition = pathToGo[index];
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
                    Debug.Log("end");
                    //Destroy(gameObject);
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

        //var lookDirection = endPosition - new Vector2(transform.position.x, transform.position.y);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        return Vector3.Distance(transform.position, endPosition);
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
