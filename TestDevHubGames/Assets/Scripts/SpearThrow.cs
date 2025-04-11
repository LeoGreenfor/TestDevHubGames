using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpearThrow : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject spearPrefab;
    public Transform throwPoint;
    public float maxThrowForce = 20f;
    public float chargeRate = 10f;

    private GameObject currentSpearObject;
    public ThrowPredictor predictor;

    public float currentForce = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
            if (touch.phase == TouchPhase.Began)
            {
                StartCharging();
            }

            if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && isCharging)
            {
                Charging();
            }

            if (touch.phase == TouchPhase.Ended && isCharging)
            {
                Release();
            }
        }
    }

    private void StartCharging()
    {
        if (!isCharging)
        {
            currentSpearObject = Instantiate(spearPrefab, throwPoint.position, throwPoint.rotation, throwPoint);
            currentForce = 0f;
            isCharging = true;
        }
    }

    private void Charging()
    {
        currentForce += chargeRate * Time.deltaTime;
        currentForce = Mathf.Clamp(currentForce, 0f, maxThrowForce);

        Rigidbody rb = currentSpearObject.GetComponent<Rigidbody>();
        predictor.DrawTrajectoryLine(rb, throwPoint.forward * currentForce);
    }

    private void Release()
    {
        ThrowSpear();
        isCharging = false;
    }

    private void ThrowSpear()
    {
        Rigidbody rb = currentSpearObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(throwPoint.forward * currentForce, ForceMode.Impulse);
        }

        currentForce = 0f;
        predictor.EraseLine();
    }
}
