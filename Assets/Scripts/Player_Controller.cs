using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship_Controller))]
public class Player_Controller : MonoBehaviour
{

    private Ship_Controller controller;
    private Ship_Motor motor;

    private int selected_projectile = 0;
    /*
     * Entry 0: Green Laser shot
     * Entry 1: Missile
    */

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<Ship_Controller>();
        motor = GetComponent<Ship_Motor>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            controller.Shoot(selected_projectile);
        }

        if ( (Input.GetButtonDown("Reload")))
        {
            StartCoroutine(controller.Reload());
        }
    }
}
