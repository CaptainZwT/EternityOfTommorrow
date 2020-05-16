using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Script : MonoBehaviour
{
    private Animator controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8)
        {
            Destroy(gameObject);
        }
    }
}
