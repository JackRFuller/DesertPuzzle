using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WireSystem : MonoBehaviour {
	public List<Transform> poles;
	public Material wireMaterial;
	public bool stopPointsAreWires = false;
	public bool lineRendererWires = true;
	public bool wiresAreCloth = false;
	public int circleVertices = 3;
	public bool simple = true;
	public float wireWidth = 0.1f;
	public float wireSag = 0.1f;
	public float wireSagRandom = 0.05f;
	public int numberOfVertices = 7;
	public bool showParams = false;
	public bool showFunctions = false;
	public bool showPoles = false;

	// Use this for initialization
	void Awake () {
		if(poles == null)
		{
			poles = new List<Transform>();
			poles.Add (transform);
		}
	}
	void Start ()
	{
		//foreach (InteractiveCloth cloth in GetComponentsInChildren<InteractiveCloth>()) 
		//{
		//	foreach(Collider collid in (Collider[])GetComponentsInChildren<SphereCollider>())
		//	{
		//		cloth.AttachToCollider(collid, false, false);
		//	}
		//}
	}

}
