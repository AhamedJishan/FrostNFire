using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakpointHit : MonoBehaviour
{
	public GameObject Enemy;
	public GameObject DestroyVFX;

	private void OnCollisionEnter(Collision collision) {
		Instantiate(DestroyVFX, this.transform.position, Quaternion.identity);
		Destroy(Enemy);
	}
}
