using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetLogic : MonoBehaviour {

    [SerializeField] float resetEnableDelay = 1f;

    public void ResetBall() {
        Ball ball = FindObjectOfType<Ball>();
        GameSession gameSession = FindObjectOfType<GameSession>();        
        if (ball && gameSession) {
            ball.CorrectPaddleCollision();
            gameSession.RetractLife();
        }
    }

    private void Update() {
        if (noBallIsLocked()) StartCoroutine(delayEnablingResetButton(resetEnableDelay));
        else GetComponent<Button>().interactable = false;
    }

    public bool noBallIsLocked() {
        foreach(Ball ball in FindObjectsOfType<Ball>()) if (!ball.hasStarted) return false;
        return true;    
    }

    IEnumerator delayEnablingResetButton(float delay) {
        yield return new WaitForSeconds(delay);
        GetComponent<Button>().interactable = true;
    }

}
