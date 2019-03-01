using TuanCinema.Data.Infrastructure;
using TuanCinema.Data.Repositories;
using TuanCinema.Entities;
using TuanCinema.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuanCinema.Data.Extensions;
using TuanCinema.Web.Models;
using AutoMapper;

namespace TuanCinema.Web.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/stocks")]
    public class StocksController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Stock> _stocksRepository;
        public StocksController(IEntityBaseRepository<Stock> stocksRepository, 
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _stocksRepository = stocksRepository;
        }

        [Route("movie/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            IEnumerable<Stock> stocks = null;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                
                stocks = _stocksRepository.GetAvailableItems(id);

                IEnumerable<StockViewModel> stocksVM = Mapper.Map<IEnumerable<Stock>, IEnumerable<StockViewModel>>(stocks);

                response = request.CreateResponse<IEnumerable<StockViewModel>>(HttpStatusCode.OK, stocksVM);

                return response;
            });
        }
    }
}
