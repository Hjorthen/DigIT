using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RefuelingTests
{
    private RefuelingHandler refuelCommand;
    private ConsumableStat fuel;
    private ConsumableStat balance;
    private float unitPrice;

    [SetUp]
    public void Setup() {
        refuelCommand = new RefuelingHandler();
        unitPrice = 1;
        fuel = new ConsumableStat();
        balance = new ConsumableStat();
    }

    [Test]
    public void Refueling_WithSufficientCash_RefuelsToCapacity() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        balance.Currentvalue = unitPrice * fuel.MaxValue;

        refuelCommand.Refuel(fuel, balance, unitPrice);

        Assert.AreEqual(0, balance.Currentvalue);
        Assert.AreEqual(fuel.MaxValue, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithSufficientCash_OnlyChargesForRefueledAmount(){
        fuel.Currentvalue = 5;
        fuel.MaxValue = 10;
        float initialBalance = unitPrice * fuel.MaxValue;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance, unitPrice);

        Assert.AreEqual(initialBalance * 0.5, balance.Currentvalue);
        Assert.AreEqual(fuel.MaxValue, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithInsufficientCash_OnlyRefuelsForAffordedAmount() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        float affordableFuelUnits = 2;
        float initialBalance = unitPrice * affordableFuelUnits;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance, unitPrice);

        Assert.AreEqual(0, balance.Currentvalue);
        Assert.AreEqual(affordableFuelUnits, fuel.Currentvalue);
    }

    [Test]
    public void Refueling_WithNoCash_RefuelsNothing() {
        fuel.Currentvalue = 0;
        fuel.MaxValue = 10;
        unitPrice = 15;
        float initialBalance = 10;
        balance.Currentvalue = initialBalance;

        refuelCommand.Refuel(fuel, balance, unitPrice);

        Assert.AreEqual(0, fuel.Currentvalue);
        Assert.AreEqual(initialBalance, balance.Currentvalue);
    }

}
