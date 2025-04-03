using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private ExitManager _exitManager;

    [SerializeField]
    private GameObject _backGround;
    private Material _backGroundMat;

    [SerializeField]
    private float _bgWhitePercent;

    private int _sceneIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backGroundMat = _backGround.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        SetBackGroundColor();
        CheckWin();
        if (Input.GetKeyDown(KeyCode.R)){
            ResetScene();
        }
    }

    void CheckWin()
    {
        if(_exitManager._exitTimer <= 0)
            NextScene();
    }

    void SetBackGroundColor(){
        Color playerColor = _playerController.currentPlayer.GetComponent<MeshRenderer>().material.color;
        _backGroundMat.color = Color.white * _bgWhitePercent + playerColor * (1f - _bgWhitePercent);
    }

    private void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void NextScene(){
        _sceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(_sceneIndex);
    }
}
