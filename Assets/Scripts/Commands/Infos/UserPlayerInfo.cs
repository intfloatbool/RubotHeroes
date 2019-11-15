using UnityEngine;

public class UserPlayerInfo : SingletonDoL<UserPlayerInfo>
{
   [SerializeField] private CommandsProviderBase _commandsProviderPrefab;
   private CommandsProviderBase _commandsProvider;

   public CommandsProviderBase CommandsProvider
   {
      get
      {
         if (_commandsProvider == null)
            _commandsProvider = Instantiate(_commandsProviderPrefab, transform);
         return _commandsProvider;
      }
   }

   protected override UserPlayerInfo GetLink()
   {
      return this;
   }
   
}
