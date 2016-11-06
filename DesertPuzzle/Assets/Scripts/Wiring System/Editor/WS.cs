using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor;

[CustomEditor(typeof(WireSystem))] 

public class WS : Editor {

	public Transform holder;
	public bool showForRouting = false;
    
	public override void OnInspectorGUI ()
	{

        WireSystem script = (WireSystem)target;       

        GUILayout.Label("Add New Pole by dragging it into the slot below");
		holder = (Transform)EditorGUILayout.ObjectField(holder, typeof(Transform), true);
		//first we lay down all of our parameters
		if (!script.showParams) 
		{
			GUI.color = Color.yellow;
			if (GUILayout.Button ("Show Parameters")) {
				script.showParams = true;
			}
			GUI.color = Color.white;
		} 
		else 
		{
			GUI.color = Color.yellow;
			if (GUILayout.Button ("Hide Parameters")) {
				script.showParams = false;
			}
			GUI.color = Color.white;
			GUILayout.Label ("Wire Material");
			script.wireMaterial = (Material)EditorGUILayout.ObjectField(script.wireMaterial, typeof(Material), true);
			script.wiresAreCloth = GUILayout.Toggle(script.wiresAreCloth, "wires are cloth?");
			script.lineRendererWires = GUILayout.Toggle(script.lineRendererWires, "wires are lineRenderer?");
			if(script.lineRendererWires == false)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("circle vertices (3-32)");
				script.circleVertices = EditorGUILayout.IntField(script.circleVertices);
				GUILayout.EndHorizontal();
			}
			script.stopPointsAreWires = GUILayout.Toggle(script.stopPointsAreWires, "string across stop points only?");
			script.simple = GUILayout.Toggle(script.simple, "simple wire layout?");
			GUILayout.BeginHorizontal();
			GUILayout.Label("Wire width");
			script.wireWidth = EditorGUILayout.FloatField(script.wireWidth);
			GUILayout.EndHorizontal();
			if(script.simple)
			{
				GUILayout.Label ("For more control over wire parameters, uncheck the box above");
			}
			else
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Wire sag distance");
				script.wireSag = EditorGUILayout.FloatField(script.wireSag);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Random wire sag");
				script.wireSagRandom = EditorGUILayout.FloatField(script.wireSagRandom);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Number of vertices [2-64] (higher for smoother wire bends)");
				script.numberOfVertices = EditorGUILayout.IntField(script.numberOfVertices);
				script.numberOfVertices = Mathf.Clamp (script.numberOfVertices, 2, 64);
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Show objects for routing?");
			showForRouting = EditorGUILayout.Toggle (showForRouting);
			GUILayout.EndHorizontal ();
		}

		if (!script.showFunctions) 
		{
			GUI.color = Color.yellow;
			if(GUILayout.Button ("Show Functions")) 
			{
				script.showFunctions = true;
			}
			GUI.color = Color.white;
		} 
		else 
		{
			GUI.color = Color.yellow;
			if (GUILayout.Button ("Hide Functions")) 
			{
				script.showFunctions = false;
			}
			GUI.color = Color.white;
			//EditorGUILayout.SelectableLabel("Automatically route telephone poles", EditorStyles.boldLabel);
			if (GUILayout.Button ("Auto Route")) 
			{
					ClearRoutes ();
					AutoRoute ();
			}
			if (GUILayout.Button ("Clear Routing")) 
			{
					ClearRoutes ();
			}

			//EditorGUILayout.SelectableLabel("Automatically name telephone poles", EditorStyles.boldLabel);
			if (GUILayout.Button ("Auto Name")) 
			{
					AutoName ();
			}
			if( GUILayout.Button ("Ground Poles"))
			{
				GroundAllPoles();
			}
		}
		//EditorGUILayout.SelectableLabel("Manage Poles", EditorStyles.boldLabel);


