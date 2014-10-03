using UnityEngine;
using System.Collections;
using MathTools;

public class tube : MonoBehaviour {
	
	void Start () {
		GetComponent<MeshFilter>().mesh = createTube1();
	}
	
	void Update () {
	
	}

	Mesh createTube1 () {

		Mesh mesh = new Mesh();
		mesh.name = "tube";
		
		int nSegments = 40;

		Vector3[] vertices = new Vector3[(nSegments + 1) * 2];
		//Vector3[] normals = new Vector3[3];
		int[] triangles = new int[nSegments * 2 * 3];
		Vector2[] uv = new Vector2[(nSegments + 1) * 2];
		
		float height = 50f;
		float radius = 2f;
		
		//les vertices & uv
		for (int i = 0; i < nSegments+1; i++) {
			vertices[i] = CoordSystem.CylindricToCartesianB(new CoordSystem.Cylindrical(radius, i * 2 * Mathf.PI / nSegments, height));
			vertices[i + nSegments + 1] = CoordSystem.CylindricToCartesianB(new CoordSystem.Cylindrical(radius, i * 2 * Mathf.PI / nSegments, 0));
		}

		//les triangles	
		int index = 0;
		for (int i = 0; i < nSegments; i++) {
			triangles [index++] = i;
			triangles [index++] = i + 1;
			triangles [index++] = i + nSegments + 1;
			triangles [index++] = i + 1;
			triangles [index++] = nSegments + 1 + i + 1;
			triangles [index++] = i + nSegments + 1;
		}
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//mesh.normals = normals;
		mesh.uv = uv;

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		
		return mesh;
	}

	Mesh createTube2 () {
		
		Mesh mesh = new Mesh();
		mesh.name = "tube";

		int nSegmentsX = 50;
		int nSegmentsZ = 50;
		
		Vector3[] vertices = new Vector3[(nSegmentsX + 1) * (nSegmentsZ + 1)];
		//Vector3[] normals = new Vector3[3];
		int[] triangles = new int[nSegmentsX * nSegmentsZ * 2 * 3];
		Vector2[] uv = new Vector2[(nSegmentsX + 1) * (nSegmentsZ + 1)];
		
		float length = 8f;
		float radius = 6f;
		
		//les vertices & uv
		int index = 0;
		for (int i = 0; i < nSegmentsX+1; i++) {
			for (int j = 0; j < nSegmentsZ+1; j++) {
				vertices[index] = CoordSystem.CylindricToCartesianB(new CoordSystem.Cylindrical(radius, i * 2 * Mathf.PI / (nSegmentsX * nSegmentsZ), length));
				index++;
			}
		}
		
		//les triangles	
		index = 0;
		for (int i = 0; i < nSegmentsX; i++) {
			for (int j = 0; j < nSegmentsZ; j++) {
				triangles [index++] = i*(nSegmentsZ+1)+j;
				triangles [index++] = i*(nSegmentsZ+1)+j+1;
				triangles [index++] = (i+1)*(nSegmentsZ+1)+j;
				
				triangles [index++] = i*(nSegmentsZ+1)+j+1;
				triangles [index++] = (i+1)*(nSegmentsZ+1)+j+1;
				triangles [index++] = (i+1)*(nSegmentsZ+1)+j;
			}
		}
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//mesh.normals = normals;
		mesh.uv = uv;
		
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		
		return mesh;
	}
}
