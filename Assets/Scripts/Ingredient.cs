using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Collectable
{
    public GameObject ingredientPrefab;

    public override void Init() {
        base.Init();
        Instantiate(ingredientPrefab, transform.position + ingredientPrefab.transform.position + new Vector3(0, 2, 0), ingredientPrefab.transform.rotation, transform);
    }

    public void Update(){
        transform.Rotate(Vector3.up, Space.World);
    }

    public void OnCollisionEnter(Collision collision) {
        PlayerMan pMan = collision.collider.GetComponent<PlayerMan>();
        if (pMan) {
            audioObject.Collect(collectType);
            pMan.CollectIngredient(collectType);
            Destroy(gameObject);
        }
    }
}
