using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    [SerializeField] public bool ShowHintBoards = true;
    [SerializeField] public const int LivesBase = 10;
    [SerializeField] public int LivesCurrent;
    [SerializeField] int highestLevel = 0;
    [SerializeField] public int baseForScore = 10;
    [SerializeField] int score = 0;
    [SerializeField] List<int> scoreList;
    
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

    public int Score {
        get { return score;}
        set {
            if (score - value < 0) score = 0;
            else score = value;
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
        ShowHintBoards = !ShowHintBoards;
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
        if (ShowHintBoards) setShowHintBoardsOn.Invoke();
        else setShowHintBoardsOff.Invoke();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnScreenLoad;
    }
    
}
