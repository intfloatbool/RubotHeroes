using System;
using System.Collections;
using System.Collections.Generic;
using Commands;

using Random = UnityEngine.Random;

public static class CommandHelper
{
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
                return new ProtectionShieldCommand(robot); 
            }
            case CommandType.REBOOT:
            {
                return new ProtectionShieldCommand(robot); 
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
        int commandsSize = Enum.GetNames(typeof(CommandType)).Length;
        CommandType randomType = (CommandType) Random.Range(0, commandsSize);
        return randomType;
    }
}
