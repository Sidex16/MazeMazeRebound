using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public static Ball Instance { get; private set; }

    public event EventHandler OnBallDestroy;
    public event EventHandler OnBallHit;

    [SerializeField]
    private Transform shadowPrefab;
    [SerializeField]
    private float launchForce = 3;
    [SerializeField]
    private float trajectoryTimeStep = 0.05f;
    [SerializeField]
    private int trajectoryStepCount = 5;

    private Vector3 currentPosition;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    private Rigidbody2D shadow;
    private Collider2D shadowCollider;

    Vector3 velocity, startFingerPos, currentFingerPos;

    private bool isFrozen = false;
    private bool isShadowExists = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InputHandler.Instance.OnBallTouchOff += InputHandler_OnBallTouchOff;
        GameplayUI.Instance.OnLevelComplite += GameplayUI_OnLevelComplite;
    }

    private void GameplayUI_OnLevelComplite(object sender, EventArgs e)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;
    }

    private void InputHandler_OnBallTouchOff(object sender, System.EventArgs e)
    {
        if (!isShadowExists)
        {
            Shoot();
        }
    }

    private void Update()
    {
        if (InputHandler.Instance.GetIsBallTouch() && !isShadowExists)
        {
            startFingerPos = InputHandler.Instance.startFingerPos;
            Vector3 fingerPosition = InputHandler.Instance.GetFinger().screenPosition;
            fingerPosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
            currentPosition = transform.position + Vector3.ClampMagnitude(currentPosition - transform.position, 3);

            velocity = (startFingerPos - currentPosition) * launchForce;

            DrawTrajectory();

        }
        else
        {
            ClearTrajectory();
        }

        if (isFrozen && !isShadowExists)
        {
            if (shadow)
            {
                Collider2D collider = shadow.GetComponent<Collider2D>();
                collider.enabled = false;
            }
        }
        else
        {

            if (shadow)
            {
                Collider2D collider = shadow.GetComponent<Collider2D>();
                collider.enabled = true;
            }
        }
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
            OnBallDestroy?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float time = i * trajectoryTimeStep;
            Vector3 pos = transform.position + velocity * time + 0.5f * Vector3.zero * time * time;

            positions[i] = pos;
        }

        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }

    private void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }

    private IEnumerator FreezeAndResume()
    {
        OnBallHit?.Invoke(this, EventArgs.Empty);
        isFrozen = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();


        Vector3 originalVelocity = rb.velocity;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);

        isFrozen = false;

        rb.velocity = originalVelocity;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        isShadowExists = true;
    }

    private void Shoot()
    {
        CreateShadow();
        shadow.isKinematic = false;
        Vector3 ballForce = (currentPosition - transform.position) * launchForce * -1;
        shadow.velocity = ballForce;

        StartCoroutine(Delay());

    }

    private void CreateShadow()
    {
        shadow = Instantiate(shadowPrefab).GetComponent<Rigidbody2D>();
        shadow.transform.position = transform.position;
        shadowCollider = shadow.GetComponent<Collider2D>();
        shadowCollider.enabled = true;
        shadow.isKinematic = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFrozen)
        {

            StartCoroutine(FreezeAndResume());
        }
        if (isFrozen)
        {
            if (collision.gameObject.GetComponent<MoveUpDown>())
            {
                MoveUpDown moveUpDown = collision.gameObject.GetComponent<MoveUpDown>();

            }
        }
    }
}
