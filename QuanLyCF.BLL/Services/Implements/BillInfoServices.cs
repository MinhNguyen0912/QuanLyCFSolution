using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.BillInfo;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.BLL.Services.Implements
{
    public class BillInfoServices : IBillInfoServices
    {
        private readonly QuanLyCafeDBContext _context;
        private readonly IMapper _mapper;

        public BillInfoServices(IMapper mapper)
        {
            this._context = new QuanLyCafeDBContext();
            this._mapper = mapper;
        }
        public async Task<bool> Add(BillInfoVM request)
        {
            var obj = _context.BillInfos.FirstOrDefault(p => p.BillId == request.BillId && p.FoodId == request.FoodId);
            if (obj != null)
            {
                obj.Count += request.Count;
            }
            else
            {
                var billInfo = new BillInfo()
                {
                    BillInfoId = new Guid(),
                    FoodId = request.FoodId,
                    BillId = request.BillId,
                    Count = request.Count
                };
                await _context.BillInfos.AddAsync(billInfo);
            }
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Delete(Guid IdBillInfo)
        {
            var obj = await _context.BillInfos.FindAsync(IdBillInfo);
            obj.Count = 0;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BillInfoVM>> GetAllByIdBill(Guid IdBill)
        {
            var result = await _context.BillInfos
                .ProjectTo<BillInfoVM>(_mapper.ConfigurationProvider)
                .Where(p => p.BillId == IdBill)
                .ToListAsync();
            return result;
        }
        public Task<bool> UpdateCount(Guid IdBillInfo, int Count)
        {
            throw new NotImplementedException();
        }
    }
}
