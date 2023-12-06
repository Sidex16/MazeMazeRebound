using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float moveDistance = 2f; 
    public float moveSpeed = 2f;

    private Vector3 initialPosition;
    private int direction = 1;

    private float timer;
    private float timerMax = 1f;

    private Ball ball;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        timer -= Time.deltaTime;    

        VerticalMovement();
         
        if(timer > 0f)
        {
            MoveBall();
        }
    }

    private void VerticalMovement()
    {
        float newY = initialPosition.y + direction * moveDistance;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(initialPosition.x, newY, initialPosition.z), moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.y, newY))
        {
            direction *= -1;
        }
    }

    private void MoveBall ()
    {
        float newY = initialPosition.y + direction * moveDistance;

        ball.transform.position = Vector3.MoveTowards(ball.transform.position, new Vector3(initialPosition.x, newY, initialPosition.z), moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(ball.transform.position.y, newY))
        {
            direction *= -1; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            ball = collision.gameObject.GetComponent<Ball>();
            timer = timerMax;
        }
    }
}


