using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HookLaunchState : State
{
    public override void Execute(StateController controller)
    {
        controller.player.Look.Execute();
        controller.player.Walk.Execute();
        controller.player.Jump.Execute();
        controller.player.Fall.Execute();
        controller.player.Move.Execute();
    }
}