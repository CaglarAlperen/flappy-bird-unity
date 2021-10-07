using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Fly,
    Die
}

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    private float dieWaitTime = 1f;
    private float minY = -3.8f;
    private Vector3 velocity;
    private State state;
    private Animator animator;

    private void Awake()
    {
        velocity = Vector3.zero;
        state = State.Idle;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.Instance.Begin();
                    state = State.Fly;
                    animator.SetTrigger("Fly");
                    Jump();
                }
                break;
            case State.Fly:
                transform.position += velocity * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
                transform.localEulerAngles = transform.MyLerp(new Vector3(0f,0f,-45f), new Vector3(0f,0f,45f), velocity.y / jumpVelocity);
                if (Input.GetMouseButtonDown(0))
                    Jump();
                break;
            case State.Die:
                if (transform.position.y > minY)
                    transform.position -= new Vector3(0f, gravity, 0f) * Time.deltaTime;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Score")
            Die();
    }

    private void Jump()
    {
        velocity.y = jumpVelocity;
    }

    private void Die()
    {
        GameManager.Instance.Lose();
        state = State.Die;
        animator.SetTrigger("Die");
        StartCoroutine(WaitAndLose());
    }

    IEnumerator WaitAndLose()
    {
        yield return new WaitForSeconds(dieWaitTime);
        GameManager.Instance.Lose();
    }
}
