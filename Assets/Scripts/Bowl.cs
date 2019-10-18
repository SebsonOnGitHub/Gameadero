using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public float maxFullness;
    public float currFullness;
    public float percentFullness;
    public GameObject trocaLiquid;
    public GameObject rollCoursePrefab;

    private Vector3 liquidMaxPos;
    private Vector3 liquidMinPos;
    private Vector3 liquidMaxScale;

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
    }

    public void SetFullness(float trocaAmount) {
        currFullness = trocaAmount;
        percentFullness = currFullness / maxFullness;

        Vector3 diffLiquidPos = (liquidMaxPos - liquidMinPos) * percentFullness;
        trocaLiquid.transform.position = liquidMinPos + diffLiquidPos;

        float curveScale = Mathf.Pow(percentFullness, 0.1f);
        trocaLiquid.transform.localScale = new Vector3(liquidMaxScale.x * curveScale, liquidMaxScale.y, liquidMaxScale.z * curveScale);

        if (percentFullness == 1) {
          Filled();
        }
    }

    public void Filled() {
        SpawnObject(rollCoursePrefab, new Vector3(34, 0.9f, 20), transform.rotation, transform);
    }

    public void SpawnObject(GameObject Object, Vector3 position, Quaternion rotation, Transform parent) {
        Instantiate(Object, position, rotation, parent);
    }

}
