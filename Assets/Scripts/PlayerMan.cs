using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : Player
{
    public GameObject umbrellaPrefab;
    public float umbrellaFloat;
    public List<Collectable.CollectType> ingredientsCollected;

    private GameObject umbrella;
    private float glideAngle = 0.2f;

    public override void Init() {
        base.Init();
        Random.InitState(0);
        ingredientsCollected = new List<Collectable.CollectType>();
    }

    public override void UpdatingPlayer() {
        base.UpdatingPlayer();
        if (umbrella && rb.velocity.y <= 0) {
            rb.AddForce(new Vector3(0, (rb.mass * 0.1f) * umbrellaFloat, 0));
        }
    }

    private void FixedUpdate() {
        if (isSwimming) {
            rb.AddForce(new Vector3(0, 5, 0));
        }
    }

    public void Jump(bool swim) {
        if (isSwimming && swim) {
            rb.AddForce(new Vector3(0, 4, 0));
        }
        else if (isGrounded && !swim) {
            rb.AddForce(new Vector3(0, 2.5f, 0), ForceMode.Impulse);
        }
    }

    public void Glide() {
        if (!umbrella && !isGrounded) {
            Vector3 umbrellaPos = transform.position + new Vector3(0, 0.4f, 0);
            umbrella = Instantiate(umbrellaPrefab, umbrellaPos, umbrellaPrefab.transform.rotation, transform);
            int rnd;
            do {
                rnd = Random.Range(-1, 1);
            } while (rnd == 0) ;
            glideAngle *= rnd;
        }
        else if (!Input.GetKey(PlayerController.keyAction) || isGrounded) {
            Destroy(umbrella);
            transform.rotation = FindObjectOfType<MainCamera>().forwardKeeper.transform.rotation;
            PlayerController.currState = PlayerController.State.NONE;
        }
        else {
            if (transform.rotation.z > 0.07 || transform.rotation.z < -0.07) {
                glideAngle *= -1;
            }
            Vector3 rotPoint = transform.position + new Vector3(0, transform.position.y, 0);
            transform.RotateAround(rotPoint, FindObjectOfType<MainCamera>().forwardVec, glideAngle);
        }
    }

    public void CollectIngredient(Collectable.CollectType ingredient) {
        FindObjectOfType<GameMaster>().collectInWorld--;
        if (!ingredientsCollected.Contains(ingredient)) {
            ingredientsCollected.Add(ingredient);
            mainCollected = ingredientsCollected.Count;
        }
    }
}
