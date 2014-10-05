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
	public static float update_deltaTime;
	public static Mesh _tube_mesh;

	private CoordSystem.CylindricalB[] _tube_cyl_vertices;
	private Vector2[] rhoShiftArray;
	private float _update_timer;
	//private float _speed;
	private ulong _step;
	//private Vector3 _target;
	//private float _segRatio;

	void Start () {

		nSegmentsX = 24;
		nSegmentsZ = 24;
		length = 90f;
		radius = 2f;
		radius_offset = 1.3f;
		noise_x = 1.8f;
		noise_z = 2f;
		rand_seed = Random.value;
		rho_shift = 0.2f;

		update_deltaTime = _update_timer = 0.04f;
		//_speed = 30f;
		_step = 0;
		//_target = Vector3.zero;
		//_segRatio = length / (nSegmentsZ + 1);

		rhoShiftArray = getAllNewRhoShift((ulong)Mathf.Pow(length, 2));
		GetComponent<MeshFilter>().mesh = _tube_mesh = createTube();
	}
	
	void Update () {

		_update_timer -= Time.deltaTime;

		if (_update_timer < 0) {
			_update_timer = update_deltaTime;
			animateTube();
		}
		//Vector2 s1 = rhoShiftArray[(_step + 2) % (ulong)rhoShiftArray.Length] * 4;
		//Vector2 s2 = rhoShiftArray[(_step + 3) % (ulong)rhoShiftArray.Length] * 9;

		//transform.Rotate(new Vector3(s.x*10, s.y*10, 0));

//		int j = 3;
//
//		if (transform.position.z <= -_segRatio) {
//			animateTube();
//			transform.position = Vector3.zero;
//		}
//
//
//		Vector3 target1 = CoordSystem.CylindricToCartesianB(new CoordSystem.CylindricalB(0,rhoShiftArray[(_step + 1)%(ulong)rhoShiftArray.Length],0,_segRatio));
//		Vector3 target2 = CoordSystem.CylindricToCartesianB(new CoordSystem.CylindricalB(0,rhoShiftArray[(_step +(ulong)j+(ulong)j)%(ulong)rhoShiftArray.Length]*j*j,0,_segRatio*j));
//
//		//Vector3 target1 = new Vector3(s1.x * r, s1.y * r, r);
//		//Vector3 target2 = new Vector3(s2.x *r, s2.y *r, 2 * r);
//
//
//		//target1 -= transform.position;
//		//target2 -= transform.position;
//
//		transform.GetChild(0).localPosition = target2;
//
//		//Debug.DrawLine(Vector3.zero + transform.position, target1, Color.red);
//		//Debug.DrawLine(Vector3.zero, target1, Color.red);
//		//Debug.DrawLine(target1, target2, Color.red);
//		//transform.LookAt(sphere.position);
//
//		Debug.DrawLine(transform.position, transform.GetChild(0).position, Color.red);
//
//
//		//transform.RotateAround(sphere.position, Vector3.back, _speed * Time.deltaTime);
//
//		//transform.rotation = Quaternion.Slerp(transform.rotation, target_quat, _speed * Time.deltaTime);
//		//Quaternion rotation = Quaternion.LookRotation(target2 - target1);
//		//transform.rotation = rotation;
//		//transform.Rotate(target * _speed * Time.deltaTime);
//
//
//
//		// transform.Translate(new Vector3(-s.x, -s.y, -1) * _speed * Time.deltaTime);
//
//		if (transform.position.z <= -r) {
//		//if (_update_timer < 0) {
//			//_update_timer = update_deltaTime;
//			//_step++;
//			animateTube();
//			transform.position = Vector3.zero;
//			//transform.GetChild(0).localPosition = CoordSystem.CylindricToCartesianB(new CoordSystem.CylindricalB(0,rhoShiftArray[(_step +(ulong)j+(ulong)j)%(ulong)rhoShiftArray.Length]*j*j,0,r*j));;
//			//_target = transform.GetChild(0).position;
//
//			//transform.GetChild(0).localPosition = Vector3.zero;
//			//sphere.transform.position = Vector3.zero;
//			//transform.rotation = Quaternion.identity;
//		}
//		//} else {
//			//transform.LookAt(_target);
//
//			//Camera.main.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.GetChild(0).position), _speed * j * Time.deltaTime);
//		//transform.GetChild(0).localRotation = Quaternion.identity;
//
//			//transform.Translate(Vector3.back * _speed * Time.deltaTime);
//			//transform.Translate(Vector3.Slerp(transform.position,
//			 //                                 target1 * -1,
//			 //                                 _speed) * _speed * Time.deltaTime);
//			transform.Translate(target1.normalized * -_speed * Time.deltaTime);
//			Camera.main.transform.LookAt(transform.GetChild(0).position);
//		//}
		//print(getPosOnTubeMesh(Mathf.PI, 2.5f));

	}

	public static Vector3 getPosOnTubeMesh (float theta, float z) {

		float i_f = theta / (Mathf.PI * 2) * nSegmentsX;
		float j_f = z / length * nSegmentsZ;
		//Debug.Log (z);
		//Debug.Log (j_f);
		int[] i_s = new int[2] {(int)i_f, (int)i_f+1};
		int[] j_s = new int[2] {(int)j_f, (int)j_f+1};
		//Debug.Log(i_s[0]);
		//Debug.Log(i_s[1]);
		//Debug.Log(j_s[0]);
		//Debug.Log(j_s[1]);
		//Debug.DrawLine(_tube_mesh.vertices[j_s[0]+(i_s[1]*(nSegmentsZ+1))], _tube_mesh.vertices[j_s[1]+(i_s[0]*(nSegmentsZ+1))], Color.red);
		return Vector3.Lerp(_tube_mesh.vertices[j_s[0]+(i_s[1]*(nSegmentsZ+1))], _tube_mesh.vertices[j_s[1]+(i_s[0]*(nSegmentsZ+1))], (i_f-i_s[0])*(j_f-j_s[0]));
	}

