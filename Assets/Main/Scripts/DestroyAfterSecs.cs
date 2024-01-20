using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecs : MonoBehaviour
{
    public float destroyDelay = 3f; // Adjust this value to set the delay before destruction

    void Start() {
        Invoke("DestroyGameObject", destroyDelay);
    }

    void DestroyGameObject() {
        Destroy(gameObject);
    }
}
