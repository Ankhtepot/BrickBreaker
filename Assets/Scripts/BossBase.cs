using Assets.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour, IBoss, IPlayList {
    public float movingAnimationSpeed;
    public float attackSpeed;
    public float attackCDBase;
    public float attackCDVariation;
    [SerializeField] AudioClip SFXDyingSound;
    
    //[HideInInspector]
    public SoundSystem SFXPlayer;

    public void Start() {
        AssignSoundSystem();
    }

    public void AssignSoundSystem() {
        //print("BossBase: Assigning SFXPlayer");
        SoundSystem ss = FindObjectOfType<SoundSystem>();
        if (ss) this.SFXPlayer = ss;
        else print("BossBase/AssignSoundSystem: no SoundSystem found");
    }

    public abstract void Dying();
    public abstract void OnArrival();
    public abstract void OnCollisionEnter2D(Collision2D collision);
    public abstract void OnDeath();
    public abstract void StartEncounter();
    public abstract SoundSystem.PlayListID GetPlayListID();
}