		if (!script.showPoles) {
			GUI.color = Color.yellow;
			if (GUILayout.Button ("Show Poles")) 
			{
				script.showPoles = true;
			}
			GUI.color = Color.white;
		} 
		else 
		{
			GUI.color = Color.yellow;
			if (GUILayout.Button ("Hide Poles")) {
				script.showPoles = false;
			}
			GUI.color = Color.white;
			int i = 0;
			foreach (Transform pole in script.poles) 
			{
			    GUILayout.Label ("Pole " + i + "", EditorStyles.boldLabel);
			    script.poles [i] = (Transform)EditorGUILayout.ObjectField (pole, typeof(Transform), true);
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Remove")) 
			{
				script.poles.RemoveAt (i);
			}
			if (GUILayout.Button ("Move Up")) 
			{
				MoveIndexUp (i);
			}
			if (GUILayout.Button ("Move Down")) {
				MoveIndexDown (i);
			}
			GUILayout.EndHorizontal ();
			i++;
		    }
				}
		if(holder != null)
		{
			script.poles.Add(holder);
			holder = null;
		}


	}
	public void AutoName ()
	{
		WireSystem script = (WireSystem) target;
		for(int i = 0; i < script.poles.Count; i++)
		{
			script.poles[i].name = "Pole_" + i;
		}
	}
	void OnSceneGUI ()
	{
		WireSystem script = (WireSystem) target;
		//for(int j = 0; j < script.poles.Count-1; j++)
		//{
		//	Handles.DrawLine(script.poles[j].position, script.poles[j+1].position);
		//}

        if (script.poles == null)
        {
            if (!Selection.activeTransform)
                return;
            script.poles = new List<Transform>();
            script.poles.Add(Selection.activeTransform);
        }
        int i = 0;

		foreach(Transform pole in script.poles)
		{
			/*if(i < script.poles.Count-1)
			{
				Handles.DrawLine(script.poles[i].position, script.poles[i+1].position);
				//Debug.Log("Drew line for " + i + "     " + script.poles[i].position + ":" + script.poles[i+1].position);
			}*/
			Vector2 pos = Camera.current.WorldToScreenPoint(pole.position);
			GUILayout.BeginArea(new Rect(pos.x, Screen.height - pos.y-70, 70, 20));
			GUI.Box(new Rect(0, 0, 70, 20), "");
			if(GUI.Button(new Rect(40, 2, 26, 16), "x"))
			{
				script.poles.RemoveAt(i);
			}
			GUI.Label(new Rect(2, 2, 68, 16), "Pole " + i);
			GUILayout.EndArea();
			i++;
		}
		if (showForRouting) 
		{
			foreach(Transform pole in (Transform[])FindObjectsOfType(typeof(Transform)))
			{
				int depth = 0;
				if(pole.parent == null)
				{
					depth = 1;
				}
				if(depth == 1)
				{
					Vector2 pos = Camera.current.WorldToScreenPoint(pole.position);
					GUILayout.BeginArea(new Rect(pos.x, Screen.height - pos.y-50, 70, 20));
					GUI.Box(new Rect(0, 0, 70, 20), "");
					if(GUI.Button(new Rect(40, 2, 26, 16), "+"))
					{
						script.poles.Add(pole);
					}
					GUI.Label(new Rect(2, 2, 68, 16), "Add");
					GUILayout.EndArea();
				}
				
				i++;
			}
		}

	}
	void AutoRoute ()
	{
		WireSystem script = (WireSystem) target;
		GameObject wireObj = new GameObject();
		for(int i = 0; i < script.poles.Count-1; i++)
		{
			//first we define our current and next pole, then we pool wire points so that we can make connections
			Transform thisPole = script.poles[i];
			Transform nextPole = script.poles[i+1];
			if(script.stopPointsAreWires)
			{
				StringWire (thisPole.position, nextPole.position, wireObj);
			}
			else
			{
				List<Transform> wirePointPoolA = new List<Transform>();
				List<Transform> wirePointPoolB = new List<Transform>();
				foreach (Transform possiblePool in thisPole.GetComponentsInChildren<Transform>())
				{
					if(possiblePool.CompareTag("WirePoint"))
					{
						wirePointPoolA.Add (possiblePool);
					}
				}
				foreach (Transform possiblePool in nextPole.GetComponentsInChildren<Transform>())
				{
					if(possiblePool.CompareTag("WirePoint"))
					{
						wirePointPoolB.Add (possiblePool);
					}
				}
				if(wirePointPoolA.Count != wirePointPoolB.Count)
				{
					Debug.Log ("Error was found at pole " + i + ", the wire points are not equal!");
					return;
				}
				//This is for a script.simple system that creates taut wires
				if(script.simple)
				{
					for(int j = 0; j < wirePointPoolA.Count; j++)
					{
						StringWire (wirePointPoolA[j].position, wirePointPoolB[j].position, wireObj);
					}

				}
				else
				{
					for(int j = 0; j < wirePointPoolA.Count; j++)
					{
						StringWire (wirePointPoolA[j].position, wirePointPoolB[j].position, wireObj);
					}
				}
			}
		}
		DestroyImmediate (wireObj);
		Debug.Log("Wires succesfully routed");
	}
	void CreateCircle (Transform obj, Vector3 pos, ref List<Vector3> vertices, ref List<int> faces, ref List<Vector2> uvs)
	{
		WireSystem script = (WireSystem) target;
		for(int rot = 0; rot < script.circleVertices; rot++)
		{
			vertices.Add(pos + (obj.right*(script.wireWidth/2)));
			uvs.Add (new Vector2(0, 1));
			obj.Rotate(0, 0, 360/script.circleVertices);
		}
	}
	void ConnectAll(ref List<Vector3> vertices, ref List<int> faces, ref List<Vector2> uvs)
	{
		WireSystem script = (WireSystem) target;
		int numberOfPoints = (vertices.Count) / script.circleVertices;
		numberOfPoints -= 1;
		for(int i = 0; i < numberOfPoints; i++)
		{
			for(int j = 0; j < script.circleVertices; j++)
			{
				/*faces.Add ((i*script.circleVertices) + j);
				faces.Add ((i*script.circleVertices) + j + 1);
				faces.Add (((i + 1)*script.circleVertices) + j);
				faces.Add (((i + 1)*script.circleVertices) + j);
				faces.Add ((i*script.circleVertices) + j + 1);
				faces.Add (((i + 1)*script.circleVertices) + j + 1);*/

				if(j < script.circleVertices-1)
				{
				faces.Add ((i*script.circleVertices) + j);
				faces.Add ((i*script.circleVertices) + j + 1);
				faces.Add (((i + 1)*script.circleVertices) + j);
				faces.Add (((i + 1)*script.circleVertices) + j);
				faces.Add ((i*script.circleVertices) + j + 1);
				faces.Add (((i + 1)*script.circleVertices) + j + 1);
				}
				else
				{
					faces.Add ((i*script.circleVertices) + j );
					faces.Add ((i*script.circleVertices) );
					faces.Add (((i + 1)*script.circleVertices) + j);
					faces.Add (((i + 1)*script.circleVertices) + j);
					faces.Add ((i*script.circleVertices) );
					faces.Add (((i + 1)*script.circleVertices) );
				}
		 	}
		}
	}
	void StringWire (Vector3 start, Vector3 end, GameObject empty)
	{
		WireSystem script = (WireSystem) target;
		GameObject newWire = (GameObject)Instantiate (empty, script.poles[0].position, Quaternion.identity);
		newWire.transform.name = "Wire";
		if(script.lineRendererWires)
		{
			LineRenderer newLine = newWire.AddComponent<LineRenderer>();
			if(script.simple)
			{
				newLine.SetVertexCount(2);
				newLine.material = script.wireMaterial;
				newLine.useWorldSpace = true;
				newLine.SetWidth(script.wireWidth, script.wireWidth);
				newLine.SetPosition(0, start);
				newLine.SetPosition(1, end);
			}
			else
			{
				newLine.SetVertexCount(script.numberOfVertices);
				newLine.material = script.wireMaterial;
				newLine.useWorldSpace = true;
				newLine.SetWidth(script.wireWidth, script.wireWidth);
				float sagDistCurrent = script.wireSag + UnityEngine.Random.Range (-script.wireSagRandom, script.wireSagRandom);
				for(int v = 0; v < script.numberOfVertices; v++)
				{
					Vector3 difference = end - start;
					float normalize = (float)v/(float)(script.numberOfVertices-1);
					difference *= normalize;

					Vector3 sag = Vector3.up*(-Mathf.Sin(normalize*Mathf.PI)*sagDistCurrent);
					newLine.SetPosition(v, start + difference + sag);
				}
			}
		}
		//otherwise we create mesh wires
		else
		{
			newWire.AddComponent<MeshFilter>();
			Mesh mesh = new Mesh();
			List<Vector3> vertices = new List<Vector3>(mesh.vertices);
			List<int> faces = new List<int>(mesh.triangles);
			List<Vector2> uvs = new List<Vector2>(mesh.uv);
			if(script.simple)
			{
				GameObject lookComponent = (GameObject)Instantiate (empty, start, Quaternion.identity);
				lookComponent.transform.LookAt(end);
				newWire.transform.position = Vector3.zero;
				CreateCircle(lookComponent.transform, start, ref vertices, ref faces, ref uvs);
				CreateCircle(lookComponent.transform, end, ref vertices, ref faces, ref uvs);
				ConnectAll(ref vertices, ref faces, ref uvs);
				DestroyImmediate(lookComponent);
			}
			else
			{
				GameObject lookComponent = (GameObject)Instantiate (empty, start, Quaternion.identity);
				lookComponent.transform.LookAt(end);
				newWire.transform.position = Vector3.zero;
				float sagDistCurrent = script.wireSag + UnityEngine.Random.Range (-script.wireSagRandom, script.wireSagRandom);
				for(int v = 0; v < script.numberOfVertices; v++)
				{

					Vector3 difference = end - start;
					float normalize = (float)v/(float)(script.numberOfVertices-1);
					difference *= normalize;
					
					Vector3 sag = Vector3.up*(-Mathf.Sin(normalize*Mathf.PI)*sagDistCurrent);
					CreateCircle(lookComponent.transform, start + difference + sag, ref vertices, ref faces, ref uvs);
				}
				ConnectAll(ref vertices, ref faces, ref uvs);
				DestroyImmediate(lookComponent);
			}
			mesh.vertices = vertices.ToArray();
			mesh.triangles = faces.ToArray();
			mesh.uv = uvs.ToArray ();
			mesh.RecalculateNormals ();
			newWire.GetComponent<MeshFilter>().mesh = mesh;
			if(script.wiresAreCloth)
			{
				//InteractiveCloth cloth = (InteractiveCloth)newWire.AddComponent<InteractiveCloth>();
				//cloth.mesh = mesh;
				//GameObject collideone = (GameObject)Instantiate (empty, start, Quaternion.identity);
				//collideone.transform.name = "Cloth Collider";
				//collideone.transform.parent = script.poles[0];
				//collideone.AddComponent<SphereCollider>();
				//collideone.GetComponent<SphereCollider>().radius = script.wireWidth+ 0.02f;
				//collideone.AddComponent<Rigidbody>();
				//collideone.rigidbody.isKinematic = true;
				//GameObject collidetwo = (GameObject)Instantiate (empty, end, Quaternion.identity);
				//collidetwo.transform.name = "Cloth Collider";
				//collidetwo.transform.parent = script.poles[0];
				//collidetwo.AddComponent<SphereCollider>();
				//collidetwo.GetComponent<SphereCollider>().radius = script.wireWidth+ 0.02f;
				////cloth.AttachToCollider(collideone.collider, true, false);
				////cloth.AttachToCollider(collidetwo.GetComponent<SphereCollider>());
				//ClothRenderer renderer = (ClothRenderer)newWire.AddComponent<ClothRenderer>();
				//renderer.material = script.wireMaterial;
			}
			else
			{
				MeshRenderer renderer = (MeshRenderer)newWire.AddComponent<MeshRenderer>();
				renderer.material = script.wireMaterial;
			}
		}
        newWire.AddComponent<WireHandler>();
		newWire.transform.parent = script.poles[0];
	}
	void ClearRoutes ()
	{
		WireSystem script = (WireSystem) target;
		foreach (Transform child in script.transform.gameObject.GetComponentsInChildren<Transform>())
		{
			if(child.name == "Wire" || child.name == "Cloth Collider")
			{
				DestroyImmediate (child.gameObject);
			}
		}
	}
	void MoveIndexUp (int index)
	{
		WireSystem script = (WireSystem) target;
		if(index != script.poles.Count)
		{
			Transform oldPole = script.poles[index];
			script.poles.RemoveAt(index);
			script.poles.Insert(index+1, oldPole);
		}
	}
	void MoveIndexDown (int index)
	{
		WireSystem script = (WireSystem) target;
		if(index != 0)
		{
			Transform oldPole = script.poles[index];
			script.poles.RemoveAt(index);
			script.poles.Insert(index-1, oldPole);
		}
	}
	void GroundAllPoles ()
	{
		WireSystem script = (WireSystem) target;
		for(int i = 0; i < script.poles.Count; i++)
		{
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(script.poles[i].position + new Vector3(0, 8.0f, 0), -Vector3.up, out hit))
			{
				script.poles[i].position += new Vector3(0, 8.0f - hit.distance, 0);
			}
		}
	}
}