//	public static int getVerticesIndex (int i, int j, int maxSegZ) {
//
//		return j + (i * maxSegZ);
//	}


	public static float getRho (int i, int maxSegX, int j, int maxSegZ) {

		//maxSegX++;
		//maxSegZ++;

		return radius*(radius_offset + Mathf.PerlinNoise(Mathf.Abs(Mathf.Cos((float)i/maxSegX*Mathf.PI))*noise_x + rand_seed, Mathf.Abs(Mathf.Cos((float)j/maxSegZ*Mathf.PI))*noise_z + rand_seed));
	}

//	public static Vector2 getRhoShift (int j, int maxSegZ) {
//
//		//maxSegZ++;
//
//		return new Vector2(Mathf.PerlinNoise((float)j/maxSegZ,rand_seed)*rho_shift, Mathf.PerlinNoise(rand_seed,(float)j/maxSegZ)*rho_shift).normalized * j;
//	}

	public static Vector2[] getAllNewRhoShift (ulong l) {
		Vector2[] rhoShiftArray = new Vector2[l];
		for (ulong i = 0; i < l; i++) {
			rhoShiftArray[i] = new Vector2((Mathf.PerlinNoise((float)i/99,rand_seed)*2-1)*rho_shift, (Mathf.PerlinNoise(rand_seed,(float)i/99)*2-1)*rho_shift);
		}
		return rhoShiftArray;
	}
	
	Mesh createTube () {
		
		Mesh mesh = new Mesh();
		mesh.name = "tube";
		
		int nSegmentsX1 = nSegmentsX + 1;
		int nSegmentsZ1 = nSegmentsZ + 1;
		
		Vector3[] vertices = new Vector3[nSegmentsX1 * nSegmentsZ1];
		_tube_cyl_vertices = new CoordSystem.CylindricalB[nSegmentsX1 * nSegmentsZ1];
		Vector3[] normals = new Vector3[nSegmentsX1 * nSegmentsZ1];
		int[] triangles = new int[nSegmentsX * nSegmentsZ * 3 * 3];
		Vector2[] uv = new Vector2[nSegmentsX1 * nSegmentsZ1];
		
		//les vertices & uv

		int index = 0;
		for (int i = 0; i < nSegmentsX1; i++) {
			for (int j = 0; j < nSegmentsZ1; j++) {

				_tube_cyl_vertices[index] = new CoordSystem.CylindricalB(
					getRho(i, nSegmentsX, j, nSegmentsZ),
					rhoShiftArray[(ulong)j % (ulong)rhoShiftArray.Length] * j * j,
					i * Mathf.PI * 2 / nSegmentsX,
					((float) j / nSegmentsZ1) * length);

				vertices[index] = CoordSystem.CylindricToCartesianB(_tube_cyl_vertices[index]);
				uv[index] = new Vector2((float)i / nSegmentsX, (float)j / nSegmentsZ);
				normals[index] = Random.onUnitSphere;
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
					triangles[index++] = j;
					
					triangles[index++] = (i+1) * nSegmentsZ1 + j + 1;
					triangles[index++] = j + 1;
					triangles[index++] = j;
				}
			}
		}
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		
		return mesh;
	}

	void animateTube () {

		int nSegmentsX1 = nSegmentsX + 1;
		int nSegmentsZ1 = nSegmentsZ + 1;

		Vector3[] _tube_new_vertices = new Vector3[_tube_mesh.vertices.Length];
		CoordSystem.CylindricalB[] _tube_new_cyl_vertices = new CoordSystem.CylindricalB[_tube_cyl_vertices.Length];
		Vector2[] _tube_new_uv = new Vector2[_tube_mesh.uv.Length];

		_step = _step < (ulong)rhoShiftArray.Length ? _step + 1 : 0;

		int index = 0;
		for (int i = 0; i < nSegmentsX1; i++) {
			for (int j = 0; j < nSegmentsZ1; j++) {

				_tube_new_cyl_vertices[index] = new CoordSystem.CylindricalB(
					_tube_cyl_vertices[(index + 1) % _tube_mesh.vertices.Length].rho,
					rhoShiftArray[(_step + (ulong)j) % (ulong)rhoShiftArray.Length] * j * j,
					i * Mathf.PI * 2 / nSegmentsX,
					((float) j / nSegmentsZ1) * length);
					
				_tube_new_vertices[index] = CoordSystem.CylindricToCartesianB(_tube_new_cyl_vertices[index]);
//				if( index + 1 < _tube_mesh.uv.Length) {
//					_tube_new_uv[index] = _tube_mesh.uv[index + 1];
//				} else {
//					//_tube_new_uv[index-1] = Vector2.zero;//_tube_mesh.uv[_tube_mesh.uv.Length-1];
//					_tube_new_uv[index-1] = new Vector2(_tube_mesh.uv[0].y, _tube_mesh.uv[0].x);
//				}
				_tube_new_uv[index] = _tube_mesh.uv[(index + 1) % (_tube_mesh.uv.Length/5)];
				//Debug.Log(_tube_mesh.uv[(index + 1) % _tube_mesh.uv.Length]);
				//_tube_new_uv[index] = new Vector2((float)i / nSegmentsX, (float)j / nSegmentsZ);
				index++;
			}
		}

		_tube_cyl_vertices = _tube_new_cyl_vertices.Clone() as CoordSystem.CylindricalB[];
		_tube_mesh.vertices = _tube_new_vertices.Clone() as Vector3[];
		_tube_mesh.uv = _tube_new_uv.Clone() as Vector2[];

		//_tube_mesh.RecalculateBounds();
		//GetComponent<MeshFilter>().mesh
		//_tube_mesh.RecalculateNormals();
	}

}
