using Assets.Interfaces;
using Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [Header("physical setup")]
    [SerializeField] Paddle paddle1;
    [SerializeField] public float launchPower = 10f;
    [Header("Sounds setup")]
    [SerializeField] AudioClip unbreakableSound;
    [SerializeField] AudioClip paddleSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip wallBounceSound;

    //states
    public bool hasStarted = false;
    public Boolean isGlueApplied = false;
    /*must have this Vector3 because when GlueIsActive, PaddleBallRelation must change,
     * but need to keep basic relation for new ball or correction */
    Vector3 PaddleBallRelation;
    Vector3 basePaddleBallRelation;
    GameSession gameSession;
    SoundSystem SFXPlayer;

    // Use this for initialization
    void Start() {
        gameSession = FindObjectOfType<GameSession>();
        SFXPlayer = FindObjectOfType<SoundSystem>();
        //if (SFXPlayer) print("Ball: SFXPlayer assigned");
        if (gameSession) basePaddleBallRelation = gameSession.basePaddleBallRelation;
        PaddleBallRelation = basePaddleBallRelation;
        //print("PaddleBallRelation: " + PaddleBallRelation.ToString());
        if (gameSession) gameSession.AddBall();
        ManageFireballOnStart();
        if (isOtherBallPresent()) hasStarted = true;
    }

    private void ManageFireballOnStart() {
        if (this.tag == tags.FIREBALL)
            foreach (ParticleSystem effect in GetComponentsInChildren<ParticleSystem>())
                effect.Play();
    }

    // Update is called once per frame
    void Update() {
        if (!hasStarted) {
            lockToPaddle();
            launchOnClick();
        }
        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void launchOnClick() {
        if (!gameSession) gameSession = FindObjectOfType<GameSession>();
        if (Input.GetMouseButtonDown(0) && gameSession && gameSession.inputIsEnabled) {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(PaddleBallRelation.x * launchPower, PaddleBallRelation.y * launchPower);
        }
    }

    public Vector2 GetPaddleBallRelation() {
        return transform.position - paddle1.transform.position;
    }

    private void lockToPaddle() {
        transform.position = paddle1.transform.position + PaddleBallRelation;
    }

    private bool isOtherBallPresent() {
        if (FindObjectsOfType<Ball>().Length > 1) return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        String collisionTag = collision.gameObject.tag;
        CollisionWithPaddle(collision, collisionTag);
        PlaySFX(collision.gameObject);
        TweakVelocity();
    }

    private void CollisionWithPaddle(Collision2D collision, string collisionTag) {
        if (collisionTag == tags.PADDLE) {
            if (isGlueApplied) LockingToPaddle(collision);
            float paddleMovement = paddle1.GetMovementProps() * 10;
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            //print("Ball: Adding to velocity: paddleMovement: " + paddleMovement);
            velocity += new Vector2(paddleMovement, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlaySFX(collision.gameObject);
        if (collision.tag == tags.LOSE_COLLIDER) {
            //Debug.Log("Ball Triggerers LoseCollider");
            ManageLoseCollider();
        }
        if (collision.tag == tags.CORRECTOR) CorrectPaddleCollision();
    }

    public void CorrectPaddleCollision() {
        //Debug.Log("Correcting Ball");
        hasStarted = false;
        PaddleBallRelation = basePaddleBallRelation;
        lockToPaddle();
    }

    private void TweakVelocity() {
        Vector2 tweak = new Vector2(UnityEngine.Random.Range(0f, 0.2f), UnityEngine.Random.Range(0f, 0.2f));
        GetComponent<Rigidbody2D>().velocity += tweak;
    }

    private void PlaySFX(GameObject objectOfCollision) {
        //Debug.Log("Ball collides with " + collision.gameObject.tag);
        if (SFXPlayer && SFXPlayer.GetVolume() != 0f && hasStarted) {
            //print("Ball: PlaySFX: collision.tag: " + objectOfCollision.gameObject.tag);
            switch (objectOfCollision.tag) {
                case tags.UNBREAKABLE: SFXPlayer.PlayClipOnce(unbreakableSound); break;
                case tags.PADDLE: SFXPlayer.PlayClipOnce(paddleSound); break;
                case tags.WALL: SFXPlayer.PlayClipOnce(wallBounceSound); break;
                default: {
                        IPlayList B = objectOfCollision.GetComponent<IPlayList>();
                        if (B != null) {
                            //print("Ball: PlaySFX: collision.gameObject is IPLayList class, PlayListID is: " + B.GetPlayListID().ToString());
                            SFXPlayer.PlayRandomSoundFromList(B.GetPlayListID());
                        } else {
                            print("Ball/PlaySFX: failed getting PlayListID, playing BrickSounds");
                            SFXPlayer.PlayRandomSoundFromList(SoundSystem.PlayListID.Brick);
                        }; break;
                    }
            }
        }
    }

    private void ManageLoseCollider() {
        //print("Should play lose sound");
        if (SFXPlayer) SFXPlayer.PlayClipOnce(loseSound);
        if (FindObjectsOfType<Ball>().Length > 1) Destroy(gameObject);
        else CorrectPaddleCollision();
        if (gameSession) gameSession.RetractBall();
    }

    private void LockingToPaddle(Collision2D collision) {
        if (collision.rigidbody != null && collision.rigidbody.tag == tags.PADDLE) {
            //Debug.Log("Locking ball to Paddle on Collision");
            PaddleBallRelation = GetPaddleBallRelation();
            lockToPaddle();
            hasStarted = false;
        }
    }

}
