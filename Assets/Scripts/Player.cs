using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int cansCollected;
    public float growthSize;

    private void Start() {
        cansCollected = 0;
    }

    public void CollectCan() {
        cansCollected++;
        gameObject.transform.localScale += new Vector3(growthSize, growthSize, growthSize);
    }

}
