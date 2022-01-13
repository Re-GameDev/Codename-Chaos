using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CenterFocus : MonoBehaviour
{
    // [SerializeField] float smoothDamp = 0.5f;
    List<Transform> focus = new List<Transform>();

    void Start()
    {

    }
    
    public void AddTargetToFocus(Transform obj ) => focus.Add(obj);

    public void RemoveTargetFromFocus( Transform obj ) => focus.Remove(obj);

    public void ClearTargetFocus() => focus.Clear();

    void LateUpdate()
    {
        if( focus.Count == 0 )
            return;
        Vector3 _target = Vector3.zero;
        foreach( var obj in focus )
            _target += obj.position;
        _target = _target / focus.Count;
        transform.LookAt( _target, -Physics.gravity );
    }
}
