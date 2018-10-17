using Assets.Interfaces;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAdapter : MonoBehaviour {

    [SerializeField] MrBrickworm MrBrickworm;

    private void Start() {
        MrBrickworm = FindObjectOfType<MrBrickworm>();

    }

    void SetBossArrived() {
        //MrBrickworm.arrived = true;
    }

    void StartPlatform() {
        if (MrBrickworm) MrBrickworm.startPlatformPS();
        else print("AnimationAdapter/MrBrickworm: no boss found");
    }

    void StopPlayback() {
        print("Stopping animation");
        GetComponent<Animator>().StopPlayback();
    }

    void TestLayer() {
        print("AnimationAdapter/TestLayer: MoveLegs animation is running");
    }
}
