// C#
using MediatR;
using NZ.HRM.Domain.Entities;

public class MenuQueryHandler //: IRequestHandler<GetAllMenusQuery, List<Menu>>
{

    private readonly IMenuRepository _repo;
    public MenuQueryHandler(IMenuRepository repo) => _repo = repo;


    //public async Task<Menu?> Handle(GetMenuByIdQuery request)
    //{
    //    return await _repo.FindByIdAsync(request.Id);
    //}

    //public async Task<List<Menu>> Handle(GetAllMenusQuery request)
    //{
    //    return await _repo.GetAllAsync();
    //}
}
