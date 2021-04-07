using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIController : MonoBehaviour
{
    public List<Transform> _myWay;
    private Transform currentNode;
    public Transform[] _leftPath, _rightPath;
    private Rigidbody _aiRigidbody;
    private int Run = Animator.StringToHash("Running");
    private int Hit = Animator.StringToHash("Hit");
    private int Fall = Animator.StringToHash("Fall");
    private bool _look;
    private bool _aiCanMove=true;
    private Animator _aiAnims;
    private Vector3 _firstPos, _firstRot;
    [SerializeField]private float _speed;

    private int _nodeCount=1;
    
    private void Awake()
    {
        _aiRigidbody = GetComponent<Rigidbody>();
        _leftPath = GameObject.FindGameObjectWithTag("LeftPath").GetComponentsInChildren<Transform>();
        _rightPath = GameObject.FindGameObjectWithTag("RightPath").GetComponentsInChildren<Transform>();
       
        _aiAnims= GetComponent<Animator>();
        _firstPos = transform.position;
        _firstRot = transform.eulerAngles;
    }
    private void Start()
    {
        ChooseWay();
        _aiAnims.CrossFade(Run, .1f);
        currentNode = _myWay[_nodeCount];
        transform.LookAt(currentNode.transform.position);
    }
    private void Update()
    {
        if (GameManager.state==GameManager.State.Running)
        {
            AIMove();

        }
    }
    private void ChooseWay()
    {
        if (transform.position.x < 0)
        {
            for (int i = 1;i<_leftPath.Length ; i++)
            {
                _myWay.Add(_leftPath[i].transform);
            }
        }
        if (transform.position.x > 0)
        {

            for (int i = 1; i < _rightPath.Length; i++)
            {
                _myWay.Add(_rightPath[i].transform);
            }
        }
    }
    Vector3 temp;
    private void AIMove()
    {
        float distance = Vector3.Distance(transform.position, currentNode.transform.position);

        if (distance > 1)
        {
            if (currentNode != _myWay[_myWay.Count-1] && _aiCanMove)
            {
                transform.position += (transform.forward) * (_speed *Time.smoothDeltaTime);
            }
            //Vector3 pos = Vector3.MoveTowards(transform.position, currentNode.transform.position, _speed * Time.fixedDeltaTime);
            //_aiRigidbody.MovePosition(pos);
        }
        else
        {
            _look = false;
            _nodeCount++;
            currentNode = _myWay[_nodeCount];
            temp = new Vector3(currentNode.transform.position.x + Random.Range(-2f, 2f), currentNode.transform.position.y, currentNode.transform.position.z + Random.Range(-1f, 1f));
            transform.DOLookAt(temp, Time.deltaTime * 2).OnComplete(() => _look = true);
        }
        if (_look)
        {
           // transform.LookAt(temp);
        }
    }
    private void ResetValues()
    {
        transform.position = _firstPos;
        transform.eulerAngles = _firstRot;
        _nodeCount = 1;
        currentNode = _myWay[_nodeCount];
        transform.LookAt(currentNode.transform.position);

    }
    WaitForSeconds duraiton = new WaitForSeconds(1);
    private IEnumerator WaitForAnim()
    {
        yield return duraiton;
        ResetValues();
        _aiCanMove = true;
        _aiAnims.CrossFade(Run, .1f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            _aiCanMove = false;
            _aiAnims.CrossFade(Fall, .1f);
            StartCoroutine(WaitForAnim());
            
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            gameObject.transform.DOMove((gameObject.transform.position), .5f).OnStart(() => _aiAnims.CrossFade(Hit, Time.deltaTime)).OnComplete(() => _aiAnims.CrossFade(Run, .1f));
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatePlatform"))
        {

        }

    }

}
