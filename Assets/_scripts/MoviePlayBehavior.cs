using UnityEngine;
using System.Collections;

public class MoviePlayBehavior : MonoBehaviour {

  	public MovieTexture movTexture;
    void Start() {
        renderer.material.mainTexture = movTexture;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
