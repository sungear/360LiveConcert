using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BlackBoxTools.VR
{
    public class VrActivator : MonoBehaviour
    {

        public string _forceActivationOf = "";

        public HelmetActivator[] _helmetSwitchActivators = new HelmetActivator[] {
        new HelmetActivator() { _helmetName = "Oculus"},
        new HelmetActivator() { _helmetName = "Cardboard"}
    };
        [Header("If no VR asked or error:")]
        public EventActivator _activatorElse;

        [Header("Debug (Delete Later)")]
        public Text _textDebugView;
        void Awake()
        {
            CheckAndActivateDependingOfVrSupport();

        }


        public void ActivateHelmetOrElse(string name)
        {


            int i = _helmetSwitchActivators.Length - 1;

            if (!string.IsNullOrEmpty(name))
                while (i >= 0)
                {
                    if (_helmetSwitchActivators[i] != null && name==_helmetSwitchActivators[i]._helmetName)
                    {
                        if (_helmetSwitchActivators[i]._activator != null)
                        {
                            _helmetSwitchActivators[i]._activator.Activate();
                            return;
                        }
                    }
                    i--;
                }
            _activatorElse.Activate();

        }

        void CheckAndActivateDependingOfVrSupport()
        {

            bool isDesignForOculus = false;
            bool isVrEnable = false;
            string debugResume = "";
            foreach (string device in UnityEngine.VR.VRSettings.supportedDevices)
            {
                if(! "None".Equals(device)) { 
                    isVrEnable = true;
                    debugResume += (">Device Supp:" + device) + "\n";
                    if ("Oculus".Equals(device))
                        isDesignForOculus = true;
                    debugResume += (">VR Enable:" + UnityEngine.VR.VRSettings.enabled) + "\n";

                }

            }

            if ( ! string.IsNullOrEmpty(_forceActivationOf))
            {
                ActivateHelmetOrElse(_forceActivationOf);
            }
            else
            {

#if !UNITY_EDITOR
#if UNITY_ANDROID
                    if (isVrEnable && isDesignForOculus){
                        ActivateHelmetOrElse("Oculus");
        
                        debugResume += (">Selected: Oculus" ) + "\n";  
                    }
                    else if( isVrEnable ){
                        ActivateHelmetOrElse("Cardboard");
                        debugResume += (">Selected: Cardboard" ) + "\n"; 
                    }
                    else{
                        _activatorElse.Activate();
                        debugResume += (">Selected: Default" ) + "\n"; 
                    }
#else
                    _activatorElse.Activate();
                    debugResume += (">Selected: Not Android Phone" ) + "\n"; 
#endif
#else
                _activatorElse.Activate();
#endif
            }
            debugResume += ("Device:" + UnityEngine.VR.VRSettings.loadedDeviceName) + "\n";

            print(debugResume);
            if (_textDebugView)
                _textDebugView.text = debugResume;
        }

        [System.Serializable]
        public class HelmetActivator
        {
            public string _helmetName;
            public EventActivator _activator;
        }
    }
}