using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryMoveAroundControl : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;
    [SerializeField] Vector3 _jumpVelocity = new Vector3(0, 100, 0);
    Rigidbody _rigidbody;

    Vector3 _ballPosition = new Vector3(0, 0, 0);
    Vector3 _previousVelocity;
    bool _isForceAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ballPosition = transform.position;

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart() {
        Debug.Log("Late Start!");
        yield return new WaitForSeconds(1);

        Debug.Log("Call AddForce");
        AddForce();
    }

    void FixedUpdate() {
        Debug.Log(string.Format("Update - Name: {0}, Pos: {1}, Velocity: {2}, Magnitude: {3}, Frame: {4}, DeltaTime: {5}, Time: {6}",
            gameObject.name, transform.position.ToString("F4"), _rigidbody.velocity.ToString("F4"), _rigidbody.velocity.magnitude.ToString("F4"), Time.frameCount, Time.deltaTime, Time.time));

        if (_previousVelocity != null && 
            ( (_previousVelocity.z > 0 && _rigidbody.velocity.z < 0) || 
              (_previousVelocity.z < 0 && _rigidbody.velocity.z > 0) || 
              (_previousVelocity.x > 0 && _rigidbody.velocity.x < 0) || 
              (_previousVelocity.x < 0 && _rigidbody.velocity.x > 0)
            )) {
            Debug.Log("Z Direction Changed!");

            _rigidbody.AddForce((_velocity)); //  * Time.deltaTime
        }

        _previousVelocity = _rigidbody.velocity;
    }

    private void DirectControl() {
        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce((_jumpVelocity));
        }
    }

    private void Reset() {
        Debug.Log("Reset the Ball!");

        _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        _rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        transform.position = _ballPosition;
    }

    void AddForce() {
        if (!_isForceAdded) {
            _isForceAdded = true ;

            _rigidbody.AddForce((_velocity)); //  * Time.deltaTime
        }
    }
}
