#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ResponsiveElementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] bool isOver = false;
    [SerializeField] UnityEvent MouseOver;
    [SerializeField] UnityEvent MouseLeave;

    public void OnPointerEnter(PointerEventData eventData) {
        isOver = true;
        MouseOver.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData) {
        isOver = false;
        MouseLeave.Invoke();
    }
}
