using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
namespace BlackBoxTools.WIP
{
    public class OnKeysPressing : MonoBehaviour
    {

        public enum PressureType { AllIsPressed, OneIsPressed }
        public PressureType _pressureType;
        public KeyCode[] _keysPressed;
        public UnityEvent _onPressing;


        void Update()
        {

            bool isPressingKey = _pressureType == PressureType.AllIsPressed ? IsPressingAllKey() : IsPressingOneKey();
            if (isPressingKey)
                _onPressing.Invoke();
        }


        public bool IsPressingOneKey()
        {
            for (int i = 0; i < _keysPressed.Length; i++)
            {
                if (Input.GetKey(_keysPressed[i]))
                    return true;
            }
            return false;
        }

        public bool IsPressingAllKey()
        {
            for (int i = 0; i < _keysPressed.Length; i++)
            {
                if (!Input.GetKey(_keysPressed[i]))
                    return false;
            }
            return true;
        }
    }
}