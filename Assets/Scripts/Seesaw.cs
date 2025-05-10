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

        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");

        m_Rigidbody.rotation += horizontal*speed;
    }
}
