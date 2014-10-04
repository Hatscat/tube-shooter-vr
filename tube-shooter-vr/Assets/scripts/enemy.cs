using UnityEngine;
using System.Collections;
using MathTools;
public class enemy : MonoBehaviour {
	public float speed;
	public AnimationCurve spline;
	public float startAngle;
	public float endAngle;
	public float duration;
	public bool loop;
	public bool revert;
	private float timer=0;
	private bool reverted=false;
	CoordSystem.Cylindrical cylCoord;
	// Use this for initialization
	void Start () {
		cylCoord = new CoordSystem.Cylindrical ();
	}

	
	// Update is called once per frame
	void Update () {
		cylCoord.theta = Mathf.Lerp(startAngle,endAngle,spline.Evaluate(timer/duration));
		timer+=Time.deltaTime;
		if(cylCoord.theta==endAngle) {
			if(revert && (!reverted || loop)) {
				float temp = endAngle;
				endAngle = startAngle;
				startAngle = temp;
				timer=0;
				reverted=true;
			}
			else if(loop){
				timer=0;
			}
		}
	}
}
