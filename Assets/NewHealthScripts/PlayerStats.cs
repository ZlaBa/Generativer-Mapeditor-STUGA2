using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public float Air;
    public float airOverTime;


    public Slider AirBar;

    public float playerSpeed = 1f;
    public float movementAirFactor = 2f;

    Rigidbody2D Kosmonaut;

    // Start is called before the first frame update
    void Start()
    {
        Kosmonaut = GetComponent<Rigidbody2D>();
        AirBar.maxValue = Air;

        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateValues();
    }

    private void CalculateValues()
    {
        Air -= airOverTime * Time.deltaTime;

        if (Kosmonaut.velocity.magnitude >= playerSpeed)
        {
            Air -= movementAirFactor * Time.deltaTime;
        }
        if (Air <= 0)
        {
            //print("YOU DIED!");
            FindObjectOfType<GameStateManager>().GameOver();
        }
        updateUI();
    }

    private void updateUI()
    {
        Air = Mathf.Clamp(Air, 0, 360f);
        AirBar.value = Air;
    }
}
