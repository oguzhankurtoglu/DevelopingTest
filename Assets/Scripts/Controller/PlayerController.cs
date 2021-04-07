using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Joystick _joyStick;
    private Animator _playerAnims;
    public static bool _canMove=true;
    private int Idle = Animator.StringToHash("Idle");
    private int Run = Animator.StringToHash("Running");
    private int Fall = Animator.StringToHash("Fall");
    [SerializeField] private float _sensivity;
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _joyStick = FindObjectOfType<Joystick>();
        _playerAnims = GetComponent<Animator>();
    }
   
    private void PlayerMove()
    {
        float XAxis = _joyStick.Horizontal * _sensivity;
        float ZAxis = _joyStick.Vertical * _sensivity;
        _playerRigidbody.velocity = new Vector3(XAxis,_playerRigidbody.velocity.y,ZAxis);
        if (Input.GetMouseButton(0))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(_joyStick.Horizontal
                  , _joyStick.Vertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }
        if (Input.GetMouseButtonUp(0) && _playerRigidbody.velocity.sqrMagnitude == 0)
            _playerAnims.CrossFade(Idle, .1f);
        if (Input.GetMouseButtonDown(0))
            _playerAnims.CrossFade(Run, .1f);
    }
    private void Update()
    {
        if (GameManager.state==GameManager.State.Running&&_canMove)
        {
            PlayerMove();
        }       
    }
    private void ResetPosition()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }
    WaitForSeconds duraiton = new WaitForSeconds(2.5f);
    private IEnumerator WaitForAnim()
    {
        _canMove = false;
        yield return duraiton;
        ResetPosition();
        _playerAnims.CrossFade(Idle, .1f);
        _canMove = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            _playerAnims.CrossFade(Fall, .1f);
            StartCoroutine(WaitForAnim());

        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            _canMove = false;
            transform.DOMove(collision.transform.position, 1f);
            GameManager.state = GameManager.State.Finish;
        }
    }

}
