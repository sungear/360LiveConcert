using UnityEngine;
using System.Collections;
namespace BlackBoxTools.Viewer360Alpha
{

    public class Camera360AbstractControl : MonoBehaviour
    {


        [Tooltip("In Degree by Second")]
        public float _cameraMovingSpeed = 90f;
        [Tooltip("In pourcent zoom by Second")]
        public float _cameraZoomingSpeed = 0.5f;

        public Camera360Move _cameraMoveAffected;
        public Camera360Zoom _cameraZoomAffected;


        private Vector2 _movingDirectionAsked;
        private float _zoomDirectionAsked;

        public void TurnLeft() { TurnCameraInDirection(Vector2.left); }
        public void TurnRight() { TurnCameraInDirection(-Vector2.left); }
        public void TurnUp() { TurnCameraInDirection(Vector2.up); }
        public void TurnDown() { TurnCameraInDirection(-Vector2.up); }

        public void Recenter()
        {
            Vector3 direction = Vector3.zero;
            float horizontal = _cameraMoveAffected.GetHorizontalPosition();
            float vertical = _cameraMoveAffected.GetVerticalPosition();
            if (Mathf.Abs(horizontal) < 5f && Mathf.Abs(vertical) < 5f) return;

            direction.x = -horizontal / 15f;
            direction.y = -vertical / 15f;
            TurnCameraInDirection(direction);

        }

        public void TurnCameraInDirection(Vector2 direction)
        {
            _movingDirectionAsked = direction;
        }

        public void Zoom() { Zoom(1f); }
        public void Unzoom() { Zoom(-1f); }

        public void Zoom(float zoomDirection)
        {
            _zoomDirectionAsked = zoomDirection;
        }

        void Update()
        {
            if (_cameraMoveAffected)
            {
                Vector2 deltaTimeDirection = _movingDirectionAsked * Time.deltaTime * _cameraMovingSpeed;
                _cameraMoveAffected.AddPositionTo(deltaTimeDirection.x, deltaTimeDirection.y);
            }

            if (_cameraZoomAffected)
            {
                float deltaTimeZoom = _zoomDirectionAsked * Time.deltaTime * _cameraZoomingSpeed;
                _cameraZoomAffected.AddZoom(deltaTimeZoom);
            }

            _movingDirectionAsked = Vector2.MoveTowards(_movingDirectionAsked, Vector2.zero, Time.deltaTime * 3f);
            _zoomDirectionAsked = Mathf.MoveTowards(_zoomDirectionAsked, 0f, Time.deltaTime * 3f);
        }
    }
}