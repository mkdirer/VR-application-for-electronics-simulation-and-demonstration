using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectriceLineScript : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform[] _jumperTransforms;

    [SerializeField] private Texture[] textures;

    private int stepOfAnimation;

    [SerializeField] private float fps = 30f;
    private float counter;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        counter += Time.deltaTime;

        if(counter >= 1f/fps)
        {
            stepOfAnimation++;
            if(stepOfAnimation == textures.Length)
            {
                stepOfAnimation = 0;
            }
            _lineRenderer.material.SetTexture("_MainTex", textures[stepOfAnimation]);

            counter = 0f;
        }


        _lineRenderer.positionCount = _jumperTransforms.Length;
        for (int i = 0; i < _jumperTransforms.Length; i++)
        {
            _lineRenderer.SetPosition(i, _jumperTransforms[i].position);
        }
    }
}
