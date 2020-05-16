using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship_Motor))]
public class Ship_Controller : MonoBehaviour
{
    //[SerializeField]
    public float health = 100f,
                  turn_speed = 50f,
                  lift_speed = 8f,
                  armor = 1f;

    // Shooting information
    private float reload_speed = 0.35f;
    [HideInInspector]
    public float nextTimeToFire;
    //[Header ("Weapon Stats")]
    public int MaxAmmo = 36,
               CurrentAmmo,
               MaxMagazine = 6,
               CurrentMagazine;

    //

    public bool status = true,
                isReloading = false;
    

    private Ship_Motor motor;
    public AudioSource voice;
    public GameObject center;

    public AudioClip[] sounds;
    /*
     * Entry 0: Death Sound
     * Entry 1: Shooting Sound
     * Entry 2: Reloading Sound
     * Entry 3: Out Of Ammo Sound
    */

    public GameObject[] weapons;
    /*
     * Entry 0: Laser Battery
    */

    public GameObject[] projectiles;
    /*
     * Entry 0: Green Laser
     * Entry 1: Missile
    */
    public GameObject[] effects;
    /*
     * Entry 0: Explosion
    */

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<Ship_Motor>();

        center = gameObject.transform.Find("Body").Find("main").gameObject;

        voice = center.GetComponent<AudioSource>();

        CurrentAmmo = MaxAmmo;
        CurrentMagazine = MaxMagazine;
    }

    // Update is called once per frame
    void Update()
    {
        if ((health <= 0) && (status))
        {
            status = false;
            health = 0;
            StartCoroutine(destruct(2f));
        }
    }

    //
    void FixedUpdate()
    {

    }

    // Ship Helper Functions

    public void Shoot(int proj_type = 0)
    {
        if ((CurrentMagazine > 0) && (Time.time >= nextTimeToFire) && (!isReloading) )
        {
            // remove ability to fire pending reload
            nextTimeToFire = Time.time + reload_speed;
            // Reduce the ammo for this ship by 1
            CurrentMagazine--;

            // Play sound effect
            voice.clip = sounds[1];
            voice.Play();

            // Fire shot
            Instantiate(projectiles[proj_type], (new Vector2(weapons[0].transform.position.x, weapons[0].transform.position.y + 1f)), Quaternion.Euler(0, 0, 90));

            // Start the Reload coroutine
            //StartCoroutine(reload());
        }
    }

    // after waitTime, destroy the ship
    private IEnumerator destruct(float waitTime)
    {
        voice.clip = sounds[0];
        voice.Play();
        yield return new WaitForSeconds(waitTime/2);

        for (int i = 0; i < 8; i++)
        {
            float x_offset = Random.Range(-0.5f, 0.5f),
                    y_offset = Random.Range(-0.5f, 0.5f);
            Instantiate(effects[0], (new Vector2(center.transform.position.x, center.transform.position.y)
                                        + new Vector2(x_offset, y_offset)), Quaternion.identity);
        }

        yield return new WaitForSeconds(waitTime / 2);
        Destroy(gameObject);
    }

    // Reload the ammunition
    public IEnumerator Reload()
    {
        if (!isReloading)
        {
            if (CurrentAmmo < 1)
            {
                voice.clip = sounds[3];
                voice.Play();
            }
            else
            {
                isReloading = true;
                yield return new WaitForSeconds(1f / reload_speed);

                int neededammo = MaxMagazine - CurrentMagazine;

                if (CurrentAmmo >= neededammo)
                {
                    CurrentMagazine += neededammo;
                    CurrentAmmo -= neededammo;
                }
                else if (CurrentAmmo > 0)
                {
                    CurrentMagazine += CurrentAmmo;
                    CurrentAmmo = 0;
                }
                else
                {
                    voice.clip = sounds[3];
                    voice.Play();
                }

                voice.clip = sounds[2];
                voice.Play();
                isReloading = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " was hit.");
        if (collision.gameObject.name == "Border")
        {
            motor.can_move = false;
        }
    }
}
