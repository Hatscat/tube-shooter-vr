using UnityEngine;
using System.Collections;
using MathTools;

public class tube : MonoBehaviour {

	public static int nSegmentsX;
	public static int nSegmentsZ;
	public static float length;
	public static float radius;
	public static float radius_offset;
	public static float noise_x;
	public static float noise_z;
	public static float rand_seed;
	public static float rho_shift;
	
	private Mesh _tube_mesh;
	private Vector3[] _tube_vertices;

	void Start () {

		nSegmentsX = 50;
		nSegmentsZ = 100;
		length = 100f;
		radius = 2f;
		radius_offset = 1.3f;
		noise_x = 5f;
		noise_z = 5f;
		rand_seed = Random.value;
		rho_shift = 0.7f;

		GetComponent<MeshFilter>().mesh = _tube_mesh = createTube();
		_tube_vertices = _tube_mesh.vertices;
	}
	
	void Update () {
	
	}

	public static Vector3 getPosOnTubeMesh (float theta, float z) {

		int i = (int)(theta * nSegmentsX / (Mathf.PI * 2) * (nSegmentsX + 1));
		int j = (int)(z / length + 0.5f);
		
		return CoordSystem.CylindricToCartesianB(
			new CoordSystem.CylindricalB(
				getRho(i, nSegmentsX, j, nSegmentsZ),
				getRhoShift(j, nSegmentsZ),
				i * Mathf.PI * 2 / nSegmentsX,
				((float) j / (nSegmentsZ+1)) * length)
			);
	}

	public static int getVerticesIndex (int i, int j, int maxSegZ) {

		return j + (i * maxSegZ);
	}


	public static float getRho (int i, int maxSegX, int j, int maxSegZ) {

		maxSegX++;
		maxSegZ++;

		return radius*(radius_offset + Mathf.PerlinNoise(Mathf.Abs(Mathf.Cos((float)i/maxSegX*Mathf.PI))*noise_x + rand_seed, Mathf.Abs(Mathf.Cos((float)j/maxSegZ*Mathf.PI))*noise_z + rand_seed));
	}

	public static Vector2 getRhoShift (int z, int maxSegZ) {

		maxSegZ++;

		return new Vector2(Mathf.PerlinNoise((float)z/maxSegZ,rand_seed)*z*rho_shift, Mathf.PerlinNoise(rand_seed,(float)z/maxSegZ)*z*rho_shift);
	}
	
	Mesh createTube () {
		
		Mesh mesh = new Mesh();
		mesh.name = "tube";
		
		int nSegmentsX1 = nSegmentsX + 1;
		int nSegmentsZ1 = nSegmentsZ + 1;
		
		Vector3[] vertices = new Vector3[nSegmentsX1 * nSegmentsZ1];
		//Vector3[] normals = new Vector3[3];
		int[] triangles = new int[nSegmentsX * nSegmentsZ * 3 * 3];
		Vector2[] uv = new Vector2[nSegmentsX1 * nSegmentsZ1];
		
		//les vertices & uv

		int index = 0;
		for (int i = 0; i < nSegmentsX1; i++) {
			for (int j = 0; j < nSegmentsZ1; j++) {
				
				vertices[index] = CoordSystem.CylindricToCartesianB(
					new CoordSystem.CylindricalB(
						getRho(i, nSegmentsX, j, nSegmentsZ),
						getRhoShift(j, nSegmentsZ),
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
