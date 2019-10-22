using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public GameObject capPrefab;
    public GameObject leftTrigger;
    public GameObject rightTrigger;

    private bool leftDown;
    private bool rightDown;
    private bool completed;

    public void Start() {
        Init();
    }

    public void Init() {
        leftDown = false;
        rightDown = false;
        completed = false;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject == leftTrigger && !leftDown) {
            leftDown = true;
            rightDown = false;
        }
        else if (collider.gameObject == rightTrigger && !rightDown) {
            rightDown = true;
            leftDown = false;
            if (!completed) {
                SpawnObject(capPrefab, new Vector3(-43, 3, -4), capPrefab.transform.rotation, transform);
                completed = true;
            }

        }
    }

    public void SpawnObject(GameObject Object, Vector3 position, Quaternion rotation, Transform parent) {
        Instantiate(Object, position, rotation, parent);
    }
}
