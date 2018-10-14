using Assets.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IPlayList {

    [SerializeField] int hitPoints = 0;
    [SerializeField] Sprite[] damageSprites;
    [SerializeField] ParticleSystem destroyEffect;

    //status
    GameSession currentLevel;
    SpriteRenderer spriteRenderer;
    PickupManager pickupManager;
    new ParticleSystem.MainModule particleSystem;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupManager = FindObjectOfType<PickupManager>();
        //destroyEffect = GetComponent<ParticleSystem>();
        particleSystem = destroyEffect.main;

        //if (tag != "Unbreakable") {
        //    currentLevel.AddBrick();
        //}
        hitPoints = damageSprites.Length;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //print("Collided with " + collision.gameObject.tag + " Name: " + collision.gameObject.name);
        if (collision.gameObject.tag != "Brick" ) {
            if (tag != "Unbreakable") {
                DamageBrick();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("Brick: Trigger fired with " + collision.tag + " Name: " + collision.name);
        if (collision.tag == "Fireball" || collision.tag == "LoseCollider") {
            destroyBrick();
        }
        if ((collision.tag == "Projectile" && tag != "Unbreakable")) {
            DamageBrick();
        }
    }

    public void DamageBrick() {
        hitPoints--;
        if (hitPoints < 0) {
            destroyBrick();
        } else {
            if (damageSprites[hitPoints]) {
                spriteRenderer.sprite = damageSprites[hitPoints];
            } else {
                Debug.LogError("Sprite " + name + " has missing sprite assigned");
            }
        }
    }

    public void destroyBrick() {
        //if (tag != "Unbreakable") {
        //    currentLevel.RetractBrick();
        //}
        pickupManager.ProcessPickupChanceOfSpawning(transform.position);
        particleSystem.startColor = spriteRenderer.color;
        Instantiate(destroyEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public SoundSystem.PlayListID GetPlayListID() {
        return SoundSystem.PlayListID.Brick;
    }
}
