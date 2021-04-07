using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static State state;
    [SerializeField] private GameObject _caseRun;
    [SerializeField] private GameObject _casePaint;
    [SerializeField] private GameObject _newCamPos;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private GameObject _startButton;
    public enum State
    {
        Start,
        Running,
        Finish,
        Painting
    }
    private void Awake()
    {
        state = State.Start;
        _mainCam = FindObjectOfType<Camera>();
    }

    private void FinishState()
    {
        if (state==State.Finish)
        {
            StartCoroutine(WaitForFinish());  
        }
    }
    private void PaintingState()
    {
        if (state==State.Painting)
        {
            _mainCam.transform.DOMove(_newCamPos.transform.position, 1f).SetEase(Ease.Linear);
            
        }
    }
    private void Update()
    {
        FinishState();
        PaintingState();
    }
    WaitForSeconds duraiton = new WaitForSeconds(2.5f);
    private IEnumerator WaitForFinish()
    {
        yield return duraiton;
        _caseRun.SetActive(false);
        _casePaint.SetActive(true);
        _newCamPos = GameObject.FindGameObjectWithTag("NewCamPos");
        _mainCam.orthographic = true;
        state = State.Painting;
    }
   public void StartButton()
    {
        state = State.Running;
        _startButton.SetActive(false);
    }
    public void Restart()
    {
        state = State.Start;
        PlayerController._canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
