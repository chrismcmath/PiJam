using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiPieceController : MonoBehaviour {
    public enum PiPieceState {LINE=0,CIRCLE,TO_CIRCLE,TO_LINE};

    public float LineColliderWidth = 0.1f;
    public float CircleColliderRadius = 0.5f;

    public GameObject Circle;
    public GameObject Line;

    public BoxCollider2D LineCollider;
    public CircleCollider2D CircleCollider;

    public Collider2D LineBounds;
    public Collider2D CircleBounds;

    protected bool _CanToggleViaChain = true;
    protected PiPieceState _State = PiPieceState.LINE;
    public PiPieceState State {
        get { return _State; }
    }

    protected float _TransformCounter = 0f;
    protected ParticleSystem _Particles;

    public void Start() {
        _Particles = GetComponentInChildren<ParticleSystem>();
        if (Circle.activeInHierarchy) {
            _State = PiPieceState.CIRCLE;
        } else {
            _State = PiPieceState.LINE;
        }
    }

    public void FixedUpdate() {
        float fraction;
        switch(_State) {
            case PiPieceState.TO_LINE:
                fraction = 1f - (_TransformCounter / Consts.PiPieceTransformPeriod);
                _TransformCounter -= Time.fixedDeltaTime;
                LineCollider.size = new Vector2(1f, LineColliderWidth * fraction);
                if (_TransformCounter < 0f) {
                    _State = PiPieceState.LINE;
                    LineCollider.size = new Vector2(1f, LineColliderWidth);
                }
                break;
            case PiPieceState.TO_CIRCLE:
                fraction = 1f - (_TransformCounter / Consts.PiPieceTransformPeriod);
                _TransformCounter -= Time.fixedDeltaTime;
                CircleCollider.radius = CircleColliderRadius * fraction;
                if (_TransformCounter < 0f) {
                    _State = PiPieceState.CIRCLE;
                    CircleCollider.radius = CircleColliderRadius;
                }
                break;
        }
    }

    public void ToggleViaChain() {
        if (_CanToggleViaChain) {
            StartCoroutine(ToggleViaAfterWait());
            ChainHaitus();
        }
    }

    protected void Toggle() {
        _Particles.Play();
        Chain(); //Need to chain first to get the current neighbours

        if (_State == PiPieceState.LINE || _State == PiPieceState.TO_LINE) {
            ToCircle();
        } else if (_State == PiPieceState.CIRCLE || _State == PiPieceState.TO_CIRCLE) {
            ToLine();
        }
    }

    protected void Chain() {
        ChainHaitus();
        List<PiPieceController> neighbours = GetNeighbours();

        foreach (PiPieceController piPiece in neighbours) {
            piPiece.ToggleViaChain();
        }
    }

    protected List<PiPieceController> GetNeighbours() {
        List<PiPieceController> neighbours = new List<PiPieceController>();

        Collider2D thisCollider = GetRelevantBoundsCollider();
        List<GameObject> colliderGOs = new List<GameObject>(GameObject.FindGameObjectsWithTag(Consts.TAG_PIPIECE_COLLIDER));
        foreach (GameObject c in colliderGOs) {
            if (c.GetComponent<Collider2D>() == null) {
                Debug.Log("well how the hell did this happen? " + c.name);
            }
            if (c.GetComponent<Collider2D>() != null && thisCollider.bounds.Intersects(c.GetComponent<Collider2D>().bounds)) {
                neighbours.Add(c.GetComponentInParent<PiPieceController>());
            }
        }
        return neighbours;
    }

    protected void ChainHaitus() {
        _CanToggleViaChain = false;
        StartCoroutine(ResetCanToggleViaChainAfterWait());
    }

    protected IEnumerator ToggleViaAfterWait() {
        yield return new WaitForSeconds(Consts.ToggleChainDelay);
        Toggle();
    }

    protected IEnumerator ResetCanToggleViaChainAfterWait() {
        yield return new WaitForSeconds(Consts.ToggleChainDelayReset);
        _CanToggleViaChain = true;
    }

    protected Collider2D GetRelevantBoundsCollider() {
        if (Circle.activeSelf) {
            return CircleBounds;
        } else if (Line.activeSelf) {
            return LineBounds;
        }
        return null;
    }
    
    protected void ToCircle() {
        _TransformCounter = Consts.PiPieceTransformPeriod;
        _State = PiPieceState.CIRCLE;

        //CircleCollider.radius = 0f;

        Circle.SetActive(true);
        Line.SetActive(false);
    }

    protected void ToLine() {
        _TransformCounter = Consts.PiPieceTransformPeriod;
        _State = PiPieceState.LINE;

        //LineCollider.size = new Vector2(1f, 0f);

        Circle.SetActive(false);
        Line.SetActive(true);
    }
}
