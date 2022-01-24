using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shops/Upgrade")]
public class UpgradeHandler : ShopHandler
{
    [SerializeField]
    public List<PlayerUpgradeObject> upgrades;

    public override void HandleCommand(string command, PlayerMiningController player)
    {
        string[] parts = command.Split(" ");
        if(parts.Length <= 1) {
            PrintUsage();
            return;
        }
        switch(parts[1]) {
            case "list":
                if(parts.Length == 2)
                    PrintTypes();
                if(parts.Length == 3) 
                    if(Enum.TryParse<PlayerUpgradeType>(parts[2], true, out PlayerUpgradeType upgradeType)){
                        PrintUpgradesForType(upgradeType);
                    } else {
                        GameConsole.WriteLine("Unknown upgrade type");
                    }

                break;
            case "buy":
                if(parts.Length == 4) {
                    var equipment = player.GetComponent<PlayerEquipment>();
                    if(Enum.TryParse<PlayerUpgradeType>(parts[2], true, out PlayerUpgradeType upgradeType)) {
                        HandleBuyUpgrade(upgradeType, parts[3], equipment);
                    } else {
                        GameConsole.WriteLine("Unknown upgrade type");
                    }
                } else {
                    GameConsole.WriteLine("Please specify upgrade type and name");
                }
                break;
            default:
                PrintUsage();
                break;
        }
    }

    private void HandleBuyUpgrade(PlayerUpgradeType type, string upgradeName, PlayerEquipment equipment)
    {
        var upgrade = upgrades.Where(u => u.Type == type && u.Name == upgradeName).FirstOrDefault();
        if(upgrade == null) {
            GameConsole.WriteLine($"Upgrade {upgradeName} of type {type} is unknown");
            return;
        }

        upgrade.AttachTo(equipment);
    }

    private void PrintUpgradesForType(PlayerUpgradeType type)
    {
        foreach(var upgrade in upgrades.Where(u => u.Type == type)) {
            GameConsole.WriteLine($"{upgrade.Name} - ${upgrade.BasePrice}$");
        }
    }

    private void PrintTypes()
    {
        var types = upgrades.Select(u => u.Type).Distinct();

        foreach(var type in types) {
            GameConsole.WriteLine(Enum.GetName(typeof(PlayerUpgradeType), type));
        }
    }

    private void PrintUsage()
    {
        string usage = @"Usage:
        Command                         Description
        upgrade list                    Prints a list of available upgrade types
        upgrade list <type>             Prints a list of upgrades available for a type
        upgrade buy <type> <upgrade>    Purchases an upgrade of of a type
        ";
        GameConsole.WriteLine(usage);
    }

    public override void Init()
    {
    }
}
