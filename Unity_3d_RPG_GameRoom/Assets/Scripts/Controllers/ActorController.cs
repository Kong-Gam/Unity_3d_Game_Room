using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    protected CharacterController _characterController;
    protected Animator _anim;

    protected float _moveSpeed; // Actor �̵� �ӵ�

    protected float _speedSmoothTime = 0.1f; // Smooth�� �̵��� ���� �����ð�

    protected float _speedSmoothVelocity; // SmoothDamp �޼��忡�� ���ϵ� ���� �ޱ� ���� ����

    protected float _currentVelocityY; // Y���� �ӵ� (�߷�ũ��)

    protected float _currentSpeed => // Actor X,Z �ӵ�(������� ���� �ӵ�)
        new Vector2(_characterController.velocity.x, _characterController.velocity.z).magnitude;

    public Defines.Actors ActorsType { get; protected set; } = Defines.Actors.Unknown; // ActorType �ʱ�ȭ
    public abstract void Init();
    private void Start()
    {
        Init();
    }
}
