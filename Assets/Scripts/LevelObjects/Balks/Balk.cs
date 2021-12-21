using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Balk : MonoBehaviour
{
    [SerializeField] private float _forceOnAttach;
    [SerializeField] private Rigidbody _jointRigidbody;
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;
    [SerializeField] private Transform _lookAtPoint;
    [SerializeField] private SphereCollider _sphereCollider;

    private bool _isAttachingPlayer;
    private CameraMover _cameraMover;
    private Rigidbody _rigidbody;
    public Character CurrentCharacter { get; protected set; }

    public bool HasCharacter => CurrentCharacter;
    public Rigidbody JointRigidbody => _jointRigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;
    public Vector3 LookAtPoint => _lookAtPoint.position;

    public Vector3 PushVector = Vector3.zero;

    public bool IsAttachingPlayer => _isAttachingPlayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cameraMover = Camera.main.GetComponent<CameraMover>();
    }

    public void DetachCharacter()
    {
        if (HasCharacter)
        {
            CurrentCharacter = null;
            PushVector = Vector3.zero;
            _isAttachingPlayer = false;
        }
    }

    public void PushCharacter(Vector3 pushVector)
    {
        CurrentCharacter.BalkPush(pushVector);
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _forceOnAttach, ForceMode.Impulse);
    }

    public virtual void ScaleCamera(float dragValue)
    {
        _cameraMover.ScaleFOV(dragValue);
    }

    public virtual void Interaction(Character character)
    {
        var Player = character.GetComponent<PlayerCollider>();
        CurrentCharacter = character;

        if (Player == null)
        { 
            if (_sphereCollider != null)
                _sphereCollider.enabled = false;
        }
         else
        {
            if (_sphereCollider != null)
            {
                _sphereCollider.enabled = true;
                _isAttachingPlayer = true;
            }
        }
    }
}
