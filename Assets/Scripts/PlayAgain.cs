using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

    public void OnPlayAgainButtonClick() {
        SceneLoader SL = FindObjectOfType<SceneLoader>();
        if (SL) SL.LoadFirstScene();
        else SceneManager.LoadScene(0);
    }
}
