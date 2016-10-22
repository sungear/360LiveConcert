using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlackBoxTools.T360{
public class DebugDraw_Line : MonoBehaviour {

    public Transform [] _linkedPoints;
    [Header("Debug Line")]
    public Color _lineColor;
    [Header("Ingame line")]
    public bool _drawIngameLine;
    public GameObject _lineContainer;
    public LineRenderer _lineRenderer;
    public float _lineSize=0.05f;
    public Material _lineMaterial;




    void Awake() {

        if (_lineContainer != null)
            Destroy(_lineContainer);
        if (_lineRenderer != null)
            Destroy(_lineRenderer);
    }

    void Start() {
        if(_drawIngameLine)
             CreateLine();
    }
	void Update () {
        int pointsCount = _linkedPoints.Length;
        /**
        if (pointsCount <= 1) return;
        if (pointsCount == 2) {

        } 
        **/

        for (int i = 0; i < _linkedPoints .Length; i++)
        {
            if (i != 0 && i != _linkedPoints.Length)
            {
                Debug.DrawLine(_linkedPoints[i - 1].position, _linkedPoints[i].position, _lineColor,Time.deltaTime);

            }
        }

        if (_lineRenderer != null) {
            _lineRenderer.SetVertexCount(_linkedPoints.Length);
            for (int i = 0; i < _linkedPoints.Length; i++)
            {
                _lineRenderer.SetPosition(i, _linkedPoints[i].position);
            }
        }

	
	}

    public void OnValidate() {

        if (_drawIngameLine == true && _lineRenderer == null)
        {
            CreateLine();

        }
        if (_drawIngameLine == false && _lineRenderer != null)
        {
            DestroyLine();
        }
    }

    private void DestroyLine()
    {
        Destroy(_lineContainer);
        Destroy(_lineRenderer);
    }

    private void CreateLine()
    {
        _lineContainer = new GameObject("TMP: LineRenderer") as GameObject;
        _lineRenderer = _lineContainer.AddComponent<LineRenderer>() as LineRenderer;
        _lineRenderer.SetColors(_lineColor, _lineColor);
        _lineRenderer.material = new Material(Shader.Find("Diffuse"));
        _lineRenderer.material.color = _lineColor;
        _lineRenderer.SetWidth(_lineSize, _lineSize);
    }

    public void OnDestroy() {
        if (_lineContainer != null)
        {
            Destroy(_lineContainer);
        }
        if (_lineRenderer != null)
        {
            Destroy(_lineRenderer);
        }
    }
}}
