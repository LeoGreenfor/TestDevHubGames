using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovementsController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float leftLimit;
    [SerializeField]
    private float rightLimit;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(int input)
    {
        Vector3 direction = new Vector3(Mathf.Round(input), 0, 0);

        if (direction != Vector3.zero)
        {
            Vector3 newPos = rb.position + direction;
            newPos.x = Mathf.Clamp(newPos.x, leftLimit, rightLimit);
            rb.MovePosition(newPos);
        }
    }

    /*void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            move += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            move += Vector3.right;

        Vector3 newPos = rb.position + move;
        newPos.x = Mathf.Round(newPos.x);

        rb.MovePosition(newPos);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputX * moveSpeed, rb.velocity.y, 0f);
        rb.velocity = movement;
    }*/
}
