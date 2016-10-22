using UnityEngine;
using System.Collections;
namespace BlackBoxTools.Viewer360Alpha
{

    public class KeyboardInputToZoomCamera360 : MonoBehaviour
    {

        public Camera360Zoom _cameraZoomAffected;
        public float _zoomSpeedInPourcent = 0.3f;



        void Update()
        {
            float zoomPourentToAdd = 0;
            float zoomSpeedVariable = _zoomSpeedInPourcent * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                zoomPourentToAdd = Input.GetAxis("Vertical") * zoomSpeedVariable;
            }
            else if (Input.GetKey(KeyCode.KeypadPlus))
            {
                zoomPourentToAdd = zoomSpeedVariable;
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                zoomPourentToAdd = -zoomSpeedVariable;
            }
            _cameraZoomAffected.AddZoom(zoomPourentToAdd);
        }



    }
}