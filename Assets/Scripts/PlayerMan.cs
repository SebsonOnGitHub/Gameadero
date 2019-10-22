using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : Player
{
    public GameObject umbrellaPrefab;
    public float umbrellaFloat;
    public List<Collectable.CollectType> ingredientsCollected;

    private GameObject umbrella;

    public override void Init() {
        base.Init();
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
            Vector3 umbrellaPos = transform.position + new Vector3(0.4f, 0.4f, 0);
            umbrella = Instantiate(umbrellaPrefab, umbrellaPos, umbrellaPrefab.transform.rotation, transform);
        }
        else if (!Input.GetKey(PlayerController.keyAction) || isGrounded) {
            Destroy(umbrella);
            PlayerController.currState = PlayerController.State.NONE;
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
