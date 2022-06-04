using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Retailer)]
    public class TransactionController : Controller
    {
        private ITransactionService _transactionService;
        private ICustomerService _customerService;

        public TransactionController()
        {
            _customerService = new CustomerService();
            _transactionService = new TransactionService();
        }

        public ActionResult Transaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTransactionData(int customerId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var transactions = _transactionService.GetTransaction(RetailerId, customerId, fromDate, toDate).ToList();
                return Json(new { Data = transactions, Success = true, Message = "Transaction fetched successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<TransactionModel>(), Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var customers = _customerService.GetCustomerData(RetailerId);
                return Json(new { customers = customers, Success = true, Message = "Filters data fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpsertTransaction(int? id)
        {
            ViewBag.TransactionId = id;
            return View();
        }

        [HttpGet]
        public ActionResult GetTransactionData(int? id)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var customers = _customerService.GetCustomerData(RetailerId);
                TransactionModel transactionModel = new TransactionModel();
                if (id != null && id > 0)
                {
                    transactionModel = _transactionService.GetTransactionById(Convert.ToInt32(id));
                }
                return Json(new { customers = customers, transaction = transactionModel, Success = true, Message = "Filters data fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new TransactionModel(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpsertTransaction(TransactionModel transactionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tuple = _transactionService.UpsertTransaction(transactionModel);

                    try
                    {
                        CustomerModel customerModel = _customerService.GetCustomerDataById(transactionModel.CustomerId);

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Apex Computer ");
                        sb.AppendLine($"Date: {transactionModel.TransactionDate}");
                        sb.AppendLine($"{customerModel.FirstName + " " + customerModel.LastName} ");
                        sb.AppendLine($"{transactionModel.PaymentTypeText} ");
                        sb.AppendLine($"Amount:{transactionModel.Amount.ToString("0.00")} ");
                        sb.Append($"(Apex)");

                        CommonOperations.SendSms(sb.ToString(), customerModel.PhoneNo);
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                    }

                    return Json(new { Success = tuple.Item1, Message = tuple.Item2 });
                }
                else
                {
                    return Json(new { Success = false, Message = "Please enter required fields." });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        [HttpPost]
        public ActionResult DeleteTransaction(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction(nameof(Transaction));

                var tuple = _transactionService.DeleteTransaction(id);
                return RedirectToAction(nameof(Transaction));
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult TransactionReport()
        {
            return View();
        }

        public ActionResult GetTransactionReport(int customerId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var transactions = _transactionService.GetTransactionReport(RetailerId, customerId, fromDate, toDate).ToList();
                return Json(new { Data = transactions, Success = true, Message = "Transaction fetched successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<TransactionModel>(), Success = false, Message = ex.Message });
            }
        }

        public ActionResult GetOutStandingAmountForCustomer(int customerId)
        {
            try
            {
                var OutStandingAmount = _transactionService.GetOutStandingAmountForCustomer(customerId);
                return Json(new { Data = OutStandingAmount, Success = true, Message = "OutStanding Amount For Customer fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<TransactionModel>(), Success = false, Message = ex.Message });
            }
        }
    }
}