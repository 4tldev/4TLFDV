using UnityEngine;
using System.Collections.Generic;

public class Farmhand : Worker
{
    [Header("Farmhand Flags")]
    public bool allowPlant = true;
    public bool allowWater = true;
    public bool allowHarvest = true;

    [Header("Task Timings (seconds)")]
    [SerializeField] public float actionTimeFillWater = 1f;
    [SerializeField] public float actionTimeWaterCrop = 1f;
    [SerializeField] public float actionTimeGrabSeed = 1f;
    [SerializeField] public float actionTimePlantSeed = 1f;

    public const int MaxEnergy = 100;
    public const int EnergyCostPerFarmingAction = 5;

    // Capability control
    public bool CanPlant { get; set; } = false;
    public bool CanWater { get; set; } = false;
    public bool CanHarvest { get; set; } = false;

    // Target tile currently assigned

    // public FarmTile CurrentTargetTile { get; set; }

    // Static tracking for conflict avoidance
    public static List<Farmhand> ActiveFarmhands = new List<Farmhand>();

    // HandState
    public HANDSTATE CurrentHandState { get; private set; } = HANDSTATE.EMPTY;

    public void SetHand(HANDSTATE hand)
    {
        CurrentHandState = hand;
    }

    public void ClearHand()
    {
        CurrentHandState = HANDSTATE.EMPTY;
    }

    private void OnEnable()
    {
        ActiveFarmhands.Add(this);
    }

    private void OnDisable()
    {
        ActiveFarmhands.Remove(this);
    }

    public override void ReduceEnergy(int amount)
    {
        base.ReduceEnergy(amount);

        if (Energy < EnergyCostPerFarmingAction)
        {
            ReturnToHome(); // Start rest cycle
        }
    }

    public override void RestoreEnergy()
    {
        base.RestoreEnergy();

        if (CanPlant)
            SetState(new State_Worker_Plant(this));
        else if (CanWater)
            SetState(new State_Worker_Water(this));
        else if (CanHarvest)
            SetState(new State_Worker_Harvest(this));
        else
            SetState(new State_Worker_Idle(this));
    }
}
