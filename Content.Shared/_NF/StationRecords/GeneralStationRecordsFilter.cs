using Robust.Shared.Serialization;

namespace Content.Shared._NF.StationRecords;

[Serializable, NetSerializable]
public sealed class AdjustStationJobMsg : BoundUserInterfaceMessage
{
    public string JobProto { get; }
    public int Amount { get; }

    public AdjustStationJobMsg(string jobProto, int amount)
    {
        JobProto = jobProto;
        Amount = amount;
    }
}
//scav
public sealed class SetStationJobMsg : BoundUserInterfaceMessage
{
    public string JobProto { get; }
    public int Amount { get; }

<<<<<<< Updated upstream

[Serializable, NetSerializable]
public sealed class SetStationJobMsg : BoundUserInterfaceMessage
{
    public string JobProto { get; }
    public int Amount { get; }

=======
>>>>>>> Stashed changes
    public SetStationJobMsg(string jobProto, int amount)
    {
        JobProto = jobProto;
        Amount = amount;
    }
}
<<<<<<< Updated upstream

=======
//endscav
>>>>>>> Stashed changes
[Serializable, NetSerializable]
public sealed class SetStationAdvertisementMsg : BoundUserInterfaceMessage
{
    public string Advertisement { get; }

    public SetStationAdvertisementMsg(string advertisement)
    {
        Advertisement = advertisement;
    }
}
