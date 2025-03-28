using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerInputActions inputs { get; private set; }
    public PlayerInputActions.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        inputs = new PlayerInputActions();
        playerActions = inputs.Player;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
