using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRamp : MonoBehaviour
{
    public RollingCan rollPrefab;

    private float time = 0;
    private float delay = 2;
    private List<RollingCan> cans = new List<RollingCan>();
    private int currCan = 0;
    private int maxCans = 4;
    private List<int> randPos = new List<int>(new int[]{0, 2, 3});

    private void Update() {
        time += Time.deltaTime;

        if (time > delay) {
            int randX = randPos[Random.Range(0, 3)];
            Debug.Log(randX);
            if (cans.Count < maxCans) {
                cans.Add(Instantiate(rollPrefab, transform.position + new Vector3(0, 11, 3), rollPrefab.transform.rotation, transform.parent));
            }
            else {
                cans[currCan].SpawnAgain(randX);
                currCan = (currCan + 1) % maxCans;
            }
            time = 0;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("PlayerMan")) {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, 20));
        }
    }
}
