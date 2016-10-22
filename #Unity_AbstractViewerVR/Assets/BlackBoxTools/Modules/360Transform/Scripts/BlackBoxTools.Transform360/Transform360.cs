using UnityEngine;
using System.Collections;
using System;
using BlackBoxTools.T360;
 public interface I_Transform360 {

    void SetPositionTo(float latitude, float longitude, float height=1f);
 
}
[RequireComponent(typeof(Transform))]
public class Transform360 : MonoBehaviour, I_Transform360 {

    [Header("Reference Transform")]
    [SerializeField]
    [Tooltip("The transform linked and affected by the 360 positioning requests")]
    private Transform _affectedTransform;



    [SerializeField]
    [Tooltip("The transform referenced as the gravity center of the 360 sphere")]
    private Transform _centerTransform;


    [Header("Data")]
    [SerializeField]
    [Tooltip("Position ")]
    private Position360 _position360 = new Position360();
    public Position360 GetPosition360()
    {
        return _position360;
    }

    [SerializeField]
    [Tooltip("Euleur rotation  apply to a object on the surface directed to the nord pole of a sphere")]
    private Rotation360 _rotation360 = new Rotation360();
    public Rotation360 GetRotation360()
    {
        return _rotation360;
    }

    [Header("Debug (Do not touch)")]
    [SerializeField]
    [Tooltip("LastPosition registered give the possibility to know if the transform has been change of position.")]
    private Vector3 _lastPositionRegistered;

    public void SetPositionTo(float longitude, float latitude, float height = 1)
    {
        _position360.SetLatitude(latitude);
        _position360.SetLongitude(longitude);
        _position360.SetHeight(height);
        SetPositionTo(_position360);
    }
    public void SetPositionTo(Position360 position) {

        Vector3 newPosition = GetVertexOf(_centerTransform.position, _centerTransform.rotation, position);
        _affectedTransform.position = newPosition;
        _position360 = position;
        SetRotation(_rotation360);
    }
    internal static Vector3 GetVertexOf(Vector3 gravityPosition, Quaternion gravityDirection, Position360 position360)
    {
        Vector3 newPosition = gravityPosition;
        Vector3 euleurDirection = new Vector3(-position360.GetLatitude(), position360.GetLongitude(), 0);
        Quaternion euleurRotationof360Position = Quaternion.Euler(euleurDirection);
        Quaternion realDirection = gravityDirection * euleurRotationof360Position;

        Vector3 direction = realDirection * Vector3.forward;
        direction *= position360.GetHeight();
        //Debug.Log(" Height: " + position360.GetHeight());
        newPosition += direction;
        return newPosition;
    }


    public Position360 GetPosition360Of(Transform pointInSpace)
    {
        return GetPosition360Of(pointInSpace.position);
    }
    public Position360 GetPosition360Of(Vector3 pointInSpace)
    {
        Position360 result = new Position360();
        result.SetHeight(Vector3.Distance(pointInSpace, _centerTransform.position));
        //TODO Base on XYZ world position find out the X,Y of the transform360.
        return result;

    }

 

    public void SetRotation(Rotation360 rotation) {

        Vector3 direction = _centerTransform.position - _affectedTransform.position;
        
        Quaternion basicRotation = direction!=Vector3.zero? Quaternion.LookRotation(direction, _centerTransform.forward)
            :Quaternion.identity;
        // basicRotation = basicRotation * Quaternion.Euler(new Vector3(-90, 0, 0));
        _affectedTransform.rotation = basicRotation;
        _rotation360 = rotation;

    }

    public void OnValidate()
    {
        _affectedTransform = this.transform;
        SetPositionTo(_position360);
    }


    public Position360 GetPosition360FromTransformPosition()
    {
        return GetPosition360FromPosition(_affectedTransform.position);
    }
    public Position360 GetPosition360FromPosition(Vector3 newPosition)
    {
        if (_centerTransform == null)
            return new Position360();
        Vector3 directionNewPoint = newPosition - _centerTransform.position;
        Vector3 directionNewPointInRoot = _centerTransform.InverseTransformDirection(directionNewPoint);
        Vector2 polarXY = CartesianToPolar(directionNewPointInRoot);
        //Debug.Log("X " + polarXY.x + "  Y " + polarXY.y);
        return new Position360(polarXY.y, -polarXY.x, directionNewPoint.magnitude);
    }

    public Vector2 CartesianToPolar(Vector3 point)
    {
        Vector2 polar;

        //calc longitude
        polar.y = Mathf.Atan2(point.x, point.z);

        //this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
        float xzLen = new Vector2(point.x, point.z).magnitude;
        //atan2 does the magic
        polar.x = Mathf.Atan2(-point.y, xzLen);

        //convert to deg
        polar *= Mathf.Rad2Deg;

        return polar;
    }


    public Vector3 PolarToCartesian(Vector2 polar)
    {

        //an origin vector, representing lat,lon of 0,0. 

        Vector3 origin = new Vector3(0, 0, 1);
        //build a quaternion using euler angles for lat,lon
        Quaternion rotation = Quaternion.Euler(polar.x, polar.y, 0);
        //transform our reference vector by the rotation. Easy-peasy!
        Vector3 point = rotation * origin;

        return point;
    }

    public void Reset()
    { _affectedTransform = this.transform; }

    public void SetPositionWithScreenPosition(Vector2 screenPosition, float height = 1f)
    {
        Vector2 screenPosPourcent = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
        screenPosPourcent.y -= 0.5f;
        screenPosPourcent.x -= 0.5f;
        screenPosPourcent.y *= 180f;
        screenPosPourcent.x *= 360f;

        //Debug.Log("Pos:" + screenPosition);
        //Debug.Log("Pos Pourcent:" + screenPosPourcent);

        SetPositionTo(screenPosPourcent.x, screenPosPourcent.y, height);


    }
    public void SetPositionWithScreenPositionAsPourcent(Vector2 screenPosition, float height = 1f)
    {
        Vector2 screenPosPourcent = new Vector2(screenPosition.x, screenPosition.y);
        screenPosPourcent.y -= 0.5f;
        screenPosPourcent.x -= 0.5f;
        screenPosPourcent.y *= 180f;
        screenPosPourcent.x *= 360f;

        //Debug.Log("Pos:" + screenPosition);
        //Debug.Log("Pos Pourcent:" + screenPosPourcent);

        SetPositionTo(screenPosPourcent.x, screenPosPourcent.y, height);


    }


    void LateUpdate() {


        Position360 currentPosition = GetPosition360FromTransformPosition();
        //Debug.Log(currentPosition);
        Position360 position = GetPosition360();
        if (currentPosition != position)
            SetPositionTo(currentPosition);

    }
    
}
