using UnityEngine;
using System.Collections;

public class AvatarWallColliderController : MonoBehaviour {

    public void Start() {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log("OnTriggerEnter2D " + coll.gameObject.name + ", " + coll.gameObject.layer + " ? " + LayerMask.NameToLayer(Consts.LAYER_PLATFORM));
        if (coll.gameObject.layer == LayerMask.NameToLayer(Consts.LAYER_PLATFORM)) {
            //Debug.Log("broadcast");
            Messenger.Broadcast(AvatarController.AVATAR_COLLIDED_WITH_PLATFORM);
        }
    }
}
