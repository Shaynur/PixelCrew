using UnityEditor;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _speed = 1;

    private void FixedUpdate()
    {
        SetChildsPositions(Time.time * _speed);
    }

    private void SetChildsPositions(float rotateAngle)
    {
        float children = transform.childCount;
        if (children == 0)
        {
            enabled = false;
            Destroy(gameObject, 1f);
        }
        float step = Mathf.PI * 2f / children;
        var center = transform.position;
        for (int i = 0; i < children; ++i)
        {
            var childTransform = transform.GetChild(i);
            var pos = childTransform.position;
            pos.y = center.y + Mathf.Sin(step * i + rotateAngle) * _radius;
            pos.x = center.x + Mathf.Cos(step * i + rotateAngle) * _radius;
            childTransform.position = pos;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetChildsPositions(0);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.forward, Vector3.up, 360, _radius);
    }
#endif
}
