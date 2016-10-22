using UnityEngine;
using System.Collections;
using System;

namespace BlackBoxTools.Viewer360Alpha{

public class Camera360Move : MonoBehaviour {


    [SerializeField]
    private Transform _affectedTarget;
    

    [Range(-180f,180f)]
    [SerializeField]
    private float _horizontalPosition;

    [Range(-90f, 90f)]
    [SerializeField]
    private float _verticalPosition;


    [Header("Option")]
    public bool _clampVerticalBorder=true;
    [Range(0f,90f)]
    public float _clampVerticalValue = 90f;
    public bool _clampHorizontalBorder=false;
    [Range(0f, 180f)]
    public float _clampHorizontalValue = 180f;


    

    public void AddPositionTo(float horizontalPosition, float verticalPosition)
    {
        float newVerticalAsked = _verticalPosition + verticalPosition;
        float newHorizontalAsked = _horizontalPosition + horizontalPosition;

        if(_clampHorizontalBorder)
            newVerticalAsked = Mathf.Clamp(newVerticalAsked, -_clampVerticalValue, _clampVerticalValue);
        else 
            newVerticalAsked = Mathf.Clamp(newVerticalAsked,-90f ,90f);

        if (_clampHorizontalBorder)
            newHorizontalAsked = Mathf.Clamp(newHorizontalAsked, -_clampHorizontalValue, _clampHorizontalValue);
        else
            newHorizontalAsked = BorderClampSwitch(newHorizontalAsked, 180f);

        _verticalPosition = newVerticalAsked;
        _horizontalPosition = newHorizontalAsked;
        SetPositionTo(_horizontalPosition, _verticalPosition);
    }

    private static float BorderClampSwitch(float value, float limit)
    {
        if (value > limit)// 96 > 90
            value = -limit + (value - limit); //-84 = -90 + (96-90)
        if (value < -limit) //-93 < -90
            value = limit + (value + limit); //87 = 90 + ((-93)+(90)) 
        return value;
    }

    public void SetPositionTo(float horizontalPosition, float verticalPosition)
    {
        if (_affectedTarget == null) return;
        _affectedTarget.localRotation = Quaternion.Euler(new Vector3(-_verticalPosition, _horizontalPosition, 0));
    }


    public float GetHorizontalPosition() { return _horizontalPosition; }
    public float GetVerticalPosition() { return _verticalPosition; }

    public void Reset()
    {
        if (_affectedTarget == null) return;
        _affectedTarget = this.transform;
    }
    
    public void OnValidate() {

        SetPositionTo(_horizontalPosition, _verticalPosition);
    }
}
}
