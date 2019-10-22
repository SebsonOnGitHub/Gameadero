using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectType { NONE, CAN, ORANGE, APPLE, SUGAR, WATER, LOVE };
    public CollectType collectType;
    public AudioObject audioObject;

    public void Start() {
        Init();
    }

    public virtual void Init() {
        audioObject = FindObjectOfType<AudioObject>();
    }
}
