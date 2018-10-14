using Assets.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, ITriggerable {

    public void OnTrigger() {
        //print("MovingPlatform: triggered!");
        Animator anim = GetComponent<Animator>();
        if (anim) anim.SetTrigger("Move");
        else print("MovingPlatform: missing animator");
    }
}
