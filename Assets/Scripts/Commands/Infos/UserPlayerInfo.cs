using System.Collections.Generic;
using UnityEngine;

public class UserPlayerInfo : SingletonDoL<UserPlayerInfo>
{
   [SerializeField] private CommandsProviderBase _playerCommandsProviderPrefab;
   private CommandsProviderBase _playerCommandsProvider;

   [SerializeField] private CommandsProviderBase _enemyCommandsProviderPrefab;
   private CommandsProviderBase _enemyCommandsProvider;
   
   private Player _userPlayer;
   private Player UserPlayer
   {
      get
      {
         if(_userPlayer == null)
            _userPlayer = new Player();
         return _userPlayer;
      }
      set => _userPlayer = value;
   }

   private Player _enemyPlayer;
   private Player EnemyPlayer
   {
      get
      {
         if(_enemyPlayer == null)
            _enemyPlayer = new Player();
         return _enemyPlayer;
      }
      set => _enemyPlayer = value;
   }


   public CommandsProviderBase PlayerCommandsProvider
   {
      get
      {
         if (_playerCommandsProvider == null)
         {
            _playerCommandsProvider = Instantiate(_playerCommandsProviderPrefab, transform);
            _playerCommandsProvider.Player = UserPlayer;
         }
         return _playerCommandsProvider;
      }
      set
      {
         if (_playerCommandsProvider != null)
         {
            Destroy(_playerCommandsProvider.gameObject);
            _playerCommandsProvider = null;
         }

         _playerCommandsProvider = SetCommandProviderByPlayer(value, UserPlayer);
      }
   }
   
   public CommandsProviderBase EnemyCommandsProvider
   {
      get
      {
         if (_enemyCommandsProvider == null)
         {
            _enemyCommandsProvider = Instantiate(_enemyCommandsProviderPrefab, transform);
            _enemyCommandsProvider.Player = EnemyPlayer;
         }
         return _enemyCommandsProvider;
      }

      set
      {
         if (_enemyCommandsProvider != null)
         {
            Destroy(_enemyCommandsProvider.gameObject);
            _enemyCommandsProvider = null;
         }

         _enemyCommandsProvider = SetCommandProviderByPlayer(value, EnemyPlayer);
      }
   }

   private CommandsProviderBase SetCommandProviderByPlayer(CommandsProviderBase source, Player player)
   {
      CommandsProviderBase clone = Instantiate(source, transform);
      clone.Player = player;
      return clone;
   }

   protected override UserPlayerInfo GetLink()
   {
      return this;
   }

}
