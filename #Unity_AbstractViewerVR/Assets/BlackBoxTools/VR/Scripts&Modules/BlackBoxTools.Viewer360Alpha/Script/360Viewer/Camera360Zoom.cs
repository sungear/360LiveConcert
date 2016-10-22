using UnityEngine;
using System.Collections;
namespace BlackBoxTools.Viewer360Alpha
{

    public class Camera360Zoom : MonoBehaviour
    {

        [SerializeField]
        private Camera _cameraAffected;
        [SerializeField]
        [Tooltip("Field of view apply if the user zoom totaly")]
        private float _maxZoomAsFieldOfView = 5f;

        [SerializeField]
        [Tooltip("Field of view apply if the user do not zoom totaly")]
        private float _minZoomAsFieldOfView = 45f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _pourcentZoom = 0f;

        public void SetZoomTo(float pourcentZoom)
        {
            if (_cameraAffected == null) return;
            pourcentZoom = Mathf.Clamp(pourcentZoom, 0f, 1f);
            float newFieldOfView = _maxZoomAsFieldOfView + (_minZoomAsFieldOfView - _maxZoomAsFieldOfView) * (1f - pourcentZoom);

            _pourcentZoom = pourcentZoom;
            _cameraAffected.fieldOfView = newFieldOfView;

        }

        public void AddZoom(float pourcentZoomAdded)
        {
            SetZoomTo(_pourcentZoom + pourcentZoomAdded);

        }


        void OnValidate()
        {
            SetZoomTo(_pourcentZoom);
        }

    }
}