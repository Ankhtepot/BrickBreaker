using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float leftConstraint = 1;
    [SerializeField] float rightConstraint = 15;
    [SerializeField] float bottomConstraint = 0.63f;
    [SerializeField] bool autoPlay = false;
    [SerializeField] float ZTransformPosition = -5f;
    [SerializeField] float movementDirection;

    //cached variables
    float mouseXInWU;
    Ball ball;
    GameSession gameSession;

    float xPos;

    void Start() {
        SwitchOffPickupSprites();
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        xPos = transform.position.x;
    }

    private static void SwitchOffPickupSprites() {
        FindObjectOfType<GlueActiveEffect>().GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update() {
        if (gameSession && gameSession.inputIsEnabled) {
            if (autoPlay) {
                Autoplay();
            } else {
                MoveWithMouse();
            }
            RegisterMovementOnX(); 
        }
    }

    private void RegisterMovementOnX() {
        if (xPos != transform.position.x) movementDirection = transform.position.x - xPos;
        xPos = transform.position.x;
    }

    public float GetMovementProps() {
        return movementDirection;
    }

    private void Autoplay() {
        transform.position = new Vector3(ball.transform.position.x, transform.position.y, ZTransformPosition);
    }

    private void MoveWithMouse() {
        mouseXInWU = Input.mousePosition.x / Screen.width * 16;
        float transformedMouseX = Mathf.Clamp(mouseXInWU, leftConstraint, rightConstraint);
        transform.position = new Vector3(transformedMouseX, bottomConstraint, ZTransformPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("Paddle: trigger fired from " + collision.name);
    }

    private void OnCollisionStay(Collision collision) {
        Ball ball = FindObjectOfType<Ball>();
        float launchPower = ball.launchPower;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f * launchPower, ball.GetPaddleBallRelation().y * launchPower );
    }


}
