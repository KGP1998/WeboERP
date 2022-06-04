using DairyWeb.Entity;
using DairyWeb.Models;
using System;
using System.Collections.Generic;

namespace DairyWeb.Repository
{
    public interface ITransactionRepository : IBaseRepository<TransactionMaster>
    {
        Tuple<bool, string> UpsertTransaction(TransactionMaster transaction);
        Tuple<bool, string> DeleteTransaction(int transactionId);
        IEnumerable<TransactionModel> GetTransaction(int retailerId, int customerId, DateTime? fromDate, DateTime? toDate);
        IEnumerable<TransactionReportModel> GetTransactionReport(int retailerId, int customerId, DateTime? fromDate, DateTime? toDate);
        TransactionModel GetTransactionById(int id);
        int GetOutStandingAmountForCustomer(int customerId);
    }
}
