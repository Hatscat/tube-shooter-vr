using UnityEngine;
using System.Collections;
using MathTools;

public class tube : MonoBehaviour {

	private Mesh _tube_mesh;
	private Vector3[] _tube_vertices;

	void Start () {

		GetComponent<MeshFilter>().mesh = _tube_mesh = createTube();
		_tube_vertices = _tube_mesh.vertices;
	}
	
	void Update () {
	
	}

	Mesh createTube () {
		
		Mesh mesh = new Mesh();
		mesh.name = "tube";
		
		int nSegmentsX = 50;
		int nSegmentsZ = 100;
		
		int nSegmentsX1 = nSegmentsX + 1;
		int nSegmentsZ1 = nSegmentsZ + 1;
		
		Vector3[] vertices = new Vector3[nSegmentsX1 * nSegmentsZ1];
		//Vector3[] normals = new Vector3[3];
		int[] triangles = new int[nSegmentsX * nSegmentsZ * 3 * 3];
		Vector2[] uv = new Vector2[nSegmentsX1 * nSegmentsZ1];
		
		float length = 100f;
		float radius = 2f;
		float radius_offset = 1.3f;
		
		//les vertices & uv
		int index = 0;
		float noise_x = 5f;
		float noise_z = 5f;
		float rand_seed = Random.value;
		float shift = 0.7f;
		
		for (int i = 0; i < nSegmentsX1; i++) {
			for (int j = 0; j < nSegmentsZ1; j++) {
				
				vertices[index] = CoordSystem.CylindricToCartesianB(
					new CoordSystem.CylindricalB(
					radius * (radius_offset + Mathf.PerlinNoise(Mathf.Abs(Mathf.Cos((float)i/nSegmentsX1*Mathf.PI)) * noise_x + rand_seed, Mathf.Abs(Mathf.Cos((float)j/nSegmentsZ1*Mathf.PI)) * noise_z + rand_seed)),
					new Vector2(Mathf.PerlinNoise((float)j/nSegmentsZ1,rand_seed)*j*shift, Mathf.PerlinNoise(rand_seed,(float)j/nSegmentsZ1)*j*shift),
					i * Mathf.PI * 2 / nSegmentsX,
					((float) j / nSegmentsZ1) * length)
					);
				index++;
			}
		}
		
		//les triangles	
		index = 0;
		for (int i = 0; i < nSegmentsX; i++) {
			for (int j = 0; j < nSegmentsZ; j++) {
				triangles[index++] = i * nSegmentsZ1 + j;
				triangles[index++] = i * nSegmentsZ1 + j + 1;
				triangles[index++] = (i+1) * nSegmentsZ1 + j;
				
				triangles[index++] = i * nSegmentsZ1 + j + 1;
				triangles[index++] = (i+1) * nSegmentsZ1 + j + 1;
				triangles[index++] = (i+1) * nSegmentsZ1 + j;

				if (i == nSegmentsX - 1) {

					triangles[index++] = (i+1) * nSegmentsZ1 + j;
					triangles[index++] = (i+1) * nSegmentsZ1 + j + 1;
					triangles[index++] = nSegmentsZ1 + j;
					
					triangles[index++] = i * nSegmentsZ1 + j + 1;
					triangles[index++] = nSegmentsZ1 + j + 1;
					triangles[index++] = (i+1) * nSegmentsZ1 + j;
				}
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
