using UnityEngine;
using System.Collections;

public class GizmoEuleurScript : MonoBehaviour {

    public Transform _mirroredPoint;
    public bool _useWorldRotation;

    public Transform _root;
    public Transform _center;
    public Transform _x;
    public Transform _y;
    public Transform _z;
    


	void Update () {
        _root.position = _mirroredPoint.position;
        Vector3 euleurAngle = _useWorldRotation? _mirroredPoint.rotation.eulerAngles :_mirroredPoint.localRotation.eulerAngles;
        _z.localRotation = Quaternion.Euler( euleurAngle);
        euleurAngle.z = 0;
        _y.localRotation = Quaternion.Euler(euleurAngle);
        euleurAngle.y = 0;
        _x.localRotation = Quaternion.Euler(euleurAngle);
    
    }
}
