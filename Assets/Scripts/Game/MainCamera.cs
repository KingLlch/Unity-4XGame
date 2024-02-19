using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private float speed;

    private float _mouseWheel;

    private void Update()
    {
        if ((Camera.fieldOfView <= 150) && (Camera.fieldOfView >= 40))
        {
            _mouseWheel -= Input.GetAxis("Mouse ScrollWheel")*20;

        }
        
        else if (Camera.fieldOfView < 40) { _mouseWheel = -60; }
        else if (Camera.fieldOfView > 150) { _mouseWheel = 50; }
        Camera.fieldOfView = 100 + _mouseWheel;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        gameObject.transform.position = transform.position + new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        if (gameObject.transform.position.x > 38) { gameObject.transform.position = new Vector3(38, transform.position.y, transform.position.z); }
        if (gameObject.transform.position.x < -60) { gameObject.transform.position = new Vector3(-60, transform.position.y, transform.position.z); }
        if (gameObject.transform.position.y > 45) { gameObject.transform.position = new Vector3(transform.position.x,45, transform.position.z); }
        if (gameObject.transform.position.y < -35) { gameObject.transform.position = new Vector3(transform.position.x,-35, transform.position.z); }

    }
}
