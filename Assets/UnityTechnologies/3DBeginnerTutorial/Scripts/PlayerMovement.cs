using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigitbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
 
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigitbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical,0f);

        bool isWalking= hasHorizontalInput|| hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForawrd = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForawrd);

    }
    void OnAnimatorMove()
    {
        m_Rigitbody.MovePosition(m_Rigitbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigitbody.MoveRotation(m_Rotation);
    }
}
