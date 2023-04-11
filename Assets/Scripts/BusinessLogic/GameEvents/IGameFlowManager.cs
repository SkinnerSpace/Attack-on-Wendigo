using System;

public interface IGameFlowManager
{
    void SubscribeOnGameOver(Action onGameOver);
    void SubscribeOnVictory(Action onVictory);
}
