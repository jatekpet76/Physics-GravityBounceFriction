using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] float _step = 1;
    [SerializeField] bool _gamepadEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        var zPos = ((Input.GetKey(KeyCode.J) ? _step: 0) * Time.deltaTime) + 
            ((Input.GetKey(KeyCode.L) ? -1*_step: 0) * Time.deltaTime);
        var xPos = ((Input.GetKey(KeyCode.I) ? _step: 0) * Time.deltaTime) +
            ((Input.GetKey(KeyCode.K) ? -1*_step: 0) * Time.deltaTime);
        var yPos = ((Input.GetKey(KeyCode.U) ? _step: 0) * Time.deltaTime) +
            ((Input.GetKey(KeyCode.O) ? -1*_step: 0) * Time.deltaTime);

        if (_gamepadEnabled) { 
            xPos = Input.GetAxis("Vertical") *_step * Time.deltaTime;
            zPos = Input.GetAxis("Horizontal") *_step * Time.deltaTime;
        }

        var euler = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(euler.x + xPos, euler.y + yPos, euler.z + zPos);
    }
}
