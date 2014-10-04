using UnityEngine;
using System.Collections;
using MathTools;

public class player : MonoBehaviour {
	public Transform tube;
	public float speed;
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

	CoordSystem.Cylindrical cylCoord;
	// Use this for initialization
	void Start () {
		cylCoord = new CoordSystem.Cylindrical ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")) {
			RaycastHit hit;
			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			if (Physics.Raycast(ray, out hit) && Time.time > nextFireLaser) {
				nextFireLaser = Time.time + fireRateLaser;
				shoot = Instantiate(laser, shotSpawn.position, transform.rotation) as GameObject;
				bullet shootParam = shoot.GetComponent<bullet>();
				shootParam.startAngle = cylCoord.theta;
				shootParam.endAngle =  180; //tocyldyndre(hit.point).theta;
				shootParam.duration =  1;
				shootParam.speed = laserSpeed;
				shootParam.transform.parent = transform;
			}
		}

		cylCoord.theta += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		if(cylCoord.theta > 10) {
			cylCoord.theta = 10;
			tube.transform.Rotate(Vector3.forward*speed*Time.deltaTime);
		}
		else if(cylCoord.theta < -10) {
			cylCoord.theta = -10;
			tube.transform.Rotate(-Vector3.forward*speed*Time.deltaTime);
		}
		//transform.Translate(MathTools.CoordSystem.CylindricToCartesian(cylCoord));

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
