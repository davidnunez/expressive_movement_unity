using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Director : MonoBehaviour {
	public GameObject target;
	
	public string currentEaseType = "linear";
	public int numberWaypoints = 10;
	
	public bool forwardCondition = false;
	public bool backwardCondition = false;
	
	public GameObject w000;	
	public GameObject w001;	
	public GameObject w002;	
	public GameObject w010;	
	public GameObject w011;	
	public GameObject w012;	
	public GameObject w020;	
	public GameObject w021;	
	public GameObject w022;	

	public GameObject w100;	
	public GameObject w101;	
	public GameObject w102;	
	public GameObject w110;	
	public GameObject w111;	
	public GameObject w112;	
	public GameObject w120;	
	public GameObject w121;	
	public GameObject w122;
	
	public GameObject w200;	
	public GameObject w201;	
	public GameObject w202;	
	public GameObject w210;	
	public GameObject w211;	
	public GameObject w212;	
	public GameObject w220;	
	public GameObject w221;	
	public GameObject w222;	
	
	public enum ConditionPath {fu,fn,fd,nu,nn,nd,bu,bn,bd};
	public enum GUIState {intro, movie, studyIntro, runningTrial};
	
	public GUIState guiState = GUIState.intro;
	
	public ConditionPath conditionPath;
	
	public string filename = "data.txt";
	public float measure_seconds = 3.0f;
	public MoviePlayBehavior natural_movie;
	public MoviePlayBehavior unnatural_movie;
	public GameObject guiCamera;
	public class Condition {
		   	public ConditionPath conditionPath {get; set;}
    	public string easeType {get; set;}
		public bool smoothPath {get; set;}
		public Condition(ConditionPath _conditionPath, string _easeType, bool _smoothPath) {
			conditionPath = _conditionPath;
			easeType = _easeType;
			smoothPath = _smoothPath;
		}
		
 
	}
	
	public Condition[] conditions;
	

	void OnGUI () {
		GUI.skin.label.fontSize = 80;

		switch(guiState) {
		case GUIState.intro:
		// Make a background box
			GUI.Box(new Rect(Screen.width/2-500,Screen.height/2,1000,500), "Thank you for your participation in this study. You should feel free to ask questions and can stop participating at any time.\n\n" +
				"First, when you are ready, please click the 'Begin' button, relax, and watch a couple of movies about arms for about a minute.");
			if(GUI.Button(new Rect(Screen.width/2-40,Screen.height/2+200,80,20), "Begin")) {
				guiState = GUIState.movie;
				StartCoroutine("PlayMovies");
			}
        	measure_seconds = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), measure_seconds, 0.0F, 10.0F);

			break;
		case GUIState.movie:
			break;
		case GUIState.studyIntro:
			GUI.Box(new Rect(Screen.width/2-500,Screen.height/2,1000,500), "Now, we'd like you to help us study a simulated robot arm.\n\n" +
				"The robot's job is simply to touch a red ball.\n" +// It is a slow learner and doesn't have a very good memory.\n\
				"You will observe it attempting to complete its task.\n" +
				"At the end of each trial you will be asked a few questions about how well the robot completed the task.\n" +
				"You will also be asked what you think the robot might be 'feeling' as it was doing its work.\n\n" +
				"When you are ready to start, click the 'Begin' button.");
			
				if(GUI.Button(new Rect(Screen.width/2-40,Screen.height/2+200,80,20), "Begin")) {
					guiState = GUIState.runningTrial;
					//StartCoroutine("RunTrial");
				}
			break;
		case GUIState.runningTrial:
			guiCamera.SetActive(false); // = false;
			break;
		}
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		/*if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
		}*/
	}

