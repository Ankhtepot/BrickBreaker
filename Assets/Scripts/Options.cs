using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    [SerializeField] public bool showHintBoards = true;
    [SerializeField] public int LivesBase = 10;
    [SerializeField] public int LivesCurrent;
    [SerializeField] int HighestLevel = 0;
    
    [SerializeField] UnityEvent setShowHintBoardsOn;
    [SerializeField] UnityEvent setShowHintBoardsOff;

    //caches
    SceneLoader sceneLoader;

    private void Start() {
        SceneManager.sceneLoaded += OnScreenLoad;
        sceneLoader = FindObjectOfType<SceneLoader>();
        LivesCurrent = LivesBase;
    }

    public void ToggleShowHintBoards() {
        //print("Options/ToggleShowHintBoards: reached");
        //if (showHintBoards) setShowHintBoardsOff.Invoke();
        //else setShowHintBoardsOn.Invoke();
        showHintBoards = !showHintBoards;
    }

    private void OnScreenLoad(Scene loadedScene, LoadSceneMode mode) {
        BroadcastShowHintBoards();
        if (sceneLoader && !sceneLoader.isCurrentSceneLevel()) LivesCurrent = LivesBase;
        else print("Options/OnScreenLoad: missing sceneLoader");
    }

    private void BroadcastShowHintBoards() {
        //if (ShowHintBoards) setShowHintBoardsOn.Invoke();
        if (showHintBoards) setShowHintBoardsOn.Invoke();
        else setShowHintBoardsOff.Invoke();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnScreenLoad;
    }

    public void SetHighestLevel(int level) {
        HighestLevel = level;
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
