using UnityEngine;
using System.Collections;

public class AvatarWallColliderController : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer(Consts.LAYER_PLATFORM)) {
            Messenger.Broadcast(AvatarController.AVATAR_COLLIDED_WITH_PLATFORM);
        }
    }
}
