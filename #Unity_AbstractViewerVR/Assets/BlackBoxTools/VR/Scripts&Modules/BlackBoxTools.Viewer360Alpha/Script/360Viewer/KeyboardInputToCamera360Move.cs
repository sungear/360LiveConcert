using UnityEngine;
using System.Collections;
namespace BlackBoxTools.Viewer360Alpha
{

    public class KeyboardInputToCamera360Move : MonoBehaviour
    {
        public Camera360Move _cameraMoveTargeted;
        public float _horizontalSpeedAngle = 30f;
        public float _verticalSpeedAngle = 30f;



        void Update()
        {


            if (Input.GetKey(KeyCode.LeftControl)) return;
            Vector2 moveSpeed = Vector2.zero;
            moveSpeed.x = Input.GetAxis("Horizontal") * _horizontalSpeedAngle * Time.deltaTime;
            moveSpeed.y = Input.GetAxis("Vertical") * _verticalSpeedAngle * Time.deltaTime;

            _cameraMoveTargeted.AddPositionTo(moveSpeed.x, moveSpeed.y);


        }
    }
}