using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Quaternion _instanceRotation;

    [SerializeField] private Transform _instancePoint;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public override void InstallBindings()
    {
        GameObject player = Container.InstantiatePrefab(_playerPrefab, _instancePoint.position, _instanceRotation, null);

        _virtualCamera.LookAt = player.transform;
        _virtualCamera.Follow = player.transform;
    }
}