using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    [SerializeField] float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Shit works");
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float currentAngle = m_Rigidbody.rotation;
        float newAngle = currentAngle + horizontal * speed;
        m_Rigidbody.MoveRotation(newAngle);
    }
}
