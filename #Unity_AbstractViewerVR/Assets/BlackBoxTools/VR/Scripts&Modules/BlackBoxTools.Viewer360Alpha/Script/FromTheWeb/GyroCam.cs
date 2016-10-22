///SOURCE FROM WEB: http://forum.unity3d.com/threads/sharing-gyroscope-controlled-camera-on-iphone-4.98828/page-2
// Gyroscope-controlled camera for iPhone  Android revised 2.26.12
// Perry Hoberman <hoberman@bway.net>
//
// Usage:
// Attach this script to main camera.
// Note: Unity Remote does not currently support gyroscope.
//
// This script uses three techniques to get the correct orientation out of the gyroscope attitude:
// 1. creates a parent transform (camParent) and rotates it with eulerAngles
// 2. for Android (Samsung Galaxy Nexus) only: remaps gyro.Attitude quaternion values from xyzw to wxyz (quatMap)
// 3. multiplies attitude quaternion by quaternion quatMult
// Also creates a grandparent (camGrandparent) which can be rotated with localEulerAngles.y
// This node allows an arbitrary heading to be added to the gyroscope reading
// so that the virtual camera can be facing any direction in the scene, no matter what the phone's heading
//
// Ported to C# by Simon McCorkindale <simon <at> aroha.mobi>

using UnityEngine;
namespace BlackBoxTools.Viewer360Alpha
{


    public class GyroCam : MonoBehaviour
    {
        private bool gyroBool;
        private Gyroscope gyro;
        private Quaternion rotFix;


        [Tooltip("This Transform is affected by the gryoscope")]
        [SerializeField]
        private Transform _affectedByGyroscope;


        public void Start()
        {


#if UNITY_3_4
        gyroBool = Input.isGyroAvailable;
#else
            gyroBool = SystemInfo.supportsGyroscope;
#endif

            if (gyroBool)
            {

                gyro = Input.gyro;
                gyro.enabled = true;
                Vector3 initialRotation = new Vector3(90, 180, 0);

                if (Screen.orientation == ScreenOrientation.LandscapeLeft)
                {
                    rotFix = new Quaternion(0, 0, 1, 0);
                }
                else if (Screen.orientation == ScreenOrientation.Portrait)
                {
                    rotFix = new Quaternion(0, 0, 1, 0);
                }
                else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
                {
                    rotFix = new Quaternion(0, 0, 1, 0);
                }
                else if (Screen.orientation == ScreenOrientation.LandscapeRight)
                {
                    rotFix = new Quaternion(0, 0, 1, 0);
                }
                else
                {
                    rotFix = new Quaternion(0, 0, 1, 0);
                }

                //Screen.sleepTimeout = 0;
                _affectedByGyroscope.transform.eulerAngles = initialRotation;
            }
            else
            {
#if UNITY_EDITOR
                print("NO GYRO");
#endif
            }
        }

        public void Update()
        {
            gyroBool = SystemInfo.supportsGyroscope;
            if (gyroBool)
            {
                Quaternion quatMap;
#if UNITY_IPHONE
                quatMap = gyro.attitude;
#elif UNITY_ANDROID
                quatMap = new Quaternion(gyro.attitude.x, gyro.attitude.y, gyro.attitude.z, gyro.attitude.w);
#endif
                _affectedByGyroscope.localRotation = Quaternion.Euler(90, 180, 0) * (quatMap * rotFix);
            }
        }


    }
}