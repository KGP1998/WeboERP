using DairyWeb.Entity;
using DairyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyWeb.Repository
{
    public interface IOrderRepository : IBaseRepository<OrderMaster>
    {
        IEnumerable<OrderModels> GetOrderData(int retailerId, DateTime? fromDate, DateTime? toDate);

        Tuple<bool, string> UpsertOrder(OrderMaster orderMaster);
        Tuple<bool, string> DeleteOrder(int orderId);
        Dictionary<string, decimal?> ChangeCustomerDetails(int customerId, int shift);
        List<OrderModels> GetDailyOrdersData(int retailerId, int routeId, int shift,DateTime saleDate);
        DataSet CustomerDetailsReport(DateTime fromDate, DateTime toDate, int customerId, int retailerId);
        OrderMaster GetOrderById(int id);
    }
}
