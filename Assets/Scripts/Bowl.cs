using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public float maxFullness = 2;
    public float currFullness = 0;

    public void SetFullness(float trocaAmount) {
        currFullness = trocaAmount;
    }
}
