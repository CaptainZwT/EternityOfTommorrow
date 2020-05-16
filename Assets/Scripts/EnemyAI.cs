using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship_Controller))]

public class EnemyAI : MonoBehaviour
{

    public Ship_Controller target;

    private Ship_Controller controller;
    private Rigidbody2D body;
    private Ship_Motor motor;

    private Transform UICanvas;

    private Transform HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Ship_Controller>();
        body = GetComponent<Rigidbody2D>();
        motor = GetComponent<Ship_Motor>();

        UICanvas = transform.Find("UI");
        HealthBar = UICanvas.transform.Find("HealthBar").Find("health");

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = ((Vector2)target.center.transform.position - (Vector2)body.transform.position).normalized;
        Vector2 force = (direction * (controller.turn_speed)) * (Time.deltaTime * controller.lift_speed);

        body.AddForce(force);

        UpdateUI();
    }

    // Helper functions
    public void UpdateUI()
    {
        HealthBar.localScale = new Vector3((controller.health / 100), 1, 1);
    }
}
