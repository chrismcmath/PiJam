using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {
    public enum Direction {LEFT=0,RIGHT, STILL};

    public static string AVATAR_COLLIDED_WITH_PLATFORM = "AVATAR_COLLIDED_WITH_PLATFORM";

    private float _TargetX = 0f;
    public Direction _WalkingDirection = Direction.RIGHT;

    public void Awake() {
        _TargetX = transform.position.x;

        Messenger<Vector3>.AddListener(ClickController.PLAYER_INPUT, OnPlayerInput);
        Messenger.AddListener(AVATAR_COLLIDED_WITH_PLATFORM, OnCollisionWithPlatform);
    }

    public void OnDestroy() {
        Messenger<Vector3>.RemoveListener(ClickController.PLAYER_INPUT, OnPlayerInput);
    }

    public void Update() {
        switch (_WalkingDirection) {
            case Direction.LEFT:
                if (_TargetX < transform.position.x) {
                    SetAvatarXSpeed(Consts.AvatarSpeed * -1);
                } else {
                    SetAvatarXSpeed(0f);
                }
                break;
            case Direction.RIGHT:
                if (_TargetX > transform.position.x) {
                    SetAvatarXSpeed(Consts.AvatarSpeed);
                } else {
                    SetAvatarXSpeed(0f);
                }
                break;
        }
    }

    private void OnPlayerInput(Vector3 position) {
        if (transform.position.x > position.x) {
            _WalkingDirection = Direction.LEFT;
            _TargetX = position.x;
        } else if (transform.position.x < position.x) {
            _WalkingDirection = Direction.RIGHT;
            _TargetX = position.x;
        }
    }

    private void OnCollisionWithPlatform() {
        _WalkingDirection = Direction.STILL;
    }

    private void SetAvatarXSpeed(float speed) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
