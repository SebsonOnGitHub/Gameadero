using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public GameObject collectPrefab;
    public GameObject ingredientPrefab;
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
                SpawnObject(collectPrefab, new Vector3(-43, 3, -5), collectPrefab.transform.rotation, transform);
                completed = true;
            }

        }
    }

    public void SpawnObject(GameObject Object, Vector3 position, Quaternion rotation, Transform parent) {
        GameObject tempIngr = Instantiate(Object, position, rotation, parent);
        tempIngr.GetComponent<Ingredient>().ingredientPrefab = ingredientPrefab;
        tempIngr.GetComponent<Ingredient>().collectType = Collectable.CollectType.APPLE;
    }
}
