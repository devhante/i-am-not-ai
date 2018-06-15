using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 Normal { get; private set; }
    Transform controller;
    float radius;

    private void Awake()
    {
        controller = transform.GetChild(0);
        radius = GetComponent<RectTransform>().sizeDelta.x * 0.5f;
    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("PointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine("PointerDown");
        controller.position = transform.position;
    }

    IEnumerator PointerDown()
    {
        while(true)
        {
            Vector3 centerPos = transform.position;
            Vector3 mousePos = Input.mousePosition;

            float distance = Vector3.Distance(centerPos, mousePos);
            if (distance > 64) mousePos = centerPos + Vector3.ClampMagnitude((mousePos - centerPos), radius);
            controller.position = mousePos;

            Normal = Vector3.Normalize(mousePos - centerPos);
            Debug.Log(Normal);

            yield return null;
        }
    }
}
