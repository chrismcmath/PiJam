using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {
    public enum Direction {LEFT=0,RIGHT, STILL};

    public static string AVATAR_COLLIDED_WITH_PLATFORM = "AVATAR_COLLIDED_WITH_PLATFORM";

    public static bool HasPi = false;

    public GameObject PiView;
    public Transform ViewRoot;

    private float _TargetX = 0f;
    private Direction _WalkingDirection = Direction.RIGHT;
    private Animator _Animator;
    private ParticleSystem _Particles;

    public void Awake() {
        _TargetX = transform.position.x;

        Messenger<Vector3>.AddListener(ClickController.PLAYER_INPUT, OnPlayerInput);
        Messenger.AddListener(AVATAR_COLLIDED_WITH_PLATFORM, OnCollisionWithPlatform);
        Messenger<PiPieceController, PiPieceController.PiPieceState>.AddListener(InteractablePiPieceController.PI_PIECE_CLICKED, OnPiPieceClicked);

        _Animator = GetComponentInChildren<Animator>();
        _Particles = PiView.GetComponentInChildren<ParticleSystem>();
        StartCoroutine(ActivateViewAfterWait());
    }

    public void OnDestroy() {
        Messenger<Vector3>.RemoveListener(ClickController.PLAYER_INPUT, OnPlayerInput);
        Messenger.RemoveListener(AVATAR_COLLIDED_WITH_PLATFORM, OnCollisionWithPlatform);
        Messenger<PiPieceController, PiPieceController.PiPieceState>.RemoveListener(InteractablePiPieceController.PI_PIECE_CLICKED, OnPiPieceClicked);
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

        if (Input.GetKeyDown("r")) {
            LoadCheckpoint();
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

    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer(Consts.LAYER_CHECKPOINT)) {
            CheckCheckpoint(coll.gameObject.GetComponentInParent<CheckpointController>());
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        //Debug.Log("OnCollisionEnter2D " + coll.gameObject.name);
    }

    private void OnCollisionWithPlatform() {
        //Debug.Log("OnCollisionWithPlatform");
        _WalkingDirection = Direction.STILL;
        SetAvatarXSpeed(0f);
    }

    private void OnPiPieceClicked(PiPieceController piPiece, PiPieceController.PiPieceState state) {
        GameObject beamGO = Instantiate(Resources.Load<GameObject>("Prefabs/pi_beam"));
        //beamGO.transform.position = transform.position;
        beamGO.transform.position = new Vector2(-100f, 0f);
        PiBeamController beam = beamGO.GetComponent<PiBeamController>();

        if (state == PiPieceController.PiPieceState.CIRCLE) {
            beam.SetBeam(transform, piPiece.transform);
            HasPi = false;
        } else if (state == PiPieceController.PiPieceState.LINE) {
            beam.SetBeam(piPiece.transform, transform);
            HasPi = true;
        }
        StartCoroutine(ActivateViewAfterWait());
    }

    private IEnumerator ActivateViewAfterWait() {
        yield return new WaitForSeconds(Consts.BeamPeriod);
        ActiveView();
    }

    private void ActiveView() {
        if (HasPi) {
            _Particles.Play();
        } else {
            _Particles.Stop();
        }
    }

    private void SetAvatarXSpeed(float speed) {
        _Animator.SetFloat("speed", Mathf.Abs(speed));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        if (speed > 0f) {
            ViewRoot.localScale = new Vector3(1f, 1f, 1f);
        } else if (speed < 0f) {
            ViewRoot.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void CheckCheckpoint(CheckpointController checkpoint) {
        CheckpointController last = CheckpointModel.LatestCheckpoint;
        if (last == null || checkpoint.ID > last.ID) {
            CheckpointModel.LatestCheckpoint = checkpoint;
        }
    }

    private void LoadCheckpoint() {
        CheckpointController last = CheckpointModel.LatestCheckpoint;

        if (last == null) return;

        HasPi = last.HasPi;
        transform.position = last.Position.position;
        if (last.FacingRight) {
            ViewRoot.localScale = new Vector3(1f, 1f, 1f);
        } else {
            ViewRoot.localScale = new Vector3(-1f, 1f, 1f);
        }

        _WalkingDirection = Direction.STILL;
        SetAvatarXSpeed(0f);
        ActiveView();
    }
}
