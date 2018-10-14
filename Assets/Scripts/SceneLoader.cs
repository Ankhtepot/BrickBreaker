using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] int sceneToReturnTo = 0;
    [SerializeField] public int SceneToBeLoaded;
    [SerializeField] public float SplashScreenDelay = 2f;
    [SerializeField] public float SplScrProlongOffset = 0.75f;

    //Caches
    Animator splashScreen;
    Options options;
    List<string> notLevelScenes = new List<string> { "Start Screen", "Win Screen", "Game Over", "Credits Scene" };
    SoundSystem SFXPlayer;

    //static SceneLoader instance = null;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start() {
        //if (instance != null && instance != this) {
        //    print("Destroying duplicate SceneLoader");
        //    Destroy(gameObject);
        //} else {
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
        splashScreen = FindObjectOfType<SplashScreen>().GetComponent<Animator>();
        SFXPlayer = FindObjectOfType<SoundSystem>();
        options = FindObjectOfType<Options>();
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
            splashScreen.SetTrigger("ShowUp");
        } else {
            print("SceneLoader: FetchLevel: splashScreen not found, fetching level without animation");
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
        int gameOverSceneNr = sceneIndexFromName("Game Over");
        LoadScene(gameOverSceneNr);
    }

    public void ManageCreditsSceneView() {
        Scene currentScreen = SceneManager.GetActiveScene();
        int creditsSceneNr = sceneIndexFromName("Credits Scene");
        //print("SceneLoader/ManageCreditsSceneView: creditsSceneNr: " + creditsSceneNr);
        if (currentScreen.name != "Credits Scene") {
            sceneToReturnTo = currentScreen.buildIndex;
            LoadScene(creditsSceneNr);
        } else if (currentScreen.name == "Credits Scene")
            LoadScene(sceneToReturnTo);
    }

    public bool isCurrentSceneLevel() {
        if (notLevelScenes.Contains(SceneManager.GetActiveScene().name)) return false;
        return true;
    }

    void OnSceneLoad(Scene loadedScene, LoadSceneMode mode) {
        //print("SceneLoader: scene buildIndex: " + SceneManager.GetActiveScene().buildIndex + " loaded. SceneLoader Hash: " + this.GetHashCode());
        ChooseMusic();
        RunSplashScreen();
    }

    private void ChooseMusic() {
        if (SFXPlayer)
            if (isCurrentSceneLevel()) SFXPlayer.PlayGameMusic();
            else SFXPlayer.PlayMenuMusic();
    }

    private void RunSplashScreen() {
        if (!splashScreen) splashScreen = FindObjectOfType<SplashScreen>().GetComponent<Animator>();
        if (!options) options = FindObjectOfType<Options>();
        if (splashScreen) {
            if (options.showHintBoards && isCurrentSceneLevel()) {
                //print("SceneLoader/OnSceneLoad: before CORoutine");
                StartCoroutine(DelaySplScrFade());
            } else splashScreen.SetTrigger("Fade");
        } else print("SceneLoader/OnSceneLoad: No splashScreen found");
    }

    IEnumerator DelaySplScrFade() {
        //print("SceneLoader: DelaySplScrFade - start ");
        yield return new WaitForSeconds(SplashScreenDelay);
        //print("SceneLoader: DelaySplScrFade - delayed " + SplashScreenDelay + "s");
        splashScreen.SetTrigger("Fade");
    }

    private string NameFromIndex(int BuildIndex) { //@Author:  Iamsodarncool/UnityAnswers
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    private int sceneIndexFromName(string sceneName) {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string testedScreen = NameFromIndex(i);
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
