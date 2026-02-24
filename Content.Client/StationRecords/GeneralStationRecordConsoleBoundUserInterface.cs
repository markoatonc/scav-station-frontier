using Content.Shared.StationRecords;
using Robust.Client.UserInterface;
using Content.Shared._NF.StationRecords; // Frontier
using Content.Shared.Roles; // Frontier
using Robust.Shared.Prototypes; // Frontier
using Content.Shared.StationRecords.Components; //scav

namespace Content.Client.StationRecords;

public sealed class GeneralStationRecordConsoleBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private GeneralStationRecordConsoleWindow? _window = default!;
    public readonly StationRecordsFilter? Filter;
    private StationRecordFilterType _currentFilterType;
    public GeneralStationRecordConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<GeneralStationRecordConsoleWindow>();
        _window.OnKeySelected += key =>
            SendMessage(new SelectStationRecord(key));
        _window.OnFiltersChanged += (type, filterValue) =>
            SendMessage(new SetStationRecordFilter(type, filterValue));
        _window.OnJobAdd += OnJobsAdd; // Frontier: job modification buttons
        _window.OnJobSubtract += OnJobsSubtract; // Frontier: job modification buttons
        _window.OnDeleted += id => SendMessage(new DeleteStationRecord(id));
        _window.OnAdvertisementChanged += OnAdvertisementChanged; // Frontier: job modification buttons
        _window.OpenJobsButtonPressed += OnOpenJobsButtonPressed;
        _window.CloseJobsButtonPressed += OnCloseJobsButtonPressed;
    }


    private void OpenJobsButtonPressed(ProtoId<JobPrototype> job)
    {
        SendMessage(new SetStationJobMsg(job, -1));
    }
    private void CloseJobsButtonPressed(ProtoId<JobPrototype> job)
    {
        SendMessage(new SetStationJobMsg(job, 0));
    }
    private void OnOpenJobsButtonPressed(IReadOnlyDictionary<ProtoId<JobPrototype>, int?> jobList)
    {
        foreach (var job in jobList)
        {
            SendMessage(new SetStationJobMsg(job.Key, -1));
        }
    }

    private void OnCloseJobsButtonPressed(IReadOnlyDictionary<ProtoId<JobPrototype>, int?> jobList)
    {
        foreach (var job in jobList)
        {

            SendMessage(new SetStationJobMsg(job.Value.ID, 0));
        }
    }



    // Frontier: job modification buttons, ship advertisements
    private void OnJobsAdd(ProtoId<JobPrototype> job)
    {
        SendMessage(new AdjustStationJobMsg(job, 1));
    }
    private void OnJobsUnset(ProtoId<JobPrototype> job)
    {
        SendMessage(new AdjustStationJobMsg(job, 0));
    }
    private void OnJobsSet(ProtoId<JobPrototype> job)
    {
        SendMessage(new AdjustStationJobMsg(job, -1));
    }
    private void OnJobsSubtract(ProtoId<JobPrototype> job)
    {
        SendMessage(new AdjustStationJobMsg(job, -1));
    }
    private void OnAdvertisementChanged(string text)
    {
        SendMessage(new SetStationAdvertisementMsg(text));
    }
    // End Frontier
    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is not GeneralStationRecordConsoleState cast)
            return;
        _window.SetJobStatus(_window.HiringStatus);
        _window?.UpdateState(cast);
    }


}
