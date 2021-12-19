using System.Collections;
using System.Collections.Generic;
using Assets.PixelCrew.Utils;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _speed = 1;

    private float _lastRadius;

    private void FixedUpdate()
    {
        SetChildsPositions(Time.time * _speed);
    }

    private void SetChildsPositions(float angleOffset)
    {
        float children = transform.childCount;
        float angle = Mathf.PI * 2f / children;
        var center = transform.position;
        for (int i = 0; i < children; ++i)
        {
            var childTransform = transform.GetChild(i);
            var pos = childTransform.position;
            pos.y = center.y + Mathf.Sin(angle * i + angleOffset) * _radius;
            pos.x = center.x + Mathf.Cos(angle * i + angleOffset) * _radius;
            childTransform.position = pos;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.forward, Vector3.up, 360, _radius);
        if (_lastRadius != _radius)
        {
            _lastRadius = _radius;
            SetChildsPositions(0);
        }
    }
#endif
}
