using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlackBoxTools.T360{
public class DrawRecordedLine : MonoBehaviour {


    public Transform _center;
    public RecordTransform360 _transformRecorder;
    public LineRenderer _lineRender;
    public float _custumHeight=1.2f;


    public void DrawPath() {
        List<Position360> positions =
        _transformRecorder._360PositionsRecorded;

        _lineRender.SetVertexCount(positions.Count);
        for (int i = 0; i < positions.Count; i++)
        {
            Position360 pos = positions[i];
            pos.SetHeight(_custumHeight);
            Vector3 positionOfFrame = Transform360.GetVertexOf(_center.position, _center.rotation,pos );
            _lineRender.SetPosition(i, positionOfFrame);

        }

    }
}}
