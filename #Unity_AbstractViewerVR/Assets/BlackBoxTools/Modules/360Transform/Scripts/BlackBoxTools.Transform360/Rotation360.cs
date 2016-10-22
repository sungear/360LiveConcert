using UnityEngine;
using System.Collections;

namespace BlackBoxTools.T360{
/**WIP*/
[System.Serializable]
public struct Rotation360
{
    [SerializeField]
    [Tooltip("Compare to the obect position on a sphere: Ouest=-90°, Nord=0°, Est:90°")]
    [Range(-180, 180)]
    private float _northPoleAngle;
    [SerializeField]
    [Tooltip("Head tilt compare to the horizon: Sky=90°, Horizon=0°, Ground:-90°")]
    [Range(-90f, 90f)]
    private float _horizonViewAngle;
    [SerializeField]
    [Tooltip("Head tilt right to left: right=-90°, normal=0°, left:90°")]
    [Range(-180, 180f)]
    private float _headRotationAngle;
}}