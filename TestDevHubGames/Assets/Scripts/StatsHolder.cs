using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private float damageDelaySeconds;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            StartCoroutine(ColorCoolDown());
        }
    }

    private IEnumerator ColorCoolDown()
    {
        material.color = damageColor;

        yield return new WaitForSeconds(damageDelaySeconds);

        material.color = defaultColor;
    }
}
