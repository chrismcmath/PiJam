using UnityEngine;
using System.Collections;
 
 // via: http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
 public class SmoothCamera2D : MonoBehaviour {
     public float _DampTime = 0.15f;
     public Transform Target;
 
     private Vector3 _Velocity = Vector3.zero;

     public void Update () {
         if (Target) {
             Vector3 point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
             Vector3 delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
             Vector3 destination = transform.position + delta;
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref _Velocity, _DampTime);
         }
     }
 }
