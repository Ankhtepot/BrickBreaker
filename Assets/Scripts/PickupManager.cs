using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
    [Header("Setup")]
    [SerializeField] GameObject[] ListOfUsedPickups;
    [SerializeField] float PickupDropChance = 0.7f;
    [Header("Glue setup")]
    [SerializeField] float GlueDuration = 10f;
    [Header("Laser setup")]
    [SerializeField] float LaserDuration = 10f;
    [SerializeField] MagicShot magicShot;
    [SerializeField] float MagicShotYOffset = 0.2f;
    [Header("Fireball setup")]
    [SerializeField] float FireballDuration = 10f;
    [SerializeField] Sprite FireballSprite;
    [SerializeField] Sprite NormalBallSprite;
    [Header("Enlarge setup")]
    [SerializeField] float EnlargeDuration = 10f;
    [Header("Multiple setup")]    
    [SerializeField] int MaxBallsSpawnedAtOnce = 10;
    //[SerializeField] Fireball fireball;
    //[SerializeField] int GlueCount = 0;
    //[SerializeField] int LaserCount = 0;
    //[SerializeField] int FireballCount = 0;
    //[SerializeField] int EnlargeCount = 0;

    //status
    GameSession gameSession;
    bool isLaserActive = false;
    bool isEnlargeActive = false;
    Vector3 MagicShotOffset;
    private int GlueCount = 0;
    private int LaserCount = 0;
    private int FireballCount = 0;
    private int EnlargeCount = 0;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        MagicShotOffset = new Vector3(0, MagicShotYOffset, 0);
        
    }

    private void Update() {
        if (isLaserActive && Input.GetMouseButtonDown(0)) {
            FireMagicShot();
        }
    }

    private void FireMagicShot() {
        MagicBall[] balls = FindObjectsOfType<MagicBall>();
        foreach (MagicBall ball in balls) {
            Instantiate(magicShot, ball.transform.position + MagicShotOffset, Quaternion.identity);
        }
    }

    public GameObject[] GetList() {
        return ListOfUsedPickups;
    }

    public void ProcessPickupChanceOfSpawning(Vector2 spawnPosition) {
        int chanceRoll = UnityEngine.Random.Range(1, 101);
        if (chanceRoll <= PickupDropChance) {
            SpawnPickup(spawnPosition);
        }
    }

    private void SpawnPickup(Vector2 spawnPosition) {
        int chanceRatio = UnityEngine.Random.Range(0, ListOfUsedPickups.Length);
        Instantiate(ListOfUsedPickups[chanceRatio], spawnPosition, new Quaternion(0, 0, 0, 0));
    }

    public void ApplyEffect(Pickup.PickupType pickupType) {
        //Debug.Log("Applying effect of Pickup: " + pickupType.ToString());
        switch (pickupType) {
            case (Pickup.PickupType.Glue): StartCoroutine(ActivateGlue()); break;
            case (Pickup.PickupType.Life): ActivateLife(); break;
            case (Pickup.PickupType.Laser): StartCoroutine(ActivateLaser()); break;
            case (Pickup.PickupType.Multiple): ActivateMultiple(); break;
            case (Pickup.PickupType.Enlarge): StartCoroutine(ActivateEnlarge()); break;
            case (Pickup.PickupType.Fireball): StartCoroutine(ActivateFireball()); break;
        }
    }

    IEnumerator ActivateFireball() {
        foreach (Ball ball in FindObjectsOfType<Ball>()) {
            ball.GetComponent<SpriteRenderer>().sprite = FireballSprite;
            ball.tag = "Fireball";
            foreach(ParticleSystem effect in ball.GetComponentsInChildren<ParticleSystem>())
                effect.Play();
            //ball.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        foreach(Brick brick in FindObjectsOfType<Brick>()) {
            brick.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        FireballCount++;
        
        //print("Activating Fireball");
        yield return new WaitForSeconds(FireballDuration);
        //print("End of waiting" + Time.time);
        FireballCount--; //print("Laser Count= " + LaserCount);
        if (FireballCount <= 0) {
            foreach (Ball ball in FindObjectsOfType<Ball>()) {
                ball.GetComponent<SpriteRenderer>().sprite = NormalBallSprite;
                ball.tag = "Ball";
                foreach (ParticleSystem effect in ball.GetComponentsInChildren<ParticleSystem>()) { 
                    //print("PickupManager: ActivateFireball: effect name to switch off: " + effect.name);
                    if(effect.name != "sparkles") effect.Stop();
                }
            }
            foreach (Brick brick in FindObjectsOfType<Brick>()) {
                brick.GetComponent<PolygonCollider2D>().isTrigger = false;
            }
            //Debug.Log("Suspending Fireball");
        }
    }

    IEnumerator ActivateEnlarge() {
        if (!isEnlargeActive) {
            foreach (EnlargeShield shield in FindObjectsOfType<EnlargeShield>()) {
                //shield.GetComponent<SpriteRenderer>().enabled = true;
                shield.GetComponent<Animator>().SetTrigger("Arrive");
                shield.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
        EnlargeCount++;
        isEnlargeActive = true;
        //print("Start waiting" + Time.time);
        yield return new WaitForSeconds(EnlargeDuration);
        //print("End of waiting" + Time.time);
        EnlargeCount--; //print("Laser Count= " + LaserCount);
        if (EnlargeCount <= 0) {
            foreach (EnlargeShield shield in FindObjectsOfType<EnlargeShield>()) {
                shield.GetComponent<Animator>().SetTrigger("Vanish");
                //shield.GetComponent<SpriteRenderer>().enabled = false;
                shield.GetComponent<PolygonCollider2D>().enabled = false;
            }
            //Debug.Log("Suspending Laser");
            isEnlargeActive = false;
        }
    }

    

    private void ActivateMultiple() {
        if (gameSession.BallAmount() < MaxBallsSpawnedAtOnce + 1) {
            Ball[] balls = FindObjectsOfType<Ball>();
            //Debug.Log("ActivateMultiple(): number of balls in a field: " + balls.Length);
            foreach (Ball oldBall in balls) {
                for (int i = 0; i < 3; i++) {
                    Vector2 spawnPosition = new Vector2(Mathf.Clamp(oldBall.transform.position.x + i/10 + 0.2f, 1, 15), Mathf.Clamp(oldBall.transform.position.y + i + 0.2f, 1, 11));
                    //Vector3 altSpawnPosition = new Vector2(UnityEngine.Random.Range(0.2f, 0.5f), UnityEngine.Random.Range(0.2f, 0.5f));
                    if (FindObjectsOfType<Ball>().Length < MaxBallsSpawnedAtOnce + 1) {
                        Ball newBall = Instantiate(oldBall, spawnPosition, Quaternion.identity);
                        if (oldBall.isGlueApplied) newBall.isGlueApplied = true;
                        newBall.GetComponent<Rigidbody2D>().velocity = oldBall.GetComponent<Rigidbody2D>().velocity;
                        //Debug.Log("ActivateMultiple(): Spawned new Ball, balls total: " + gameSession.BallAmount());
                    }
                }
            }
        }
    }

    IEnumerator ActivateLaser() {
        if (!isLaserActive) {
            foreach (MagicBall ball in FindObjectsOfType<MagicBall>()) {                
                ball.GetComponent<Animator>().SetTrigger("Arrive");
            }
        }
        LaserCount++;
        isLaserActive = true;
        //print("Start waiting" + Time.time);
        yield return new WaitForSeconds(LaserDuration);
        //print("End of waiting" + Time.time);
        LaserCount--; //print("Laser Count= " + LaserCount);
        if (LaserCount <= 0) {
            foreach (MagicBall ball in FindObjectsOfType<MagicBall>()) {
                ball.GetComponent<Animator>().SetTrigger("Vanish");
            }
            //Debug.Log("Suspending Laser");
            isLaserActive = false;
        }
    }

    //IEnumerator VanishLaser(Animator anim) {
    //    anim.SetTrigger("Vanish");
    //    yield return new WaitForSeconds(1f);
    //}

    private void ActivateLife() {
        //Debug.Log("PickupManager: Activating addLife");
        gameSession.AddLife();
    }

    IEnumerator ActivateGlue() {
        GlueActiveEffect glueEffectOnPaddle = FindObjectOfType<GlueActiveEffect>();
        if (glueEffectOnPaddle) {
            glueEffectOnPaddle.GetComponent<SpriteRenderer>().enabled = true;
            glueEffectOnPaddle.GetComponent<ParticleSystem>().Play(); 
        }
        GlueCount++;
        foreach (Ball ball in FindObjectsOfType<Ball>()) {
            ball.isGlueApplied = true;            
        }

        //print("Start waiting" + Time.time);
        yield return new WaitForSeconds(GlueDuration);
        //print("End of waiting" + Time.time);
        GlueCount--;
        if (GlueCount <= 0) {
            if (glueEffectOnPaddle) {
                glueEffectOnPaddle.GetComponent<ParticleSystem>().Stop();
                SwitchSpriteOff<GlueActiveEffect>(); 
            }
            //Debug.Log("Suspending Glue");
            foreach (Ball ball in FindObjectsOfType<Ball>()) {
                ball.isGlueApplied = false;                
            }
        }
    }

    private void SwitchSpriteOff<T>() where T : Component {
        FindObjectOfType<T>().GetComponent<SpriteRenderer>().enabled = false;
    }

}
