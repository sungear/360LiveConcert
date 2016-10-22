using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
namespace BlackBoxTools.Viewer360Alpha
{

    public class UI_VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        private Image _backGroundImage;

        [SerializeField]
        private Image _joystickImage;

        [SerializeField]
        private Vector2 _joystickDirection;



        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        internal float GetHorizontal()
        {
            return _joystickDirection.x;
        }

        internal float GetVertical()
        {
            return _joystickDirection.y;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetJoystickPosition(Vector3.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {

            Vector2 position;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backGroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out position))
            {

                position.x = (position.x / _backGroundImage.rectTransform.sizeDelta.x) * 2f;
                position.y = (position.y / _backGroundImage.rectTransform.sizeDelta.y) * 2f;
                if (position.magnitude > 1f)
                    position = position.normalized;
                Debug.Log("dd" + position);
                SetJoystickPosition(position);
            }

        }

        private void SetJoystickPosition(Vector2 position)
        {
            _joystickDirection = position;
            _joystickImage.rectTransform.anchoredPosition =
                            new Vector3(position.x * (_backGroundImage.rectTransform.sizeDelta.x / 3f)
                            , position.y * (_backGroundImage.rectTransform.sizeDelta.y / 3f)
                            );
        }
    }
}