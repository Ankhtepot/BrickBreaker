using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    static Globals instance = null;

    // Use this for initialization
    void Start () {
            if (instance != null && instance != this) {
                print("Destroying duplicate Globals");
                Destroy(gameObject);
            } else {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
	
	// Update is called once per frame
	void Update () {
		
	}
}
