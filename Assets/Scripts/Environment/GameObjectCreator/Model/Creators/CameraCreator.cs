using Cinemachine;

public class CameraCreator : Creator
{
    public CinemachineVirtualCamera CinemachineCamera { get => newGameObject.GetComponentInChildren<CinemachineVirtualCamera>(); }
}
