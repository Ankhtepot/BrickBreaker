#pragma warning disable 0414

using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] int sceneToReturnTo = 0;
    [SerializeField] public int SceneToBeLoaded;
    [SerializeField] public float SplashScreenDelay = 2f;
    [SerializeField] public float SplScrProlongOffset = 0.75f;

    //Caches
    [SerializeField] Button continueButton;
    Animator splashScreen;
    Options options;
    List<string> notLevelScenes = new List<string> {
        scenes.START, scenes.WIN, scenes.GAME_OVER, scenes.CREDITS
    };
    SoundSystem SFXPlayer;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start() {
        if (splashScreen) splashScreen = FindObjectOfType<SplashScreen>().GetComponent<Animator>();
        SFXPlayer = FindObjectOfType<SoundSystem>();
        options = FindObjectOfType<Options>();
        ManageContinueButtonText();
    }

    public void LoadScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        FetchLevel(currentSceneIndex + 1);
    }

    public void LoadScene(int sceneNr) {
        FetchLevel(sceneNr);
    }

    void FetchLevel(int sceneToBeLoaded) {
        //Debug.Log("Loading Screen index: " + (sceneToBeLoaded));
        SceneToBeLoaded = sceneToBeLoaded;
        if (splashScreen) {
            splashScreen.SetTrigger(triggers.SHOW_UP);
        } else {
            print("SceneLoader/FetchLevel: splashScreen not found, fetching level without animation");
            SceneManager.LoadScene(sceneToBeLoaded);
        }
    }

    public void LoadFirstScene() {
        LoadScene(0);
    }

    public void QuitApplication() {
        Application.Quit();
        print("Request to quit received");
    }

    public void LoadGameOverScene() {
        int gameOverSceneNr = sceneIndexFromName(scenes.GAME_OVER);
        LoadScene(gameOverSceneNr);
    }

    public void ManageCreditsSceneView() {
        Scene currentScreen = SceneManager.GetActiveScene();
        int creditsSceneNr = sceneIndexFromName(scenes.CREDITS);
        //print("SceneLoader/ManageCreditsSceneView: creditsSceneNr: " + creditsSceneNr);
        if (currentScreen.name != scenes.CREDITS) {
            sceneToReturnTo = currentScreen.buildIndex;
            LoadScene(creditsSceneNr);
        } else if (currentScreen.name == scenes.CREDITS)
            LoadScene(sceneToReturnTo);
    }

    public bool isCurrentSceneLevel() {
        if (notLevelScenes.Contains(SceneManager.GetActiveScene().name)) return false;
        return true;
    }

    void OnSceneLoad(Scene loadedScene, LoadSceneMode mode) {
        //print("SceneLoader: scene buildIndex: " + SceneManager.GetActiveScene().buildIndex + " loaded. SceneLoader Hash: " + this.GetHashCode());
        ChooseMusicByScene();
        RunSplashScreen();
        ManageContinueButtonText();
        AssignContinueButton();
    }

    private void AssignContinueButton() {
        if(SceneManager.GetActiveScene().buildIndex == 0 && continueButton == null) {
            print("SceneLoader/AssignContinueButton: assigning button");
            continueButton = GameObject.Find(gameobjects.CONTINUE_BUTTON).GetComponent<Button>();
            continueButton.onClick.AddListener(ContinueButtonClick);
        }
    }

    private void ChooseMusicByScene() {
        if (SFXPlayer) {
            if (isCurrentSceneLevel()) SFXPlayer.PlayGameMusic();
            else SFXPlayer.PlayMenuMusic();
        } //else print("SceneLoader/ChooseMusicByScene: no SFX player found");
    }

    private void RunSplashScreen() {
        if (!splashScreen) splashScreen = FindObjectOfType<SplashScreen>().GetComponent<Animator>();
        if (!options) options = FindObjectOfType<Options>();
        if (splashScreen) {
            if (options.showHintBoards && isCurrentSceneLevel()) {
                //print("SceneLoader/OnSceneLoad: before CORoutine");
                StartCoroutine(DelaySplScrFade());
            } else splashScreen.SetTrigger(triggers.FADE);
        } else print("SceneLoader/OnSceneLoad: No splashScreen found");
    }

    public void ManageContinueButtonText() {
        if (isCurrentSceneName(scenes.START)) {
            TextMeshProUGUI targetText =
                GameObject.Find(gameobjects.TARGET_TEXT).GetComponentInChildren<TextMeshProUGUI>();
            if (options && targetText) {
                String levelToGoTo = "Level ";
                switch (options.HighestLevel) {
                    case -1: case 0: case 1:
                        levelToGoTo += sceneIndexFromName(scenes.FIRST_LEVEL).ToString(); break;
                    case intconstants.MRBRICKWORM: levelToGoTo = scenes.MRBRICKWORM; break;
                    default: levelToGoTo += options.HighestLevel.ToString(); break;
                }
                targetText.text = levelToGoTo;
            } 
        }
    }

    public void ContinueButtonClick() {
        
        int targetLevel = 1;
        if (options) {
            print("SceneLoader/ContinueButtonClick: HighestLevel is: " + options.HighestLevel);
            switch (options.HighestLevel) {
                case -1: case 0: case 1: targetLevel = intconstants.FIRSTLEVEL; break;
                case intconstants.MRBRICKWORM:
                    targetLevel = sceneIndexFromName(scenes.MRBRICKWORMLEVEL); break;
            }
        } else if (!options) print("SceneLoader/ContinueButtonClick: missing options");
        LoadScene(targetLevel);
    }

    IEnumerator DelaySplScrFade() {
        //print("SceneLoader: DelaySplScrFade - start ");
        yield return new WaitForSeconds(SplashScreenDelay);
        //print("SceneLoader: DelaySplScrFade - delayed " + SplashScreenDelay + "s");
        splashScreen.SetTrigger(triggers.FADE);
    }

    private string NameFromIndex(int BuildIndex) { ///@Author:  Iamsodarncool/UnityAnswers
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    private int sceneIndexFromName(string sceneName) {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string testedScreen = NameFromIndex(i);
            //print("SceneLoader/sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }

    private bool isCurrentSceneName(String sceneName) {
        if (SceneManager.GetActiveScene().name == sceneName) return true;
        return false;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
