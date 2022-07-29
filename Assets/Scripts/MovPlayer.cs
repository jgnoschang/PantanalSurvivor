using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovPlayer : MonoBehaviour
{
    [Header("Player controler")]
    private Animator p_Animator;
    private Vector3 p_Movimento;
    [SerializeField] private float Gravity;
    Quaternion p_Rotacao = Quaternion.identity;
    static public CharacterController p_CharacterController;
    public int velocidade;
    public float velocidadeDeRotacao = 25f;

    [Header("Camera Controler")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;
    [SerializeField] float MaxScrollRange = 15;
    private Transform p_camera;



    void Start()
    {
        p_Animator = GetComponent<Animator>();
        p_CharacterController = GetComponent<CharacterController>();
        p_camera = FindObjectOfType<Camera>().transform;
    }

    void FixedUpdate()
    {
        CamControler();
        float horizontal = Input.move.ReadValue<Vector2>().x;
        float vertical = Input.move.ReadValue<Vector2>().y;

        p_Movimento.Set(horizontal, Gravity, vertical);

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool Andando = hasHorizontalInput || hasVerticalInput;
        //p_Animator.SetBool("Andando", Andando);

        Vector3 frente = Vector3.RotateTowards(transform.forward, new Vector3 (p_Movimento.x, 0 , p_Movimento.z), velocidadeDeRotacao * Time.deltaTime, 0f);
        p_Rotacao = Quaternion.LookRotation(frente);
        if (Andando)
        {
            transform.rotation = Quaternion.Lerp (transform.rotation, p_Rotacao, Time.deltaTime * 10);
        }
        p_CharacterController.Move(p_Movimento * Time.deltaTime * velocidade);
    }

    /*void OnAnimatorMove()
    {
        p_CharacterController.MovePosition(p_CharacterController.position + p_Movimento * p_Animator.deltaPosition.magnitude);
        p_CharacterController.MoveRotation(p_Rotacao);
    }*/

    private void CamControler() {
        if (Input.zoom.ReadValue<float>() != 0) {
            float camY = offset.y;
            camY += Input.zoom.ReadValue<float>() / 60;
            offset.y = Mathf.Clamp(camY, 2, MaxScrollRange);
        }
        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(p_camera.position, desiredPosition, smoothSpeed);
        p_camera.position = smoothedPosition;

        p_camera.LookAt(transform);
    }
}
