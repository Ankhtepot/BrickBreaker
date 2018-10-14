using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBall : MonoBehaviour {
    [Header("physical setup")]
    //[SerializeField] public float launchPower = 10f;
    [SerializeField] bool isFireball = false;
    //[Header("Sounds setup")]
    //[SerializeField] AudioClip[] bounceSounds;
    //[SerializeField] AudioClip unbreakableSound;
    //[SerializeField] AudioClip paddleSound;
    //[SerializeField] AudioClip loseSound;
    //[SerializeField] AudioClip wallBounceSound;

    //states
    //SoundSystem SFXPlayer;

    void Start() {
        if (isFireball) gameObject.tag = "Fireball";
        //SFXPlayer = FindObjectOfType<SoundSystem>();
        //if (SFXPlayer) print("Ball: SFXPlayer assigned");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //String collisionTag = collision.gameObject.tag;
        //if (collisionTag == "Paddle" && isGlueApplied) LockingToPaddle(collision);
        //PlaySFX(collision);
        TweakVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "LoseCollider") {
            //Debug.Log("Ball Triggerers LoseCollider");
            ManageLoseCollider();
        }
    }

    private void TweakVelocity() {
        Vector2 tweak = new Vector2(UnityEngine.Random.Range(0f, 0.2f), UnityEngine.Random.Range(0f, 0.2f));
        GetComponent<Rigidbody2D>().velocity += tweak;
    }

    private void ManageLoseCollider() {
        Destroy(gameObject);
    }

    

    //private bool isOtherBallPresent() {
    //    if (FindObjectsOfType<Ball>().Length > 1) return true;
    //    return false;
    //}

    //private void PlaySFX(Collision2D collision) {
    //    //Debug.Log("Ball collides with " + collision.gameObject.tag);
    //    if (SFXPlayer && SFXPlayer.GetVolume() != 0f) {
    //        switch (collision.gameObject.tag) {
    //            case "Unbreakable": SFXPlayer.PlayClip(unbreakableSound); break;
    //            case "Paddle": SFXPlayer.PlayClip(paddleSound); break;
    //            case "Wall": SFXPlayer.PlayClip(wallBounceSound); break;
    //            case "LoseColider": AudioSource.PlayClipAtPoint(loseSound, transform.position, SFXPlayer.GetVolume()); break;
    //            default: SFXPlayer.PlayClip(bounceSounds[UnityEngine.Random.Range(0, bounceSounds.Length)]); break;
    //        }
    //    }
    //}


}
