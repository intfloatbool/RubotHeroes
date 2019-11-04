using System.Collections.Generic;
using Abstract;
using Commands;
using UnityEngine;

public class MoveRandomizer : OnGameEndTrigger
{
    protected override void OnGameEnd(RobotCommandRunner winner)
    {
        Debug.Log("Set commands for winner!");
        Robot robot = winner.Robot;
        winner.RunCommands(new List<ICommand>
            {
                new RandomMoveCommand(robot),
                new RandomMoveCommand(robot),
                new RandomMoveCommand(robot),
                new JumpCommand(robot),
                new JumpCommand(robot),
                new RandomMoveCommand(robot),
                new RandomMoveCommand(robot)
            }
        );
    }
}
