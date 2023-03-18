// An interaction entity is an entity which the player can interact with. 
// Usually when nearby to start a dialogue or enter a shop. 
public interface InteractionEntity {
    public enum InteractionType {
        DIALOGUE,
        SHOP
    }
    bool Active {
        set;
    }

    InteractionType Type {
        get;
    }
}
