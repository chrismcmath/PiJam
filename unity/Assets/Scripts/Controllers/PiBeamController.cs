using UnityEngine;
using System.Collections;

public class PiBeamController : MonoBehaviour {
    private Transform _Source;
    private Transform _Destination;
    private float _Counter = 0f;
    private bool _Active = false;
    private ParticleSystem _System;

    public void SetBeam(Transform source, Transform destintation) {
        _Source = source;
        _Destination = destintation;
        _Counter = Consts.BeamPeriod;
        _Active = true;
        _System = GetComponent<ParticleSystem>();

        transform.position = _Source.position;
    }

    public void Update() {
        if (_Active) {
            if (_Counter > 0f) {
                Vector2 current = _Source.position + ((Consts.BeamPeriod - _Counter) / Consts.BeamPeriod) * (_Destination.position - _Source.position);
                _Counter -= Time.deltaTime;
                transform.position = current;
                Debug.Log("set as  " + current);
            } else if (_Counter <= 0f) {
                StartCoroutine(DestroyAfterWait());
                _Active = false;
                _System.Stop();
            }
        }
    }

    public void LateUpdate() {
        if (_Active && _System.isStopped) {
            _System.Play();
        }
    }

    private IEnumerator DestroyAfterWait() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
