using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpearEnemy : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject spearPrefab;
    public Transform throwPoint;
    public float maxThrowForce = 12f;
    public float chargeRate = .2f;
    public float minSecondsCharge;
    public float maxSecondsCharge;

    private GameObject currentSpearObject;

    public IEnumerator StartThrow()
    {
        float chargeSec = Random.Range(minSecondsCharge, maxSecondsCharge);
        currentSpearObject = Instantiate(spearPrefab, throwPoint.position, throwPoint.rotation, throwPoint);

        yield return new WaitForSeconds(chargeSec);

        Rigidbody rb = currentSpearObject.GetComponent<Rigidbody>();
        float currentForce = chargeSec * chargeRate;

        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(throwPoint.forward * currentForce, ForceMode.Impulse);
        }
    }
}
