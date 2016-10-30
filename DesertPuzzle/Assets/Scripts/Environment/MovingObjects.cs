using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AnimationCurve movementCurve;

    [Header("Mesh Attributes")]
    [SerializeField]
    private Transform childObj;    
    private Collider col;    
    private MeshRenderer mesh;    
    private Material originalMaterial;
    private Material invisibleMaterial;

    private int movementCount = 0;
    private bool isMoving;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timeStartedMoving;

    bool hasBeenPaused;
    private float timeWhenPaused;

    void Awake()
    {
        GetChildComponents();
    }

    void GetChildComponents()
    {
        col = childObj.GetComponent<Collider>();
        mesh = childObj.GetComponent<MeshRenderer>();
        originalMaterial = mesh.material;
    }

    void OnEnable()
    {
        TurnOnMesh();
        SwitchMovementTargets(false);
    }

    void OnDisable()
    {
        TurnOffMesh();

        timeWhenPaused = Time.time;
        hasBeenPaused = true;
    }
    
    void TurnOnMesh()
    {        
        col.enabled = true;
        mesh.material = originalMaterial;
        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        mesh.receiveShadows = true;
    } 

    void TurnOffMesh()
    {
        
        col.enabled = false;
        mesh.material = ColorManager.Instance.InvisibleObjectMaterial;
        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mesh.receiveShadows = false;
    }
    

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        float timeSinceStarted;

        if (hasBeenPaused)
            timeSinceStarted = timeWhenPaused - timeStartedMoving;
        else
            timeSinceStarted = Time.time - timeStartedMoving;

        float percentageComplete = timeSinceStarted / movementSpeed;

        transform.position = Vector3.Lerp(startPos, targetPos, movementCurve.Evaluate(percentageComplete));

        if(percentageComplete >= 1.0f)
        {
            SwitchMovementTargets(true);
        }

        hasBeenPaused = false;
    }

    void SwitchMovementTargets(bool isIncrementing)
    {
        startPos = transform.position;

        if (isIncrementing)
        {
            movementCount++;
            if (movementCount > 1)
                movementCount = 0;
        }

        if (movementCount == 0)
        {
            targetPos = endPosition;
        }
        else
        {
            targetPos = startPosition;
        }
        timeStartedMoving = Time.time;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            other.transform.parent = null;
        }
    }
}
