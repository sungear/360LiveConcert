using UnityEngine;
using System.Collections;
namespace BlackBoxTools.Viewer360Alpha
{

    public class UI_JoystickToCamera360 : MonoBehaviour
    {


        public Camera360Move _cameraMoveTargeted;
        public UI_VirtualJoystick _joystickSource;
        public float _horizontalSpeedAngle = 30f;
        public float _verticalSpeedAngle = 30f;



        void Update()
        {


            if (Input.GetKey(KeyCode.LeftControl)) return;
            Vector2 moveSpeed = Vector2.zero;
            moveSpeed.x = _joystickSource.GetHorizontal() * _horizontalSpeedAngle * Time.deltaTime;
            moveSpeed.y = _joystickSource.GetVertical() * _verticalSpeedAngle * Time.deltaTime;

            _cameraMoveTargeted.AddPositionTo(moveSpeed.x, moveSpeed.y);


        }
    }
}