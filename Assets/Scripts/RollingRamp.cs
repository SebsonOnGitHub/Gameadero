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

    private void Update() {
        time += Time.deltaTime;

        if (time > delay) {
            if (cans.Count < maxCans) {
                cans.Add(Instantiate(rollPrefab, transform.position + new Vector3(2, 10, 2), rollPrefab.transform.rotation, transform.parent));
                //Make the 3 in xPos in the vector a random number between 0,2 and 3
            }
            else {
                cans[currCan].SpawnAgain();
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
