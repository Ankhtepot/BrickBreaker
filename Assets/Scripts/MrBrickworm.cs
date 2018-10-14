using Assets.Classes;
using Assets.Interfaces;
using Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    class MrBrickworm : Boss {
        [Header("1. Stats")]
        [SerializeField] int HealthPointsBase = 100;
        [SerializeField] int HealthPointsCurrent;
        [SerializeField] int DamageByBall = 1;
        [SerializeField] int DamageByFireball = 5;
        [SerializeField] float DelayHBarDisable = 0.5f;
        [Header("3. Chaches")] 
        [SerializeField] AudioClip SFXArriveSound;
        [SerializeField] AudioClip SFXSpellSound;
        [SerializeField] AudioClip SFXOpeningDoors;
        [SerializeField] SpriteRenderer opennedDoors;
        [SerializeField] ParticleSystem PSPlatform;
        [SerializeField] ParticleSystem PSOpennedDoors;
        [SerializeField] Animator MrBAnimator;
        [SerializeField] ProgressBar HealthBar;
        [Header("2. Movement props")]
        [SerializeField] GameObject PositionHandler;
        [SerializeField] MrBWaypoints Waypoints;
        [SerializeField] GameObject startPosition;
        [SerializeField] float RestingPeriod = 1f;
        [SerializeField] float MovementSpeed = 0.1f;

        //cached vars
        bool isMoving = false;
        [SerializeField] public bool arrived = false;
        [SerializeField] bool isResting = false;
        [SerializeField] Transform targetPosition = null;

        public override void Dying() {
            OnDeath();
        }

        new private void Start() {
            base.Start();
            GetTargetPosition();
            HealthPointsCurrent = HealthPointsBase;
            MrBAnimator = GameObject.Find(gameobjects.MRBRICKWORM).GetComponent<Animator>();
        }

        private void Update() {
            ResolveMoving();
        }

        private void ResolveMoving() {
            if (!ArrivedAtPoint() && arrived && !isResting) {
                //print("should be moving, difference between current and target is: x: " + (PositionHandler.transform.position.x - targetPosition.position.x) + " y: " + (PositionHandler.transform.position.y - targetPosition.position.y));
                PositionHandler.transform.position = Vector2.MoveTowards(PositionHandler.transform.position, targetPosition.position, MovementSpeed * Time.deltaTime);
                //transform.position += new Vector3(0.2f, 0.2f); 
            } else if(ArrivedAtPoint() && !isResting) {
                //print("MrBrickworm/ResolveMoving: Reached targetPosition");
                isResting = true;
                StartCoroutine(RestAfterMovement());
            }
        }

        private bool ArrivedAtPoint() {
            if ((PositionHandler.transform.position.x - targetPosition.position.x) <= 0.01f &&
                +(PositionHandler.transform.position.y - targetPosition.position.y) <= 0.01f)
                return true;
            return false;
        }

        private void GetTargetPosition() {
            targetPosition = Waypoints.GetWaypoints()
                [UnityEngine.Random.Range(0, Waypoints.GetWaypoints().Count())].transform;
        }

        IEnumerator RestAfterMovement() {
            yield return new WaitForSeconds(RestingPeriod);
            GetTargetPosition();
            isResting = false;
        }

        public override void OnArrival() {
            //print("MrBrickworm: OnArrival: test OK");
            opennedDoors.enabled = true;
            SFXPlayer.PlayClipOnce(SFXOpeningDoors);
            PSOpennedDoors.Play();
            StartCoroutine(Arrive());
        }

        IEnumerator Arrive() {
            yield return new WaitForSeconds(0.5f);
            SFXPlayer.PlayClipOnce(SFXSpellSound);
            MrBAnimator.SetTrigger(triggers.START);
            yield return new WaitForSeconds(2f);
            HealthBar.EnableVisuals();
            SFXPlayer.PlayClipOnce(SFXArriveSound);
            yield return new WaitForSeconds(1.5f);
            arrived = true;
        }

        public void startPlatformPS() {
            PSPlatform.Play();
        }

        public override void OnCollisionEnter2D(Collision2D collision) {
            String tag = collision.gameObject.tag;
            HealthChange(tag);
        }

        private void HealthChange(String tag) {
            float change = 0f;
            if (tag == tags.BALL) change = -DamageByBall;
            if (tag == tags.FIREBALL) change = -DamageByFireball;
            ChangeHPValue((int)change);
            HealthBar.UpdateBar(healthChangeInPorcent(change));
            if (HealthPointsCurrent <= 0) Dying();
        }

        private void ChangeHPValue(int change) {
            HealthPointsCurrent += change;
        }

        private float healthChangeInPorcent(float percent) {
            return (HealthPointsBase/100) * percent;
        }

        public override void OnDeath() {
            print("Boss \"died\"");
            StartCoroutine(DisableHealthBar());
        }

        private IEnumerator DisableHealthBar() {
            yield return new WaitForSeconds(DelayHBarDisable);
            HealthBar.DisableVisuals();
        }

        public override void StartEncounter() {
            OnArrival();
        }

        public override SoundSystem.PlayListID GetPlayListID() {
            return SoundSystem.PlayListID.Boss;
        }
    }
}
