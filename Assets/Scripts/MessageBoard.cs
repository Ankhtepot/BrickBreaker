using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MessageBoard : MonoBehaviour {

    [System.Serializable]
    public enum DismmissType { OnMouseClick, ExternalTrigger, NoTrigger }

    [SerializeField] bool isHint = true;
    [SerializeField] public bool isActive = false;
    [SerializeField] bool waitForDismiss = false;
    [SerializeField] float timeBeforeDismiss = 1.5f;
    [SerializeField] DismmissType dismmissType;
    [SerializeField] bool waitsForDismissal = false;

    //chached states
    Animator animator;

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoad;
        animator = GetComponent<Animator>();
        //AutoSwoopBoardIn();
    }

    private void AutoSwoopBoardIn() {
        SceneLoader SL = FindObjectOfType<SceneLoader>();
        if (SL.isCurrentSceneLevel()) {
            Options options = FindObjectOfType<Options>();
            if (isActive) {
                if (isHint && options != null && animator != null && options.showHintBoards) SwoopBoardIn();
                if (!waitForDismiss) StartCoroutine(DelayDismiss()); 
            } 
        }
    }

    private void Update() {
        if (dismmissType == DismmissType.OnMouseClick && Input.GetMouseButtonDown(0) && waitsForDismissal) {
            print("MessageBoard/Update: LMB presset, Dismissing Board");
            DismissBoard();
        }
    }

    private void SwoopBoardIn() {
        GetComponent<Animator>().SetTrigger(triggers.ACTIVATE);
    }

    private IEnumerator DelayDismiss() {
        yield return new WaitForSeconds(timeBeforeDismiss);
        DismissBoard();
    }

    public void DismissBoard() {
        Dismiss();
    }

    private void Dismiss() {
        waitsForDismissal = false;
        if (animator) animator.SetTrigger(triggers.DISMISS);
    }

    void OnSceneLoad(Scene loadedScene, LoadSceneMode mode) {
        AutoSwoopBoardIn();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void SetWaitForDismissalOn() {
        waitsForDismissal = true;
    }
}
