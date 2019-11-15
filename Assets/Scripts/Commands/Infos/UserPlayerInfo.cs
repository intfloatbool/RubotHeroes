using System.Collections.Generic;
using UnityEngine;

public class UserPlayerInfo : SingletonDoL<UserPlayerInfo>
{
   [SerializeField] private PlayerOwner _globalPlayerUser = PlayerOwner.PLAYER_1;
   public PlayerOwner GlobalPlayerOwner => _globalPlayerUser;
   
   [SerializeField] private PlayerOwner _enemyPlayer = PlayerOwner.PLAYER_2;
   public PlayerOwner EnemyPlayerOwner => _enemyPlayer;

   [SerializeField] private List<PlayerInfoContainer> _playerInfos;
   private Dictionary<PlayerOwner, PlayerInfoContainer> _playerInfoDict = new Dictionary<PlayerOwner, PlayerInfoContainer>();

   protected override void Awake()
   {
      InitializeDict();
      base.Awake();
   }

   protected void Start()
   {
      InitializeGlobalUser();
      InitializeEnemyAsBotByDefault();
   }

   /// <summary>
   /// Default user (PLAYER) initialization
   /// </summary>
   private void InitializeGlobalUser()
   {
      InitializePlayerByDefault<RandomCommandsProvider>(_globalPlayerUser);
   }

   private void InitializeEnemyAsBotByDefault()
   {
      InitializePlayerByDefault<BotRandomCommandsProvider>(_enemyPlayer);
   }

   private void InitializePlayerByDefault<T>(PlayerOwner owner) where T : CommandsProviderBase
   {
      if (_playerInfoDict.ContainsKey(owner))
      {
         SetCommandProviderByOwner<T>(owner);
      }
      else
      {
         Debug.LogError($"fail to initialize default player ({owner.ToString()}) not exists!");
      }
   }

   private void InitializeDict()
   {
      foreach (PlayerInfoContainer playerInfo in _playerInfos)
      {
         if (!_playerInfoDict.ContainsKey(playerInfo.Owner))
         {
            _playerInfoDict.Add(playerInfo.Owner, playerInfo);
         }
         else
         {
            Debug.LogError($"Cannot add playerinfo with owner {playerInfo.Owner}, already exists!");
         }
      }
   }

   public PlayerInfoContainer GetGlobalUser()
   {
      return _playerInfoDict[_globalPlayerUser];
   }

   public PlayerInfoContainer GetEnemyUser()
   {
      return _playerInfoDict[_enemyPlayer];
   }
   
   public PlayerInfoContainer GetPlayerInfoByOwner(PlayerOwner owner)
   {
      if(_playerInfoDict.ContainsKey(owner))
         return _playerInfoDict[owner];
      return null;
   }

   public void SetCommandProviderByOwner<T>(PlayerOwner owner) where T : CommandsProviderBase
   {
      PlayerInfoContainer infoOwner = GetPlayerInfoByOwner(owner);
      if (infoOwner.CommandsProvider != null)
      {
         Destroy(infoOwner.CommandsProvider.gameObject);
         infoOwner.CommandsProvider = null;
      }
      CommandsProviderBase provider = CommandProviderCreator.Instance.GetProvider<T>();
      provider.transform.parent = this.transform;
      infoOwner.CommandsProvider = provider;
   }

   protected override UserPlayerInfo GetLink()
   {
      return this;
   }
}
