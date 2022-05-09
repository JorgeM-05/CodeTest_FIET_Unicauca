using API.Seeker.Data;
using API.Seeker.Models;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeker.Services.Quieries 
{
    public  interface ICaRepositoryQuerySerivices
    {
        Task<DataCollection<CaRepositoryDto>> GetAllAsync(int page, int take, IEnumerable<int> Cas = null);
        Task<CaRepositoryDto> GetAsync(int id);
    }
    public class CaRepositoryQueryServices: ICaRepositoryQuerySerivices
    {
        private readonly APIContext _context;
        //readonly solo hace reeferencia a una funcionalidad de lectura
        public  CaRepositoryQueryServices (APIContext context)
        {
            _context = context;
        }
       
        public async Task<DataCollection<CaRepositoryDto>> GetAllAsync(int page, int take, IEnumerable<int> Cas = null)
        {
            var collection = await _context.CaRepository
                .Where(x => Cas == null || Cas.Contains((char)x.CAID))
                .OrderByDescending(x => x.CAID)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<CaRepositoryDto>>();
        }
        public async Task<CaRepositoryDto> GetAsync(int id)
        {
            return (await _context.CaRepository.SingleAsync(x => x.CAID == id)).MapTo<CaRepositoryDto>();
        }
    }
}
