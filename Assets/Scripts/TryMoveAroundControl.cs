using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryMoveAroundControl : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;
    [SerializeField] Vector3 _jumpVelocity = new Vector3(0, 100, 0);
    Rigidbody _rigidbody;

    Vector3 _previousPosition = new Vector3(0, 0, 0);
    Vector3 _fixedPreviousPosition = new Vector3(0, 0, 0);
    Direction _previousDirection = new Direction();
    Vector3 _ballPosition = new Vector3(0, 0, 0);

    Vector3 _previousVelocity;

    bool _isForceAdded = false;

    class Direction {
        int x = 0;
        int y = 0;

        public void Update (Vector3 changes) {
            var px = x; 
            var py = y;

            if (changes.x > 0) {
                x = 1;
            } else if (changes.x < 0) {
                x = -1;
            } else {
                x = 0;
            }

            if (changes.y > 0) {
                y = 1;
            } else if (changes.y < 0) {
                y = -1;
            } else {
                y = 0;
            }

            if (x == 0 && y == 0) {
                x = px;
                y = py;

                Debug.Log("NOT UPDATED!");
            }
        }

        public bool IsChanged(Direction dir) {
            if (dir.x == 0 && dir.y == 0) {
                return false;
            } else {
                return (x != dir.x || y != dir.y);
            }
        }

        public override string ToString () {
            return string.Format("(x: {0}, y: {1})", x , y);
        }
    }

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

    // Update is called once per frame
    void TryFailed() {
        DirectControl();

        Vector3 changes = GetPositionChanges(_previousPosition);
        Direction direction = new Direction();
        direction.Update(changes);

        Debug.Log(string.Format("Update - Name: {0}, PrePos: {1}, Pos: {2}, Change: {3}, Frame: {4}, DeltaTime: {5}, Time: {6}",
            gameObject.name, _previousPosition.ToString("F4"), transform.position.ToString("F4"), changes.ToString("F4"), Time.frameCount, Time.deltaTime, Time.time));

        if (direction.IsChanged(_previousDirection)) {
            Debug.Log(string.Format("Direction changed {0}, {1}", _previousDirection, direction));
        }

        _previousPosition = transform.position;
        _previousDirection.Update(changes);

        // AddForce();
    }
    void FixedUpdate() {
        Debug.Log(string.Format("Update - Name: {0}, PrePos: {1}, Pos: {2}, Velocity: {3}, Magnitude: {4}, Frame: {5}, DeltaTime: {6}, Time: {7}",
            gameObject.name, _previousPosition.ToString("F4"), transform.position.ToString("F4"), _rigidbody.velocity.ToString("F4"), _rigidbody.velocity.magnitude.ToString("F4"), Time.frameCount, Time.deltaTime, Time.time));

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

    Vector3 GetPositionChanges(Vector3 previousPosition) {
        return previousPosition - transform.position;
    }
}
