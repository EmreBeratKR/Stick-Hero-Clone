using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private StickManager stickManager;


    void Start()
    {
        stickManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<StickManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Passed!");
        stickManager.success = true;
    }
}
