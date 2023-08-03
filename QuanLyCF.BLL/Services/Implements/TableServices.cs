using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Table;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Enums;
using Table = QuanLyCF.DAL.Entities.Table;

namespace QuanLyCF.BLL.Services.Implements
{
    public class TableServices : ITableServices
    {
        private readonly QuanLyCafeDBContext _context;
        private readonly IMapper _mapper;

        public TableServices(IMapper mapper)
        {
            this._context = new QuanLyCafeDBContext();
            this._mapper = mapper;
        }
        public async Task<bool> Add(TableVM request)
        {
            var obj = new Table()
            {
                IdTable = new Guid(),
                Name = request.Name,
                Status = TableStatus.Available
            };
            await _context.Tables.AddAsync(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid TableId)
        {
            var obj = await _context.Tables.FindAsync(TableId);
            obj.Status = TableStatus.NotServe;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TableVM>> GetAll()
        {
            var list = await _context.Tables
                .OrderBy(p=>p.Name.Length).ThenBy(p=>p.Name)
                .ProjectTo<TableVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }

        public async Task<TableVM> GetById(Guid TableId)
        {
            var table = await _context.Tables.FindAsync(TableId);
            return _mapper.Map<TableVM>(table);
        }

        public async Task<bool> Update(Guid TableId, TableVM request)
        {
            var table = await _context.Tables.FindAsync(TableId);
            table.Name = request.Name;
            table.Status = request.Status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
