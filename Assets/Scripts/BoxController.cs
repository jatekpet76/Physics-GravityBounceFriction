using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] float _step = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var zPos = (Input.GetKey(KeyCode.J) ? _step: 0) * Time.deltaTime;
        var zNeg = (Input.GetKey(KeyCode.L) ? -1*_step: 0) * Time.deltaTime;
        var xPos = (Input.GetKey(KeyCode.I) ? _step: 0) * Time.deltaTime;
        var xNeg = (Input.GetKey(KeyCode.K) ? -1*_step: 0) * Time.deltaTime;
        var yPos = (Input.GetKey(KeyCode.U) ? _step: 0) * Time.deltaTime;
        var yNeg = (Input.GetKey(KeyCode.O) ? -1*_step: 0) * Time.deltaTime;

        var euler = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(euler.x + (xPos + xNeg), euler.y + (yPos + yNeg), euler.z + (zPos + zNeg));
    }
}
