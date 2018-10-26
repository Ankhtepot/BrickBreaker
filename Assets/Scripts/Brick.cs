using Assets.Interfaces;
using Classes;
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
//TODO: add gamesession
    new ParticleSystem.MainModule particleSystem;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupManager = FindObjectOfType<PickupManager>();
        particleSystem = destroyEffect.main;
        hitPoints = damageSprites.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //print("Collided with " + collision.gameObject.tag + " Name: " + collision.gameObject.name);
        if (collision.gameObject.tag != tags.BRICK) {
            if (tag != tags.UNBREAKABLE) {
                DamageBrick();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        String collisionTag = collision.tag;
        //print("Brick: Trigger fired with " + tag + " Name: " + collision.name);
        if (collisionTag == tags.FIREBALL || 
            collisionTag == tags.LOSE_COLLIDER || 
            (collisionTag == tags.PH_OBSTACLE && tag != tags.UNBREAKABLE)) {
            destroyBrick();
        }
        if ((collisionTag == tags.PROJECTILE && tag != tags.UNBREAKABLE)) {
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
        pickupManager.ProcessPickupChanceOfSpawning(transform.position);
        particleSystem.startColor = spriteRenderer.color;
        Instantiate(destroyEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public SoundSystem.PlayListID GetPlayListID() {
        return SoundSystem.PlayListID.Brick;
    }
}
