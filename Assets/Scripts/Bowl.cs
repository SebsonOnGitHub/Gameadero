using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private Vector3 liquidMaxPos;
    private Vector3 liquidMinPos;
    private Vector3 liquidMaxScale;

    public float maxFullness = 2;
    public float currFullness = 0;
    public float percentFullness = 0;
    public GameObject trocaLiquid;
    public GameObject RollCoursePrefab;

    private void Start() {
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
        Instantiate(RollCoursePrefab, new Vector3(7, 0.9f, 20), transform.rotation, transform);
    }

}
