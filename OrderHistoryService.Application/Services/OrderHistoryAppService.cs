using Delivery.BaseLib.Infrastructure.Transactions;
using OrderHistoryService.Application.Dtos.Responses;
using OrderHistoryService.Application.MapperProfiles;
using OrderHistoryService.Application.Services.Interfaces;
using OrderHistoryService.Domain.Exceptions;
using OrderHistoryService.Domain.Repositories;

namespace OrderHistoryService.Application.Services
{
    public class OrderHistoryAppService : IOrderHistoryAppService
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderHistoryAppService(IOrderHistoryRepository orderHistoryRepository, IUnitOfWork unitOfWork)
        {
            _orderHistoryRepository = orderHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public OrderHistoryResponse GetById(long id)
        {
            var orderHistory = _orderHistoryRepository.Find(id);

            if (orderHistory is null)
                throw new OrderHistoryNotFoundException();

            return orderHistory.OrderHistoryToOrderHistoryResponse();
        }

        public OrderHistoryResponse GetByOrderId(long orderId)
        {
            var orderHistory = _orderHistoryRepository.Find(x => x.OrderId == orderId);

            if (orderHistory is null)
                throw new OrderHistoryNotFoundException();

            return orderHistory.OrderHistoryToOrderHistoryResponse();
        }

        public IEnumerable<OrderHistoryResponse> GetByCustomerId(long customerId)
        {
            var orders = _orderHistoryRepository.Query()
                .Where(x => x.CustomerId == customerId)
                .ToList();

            return orders.OrderHistoriesToOrderHistoryResponses();
        }
    }
}