using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShadow : MonoBehaviour
{
    private float bouncesRemains = 5;
    private float lastCollisionTime;

    private void Update()
    {
        if(bouncesRemains <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float currentTime = Time.time;

        if (currentTime - lastCollisionTime >= 0.3f)
        {
            bouncesRemains--;

            lastCollisionTime = currentTime;
        }
    }
}
