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

    private void Start()
    {
        StartCoroutine("ChangeControllerPositionCoroutine");
    }

    int xAxis = 0;
    int yAxis = 0;

    private void Update()
    {
        if (name == "Joystick")
        {
            if (Input.GetKey(KeyCode.D)) xAxis = 1;
            else if (Input.GetKey(KeyCode.A)) xAxis = -1;
            else xAxis = 0;

            if (Input.GetKey(KeyCode.W)) yAxis = 1;
            else if (Input.GetKey(KeyCode.S)) yAxis = -1;
            else yAxis = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("PointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine("PointerDown");
        controller.position = transform.position;
        Normal = Vector3.zero;
    }

    IEnumerator PointerDown()
    {
        while(true)
        {
            Vector3 centerPos = transform.position;
            Vector3 mousePos = Input.mousePosition;

            float distance = Vector3.Distance(centerPos, mousePos);
            if (distance > radius) mousePos = centerPos + Vector3.ClampMagnitude((mousePos - centerPos), radius);
            controller.position = mousePos;

            Normal = Vector3.Normalize(mousePos - centerPos);

            yield return null;
        }
    }

    private IEnumerator ChangeControllerPositionCoroutine()
    {
        while (true)
        {
            Vector3 centerPos = transform.position;
            Vector3 controllerPos = controller.position;
            Vector3 destination = centerPos + new Vector3(xAxis, yAxis).normalized * radius;
            Vector3 destinationFrame = controllerPos + (destination - controllerPos).normalized * radius * 4 * Time.deltaTime;
            float distance = Vector3.Distance(centerPos, destinationFrame);

            Debug.Log((destination - destinationFrame).magnitude);

            if (distance != 0)
            {

                if (distance > radius) destinationFrame = centerPos + Vector3.ClampMagnitude((destinationFrame - centerPos), radius);
                if ((destination - destinationFrame).magnitude <= 10.0f) destinationFrame = destination;
                controller.position = destinationFrame;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
