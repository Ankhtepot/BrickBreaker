using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour {

	public void OnCreditsButtonClick() {
        SceneLoader SL = FindObjectOfType<SceneLoader>();
        if (SL) SL.ManageCreditsSceneView();
        else {
            print("CreditsButton: OnCreditButtonClick: SceneLoader not found");
        }
    }
}
