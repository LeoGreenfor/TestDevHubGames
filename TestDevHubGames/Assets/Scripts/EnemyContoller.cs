using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyContoller : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private ThrowSpearEnemy enemySpear;
    [SerializeField]
    private float attackSecondsDelay;
    private Rigidbody rb;

    private float delaySeconds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        delaySeconds += Time.deltaTime;
        if (delaySeconds < attackSecondsDelay) return;

        delaySeconds = 0;

        Vector3 lookAtPos = player.position;
        lookAtPos.x += .5f;

        transform.LookAt(lookAtPos);
        StartCoroutine(enemySpear.StartThrow());
    }
}
