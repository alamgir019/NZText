// C#
//using MediatR;
//using NZ.HRM.Infrastructure.Persistence;

//public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand>
//{
//    private readonly ApplicationDbContext _context;
//    public DeleteMenuCommandHandler(ApplicationDbContext context) => _context = context;


//    public async Task<Unit> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
//    {
//        var menu = await _context.Menus.FindAsync(new object[] { request.Id }, cancellationToken);
//        if (menu == null) throw new KeyNotFoundException("Menu not found");

//        _context.Menus.Remove(menu);
//        await _context.SaveChangesAsync(cancellationToken);
//        return Unit.Value;
//    }
//}
