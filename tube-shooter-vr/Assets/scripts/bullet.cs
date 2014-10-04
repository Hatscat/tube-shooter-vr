using UnityEngine;
using System.Collections;
using MathTools;
public class bullet : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public AnimationCurve spline;
	public float startAngle;
	public float endAngle;
	public float duration;
	CoordSystem.Cylindrical cylCoord;
	private float timer=0;
	void Start () {
		cylCoord = new CoordSystem.Cylindrical ();
	}
	// Update is called once per frame
	void Update () {
		cylCoord.z += speed * Time.deltaTime;
		cylCoord.theta = Mathf.Lerp(startAngle,endAngle,spline.Evaluate(timer/duration));
		timer+=Time.deltaTime;
		Debug.Log (cylCoord.theta);
		//transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
		if(!transform.GetChild(0).renderer.isVisible) {
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider col){
		if(col.transform.name != "tube" && col.transform.name != transform.parent.name) {
			Destroy(gameObject);
			col.collider.SendMessage("hit", null, SendMessageOptions.DontRequireReceiver);
		}
	}

}
