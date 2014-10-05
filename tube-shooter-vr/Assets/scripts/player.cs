using UnityEngine;
using System.Collections;
using MathTools;

public class player : MonoBehaviour {
	public Transform tubeTrans;
	public Transform shotSpawn;
	public GameObject laser;
	public ParticleSystem explosion;
	public float speed;
	public float tubeSpeed;
	public float fireRateLaser;
	public float laserSpeed;
	//public float fireRateMisile;
	//public int NbmaxLock;
	private float nextFireLaser=0f;
	//private float nextFireMisile=0f;
	private GameObject shoot;
	private Vector3 cartPosition;
	private float rotate=0;

	CoordSystem.Cylindrical cylCoord;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y+0.35f,Camera.main.transform.position.z+0.3f);
		transform.rotation = Camera.main.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1") && Time.time > nextFireLaser && gameObject.renderer.enabled) {
			shoot = Instantiate(laser, shotSpawn.position, Quaternion.Euler(new Vector3(Camera.main.transform.rotation.x,Camera.main.transform.rotation.y+90,Camera.main.transform.rotation.z))) as GameObject;
			bullet shootParam = shoot.GetComponent<bullet>();
			shootParam.direction = Camera.main.transform.forward;
			shootParam.speed = laserSpeed;
			shootParam.transform.parent = transform;
			nextFireLaser = Time.time + fireRateLaser;
		}
//		if(Input.GetButton("Fire2") && Time.time > nextFireMisile && gameObject.renderer.enabled) {
//			RaycastHit hit;
//			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//			if (Physics.Raycast(ray, out hit) && NbmaxLock < Nblocked){
//				//nextFireMisile = Time.time + fireRateMisile;
//				Nblocked++;
//				lockedPos[Nblocked-1] = shotSpawn.position;
//				lockedRot[Nblocked-1] = transform.rotation;
//			}
//		}
		rotate += Input.GetAxis("Horizontal") * speed*50 * Time.deltaTime;
		rotate = Mathf.Clamp(rotate,-25,25);
		transform.rotation = Quaternion.Euler(new Vector3(0,0, -rotate));
		tubeTrans.transform.Rotate(-Input.GetAxis("Horizontal")* Vector3.forward*tubeSpeed*Time.deltaTime);

	}
	void hit() {
		ParticleSystem system = Instantiate(explosion, transform.position, transform.rotation) as ParticleSystem;
		gameObject.renderer.enabled = false;
		transform.GetChild(0).renderer.enabled = false;
		gameObject.collider.enabled = false;
		system.transform.parent = transform;
		
	}
}
