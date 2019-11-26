using System;

namespace Interfaces
{
    public interface ICommandExecutor
    {
        event Action<CommandType> OnCommandTypeExecuted;
    }
}
