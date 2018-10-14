using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("LoseColider trigger on by " + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //print("LoseColider collision with " + collision.gameObject.name);
    }


}
