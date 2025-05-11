using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    [SerializeField] float speed = 100;
    [SerializeField] AudioClip left_seesaw;
    [SerializeField] AudioClip right_seesaw;
    [SerializeField] AudioClip thud;
    [SerializeField] float volume = 1;
    bool PlaySound = true;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Shit works");
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // Play crank sound when turning seesaw
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioManager.Instance.PlayClip(left_seesaw, volume);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioManager.Instance.PlayClip(right_seesaw, volume);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float currentAngle = m_Rigidbody.rotation;
        float newAngle = currentAngle + horizontal * speed;
        m_Rigidbody.MoveRotation(newAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bad way to prevent thump sound from being played too much
        if (PlaySound == true)
        {
            AudioManager.Instance.PlayClip(thud, volume);

            PlaySound = false;
            StartCoroutine(Wait());

        }
 
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        PlaySound = true;
    }
}
