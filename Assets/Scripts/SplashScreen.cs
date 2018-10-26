#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    [SerializeField] List<GameObject> messageBoards;

    //cache
    MessageBoard chosenMessageBoard = null;

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoadMessageBoard;
    }

    void triggerNextLevel() {
        SceneLoader SL = FindObjectOfType<SceneLoader>();
        Options options = FindObjectOfType<Options>();
        if (options && options.ShowHintBoards) {
            ActivateRandomMessageBoard(); 
        }
        if (SL) SceneManager.LoadScene(SL.SceneToBeLoaded);
        else print("SplashScreen/triggerNextLevel: SceneLoader not found.");
    }

    void ActivateRandomMessageBoard() {
        deactivateMessageBoards();
        messageBoards[Random.Range(0, messageBoards.Count)].
            GetComponentInChildren<MessageBoard>().isActive = true;
    }

    void deactivateMessageBoards() {
        foreach (GameObject board in messageBoards)
            board.GetComponentInChildren<MessageBoard>().isActive = false;
    }

    void OnSceneLoadMessageBoard(Scene loadedScene, LoadSceneMode mode) {

    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoadMessageBoard;
    }
}
