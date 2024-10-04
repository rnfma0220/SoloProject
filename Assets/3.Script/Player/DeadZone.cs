using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class DeadZone : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        Actor player = other.gameObject.transform.root.GetComponent<Actor>();

        player.actorState = Actor.ActorState.Dead;
    }
}
