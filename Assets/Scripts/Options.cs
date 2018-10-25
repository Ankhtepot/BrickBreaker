using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    [SerializeField] public bool showHintBoards = true;
    [SerializeField] public const int LivesBase = 10;
    [SerializeField] public int LivesCurrent;
    [SerializeField] int highestLevel = 0;
    
    [SerializeField] UnityEvent setShowHintBoardsOn;
    [SerializeField] UnityEvent setShowHintBoardsOff;

    //caches
    SceneLoader sceneLoader;

    public int HighestLevel {
        get {
            return highestLevel;
        }
        set {
            if (HighestLevel < value) highestLevel = value;
        }
    }

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
        if (sceneLoader && !sceneLoader.isCurrentSceneLevel()) {
            print("Options/OnScreenLoad: setting LivesCurrent to " + LivesBase);
            LivesCurrent = LivesBase;
        } else if(!sceneLoader) print("Options/OnScreenLoad: missing sceneLoader");
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
