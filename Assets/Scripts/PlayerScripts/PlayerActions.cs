using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //PlayerMovement
    private Rigidbody2D rb;

    private float moveH, moveV;
    
    //variable Daten
    [SerializeField] private float moveSpeed = 1.0f;
    public int Luft = 360;

    private void FixedUpdate()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;
        rb.velocity = new Vector2(moveH, moveV);
    }
    //PlayerMovementEnds

    // Update is called once per frame
    void Update()
    {
        
    }
}