//	public enum Conditions {fu,fn,fd,nu,nn,nd,bu,bn,bd
	
	// Use this for initialization
	void Start () {
		unnatural_movie.gameObject.renderer.enabled = false;
		natural_movie.gameObject.renderer.enabled = false;

		conditions = new Condition[] {
			new Condition(ConditionPath.bd, "linear", true),
			new Condition(ConditionPath.bn, "linear", true),
			new Condition(ConditionPath.bu, "linear", true),
			new Condition(ConditionPath.fd, "linear", true),
			new Condition(ConditionPath.fn, "linear", true),
			new Condition(ConditionPath.fu, "linear", true),
			new Condition(ConditionPath.nd, "linear", true),
			new Condition(ConditionPath.nn, "linear", true),
			new Condition(ConditionPath.nu, "linear", true),	
			
			new Condition(ConditionPath.bd, "linear", false),
			new Condition(ConditionPath.bn, "linear", false),
			new Condition(ConditionPath.bu, "linear", false),
			new Condition(ConditionPath.fd, "linear", false),
			new Condition(ConditionPath.fn, "linear", false),
			new Condition(ConditionPath.fu, "linear", false),
			new Condition(ConditionPath.nd, "linear", false),
			new Condition(ConditionPath.nn, "linear", false),
			new Condition(ConditionPath.nu, "linear", false),		

			
			new Condition(ConditionPath.bd, "easeInOutQuart", true),
			new Condition(ConditionPath.bn, "easeInOutQuart", true),
			new Condition(ConditionPath.bu, "easeInOutQuart", true),
			new Condition(ConditionPath.fd, "easeInOutQuart", true),
			new Condition(ConditionPath.fn, "easeInOutQuart", true),
			new Condition(ConditionPath.fu, "easeInOutQuart", true),
			new Condition(ConditionPath.nd, "easeInOutQuart", true),
			new Condition(ConditionPath.nn, "easeInOutQuart", true),
			new Condition(ConditionPath.nu, "easeInOutQuart", true),	
			
			new Condition(ConditionPath.bd, "easeInOutQuart", false),
			new Condition(ConditionPath.bn, "easeInOutQuart", false),
			new Condition(ConditionPath.bu, "easeInOutQuart", false),
			new Condition(ConditionPath.fd, "easeInOutQuart", false),
			new Condition(ConditionPath.fn, "easeInOutQuart", false),
			new Condition(ConditionPath.fu, "easeInOutQuart", false),
			new Condition(ConditionPath.nd, "easeInOutQuart", false),
			new Condition(ConditionPath.nn, "easeInOutQuart", false),
			new Condition(ConditionPath.nu, "easeInOutQuart", false),			
		
		};
		Shuffle(conditions);
		using (StreamWriter sw = File.AppendText(filename))
            {
				sw.WriteLine("Session Beginning" + Time.time);			
        		foreach (Condition condition in conditions) {
					sw.WriteLine (condition.conditionPath.ToString() + "," + condition.easeType + "," + condition.smoothPath.ToString());
				}
			}
		
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
			currentEaseType = "easeOutBounce";
		}	
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentEaseType = "easeInOutQuart";
		}	
		
		

		
		if (Input.GetKeyDown (KeyCode.T)) {
			target.renderer.enabled = !target.renderer.enabled;
			GameObject[] gos = GameObject.FindGameObjectsWithTag("waypoint");
			foreach (GameObject go in gos) {
				go.renderer.enabled = !go.renderer.enabled;	
			}
		}
		
		
		if (Input.GetKeyDown (KeyCode.R)) {
			target.transform.position = w011.transform.position;
		
		}		
				
		if (Input.GetKeyDown (KeyCode.K)) {	
			iTween.MoveTo(target, iTween.Hash("time", 5.0f, "path", iTweenPath.GetPath(conditionPath.ToString()), "easetype", currentEaseType));
			
		}
		
		if (Input.GetKeyDown (KeyCode.L)) {	
			Vector3[] path = iTweenPath.GetPath(conditionPath.ToString());
			StartCoroutine("DoAnimation", path);

		}
		
			
	}
		
	IEnumerable DoSmoothAnimation(GameObject[] waypoints) {
		iTweenPath path = new iTweenPath();
		List<Vector3> nodes = new List<Vector3>(){};
		foreach (GameObject waypoint in waypoints) {
			nodes.Add(waypoint.transform.position);
		}
		path.nodes = nodes;
		iTween.MoveTo(target, iTween.Hash("time", 5.0f, "path", path, "easetype", currentEaseType));
		yield return new WaitForSeconds(5.0f);
		
	}
	
	IEnumerator DoAnimation(GameObject[] waypoints) {
		for(int i = 0 ; i < waypoints.Length; i++) {
			iTween.MoveTo(target, iTween.Hash("time", 4.0f/waypoints.Length, "position", waypoints[i].transform.position, "easetype", currentEaseType));
			yield return new WaitForSeconds(4.0f/waypoints.Length);

		}
		yield return new WaitForSeconds(3.0f);
		iTween.MoveTo(target, iTween.Hash("time", 0.0f, "position", w011.transform.position, "easetype", currentEaseType));
	}

	IEnumerator DoAnimation(Vector3[] waypoints) {
		for(int i = 0 ; i < waypoints.Length; i++) {
			iTween.MoveTo(target, iTween.Hash("time", 4.0f/waypoints.Length, "position", waypoints[i], "easetype", currentEaseType));
			yield return new WaitForSeconds(4.0f/waypoints.Length);

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
	
	
	IEnumerator PlayMovies() {
		natural_movie.gameObject.renderer.enabled = true;
		natural_movie.movTexture.Play();
		yield return new WaitForSeconds(5);
		natural_movie.gameObject.renderer.enabled = false;
		unnatural_movie.gameObject.renderer.enabled = true;
		unnatural_movie.movTexture.Play();

		yield return new WaitForSeconds(5);
		unnatural_movie.gameObject.renderer.enabled = false;
		guiState = GUIState.studyIntro;
	}
	
	    /// <summary>
    /// Used in Shuffle(T).
    /// </summary>
    static System.Random _random = new System.Random();

    /// <summary>
    /// Shuffle the array.
    /// </summary>
    /// <typeparam name="T">Array element type.</typeparam>
    /// <param name="array">Array to shuffle.</param>
    public static void Shuffle<T>(T[] array)
    {
		var random = _random;
		for (int i = array.Length; i > 1; i--)
		{
	    	// Pick random element to swap.
	    	int j = random.Next(i); // 0 <= j <= i-1
	    	// Swap.
	    	T tmp = array[j];
	    	array[j] = array[i - 1];
	    	array[i - 1] = tmp;
		}
    }
}


	
	
