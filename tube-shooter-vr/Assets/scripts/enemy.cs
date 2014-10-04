using UnityEngine;
using System.Collections;
using MathTools;
public class enemy : MonoBehaviour {
	public float speed; //speed on Z axis
	public AnimationCurve spline; // curve of theta change
	public float startAngle; // theta start
	public float endAngle; // theta end
	public float duration; // time to go from theta start to theta end in seconds
	public bool loop;
	public bool revert; // revert back from thetaEnd to thetaEnd
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
		cylCoord.z -= speed*Time.deltaTime;
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
