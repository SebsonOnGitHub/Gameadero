using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public int maxFullness;
    public int currFullness;
    public float percentFullness;
    public GameObject trocaLiquid;
    public GameObject rollCoursePrefab;
    public float changeNeeded;
    public float changeSpeed;

    private Vector3 liquidMaxPos;
    private Vector3 liquidMinPos;
    private Vector3 liquidMaxScale;
    private float currentChange;
    private bool filled = false;

    public void Start() {
        Init();
    }

    public void Init() {
        currFullness = 0;
        percentFullness = 0;
        liquidMaxPos = trocaLiquid.transform.position;
        liquidMinPos = new Vector3(trocaLiquid.transform.position.x, trocaLiquid.transform.position.y * 0.52f, trocaLiquid.transform.position.z);
        liquidMaxScale = trocaLiquid.transform.localScale;
        trocaLiquid.transform.position = liquidMinPos;
        trocaLiquid.transform.localScale = Vector3.zero;
        changeNeeded = 0;
        changeSpeed = 0.04f;
        currentChange = 0;
    }

    private void Update() {
        if (Mathf.Abs(changeNeeded) > 0.0001f) {
            currentChange += changeSpeed * Mathf.Sign(changeNeeded);
            changeNeeded -= changeSpeed * Mathf.Sign(changeNeeded);
        }
        else {
            currentChange = currFullness;
            changeNeeded = 0;
        }

        Vector3 diffLiquidPos = (liquidMaxPos - liquidMinPos);
        trocaLiquid.transform.position = liquidMinPos + (diffLiquidPos * (currentChange / maxFullness));

        float curveScale = Mathf.Pow(currentChange / maxFullness, 0.1f);
        trocaLiquid.transform.localScale = new Vector3(liquidMaxScale.x * curveScale, liquidMaxScale.y, liquidMaxScale.z * curveScale);

        if (!filled && percentFullness == 1) {
            filled = Filled();
        }
    }

    public void ChangeFullness(int sizeChange) {
        currFullness += sizeChange;
        changeNeeded += sizeChange;
        percentFullness = (float)(currFullness) / maxFullness;
    }

    public bool Filled() {
        SpawnObject(rollCoursePrefab, new Vector3(34, 0.9f, 20), transform.rotation, transform);
        return true;
    }

    public void SpawnObject(GameObject Object, Vector3 position, Quaternion rotation, Transform parent) {
        Instantiate(Object, position, rotation, parent);
    }

}
