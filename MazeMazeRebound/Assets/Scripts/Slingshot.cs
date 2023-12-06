using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public static Slingshot Instance { get; private set; }

    public event EventHandler OnShoot;


    [SerializeField]
    private LineRenderer[] lineRenderers;
    [SerializeField]
    private Transform[] stripPositions;
    [SerializeField]
    private Transform center;
    [SerializeField]
    private Transform idlePosition;
    [SerializeField]
    private float maxLenght;
    [SerializeField]
    private BallSkinsSO ballSkinsSO;
    [SerializeField]
    private float ballPositionOffset;
    [SerializeField]
    private float force;

    private Rigidbody2D ball;
    private Collider2D ballCollider;

    private Vector3 currentPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CreateBall();

        UpdateStripPositions();
    }

    private void Start()
    {
        InputHandler.Instance.OnFingerUp += InputHandler_OnFingerUp;
        InputHandler.Instance.OnSlingshotTeleported += InputHandler_OnSlingshotTeleported;
        ball.gameObject.GetComponent<Ball>().OnBallDestroy += Ball_OnBallDestroy;
    }

    private void InputHandler_OnSlingshotTeleported(object sender, System.EventArgs e)
    {
        UpdateStripPositions();
    }

    private void Ball_OnBallDestroy(object sender, System.EventArgs e)
    {
        CreateBall();
    }

    private void InputHandler_OnFingerUp(object sender, System.EventArgs e)
    {
        OnShoot?.Invoke(this, EventArgs.Empty);
        Shoot();
    }

    private void Update()
    {
        if (InputHandler.Instance.GetIsFingerOnScreen())
        {
            Vector3 fingerPosition = InputHandler.Instance.GetFinger().screenPosition;
            fingerPosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLenght);

            SetStrips(currentPosition);

            if (ballCollider)
            {
                ballCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    private void Shoot()
    {
        ball.isKinematic = false;
        Vector3 ballForce = (currentPosition - center.position) * force * -1;
        ball.velocity = ballForce;

        ball = null;
        ballCollider = null;
    }

    private void CreateBall()
    {
        ball = Instantiate(ballSkinsSO.ballSkins[SaveManager.LoadPlayerData().skinIndex]).GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<Collider2D>();
        ballCollider.enabled = false;
        ball.isKinematic = true;
        
    }

    private void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    private void SetStrips(Vector3 position)
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].SetPosition(1, position);
        }
        if (ball)
        {
            Vector3 dir = position - center.position;
            ball.transform.position = position + dir.normalized * ballPositionOffset;
        }
    }

    private void UpdateStripPositions()
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].positionCount = 2;
            lineRenderers[i].SetPosition(0, stripPositions[i].position);
        }
    }
}
