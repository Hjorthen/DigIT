using UnityEngine;


[System.Serializable]
public class ShopEnteredHandler : MonoBehaviour, ICommandHandler
{
    [SerializeField]
    private string command = "sell";
    private PlayerMiningController enteredPlayer;
    private IPriceProvider priceProvider;

    [SerializeField]
    private ShopHandler handler;

    void Awake()
    {
        ServiceRegistry.RegisterService<ICommandHandler>(this);
    }

    void Start() {
        handler.Init();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        var playerController = collider.GetComponent<PlayerMiningController>();
        if(playerController != null) {
            GameConsole.WriteLine($"Command \"{command}\" now available");
            enteredPlayer = playerController;
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        GameConsole.WriteLine($"Command \"{command}\" no longer available");
        enteredPlayer = null;
    }

    public bool HandleCommand(string command) {
        if(command == this.command && enteredPlayer != null) {
            handler.HandleCommand(command, enteredPlayer);
        }

        return true;
    }
}
