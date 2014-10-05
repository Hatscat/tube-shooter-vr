using UnityEngine;
using System.Collections;

public class EnemySpwan : MonoBehaviour {

	public GameObject[] enemies;
	public AnimationCurve enemiesSpawnRate;
	public float startSpawnTime;
	public float endSpawnTime;
	public float minRand;
	public float maxRand;
	public float duration;
	private float _nextEnemy=0;
	private GameObject _enemy;
	private float timer=0;
	// Use this for initialization
	void Start () {
	
	}//transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y-0.35f, Camera.main.transform.position.z+transform.position.z);
	//transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x+transform.rotation.x,Camera.main.transform.rotation.y+transform.rotation.y,Camera.main.transform.rotation.z+transform.rotation.z);
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= _nextEnemy) {
			int random = Random.Range(0,enemies.Length);
			_enemy = Instantiate(enemies[random], new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y+enemies[random].transform.position.y, Camera.main.transform.position.z+enemies[random].transform.position.z), enemies[random].transform.rotation) as GameObject;
			enemy enemyParams = _enemy.GetComponent<enemy>();
			enemyParams.transform.parent = transform;
			enemyParams.transform.RotateAround(transform.position,Vector3.forward,Random.Range(0,360));
			_nextEnemy = Time.time + Mathf.Lerp(startSpawnTime,endSpawnTime,enemiesSpawnRate.Evaluate(timer/duration)) + Random.Range(minRand,maxRand);
			timer+=Time.deltaTime;
		}
	}
}
