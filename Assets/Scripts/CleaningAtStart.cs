using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAtStart : MonoBehaviour {

	void Start () {
        Destroy(GameObject.Find("Game Session"));
	}
	
}
