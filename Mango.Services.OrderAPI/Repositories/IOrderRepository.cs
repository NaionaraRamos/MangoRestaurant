using Mango.Services.OrderAPI.Models;
using System.Threading.Tasks;

namespace Mango.Services.OrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
