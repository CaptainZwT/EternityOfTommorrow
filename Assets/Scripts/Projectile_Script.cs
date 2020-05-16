using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour
{

    public float speed = 10f;

    public float damage = 10f;

    private Rigidbody2D body;

    public GameObject[] effects;

    /*
     * Explosion 
    */

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(decay());
        body = GetComponent<Rigidbody2D>();

        body.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.gameObject.name + " was hit.");
        if (hitInfo.gameObject.GetComponent<Ship_Controller>())
        {

            Ship_Controller col_ship = hitInfo.gameObject.GetComponent<Ship_Controller>();

            if (col_ship.health > 0)
            {
                col_ship.health -= damage;
            }
        }
        Instantiate(effects[0], (Vector2)transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }

    private IEnumerator decay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
