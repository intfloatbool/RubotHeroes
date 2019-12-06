using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ExampleRobot : Robot
{
    private PlayerInfoContainer _playerInfoContainer;
    private Dictionary<CommandType, ICommand> _commandsForDummyDict = new Dictionary<CommandType, ICommand>();
    protected override void Awake()
    {
        base.Awake();
        this._playerInfoContainer = UserPlayerInfo.Instance.GetGlobalUser();
    }

    private void Start()
    {
        UpdatePropertiesByGlobalPlayer();
    }
    

    public void UpdatePropertiesByGlobalPlayer()
    {
        Player player = this._playerInfoContainer.Player;
        Initialize(player);
        InitializeWeaponsOfPlayer();
    }

    private void InitializeWeaponsOfPlayer()
    {
        PlayerProperties playerProperties = _playerInfoContainer.Player.Properties;
        SetWeapons(new[]
        {
            playerProperties.GetWeaponByType(WeaponType.ROCKET_LAUNCHER),
            playerProperties.GetWeaponByType(WeaponType.FIREGUN),
            playerProperties.GetWeaponByType(WeaponType.LANDMINE)
        });
    }

    private ICommand CorrectCommandByDummyState(ICommand command)
    {
        if (_commandsForDummyDict.ContainsKey(command.CommandType))
        {
            
        }

        return command;
    }
    
    protected override void FixedUpdate()
    {
        //its just a example bot without update logic   
    }
}
