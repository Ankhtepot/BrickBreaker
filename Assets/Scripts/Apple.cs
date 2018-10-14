using Assets.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Brick, IPlayList {

    new public SoundSystem.PlayListID GetPlayListID() {
        //print("Apple: GetPlayListID reached");
        return SoundSystem.PlayListID.Apple;
    }
}
