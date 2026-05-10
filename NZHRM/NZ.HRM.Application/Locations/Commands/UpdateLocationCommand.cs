namespace NZ.HRM.Application.Locations.Commands
{
    public record UpdateLocationCommand(string Id, string LocationName, string DistrictId, string UpdatedBy);
}