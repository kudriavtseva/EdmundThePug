using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy = new Vector3(0.2f,0,0); 
	public float time_to_wait = 5;
	public float speed = 1;
	float toWait = 0;
	Vector3 pointA;
	Vector3 pointB;
	bool going_to_a = false;
	
	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(toWait > 0){
			toWait -= Time.deltaTime;
			return;
		}
		
		Vector3 my_pos = this.transform.position;
		Vector3 target;
		
		if(going_to_a) {
			target = this.pointA;
		} else {
			target = this.pointB;
		}
		
		if(!isArrived(my_pos, target)) {
			transform.Translate(MoveBy * speed * Time.deltaTime);
		} else {
			toWait = time_to_wait;
			going_to_a = !going_to_a;
			MoveBy = -MoveBy;
		}
       
	}
	
	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}
}