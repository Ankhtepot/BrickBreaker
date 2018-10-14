using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayButton : MonoBehaviour {

    [SerializeField] Trigger trigger;
    [SerializeField] float triggerDelay = 1f;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject top;
    //[SerializeField] bool isOnOffSwitch = false;
    [SerializeField] bool isOn = false;

    Animator bottomAnimator;
    Animator topAnimator;

    private void Start() {
        bottomAnimator = bottom.GetComponent<Animator>();
        topAnimator = top.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //print("Button \"" + gameObject.name + "\" triggered OnCollision");
        if (!isOn) {
            isOn = true;
            StartCoroutine(DelayedTriggerActivation()); 
        }
    }

    IEnumerator DelayedTriggerActivation() {
        if (bottomAnimator) bottomAnimator.SetTrigger("SwitchOn");
        if (topAnimator) topAnimator.SetTrigger("SwitchOn");
        else print("Button \"" + gameObject.name + "\": missing animator");
        yield return new WaitForSeconds(triggerDelay);
        trigger.TriggerOrder();
    }
}
