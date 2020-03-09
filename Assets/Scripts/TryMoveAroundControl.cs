using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryMoveAroundControl : MonoBehaviour
{
    
    [SerializeField] Vector3 _velocity;
    Rigidbody _rigidbody;

    Vector3 _previousPosition = new Vector3(0, 0, 0);
    Vector3 _fixedPreviousPosition = new Vector3(0, 0, 0);

    bool _isForceAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Format("Update - Name: {0}, Position: {1}, deltaTime: {2}, frameCount: {3}, change: {4}, time: {5}", 
            gameObject.name, transform.position, Time.deltaTime, Time.frameCount, GetPositionChanges(_previousPosition), Time.time));

        _previousPosition = transform.position;

        AddForce();
    }

    private void FixedUpdate() {
        Debug.Log(string.Format("Fixed Update - Name: {0}, Position: {1}, deltaTime: {2}, frameCount: {3}, change: {4}, time: {5}", 
            gameObject.name, transform.position, Time.deltaTime, Time.frameCount, GetPositionChanges(_fixedPreviousPosition), Time.time));
        
        _fixedPreviousPosition = transform.position;
    }

    void AddForce() {
        if (!_isForceAdded) {
            _isForceAdded = true ;

            _rigidbody.AddForce((_velocity)); //  * Time.deltaTime
        }
    }

    Vector3 GetPositionChanges(Vector3 previousPosition) {
        return previousPosition - transform.position;
    }
}
