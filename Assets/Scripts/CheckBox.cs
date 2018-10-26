using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour {

    [SerializeField] bool isOn = true;
    [SerializeField] UnityEvent toggleAction;
    [Header("Sprite setup")]
    [SerializeField] Sprite OnStateBody;
    [SerializeField] Sprite OnStateBackground;
    [SerializeField] Sprite OffStateBody;
    [SerializeField] Sprite OffStateBackground;


    // Cached states
    SpriteRenderer bodySprite;
    Options options;
    Animator animator;

    void Start() {
        bodySprite = GetComponent<SpriteRenderer>();
        options = FindObjectOfType<Options>();
        animator = GetComponent<Animator>();
        if (options) isOn = options.ShowHintBoards;
        SetCheckBoxState();
    }

    // Update is called once per frame
    void Update() {
        OnClick();
    }

    private void OnClick() {
        RaycastHit2D hit;
        //if (Input.GetButton("Fire1")) {
        if (Input.GetMouseButtonDown(0)) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.tag == "CheckBox") {
                print("Just clicked on CheckBox");
                isOn = !isOn;
                if (options) options.ShowHintBoards = isOn;
                SetCheckBoxState();
                toggleAction.Invoke();
            }
        }
    }

    private void GetOnStateBody() {
        //print("CheckBox: GetOnStateBody " + " bodySprite: " + bodySprite.sprite.name);
        if (OnStateBody) {
            bodySprite.sprite = OnStateBody;
            //print("CheckBox: GetOnStateBody isVisible: " + bodySprite.isVisible + " bodySprite: " + bodySprite.sprite.name);
        }
    }

    private void GetOffStateBody() {
        if (OffStateBody) {
            bodySprite.sprite = OffStateBody;
        } else if (OnStateBody) {
            //bodySprite.sprite = OnStateBody;
        }
    }

    public void RemoteSetOfIsOn(bool state) {
        //print("CheckBox: RemoteSetOfIsOn: state: " + state);
        isOn = state;
        SetCheckBoxState();
    }

    private void SetCheckBoxState() {
        if (!isOn && animator) {
            print("CheckBox: !isOn, should switch to off");            
            animator.SetTrigger("SwitchOff");
        } else if (isOn && animator) {
            print("CheckBox: isOn, should switch to on");
            animator.SetTrigger("SwitchOn");
        }
    }
}
