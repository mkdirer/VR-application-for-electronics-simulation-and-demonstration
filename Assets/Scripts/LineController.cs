using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer _lineRenderer;

    [SerializeField] private Transform[] _jumperTransforms;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        _lineRenderer.positionCount = _jumperTransforms.Length;
        for (int i = 0; i < _jumperTransforms.Length; i++)
        {
            _lineRenderer.SetPosition(i, _jumperTransforms[i].position);
        }
    }
}
