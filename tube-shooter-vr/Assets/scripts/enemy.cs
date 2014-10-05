using UnityEngine;
using System.Collections;
using MathTools;
public class enemy : MonoBehaviour {
	public GameObject laser;
	public ParticleSystem explosion;
	public float laserSpeed;
	public float fireRate;
	public float speed; //speed on Z axis
	public AnimationCurve spline; // curve of theta change
	public float startAngle; // theta start
	public float endAngle; // theta end
	public float duration; // time to go from theta start to theta end in seconds
	public bool loop;
	public bool revert; // revert back from thetaEnd to thetaEnd
	private float timer=0;
	private bool reverted=false;
	private float nextFire=0;
	private GameObject shoot;

	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextFire) {
			shoot = Instantiate(laser, transform.position, transform.rotation) as GameObject;
			bullet shootParam = shoot.GetComponent<bullet>();
			shootParam.direction = Camera.main.transform.forward;
			shootParam.speed = laserSpeed;
			shootParam.transform.parent = transform;
			nextFire = Time.time + fireRate;

		}
		//transform.RotateAround(transform.parent.position,Vector3.forward,Mathf.Lerp(startAngle,endAngle,spline.Evaluate(timer/duration)));
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		//timer+=Time.deltaTime;
//		if(cylCoord.theta==endAngle) {
//			if(revert && (!reverted || loop)) {
//				float temp = endAngle;
//				endAngle = startAngle;
//				startAngle = temp;
//				timer=0;
//				reverted=true;
//			}
//			else if(loop){
//				timer=0;
//			}
//		}
		if(!transform.renderer.isVisible) {
			Destroy(gameObject);
		}
	}
	void hit() {
		ParticleSystem system = Instantiate(explosion, transform.position, transform.rotation) as ParticleSystem;
		system.transform.parent = transform.parent;
		Destroy(gameObject);

	}
}
