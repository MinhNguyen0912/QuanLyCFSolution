using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.BLL.ViewModels.Bill;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;
using QuanLyCF.DAL.Enums;

namespace QuanLyCF.BLL.Services.Implements
{
    public class BillServices : IBillServices
    {
        private readonly QuanLyCafeDBContext _context;
        private readonly IMapper _mapper;

        public BillServices(IMapper mapper)
        {
            this._context = new QuanLyCafeDBContext();
            this._mapper = mapper;
        }
        public async Task<Guid> Add(AddBillVM request)
        {
            var bill = new Bill()
            {
                IdBill = new Guid(),
                DateCheckIn = DateTime.Now,
                Status = BillStatus.NotDone,
                Discount = 0,
                IdTable = request.IdTable,
                Note = request.Note,
                CreateBy = request.CreateBy
            };
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
            return bill.IdBill;
        }
        public async Task<bool> AddFood(Guid IdBill,Guid IdFood, int count)
        {
            var listBillInfo = await _context.BillInfos.Where(p => p.BillId == IdBill).ToListAsync();
            var foodExist = listBillInfo.FirstOrDefault(p => p.FoodId == IdFood);
            if (foodExist!=null)
            {
                listBillInfo.FirstOrDefault(p => p.FoodId == foodExist.FoodId).Count += count;
            }
            else
            {
                var billInfo = new BillInfo()
                {
                    BillInfoId = new Guid(),
                    BillId = IdBill,
                    FoodId = IdFood,
                    Count = count
                };
                await _context.BillInfos.AddAsync(billInfo);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<BillVM>> GetAll()
        {
            return await _context.Bills
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BillVM>> GetAllDone()
        {
            return await _context.Bills
                .Where(p => p.Status == BillStatus.Done)
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BillVM>> GetAllDoneInRangeTime(DateTime dateStart, DateTime dateEnd)
        {
            return await _context.Bills
                .Where(p => p.Status == BillStatus.Done && p.DateCheckIn >= dateStart && p.DateCheckIn <= dateEnd)
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BillVM>> GetAllInRangeTime(string dateStart, string dateEnd)
        {
            var DateStart = DateTime.Parse(dateStart);
            var DateEnd = DateTime.Parse(dateEnd);
            return await _context.Bills
                .Where(p => p.DateCheckIn >= DateStart && p.DateCheckIn <= DateEnd)
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BillVM>> GetAllNotDone()
        {
            return await _context.Bills
                .Where(p => p.Status == BillStatus.NotDone)
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BillVM>> GetAllNotDoneInRangeTime(DateTime dateStart, DateTime dateEnd)
        {
            return await _context.Bills
                .Where(p => p.Status == BillStatus.NotDone && p.DateCheckIn >= dateStart && p.DateCheckIn <= dateEnd)
                .ProjectTo<BillVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BillVM> GetById(Guid IdBill)
        {
            var obj = await _context.Bills.FindAsync(IdBill);
            return _mapper.Map<BillVM>(obj);
        }
        public async Task<BillVM> GetByTableId(Guid IdTable)
        {
            var obj = await _context.Bills.FirstOrDefaultAsync(p=>p.IdTable==IdTable&&p.Status==BillStatus.NotDone);
            if (obj==null)
            {
                return null; 
            }
            return _mapper.Map<BillVM>(obj);
        }

        public async Task<double> GetTotalPrice(Guid IdBill)
        {
            double totalPrice = 0;
            var bill = await _context.Bills.FindAsync(IdBill);
            var listBillInfos = await _context.BillInfos.Where(p => p.BillId == IdBill).ToListAsync();
            foreach (var item in listBillInfos)
            {
                var food = await _context.Foods.FirstOrDefaultAsync(p => p.FoodId == item.FoodId);
                var priceFood = food.Price;
                totalPrice += priceFood * Convert.ToDouble(item.Count) * (100 - bill.Discount) / 100;
            }
            return totalPrice;
        }

        public async Task<bool> Pay(Guid IdBill, Guid SolveBy, int discount)
        {
            var bill = await _context.Bills.FindAsync(IdBill);
            var currentTable = await _context.Tables.FindAsync(bill.IdTable);
            bill.Status = BillStatus.Done;
            bill.SolveBy = SolveBy;
            bill.Discount = discount;
            bill.DateCheckOut = DateTime.Now;
            currentTable.Status = TableStatus.Available;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Guid IdBill, UpdateBillVM request)
        {
            var bill = await _context.Bills.FindAsync(IdBill);
            bill.Status = request.Status;
            bill.Discount = request.Discount;
            bill.IdTable = request.IdTable;
            bill.Note = request.Note;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNote(Guid IdBill,string note)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(p=>p.IdBill==IdBill);
            bill.Note = note;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
