using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Services;
using System;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Retailer)]
    public class ReportsController : Controller
    {
        private readonly IOrderService _orderService;
        public ReportsController()
        {
            _orderService = new OrderService();
        }

        // GET: Reports
        public ActionResult CustomerDetailsReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowCustomerDetailsReport(DateTime fromDate, DateTime toDate, int customerId)
        {
            try
            {
                //string[] FromDateSplit = fromDate.Split('/', '-', ' ');
                //string[] ToDateSplit = toDate.Split('/', '-', ' ');

                //DateTime FinalFromDate = new DateTime(Convert.ToInt16(FromDateSplit[2]), Convert.ToInt16(FromDateSplit[1]), Convert.ToInt16(FromDateSplit[0]));
                //DateTime FinalToDate = new DateTime(Convert.ToInt16(ToDateSplit[2]), Convert.ToInt16(ToDateSplit[1]), Convert.ToInt16(ToDateSplit[0]));

                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var ds = _orderService.CustomerDetailsReport(fromDate, toDate, customerId, RetailerId);
                var reportData = CommonOperations.DataSetToJSON(ds);
                return Json(new { Data = reportData, Success = true, Message = "Order fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new DataSet(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ExportCustomerDetailsReport(int customerId, DateTime fromDate, DateTime toDate)
        {
            //string[] FromDateSplit = fromDate.Split('/', '-', ' ');
            //string[] ToDateSplit = toDate.Split('/', '-', ' ');

            //DateTime FinalFromDate = new DateTime(Convert.ToInt16(FromDateSplit[2]), Convert.ToInt16(FromDateSplit[1]), Convert.ToInt16(FromDateSplit[0]));
            //DateTime FinalToDate = new DateTime(Convert.ToInt16(ToDateSplit[2]), Convert.ToInt16(ToDateSplit[1]), Convert.ToInt16(ToDateSplit[0]));

            var RetailerId = Convert.ToInt32(Session["RetailerId"]);
            var ds = _orderService.CustomerDetailsReport(fromDate, toDate, customerId, RetailerId);

            ReportDocument RD = new ReportDocument();
            RD.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesDetailsReport.rpt"));
            RD.SetDataSource(ds.Tables[0]);

            RD.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            RD.PrintOptions.ApplyPageMargins(new PageMargins(10, 10, 10, 10));
            RD.PrintOptions.PaperSize = PaperSize.PaperA4;

            Stream stream = RD.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "CustomerList.pdf");
        }
    }
}