using UnityEngine;
using System.Collections;
using MathTools;

public class getPosOnTube : MonoBehaviour {

	public Mesh tube;
	private Vector3[] _tube_vertices;

	void Start () {
		_tube_vertices = tube.vertices;
	}

	void Update () {
	
	}

	public Vector3 getPosOnTubeMesh (float theta, float z) {

		//float rho = 
		
		//return CoordSystem.CylindricToCartesian();
		return Vector3.zero;
	}
}
