using UnityEngine;
using System.Collections;

namespace BlackBoxTools.WIP
{
[ExecuteInEditMode]
public class LookAtInEditor : MonoBehaviour {

#if UNITY_EDITOR
    public Transform _targetToLookAt;
    // Use this for initialization
    void Start ()
    {
        LookAtTarget();

    }

    void Update () {

        LookAtTarget();
    }
    void OnValidate() {
        LookAtTarget();
    }
    void LookAtTarget() {
        if(_targetToLookAt!=null)
        this.transform.LookAt(_targetToLookAt);
    }
#endif
}}
