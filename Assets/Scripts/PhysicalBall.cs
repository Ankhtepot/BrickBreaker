using Assets.Classes;
using Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBall : MonoBehaviour {
    [Header("physical setup")]
    [SerializeField] float fallSpeed = 0.05f;
    [SerializeField] bool isFireball = false;
    [Header("Sounds setup")]
    [SerializeField] AudioClip paddleSound;
    [Header("Caches")]
    [SerializeField] ParticleSystem impactEffect;

    //states
    SoundSystem SFXPlayer;

    void Start() {
        fallSpeed = UnityEngine.Random.Range(fallSpeed - 0.02f, fallSpeed + 0.02f);
        processIsFireball();
        SFXPlayer = FindObjectOfType<SoundSystem>();
        //if (SFXPlayer) print("Ball: SFXPlayer assigned");
    }

    void Update() {
        if (!GetComponent<Rigidbody2D>()) {

            //print("PhysicalBall/Update: speed: " + fallSpeed);
            transform.position = new Vector3(transform.position.x,
            transform.position.y - fallSpeed, 0);
        }
    }

    private void processIsFireball() {
        if (isFireball) gameObject.tag = tags.FIREBALL;
        foreach (ParticleSystem PS in GetComponentsInChildren<ParticleSystem>()) {
            PS.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        String collisionTag = collision.gameObject.tag;
        //print("PhysicalBall/OnCollisionEnter: collision triggerred with " + collision.gameObject.name + " with a tag:" + collisionTag);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("PhysicalBall/OnTriggerEnter: trigger triggerred with " + collision.name + " with a tag:" + collision.tag);
        if (collision.tag == tags.LOSE_COLLIDER) {
            //Debug.Log("Ball Triggerers LoseCollider");
            ManageLoseCollider();
        }
        if (collision.tag == tags.PADDLE) {
            processPaddleCollision(collision);
        }
    }

    private void processPaddleCollision(Collider2D collision) {
        if (collision.tag == tags.PADDLE && tag == tags.FIREBALL) {
            ParticleSystem effect = null;
            if (impactEffect) Instantiate(impactEffect, transform.position, Quaternion.identity);
            if (effect) effect.Play();
            SFXPlayer.PlayClipOnce(paddleSound);
            GameSession GS = FindObjectOfType<GameSession>();
            if (GS) GS.RetractLife();
            Destroy();
        }
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    private void ManageLoseCollider() {
        Destroy(gameObject);
    }
}
