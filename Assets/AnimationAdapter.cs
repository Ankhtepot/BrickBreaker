using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAdapter : MonoBehaviour {

    [SerializeField] MrBrickworm MrBrickworm;

    void SetBossArrived() {
        //MrBrickworm.arrived = true;
    }

    void StartPlatform() {
        MrBrickworm.startPlatformPS();
    }

    void StopPlayback() {
        print("Stopping animation");
        GetComponent<Animator>().StopPlayback();
    }
}
