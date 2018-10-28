using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    [SerializeField] public bool ShowHintBoards = true;
    [SerializeField] public const int LivesBase = 5;
    [SerializeField] public int LivesCurrent;
    [SerializeField] int highestLevel = 0;
    [SerializeField] int currentHighestLevel;
    [SerializeField] public int baseForScore = 10;
    [SerializeField] int score = 0;
    [SerializeField] int ScoreItemsCount = 5;
    [SerializeField] List<ScoreItem> scoreList;

    //caches
    SceneLoader sceneLoader;

    public int HighestLevel {
        get {
            return highestLevel;
        }
        set {
            if (HighestLevel < value) highestLevel = value;
            if (value == intconstants.MRBRICKWORM) currentHighestLevel = 8;
            else currentHighestLevel = value;
        }
    }

    public int Score {
        get { return score; }
        set { score = value; }
    }

    private void Start() {
        InicializeCaches();
    }

    private void InicializeCaches() {
        SceneManager.sceneLoaded += OnScreenLoad;
        sceneLoader = FindObjectOfType<SceneLoader>();
        LivesCurrent = LivesBase;
        scoreList = new List<ScoreItem>();
    }

    public void AddScore(int newScore) {
        if (score + newScore < 0) score = 0;
        else score += newScore;
    }

    public void ToggleShowHintBoards() {
        ShowHintBoards = !ShowHintBoards;
    }

    private void OnScreenLoad(Scene loadedScene, LoadSceneMode mode) {
        ProcessGameData();
    }

    private void ProcessGameData() {
        if (sceneLoader && !sceneLoader.isCurrentSceneLevel()) {
            ProcessLives();
            if(Score > 0) ProcessScore();
        } else if (!sceneLoader) print("Options/ProcessGameData: missing sceneLoader");
    }

    private void ProcessLives() {
        print("Options/OnScreenLoad: setting LivesCurrent to " + LivesBase);
        LivesCurrent = LivesBase;
    }

    private void ProcessScore() {
        ScoreItem newScore = new ScoreItem(Score, currentHighestLevel);
        print("Options/ProcessScore: Score: " + Score + " scoreList.count: " + scoreList.Count + " ScoreItemsCount: " + ScoreItemsCount);
        Score = 0;
        if (scoreList.Count > 0) {
            foreach (ScoreItem record in scoreList) record.isNewRecord = false;
            for (int i = 0; i < Mathf.Min(scoreList.Count, ScoreItemsCount); i++) {
                if (scoreList[i].Score < newScore.Score) {
                    print("Options/ProcessScore: adding new score to list at " + i + " position.");
                    scoreList.Insert(i, newScore);
                    if (scoreList.Count > ScoreItemsCount) scoreList.RemoveAt(ScoreItemsCount);
                    break;
                }
            }
        }
        if (scoreList.Count == 0 || (scoreList.Count < ScoreItemsCount && !scoreList.Contains(newScore))) {
            print("Options/ProcessScore: Adding score to first empty position");
            scoreList.Add(newScore);
        }
    }

    public List<ScoreItem> GetScoreItems() {
        return scoreList;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnScreenLoad;
    }

}
