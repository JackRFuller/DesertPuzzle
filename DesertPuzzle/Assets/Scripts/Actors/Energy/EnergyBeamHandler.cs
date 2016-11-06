using UnityEngine;
using System.Collections;

public class EnergyBeamHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform rayOrigin;
    [SerializeField]
    private LineRenderer lineRenderer;

    //Beam Attributes
    private Ray ray;
    private Vector3 rayDirection;

    //Reflection Attributes
    private int numberOfReflections;
    private int numberOfPoints;
    private Vector3 reflectionDirection;
    private RaycastHit hit;

    //Energy Shard
    private EnergyShardBehaviour energyShardScript;
    private GameObject activeEnergyShard;

    //Light Filter
    private GameObject lightFilter;
    private LightFilterHandler lightFilterScript;

    [SerializeField]
    private ObjectColor.ColorClass beamColor;
    private int colorIndex = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        colorIndex = (int)beamColor;        
    }

    public void SetBeamColor(ObjectColor.ColorClass beam)
    {
        beamColor = beam;
        colorIndex = (int)beamColor;

        //Set LightBeamColor
        Material newLineRendererMat = lineRenderer.material;
        newLineRendererMat.color = ColorManager.Instance.Colors[colorIndex];
        newLineRendererMat.SetColor("_Emission", newLineRendererMat.color);

        lineRenderer.material = newLineRendererMat;
    }
    
    public void SetBeamAttributes(Vector3 direction, int numOfReflections)
    {
        numberOfReflections = numOfReflections;
        rayDirection = direction;
    }

    public void SetBeamDirect(Vector3 direction)
    {
        rayDirection = direction;
    }
   
    public void ToggleLineRenderer()
    {
        if(lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }


    public void SendOutBeam()
    {
        if (lineRenderer.enabled == false)
            lineRenderer.enabled = true;

        numberOfReflections = Mathf.Clamp(numberOfReflections, 1, numberOfReflections);

        ray = new Ray(rayOrigin.position, rayDirection);
        Debug.DrawRay(rayOrigin.position, rayDirection, Color.red);

        numberOfPoints = numberOfReflections;

        //Set Ray Attributes
        lineRenderer.SetVertexCount(numberOfPoints);
        lineRenderer.SetPosition(0, rayOrigin.position);

        for (int i = 0; i < numberOfReflections; i++)
        {      
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag.Equals("Reflector"))
                {
                    ReflectBeam(hit, i);
                }
                else if (hit.collider.tag.Equals("LightFilter"))
                {
                    GameObject lightfilter = hit.collider.transform.parent.gameObject;
                    HitLightFilter(lightfilter, i,hit, ray.direction);
                }
                else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("EnergyShard"))
                {
                    GameObject shard = hit.collider.gameObject;
                    HitEnergyShard(shard, i);
                }
                else
                {
                    if(i < numberOfReflections - 1)
                        lineRenderer.SetPosition(i + 1, hit.point);

                    if (energyShardScript != null)
                        DeactiveEnergyShard();

                    if (lightFilterScript != null)
                        lightFilterScript.DeactivateLightFilter();
                }
            }
            else
            {
                if(i == 0)
                {
                    lineRenderer.SetVertexCount(++numberOfPoints);
                    lineRenderer.SetPosition(i, reflectionDirection * 2);
                }
                else
                {
                    if (i < numberOfReflections - 1)
                        lineRenderer.SetPosition(i + 1, hit.point);
                }

                if(energyShardScript != null)                   
                    DeactiveEnergyShard();

                if (lightFilterScript != null)
                    lightFilterScript.DeactivateLightFilter();
            }
        }
    }

    public void DeActivateBeam()
    {
        lineRenderer.enabled = false;
        DeactiveEnergyShard();
    }

    
    void ReflectBeam(RaycastHit hit, int index)
    {
        reflectionDirection = Vector3.Reflect(ray.direction, hit.normal);
        ray = new Ray(hit.point, reflectionDirection);

        lineRenderer.SetVertexCount(++numberOfPoints);
        lineRenderer.SetPosition(index + 1, hit.point);       

        //TODO: Add in Deactivate Energy 
    }

    void HitLightFilter(GameObject filter, int index, RaycastHit hit, Vector3 rayDir)
    {
        if(lightFilter == null || lightFilter != filter)
        {
            lightFilter = filter;
            lightFilterScript = lightFilter.GetComponent<LightFilterHandler>();
        }

        if (index < numberOfReflections - 1)
            lineRenderer.SetPosition(index + 1, hit.point);

        int numOfReflections = numberOfReflections - index;

        //TODO: Add in Light Filter Behaviour
        lightFilterScript.GetRayHitPoint(hit.point, rayDir, 3);
    }

    void HitEnergyShard(GameObject shard, int index)
    {
        if(activeEnergyShard == null || activeEnergyShard != shard)
        {
            activeEnergyShard = shard;
            
            energyShardScript = activeEnergyShard.GetComponent<EnergyShardBehaviour>();
        }

        if(energyShardScript)
        {
           energyShardScript.ActivateEnergyShard(colorIndex);
        }

        if (index < numberOfReflections - 1)
            lineRenderer.SetPosition(index + 1, hit.point);
    }

    void DeactiveEnergyShard()
    {
        if(energyShardScript)
            energyShardScript.DeActivateEnergyShard();

        activeEnergyShard = null;
        energyShardScript = null;
    }
}
