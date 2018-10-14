using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    [SerializeField] public bool showHintBoards = true;
    
    [SerializeField] UnityEvent setShowHintBoardsOn;
    [SerializeField] UnityEvent setShowHintBoardsOff;

    private void Start() {
        SceneManager.sceneLoaded += OnScreenLoad;
    }

    public void ToggleShowHintBoards() {
        //print("Options/ToggleShowHintBoards: reached");
        //if (showHintBoards) setShowHintBoardsOff.Invoke();
        //else setShowHintBoardsOn.Invoke();
        showHintBoards = !showHintBoards;
    }

    private void OnScreenLoad(Scene loadedScene, LoadSceneMode mode) {
        BroadcastShowHintBoards();
    }

    private void BroadcastShowHintBoards() {
        //if (ShowHintBoards) setShowHintBoardsOn.Invoke();
        if (showHintBoards) setShowHintBoardsOn.Invoke();
        else setShowHintBoardsOff.Invoke();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnScreenLoad;
    }

    //public bool ShowHintBoards {
    //    get {
    //        return showHintBoards;
    //    }
    //    set {
    //        ShowHintBoards = value;
    //        print("Options: Seting ShowMessageBoards(" + ShowHintBoards + ")");
    //    }
    //}
}
