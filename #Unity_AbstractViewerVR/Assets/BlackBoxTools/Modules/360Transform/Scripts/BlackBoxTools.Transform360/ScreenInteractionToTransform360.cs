using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BlackBoxTools.T360{
public class ScreenInteractionToTransform360 : MonoBehaviour {

    public Transform360 _objectAffected;
    public Text _displayCoordonate;
    public float _height=3;
   

    void Update () {
        _objectAffected.SetPositionWithScreenPosition(GetScreenPosition(), _height);
        _displayCoordonate.text = string.Format("Latitude: {0} \n Longitude: {1}", 
            _objectAffected.GetPosition360().GetLatitude(),
            _objectAffected.GetPosition360().GetLongitude());




    }


    public Vector2 GetScreenPosition( bool normalizedToScreenRatio=false ) {

        Vector2 position = Vector3.zero;
        if ( Input.GetMouseButton(0) ) 
             position = Input.mousePosition;
        if ( Input.touchCount > 0 )
            position = Input.touches[0].position;

        if (normalizedToScreenRatio) { 
            position.y /= (float)Screen.height;
            position.x /= (float)Screen.width;
        }

        return position;
        
    }
}}
