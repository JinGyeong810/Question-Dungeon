using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float turn = Input.GetAxis("Horizontal");
        float move = Input.GetAxis("Vertical");

        if (turn != 0 || move != 0)
        {
            if (turn != 0)
            {
                float turnAmount = turn * rotationSpeed * Time.deltaTime;
                Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
                rb.MoveRotation(rb.rotation * turnRotation);
            }

            if (move != 0)
            {
                Vector3 moveDirection = transform.forward * move * moveSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + moveDirection);
            }

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}

