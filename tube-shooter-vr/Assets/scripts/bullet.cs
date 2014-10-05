using UnityEngine;
using System.Collections;
using MathTools;
public class bullet : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public Vector3 direction;
	public float duration;
	private float timer=0;
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
		if(!transform.renderer.isVisible) {
			Destroy(gameObject);
		}
		else if(Vector3.Distance(transform.parent.position, transform.position) >= 10) {
			Destroy(gameObject);
		}

	}
	void OnTriggerEnter(Collider col){
		if(col.transform.name != transform.parent.name) {
			Destroy(gameObject);
			col.collider.SendMessage("hit", null, SendMessageOptions.DontRequireReceiver);
		}
	}

}
