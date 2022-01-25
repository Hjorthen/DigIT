using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RefuelingTests
{
    private RefuelCommand refuelCommand;
    private ConsumableStat fuel;
    private ConsumableStat balance;

    [SetUp]
    public void Setup() {
        refuelCommand = new RefuelCommand();
        refuelCommand.UnitPrice = 1;
        fuel = new ConsumableStat();
        balance = new ConsumableStat();
    }

    [Test]
    public void Refueling_WithSufficientCash_RefuelsToCapacity() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        balance.Currentvalue = refuelCommand.UnitPrice * fuel.MaxValue;

        refuelCommand.Refuel(fuel, balance);

        Assert.AreEqual(0, balance.Currentvalue);
        Assert.AreEqual(fuel.MaxValue, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithSufficientCash_OnlyChargesForRefueledAmount(){
        fuel.Currentvalue = 5;
        fuel.MaxValue = 10;
        float initialBalance = refuelCommand.UnitPrice * fuel.MaxValue;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance);

        Assert.AreEqual(initialBalance * 0.5, balance.Currentvalue);
        Assert.AreEqual(fuel.MaxValue, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithInsufficientCash_OnlyRefuelsForAffordedAmount() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        float affordableFuelUnits = 2;
        float initialBalance = refuelCommand.UnitPrice * affordableFuelUnits;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance);

        Assert.AreEqual(0, balance.Currentvalue);
        Assert.AreEqual(affordableFuelUnits, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithNoCash_RefuelsNothing() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        refuelCommand.UnitPrice = 15;
        float initialBalance = 10;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance);

        Assert.AreEqual(0, fuel.Currentvalue);
        Assert.AreEqual(initialBalance, balance.Currentvalue);
    }

}
