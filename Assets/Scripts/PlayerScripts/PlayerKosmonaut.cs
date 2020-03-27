using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GetMouseButtonDown(0) LinksKlick
//GetMouseButtonDown(1) RechtsKlick

public class PlayerKosmonaut : MonoBehaviour
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //PlayerMovement
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1f;
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
        targetPosition = new Vector3(transformedPosition.x, transformedPosition.y, z: 0);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(current: transform.position, targetPosition, maxDistanceDelta: Time.deltaTime * moveSpeed);
    }

    /*
     //WASD Movement
     private float moveH, moveV;

     //variable Daten
     [SerializeField] private float moveSpeed = 1.0f;
     //private int maxAir = 360;

     //Inputs
     private void FixedUpdate()
     {
         moveH = Input.GetAxis("Horizontal") * moveSpeed;
         moveV = Input.GetAxis("Vertical") * moveSpeed;
         rb.velocity = new Vector2(moveH, moveV);
     }

     void Update()
     {

     }
     //PlayerMovementEnds
     */
}
