using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {
    public enum Direction {LEFT=0,RIGHT, STILL};

    public static string AVATAR_COLLIDED_WITH_PLATFORM = "AVATAR_COLLIDED_WITH_PLATFORM";

    private float _TargetX = 0f;
    private Direction _WalkingDirection = Direction.RIGHT;
    private Animator _Animator;

    public void Awake() {
        _TargetX = transform.position.x;

        Messenger<Vector3>.AddListener(ClickController.PLAYER_INPUT, OnPlayerInput);
        Messenger.AddListener(AVATAR_COLLIDED_WITH_PLATFORM, OnCollisionWithPlatform);

        _Animator = GetComponentInChildren<Animator>();
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

    public void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log("OnCollisionEnter2D " + coll.gameObject.name);
    }

    private void OnCollisionWithPlatform() {
        //Debug.Log("OnCollisionWithPlatform");
        _WalkingDirection = Direction.STILL;
        SetAvatarXSpeed(0f);
    }

    private void SetAvatarXSpeed(float speed) {
        _Animator.SetFloat("speed", Mathf.Abs(speed));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        if (speed > 0f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (speed < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
