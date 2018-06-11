using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 Normal { get; private set; }
    Transform controller;

    private void Awake()
    {
        controller = transform.GetChild(0);
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
            float ratio = 64.0f / distance;
            Vector3 position = centerPos + (mousePos - centerPos) * ratio;
            controller.position = position;

            Normal = Vector3.Normalize(mousePos - centerPos);
            //float degree = Mathf.Atan2(mousePos.y - centerPos.y, mousePos.x - centerPos.x) * 180.0f / Mathf.PI;
            Debug.Log(Normal);

            yield return null;
        }
    }
}
