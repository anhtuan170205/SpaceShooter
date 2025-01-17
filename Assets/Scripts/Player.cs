using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float paddingHorizontal = 0.5f;
    [SerializeField] float paddingVertical = 0.5f;
    Vector2 moveInput;

    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        initBound();
    }

    void Update()
    {
        Move();
    }
    void initBound()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void Move()
    {
        Vector3 delta = moveInput * speed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingHorizontal, maxBounds.x - paddingHorizontal);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingVertical, maxBounds.y - paddingVertical);
        transform.position = newPos;
    }
    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
