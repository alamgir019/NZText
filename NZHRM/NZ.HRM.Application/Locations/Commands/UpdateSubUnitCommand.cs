namespace NZ.HRM.Application.Locations.Commands
{
    public record UpdateSubUnitCommand(string Id, string SubUnitName, string UnitId, string UpdatedBy);
}