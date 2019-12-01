using System;
using System.Collections;
using System.Collections.Generic;
using Commands;

using Random = UnityEngine.Random;

public static class CommandHelper
{
    private static List<CommandType> _readyCommands = new List<CommandType>()
    {
        CommandType.JUMP,
        CommandType.REBOOT,
        CommandType.RANDOM_MOVE,
        CommandType.MEELE_ATTACK,
        CommandType.PROTECTED_SHIELD,
        CommandType.LAUNCH_MISSLE,
        CommandType.PUT_LANDMINE
    };
    
    /// <summary>
    /// Commands which can be able to select from UI!
    /// </summary>
    public static List<CommandType> ReadyCommands => _readyCommands;

    public static IEnumerable<ICommand> GetRandomCommands(int countOfRandomCommands,Robot robot)
    {
        for (int i = 0; i < countOfRandomCommands; i++)
        {
            yield return GetRandomCommand(robot);
        }

    }

    public static IEnumerable<CommandType> GetRandomCommandTypes(int countOfRandomCommands)
    {
        for (int i = 0; i < countOfRandomCommands; i++)
        {
            yield return GetRandomCommandType();
        }
    }

    public static IEnumerable<ICommand> GetCommandsByTypes(IEnumerable<CommandType> commandTypes, Robot robot)
    {
        foreach (CommandType type in commandTypes)
        {
            yield return GetCommandByType(type, robot);
        }
    }
    private static ICommand GetCommandByType(CommandType commandType, Robot robot)
    {
        switch (commandType)
        {
            case CommandType.JUMP:
            {
                return new JumpCommand(robot); 
            }
            case CommandType.RANDOM_MOVE:
            {
                return new RandomMoveCommand(robot); 
            }
            case CommandType.MEELE_ATTACK:
            {
                return new MeeleAttackCommand(robot); 
            }
            case CommandType.LAUNCH_MISSLE:
            {
                return new LaunchMissleCommand(robot); 
            }
            case CommandType.PUT_LANDMINE:
            {
                return new PutLandmineCommand(robot);
            }
            case CommandType.PROTECTED_SHIELD:
            {
                return new ProtectionShieldCommand(robot); 
            }
            case CommandType.OVERLOAD:
            {
                return new OverloadCommand(robot); 
            }
            case CommandType.REBOOT:
            {
                return new RebootCommand(robot); 
            }
            default:
                throw new Exception($"Command with type {commandType} not found!!!");
        }
    }
    
    private static ICommand GetRandomCommand(Robot robot)
    {
        //TODO realize more commands!
        //READY COMMANDS!
        CommandType randomType = GetRandomCommandType();

        return GetCommandByType(randomType, robot);
    }

    private static CommandType GetRandomCommandType()
    {
        return _readyCommands[Random.Range(0, _readyCommands.Count)];
    }
}
