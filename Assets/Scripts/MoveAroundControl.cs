using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundControl : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _velocity;
    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.AddForce((_velocity * Time.deltaTime));
    }
}
