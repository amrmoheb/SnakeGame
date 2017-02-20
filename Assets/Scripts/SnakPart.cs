using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakPart : MonoBehaviour {
	public GameObject MainTarget;
	public GameObject MyChild;
	public Vector3 TempTarget;
    public bool SetTempTarget = false;
    private Vector3 CurrentTarget;
	float PartSpeed;
	bool arrived=false;
	
	// Use this for initialization
	void Start () {
		PartSpeed =  GameManger.speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if(MainTarget!=null)
        {
		CheckOnArrive ();
		}
	}
	void CheckOnArrive(){
		if (Vector3.Distance (transform.position, MainTarget.transform.position) >=1.2f)
        {
			if(GameManger.snakeDead==false)
            {
			transform.position = Vector3.MoveTowards(transform.position, MainTarget.transform.position,PartSpeed);
			}
		}
	}
}