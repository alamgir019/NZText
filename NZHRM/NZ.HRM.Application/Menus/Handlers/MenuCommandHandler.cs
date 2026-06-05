// C#
using MediatR;
using NZ.HRM.Domain.Entities;

public class MenuCommandHandler //: IRequestHandler<CreateMenuCommand, string>
{

    private readonly IMenuRepository _repo;
    public MenuCommandHandler(IMenuRepository repo) => _repo = repo;

    public async Task<string> Handle(CreateMenuCommand request)
    {
        //var menu = new Menu
        //{
        //    Name = request.Name,
        //    ParentId = request.ParentId,
        //    Url = request.Url,
        //    Icon = request.Icon,
        //    SortOrder = request.Order,
        //    IsActive = true
        //};
        //await _repo.AddAsync(menu);
        //await _repo.SaveChangesAsync();
        //return menu.Id;
        return string.Empty;
    }

    public async Task<Unit> Handle(DeleteMenuCommand request)
    {
        //var menu = await _repo.FindByIdAsync(request.Id);
        //if (menu == null) throw new KeyNotFoundException("Menu not found");

        //await _repo.RemoveAsync(menu);
        //await _repo.SaveChangesAsync();
        return Unit.Value;
    }


    public async Task<Unit> Handle(UpdateMenuCommand request)
    {
        //var menu = await _repo.FindByIdAsync(request.Id);
        //if (menu == null) throw new KeyNotFoundException("Menu not found");

        //menu.Name = request.Name;
        //menu.ParentId = request.ParentId;
        //menu.Url = request.Url;
        //menu.Icon = request.Icon;
        //menu.SortOrder = request.Order;

        //await _repo.SaveChangesAsync();
        return Unit.Value;
    }
}
