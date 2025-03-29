using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraTransition : MonoBehaviour
{
    bool _transitioning = false;
    Vector3 _targetPosition;
    Vector3 _initialPosition;
    float _transitionTime = 1.0f;

    public void Transition(Vector3 targetPosition)
    {
        if(_transitioning)
            return;

        _transitioning = true;
        _targetPosition = targetPosition;
        _initialPosition = transform.position;
        _transitionTime = 0.0f;
    }

    void FixedUpdate()
    {
        if(!_transitioning)
            return;

        _transitionTime += Time.fixedDeltaTime;
        MathF.Min(_transitionTime, 1.0f);
        transform.position = Vector3.Lerp(_initialPosition, _targetPosition, _transitionTime);
        
        if (_transitionTime >= 1.0f)
        {
            transform.position = _targetPosition;
            _transitioning = false;
        }
    }
}
