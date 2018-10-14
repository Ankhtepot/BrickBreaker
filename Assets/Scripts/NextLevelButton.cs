using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour {

    public void LoadNextScene() {
        SceneLoader SL = FindObjectOfType<SceneLoader>();
        if (SL) SL.LoadScene();
        else {
            print("NextLevelButton: LoadNextScene: failed, no SceneLoader present");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
