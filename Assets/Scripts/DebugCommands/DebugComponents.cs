using System;
using Character;

[Serializable]
public class DebugComponents
{
    public DebugController debugController;
    public WendigoSpawner spawner;
    public PlayerCharacter player;
    public Helicopter helicopter;
    public Airdrop airdrop;
    public HintMessage hint;
    public HintManager hintManager;
}
