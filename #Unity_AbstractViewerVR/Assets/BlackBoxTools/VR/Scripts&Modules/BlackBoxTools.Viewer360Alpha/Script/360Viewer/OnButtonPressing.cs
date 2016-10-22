using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace BlackBoxTools.Viewer360Alpha
{

    public class OnButtonPressing : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent _onPressing;
        public void OnPointerDown(PointerEventData eventData)
        {
            UserIsInPress = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UserIsInPress = false;
        }

        public bool UserIsInPress;

        void Update()
        {
            if (UserIsInPress)
            {
                _onPressing.Invoke();
            }
        }
    }


}