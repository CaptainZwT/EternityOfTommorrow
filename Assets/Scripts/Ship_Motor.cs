using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship_Motor : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;

    private Ship_Controller controller;

    private Rigidbody2D body;

    public bool can_move = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Ship_Controller>();
        body = GetComponent<Rigidbody2D>();
    }

    public void move(float hor_axis, float ver_axis)
    {
        if (controller)
        {
            if ((controller.status) && (can_move))
            {
                moveDirection = new Vector2(hor_axis * controller.turn_speed, Input.GetAxis("Vertical") * controller.lift_speed);

                body.MovePosition(body.position + moveDirection * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!can_move)
        {
            body.velocity = new Vector2(0, 0);
            body.angularVelocity = 0f;
        }
    }
}
