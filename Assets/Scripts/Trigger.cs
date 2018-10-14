using Assets.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    [SerializeField] bool isTriggerInSequence = false;
    [SerializeField] float minTriggerDelay = 0f;
    [SerializeField] float maxTriggerDelay = 0f;
    [SerializeField] GameObject[] Targets;

    public void TriggerOrder() {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence() {
        for (int i = 0; i < Targets.Length; i++) {
            float delay = Random.Range(minTriggerDelay, maxTriggerDelay);
            ITriggerable O = Targets[i].GetComponent<ITriggerable>();
            if (O != null) {
                if (isTriggerInSequence) yield return StartCoroutine(DelayInSequence(delay, O));
                else StartCoroutine(DelayInSequence(delay, O));
            } else print("Trigger: StartSequence: object: " + O.ToString()+ " is not ITriggerable.");
        }

    }

    IEnumerator DelayInSequence(float delay, ITriggerable O) {
        yield return new WaitForSeconds(delay);
        O.OnTrigger();
    }
}
