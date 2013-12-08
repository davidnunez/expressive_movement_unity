using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public GameObject target;
	
	public string currentEaseType = "linear";
	public int numberWaypoints = 10;
	
	public bool forwardCondition = false;
	public bool backwardCondition = false;
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space)) {
			print ("space key was pressed");
			StartCoroutine("DoAnimation");
		}
		
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			print ("space key was pressed");
			currentEaseType = "linear";
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			print ("space key was pressed");
			currentEaseType = "easeInOutQuad";
		}		
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			print ("space key was pressed");
			currentEaseType = "bounce";
		}	
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentEaseType = "easeInOutQuart";
		}	
		
		
		if (Input.GetKeyDown (KeyCode.Q)) {
			print ("space key was pressed");
			numberWaypoints = 3;
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			numberWaypoints = 5;
		}		
		if (Input.GetKeyDown (KeyCode.E)) {
			numberWaypoints = 10;
		}		
		if (Input.GetKeyDown (KeyCode.E)) {
			numberWaypoints = 20;
		}		
		
		if (Input.GetKeyDown (KeyCode.F)) {
			forwardCondition = true;
			backwardCondition = false;
		}	
		
		if (Input.GetKeyDown (KeyCode.F)) {
			forwardCondition = true;
			backwardCondition = false;
		}	
		
		if (Input.GetKeyDown (KeyCode.B)) {
			forwardCondition = false;
			backwardCondition = true;
		}	
		
		
		if (Input.GetKeyDown (KeyCode.N)) {
			forwardCondition = false;
			backwardCondition = false;
		}	
		
		if (Input.GetKeyDown (KeyCode.M)) {
			forwardCondition = true;
			backwardCondition = true;
		}			
		
		if (Input.GetKeyDown (KeyCode.K)) {
			target.renderer.enabled = !target.renderer.enabled;
		}
		
		if (Input.GetKeyDown (KeyCode.G)) {
			iTween.MoveTo(target, iTween.Hash("time", 10.0f, "path", iTweenPath.GetPath("p1"), "easetype", currentEaseType));
				
		}
			
	}
		
		
	


	IEnumerator DoAnimation () {
		float time = Random.Range(15.0f, 25.0f);
		
		Vector3[] waypoints = new Vector3[numberWaypoints];
		
		for(int i = 0 ; i < waypoints.Length; i++) {
			waypoints[i].x = Random.Range(-10, 10);
			waypoints[i].y = Random.Range(0, 10);
			waypoints[i].z = Random.Range(-6, 14);
		
			if (forwardCondition && !backwardCondition) {
				waypoints[i].z = Random.Range(-6, 0);
			}
			if (backwardCondition && !forwardCondition){
				waypoints[i].z = Random.Range(0, 14);
			}
			if (backwardCondition && forwardCondition) {
				waypoints[i].z = Random.Range(-1,1);
			}
		
		}
		
		
		
		
		foreach (Vector3 waypoint in waypoints) {
			iTween.MoveTo(target, iTween.Hash("time", time/numberWaypoints, "position", waypoint, "easetype", currentEaseType));
			yield return new WaitForSeconds(time/numberWaypoints);
				
		}

	}
}
