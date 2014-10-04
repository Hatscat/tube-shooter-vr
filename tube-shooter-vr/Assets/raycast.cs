using UnityEngine;
using System.Collections;

public class spaceship : MonoBehaviour {
	public GameObject bullet_model;
	public GameObject laser_particle;
	
	private float _speed;
	private Vector3 _sun_pos;
	private Camera _cam;
	
	void Start () {
		_speed = 1f;
		_cam = Camera.main;
		_sun_pos = Vector3.zero;
	}
	
	void Update () {
		
		rigidbody.velocity = Vector3.zero;
		
		float _distance2z0 = Vector3.Distance(_cam.transform.position, _sun_pos);
		Vector3 _mouse_pos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distance2z0));
		transform.LookAt(_mouse_pos);
		
		if (Vector3.Distance(transform.position, _mouse_pos) > 0.6f) {
			transform.Translate(Vector3.forward * _speed * Time.deltaTime);
		}
		
		if (Input.GetMouseButtonDown(0)) {
			Instantiate(bullet_model, transform.position + transform.forward * 0.3f, transform.rotation);
		} else if (Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
			Ray ray = new Ray(Camera.main.transform.position, transform.forward);
			if (Physics.Raycast(ray, out hit)) {
				Instantiate(laser_particle, transform.position, transform.rotation);
				//Debug.DrawLine(transform.position, hit.transform.position, Color.red, 0.5f, true);
				hit.collider.SendMessage("hit", null, SendMessageOptions.DontRequireReceiver);
			}
			
		}
	}
}
