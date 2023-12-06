using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{

    public float moveDistance = 2f; 
    public float moveSpeed = 2f; 

    private Vector3 initialPosition;
    public int direction = 1; 

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

        HorizontalMovement();

        if (timer > 0f)
        {
            MoveBall();
        }
    }

    private void HorizontalMovement()
    {
        float newX = initialPosition.x + direction * moveDistance;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, initialPosition.y, initialPosition.z), moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.x, newX))
        {
            direction *= -1;
        }
    }

    private void MoveBall()
    {
        float newX = initialPosition.x + direction * moveDistance;

        ball.transform.position = Vector3.MoveTowards(ball.transform.position, new Vector3(newX, initialPosition.y, initialPosition.z), moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(ball.transform.position.x, newX))
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


