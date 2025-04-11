using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpearObject : MonoBehaviour
{
    [SerializeField]
    private float delayTime;
    public int Damage;

    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        yield return new WaitUntil(() => rb.velocity.magnitude > 0.1f);
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
