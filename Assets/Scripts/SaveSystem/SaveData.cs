using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SaveData 
{
    //Inventory
    public InventorySaveData playerInventory;
    public InventorySaveData playerEquipment;
    public InventorySaveData playerAutoSell;
    public InventorySaveData playerAutoUse;

    //Fighter stats
    public float playerBaseMaxHealth;
    public float playerHealth;
    public float playerBaseAttackSpeed;
    public float playerBaseAttackDamage;
    public float playerBaseArmor;
    public float playerBaseRegenAmount;

    public int playerLevel;
    public float playerExperience;
    public float playerBaseExperienceToNextLevel;

    //Builder stats
    public int buiderLevel;
    public float builderCurrentExp;
    public float builderExpToNextLevel;

    //Trader stats
    public int traderSellCount; 
    public int traderSellBonusCount;
    public float traderSellBonusGold;
    public int traderLevel;
    public float traderExperience;
    public float traderBaseExperienceToNextLevel;

    //Farmer stats
    public int farmerExchangeAmount;
    public float farmerExchangeBonusRate;
    public int farmerUseAmount;

    public int farmerLevel;
    public float farmerExperience;
    public float farmerBaseExperienceToNextLevel;

    //Miner stats
    public float MiningSpeed;
    public int MiningQuality;
    public float MiningEnergyPerSecond;

    public int minerLevel;
    public float minerExperience;
    public float minerBaseExperienceToNextLevel;

    //Lummber stats
    public float ChoppingSpeed;
    public int ChoppingQuality;
    public int ChoppingEnergyPerSecond;

    public int lummberLevel;
    public float lummerExperience;
    public float lummerBaseExperienceToNextLevel;

    //Blacksmith stats
    public int blacksmithLevel;
    public float blacksmithCurrentExp;
    public float blacksmithExpToNextLevel;

    //Houses level
    public int MinerHouseLevel;
    public int LummerHouseLevel;
    public int FarmerHouseLevel;
    public int BlacksmithHouseLevel;
    public int TraderHouseLevel;
    public int BuilderHouseLevel;


    //Global resources
    public int gold;
    public int ore;
    public int wood;
    public int exchangeableEnergy;
    public int usableEnergy;

    public int maxOre;
    public int maxWood;
    public int maxExchangeableEnergy;
    public int maxUsableEnergy;
    public float elapsedTime;

    //EnemySpawner
    public int spawnerEnemyLevel;
    public int spawnerEnemiesKilledToLevel;
    public float eliteSpawnRate;
    public float bossSpawnRate; 
    public int spawnerCurrentSpawnIndex;
    public bool spawnerStopSpawning;

    public SaveData()
    {
        playerInventory = new InventorySaveData();
        playerEquipment = new InventorySaveData();
        playerAutoSell = new InventorySaveData();
        playerAutoUse = new InventorySaveData();
    }
}
