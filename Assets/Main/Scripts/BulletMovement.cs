using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float t;
    public string campTag = "Camp";
    public float searchRadius = 4f;
    public float moveSpeed = 5f;

    private GameObject targetCamp;
    public GameObject ImpactFX;

    void Start() {
        FindNearestCamp();
    }

    void Update() {
        if (targetCamp != null && t<1 && t>=0) {
            MoveTowardsCamp();
            t += Time.deltaTime * moveSpeed * 0.02f;
        }
    }

    void MoveTowardsCamp() {
		if ((transform.position - targetCamp.transform.position).sqrMagnitude <= 0.5f) Hit();

        Vector3 StartPoint = transform.position;
        Vector3 EndPoint = targetCamp.transform.position;
        Vector3 ControlPoint = EndPoint;
        ControlPoint.y += transform.position.y - EndPoint.y;
        ControlPoint = ((StartPoint + EndPoint) / 2f + ControlPoint)/2f;

        Vector3 newPosition = Mathf.Pow(1 - t, 2) * StartPoint +
                             2 * (1 - t) * t * ControlPoint +
                             Mathf.Pow(t, 2) * EndPoint;

        transform.position = newPosition;
    }

    void FindNearestCamp() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders) {
            if (collider.CompareTag(campTag)) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    targetCamp = collider.gameObject;
                }
            }
        }
    }

	private void Hit() {
        Instantiate(ImpactFX, targetCamp.transform.position, Quaternion.identity);
        targetCamp.GetComponent<CampFire>().TakeDamage();
        Destroy(this.gameObject);
	}
}
