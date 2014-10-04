using UnityEngine;
using System.Collections;
using MathTools;

public class player : MonoBehaviour {
	public Transform tubeTrans;
	public float speed;
	public float tubeSpeed;
	public float fireRateLaser;
	public float laserSpeed;
//	public float fireRateMisile;
	public Transform shotSpawn;
	public GameObject laser;
	private float nextFireLaser=0f;
//	private float nextFireMisile=0f;
	private float theta;
	private Vector3 nexPos;
	private GameObject shoot;
	private Vector3 cartPosition;

	CoordSystem.Cylindrical cylCoord;
	// Use this for initialization
	void Start () {
		cylCoord = new CoordSystem.Cylindrical ();
		cylCoord.theta = Mathf.PI/2;
		cylCoord.z = transform.position.z;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1") && Time.time > nextFireLaser) {
			shoot = Instantiate(laser, shotSpawn.position, Camera.main.transform.rotation) as GameObject;
			bullet shootParam = shoot.GetComponent<bullet>();
			shootParam.direction = Camera.main.transform.forward;
			shootParam.speed = laserSpeed;
			shootParam.transform.parent = transform;
			nextFireLaser = Time.time + fireRateLaser;
//			RaycastHit hit;
//			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//			if (Physics.Raycast(ray, out hit) && Time.time > nextFireLaser) {
//				nextFireLaser = Time.time + fireRateLaser;
//				shoot = Instantiate(laser, shotSpawn.position, transform.rotation) as GameObject;
//				bullet shootParam = shoot.GetComponent<bullet>();
//				shootParam.startAngle = cylCoord.theta;
//				shootParam.endAngle =  MathTools.CoordSystem.CartesianToCylindric();
//				shootParam.duration =  1;
//				shootParam.speed = laserSpeed;
//				shootParam.transform.parent = transform;
//			}
		}

		cylCoord.theta += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		if(cylCoord.theta  >  Mathf.PI/2 + Mathf.PI/6){
			cylCoord.theta = Mathf.PI/2 + Mathf.PI/6;
			tubeTrans.transform.Rotate(-Vector3.forward*tubeSpeed*Time.deltaTime);
		}
		else if(cylCoord.theta < Mathf.PI/2 - Mathf.PI/6) {
			cylCoord.theta = Mathf.PI/2 - Mathf.PI/6;
			tubeTrans.transform.Rotate(Vector3.forward*tubeSpeed*Time.deltaTime);
		}

		Debug.DrawRay(transform.position, new Vector3(-Mathf.Cos(cylCoord.theta),-Mathf.Sin(cylCoord.theta),0) *1000, Color.white);

		//cartPosition = tube.getPosOnTubeMesh(cylCoord.theta,cylCoord.z);
		//transform.position = new Vector3(cartPosition.x, cartPosition.y,transform.position.z);
		//transform.Translate(cartPosition);

//		if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space))  && Time.time > nextFireLaser) 
//		{
//			nextFireLaser = Time.time + fireRateLaser;
//			//Instantiate(laser, shotSpawn.position, transform.rotation);
//		}
//		if ((Input.GetButton("Fire2") || Input.GetKeyDown(KeyCode.Space))  && Time.time > nextFireMisile) 
//		{
//			nextFireMisile = Time.time + fireRateMisile;
//			//Instantiate(misile, shotSpawn.position, transform.rotation);
//		}
//		Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
//		transform.Translate(movement * speed * Time.deltaTime);
	}
	void OnCollisionEnter() {

	}
}
