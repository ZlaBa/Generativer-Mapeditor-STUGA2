using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f; 
    private Vector3 targetPosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CalculateTargetPosition();
            Debug.Log(targetPosition);
        }

        MoveToTarget();
    }

    private void CalculateTargetPosition()
    {
        var mousePosition = Input.mousePosition;
        var transformedPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        targetPosition = new Vector3(transformedPosition.x, transformedPosition.y, z:0);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards( current: transform.position, targetPosition, maxDistanceDelta: Time.deltaTime * movementSpeed);
    }
}