using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;

    PlayerControls _playerControls;

    [SerializeField] private Vector2 _movementInput;
    public float _verticalInput;
    public float _horizontalInput;
    public float _moveAmount;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        _playerControls = new PlayerControls();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChange;

        _instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if(newScene.buildIndex == SaveManager._instance.GetWorldSceneIndex())
        {
            _instance.enabled = true;
        }
        else
        {
            _instance.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (_playerControls == null) _playerControls = new PlayerControls();

        _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();

        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void HandleMovementInput()
    {
        _verticalInput = _movementInput.y;
        _horizontalInput = _movementInput.x;

        _moveAmount = Mathf.Clamp01(Mathf.Abs(_verticalInput) + Mathf.Abs(_horizontalInput));

        if (_moveAmount <= 0.5 && _moveAmount < 0)
        {
            _moveAmount = 0.5f;
        }
        else if (_moveAmount > 0.5 && _moveAmount <= 1)
        {
            _moveAmount = 1;
        }
    }
}
