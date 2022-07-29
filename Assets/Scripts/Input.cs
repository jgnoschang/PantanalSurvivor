
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    static public InputAction move;
    static public InputAction mousePos;
    static public InputAction fire;
    static public InputAction use;
    static public InputAction inventory;
    static public InputAction zoom;
    private GameControls gameControls;
    private void Awake()
    {
        gameControls = new GameControls();

    }

    public void OnEnable()
    {
        move = gameControls.Player.Move;
        move.Enable();

        mousePos = gameControls.Player.MousePos;
        mousePos.Enable();

        zoom = gameControls.Player.Zoom;
        zoom.Enable();

        fire = gameControls.Player.Fire;
        fire.Enable(); 

        use = gameControls.Player.Use;
        use.Enable();
        
        inventory = gameControls.Player.Inventory;
        inventory.Enable();
    }
    private void OnDisable()
    {
        move.Disable();

        mousePos.Disable();

        zoom.Disable();

        fire.Disable();

        use.Disable();
        
        inventory.Disable();
    }
}
