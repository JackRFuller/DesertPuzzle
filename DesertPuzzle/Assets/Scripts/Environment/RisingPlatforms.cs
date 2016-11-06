using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatforms : MonoBehaviour
{
    [SerializeField]
    private Transform platform;    
    public Transform Platform { get { return platform; } }

    [SerializeField]   
    private Vector3 startingPosition;
    [SerializeField]
    private Vector3 endPosition;
    private Vector3 startPos;
    private Vector3 targetPos;


    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AnimationCurve movementCurve;
    private bool isMoving;
    private float timeStartedMoving;

    public void InitiateMovement(bool isRising)
    {
        startPos = platform.position;

        if (isRising)
        {
            targetPos = endPosition;
        }
        else
        {
            targetPos = startingPosition;
        }

        timeStartedMoving = Time.time;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
            MovePlatform();
    }

    void MovePlatform()
    {
        float timeSinceStarted = Time.time - timeStartedMoving;
        float percentageComplete = timeSinceStarted / movementSpeed;

        platform.position = Vector3.Lerp(startPos, targetPos, movementCurve.Evaluate(percentageComplete));

        if (percentageComplete >= 1.0f)
        {
            isMoving = false;
        }
    }
}
