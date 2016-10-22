using UnityEngine;
using System.Collections;


namespace BlackBoxTools.T360{
/** TO IMPROVE:
 SetLatitude (110) -> latitude become -70 .
  
*/

   [System.Serializable]
public struct Position360  {


    [SerializeField]
    [Tooltip("Latitude position between -90 and 90. (South to North).")]
    [Range(-90f,90f)]
    private float _latitude;

    public void SetLatitude(float latitudeAngle)
    {
        if (latitudeAngle > 90f || latitudeAngle < -90f)
        {
            Debug.LogWarning("The position of the latitude is maximum -90°<=x<=90°");
            latitudeAngle = Mathf.Clamp(latitudeAngle, -90f, 90f);
        }
        _latitude = latitudeAngle;
    }
    public float GetLatitude()
    {
        return _latitude;
    }


    [SerializeField]
    [Tooltip("Longitude position between -180 and 180. (South to North).")]
    [Range(-180f, 180f)]
    private float _longitude;
    public void SetLongitude(float longitudeAngle)
    {

        if (longitudeAngle > 180f || longitudeAngle < -180f)
        {
            Debug.LogWarning("The position of the longitude is maximum -180°<=x<=180°");
            longitudeAngle = Mathf.Clamp(longitudeAngle, -90f, 90f);
        }
        _longitude = longitudeAngle;
    }
    public float GetLongitude()
    {
        return _longitude;
    }

    [SerializeField]
    [Tooltip("Height of the point compare to a spherical gravity center.")]
   
    private float _height;

    public Position360(float longitude, float latitude, float height) : this()
    {
        SetLongitude(longitude);
        SetLatitude(latitude);
        SetHeight(height);
    }

    public void SetHeight(float heightValue)
    {

        if (heightValue < 0)
        {
            Debug.LogWarning("The height can't be less then 0");
            heightValue = Mathf.Clamp(heightValue, -0, float.MaxValue);
        }
        _height = heightValue;
    }
    public float GetHeight()
    {
        return _height;
    }


    public static bool operator ==(Position360 position1, Position360 position2)
    {
        //if (object.ReferenceEquals(position1, null))
        //{
        //    return object.ReferenceEquals(position2, null);
        //}

        return (position1._longitude == position2._longitude)
            && (position1._latitude == position2._latitude)
            && (position1._height == position2._height);


    }
    public static bool operator !=(Position360 position1, Position360 position2)
    {
        //if (object.ReferenceEquals(position1, null))
        //{
        //    return object.ReferenceEquals(position2, null);
        //}

        return (position1._longitude != position2._longitude)
            || (position1._latitude != position2._latitude)
            || (position1._height != position2._height);

    }

    public override string ToString()
    {
        return string.Format("(L:{0:0.00} l:{1:0.00} H:{2:0.00})", _longitude, _latitude, _height) ;
    }


}}
