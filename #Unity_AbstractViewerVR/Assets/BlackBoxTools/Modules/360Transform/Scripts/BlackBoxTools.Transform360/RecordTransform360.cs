using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace BlackBoxTools.T360{
public class RecordTransform360 : MonoBehaviour {

    public float _frameTime = 0.1f;
    public Transform360 _transformRecorded;
    public List<Position360> _360PositionsRecorded = new List<Position360>();

    [Header("Event Handler")]
    public UnityEvent _onRecordEnd;
    public UnityEvent _onKeyRecorded;


    private bool record = false;
    private float countDown = 0f;
    
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            StartRecord();
        if (Input.GetMouseButtonUp(0))
            StopRecord();
        if (!record) return;
        countDown -= Time.deltaTime;
        if (countDown <= 0f)
        {
            countDown = _frameTime;
            AddKeyFrame();
        }
            

    }

    private void AddKeyFrame()
    {
        _360PositionsRecorded.Add( _transformRecorded.GetPosition360() );
        _onKeyRecorded.Invoke();
    }

    void StartRecord() {
        Reset();
        record = true;

    }
    void StopRecord()
    {
        record = false;
        _onRecordEnd.Invoke();

    }
    void Reset() {
        _360PositionsRecorded.Clear();
        _360PositionsRecorded = new List<Position360>();
    }


}
}