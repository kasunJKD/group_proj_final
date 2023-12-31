using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Reportcontroller : Controller
    {
        private readonly WebApplicationDbContext _context;

        public Reportcontroller(WebApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            IEnumerable<Order> objOrderList = _context.Order;
            return View(objOrderList);
        }
        public IActionResult GeneratePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(6);


                PdfPCell cell1 = new PdfPCell(new Phrase("UserId"));
                PdfPCell cell2 = new PdfPCell(new Phrase("OrderedModel"));
                PdfPCell cell3 = new PdfPCell(new Phrase("Customization_Price"));
                PdfPCell cell4 = new PdfPCell(new Phrase("Total_Price"));
                PdfPCell cell5 = new PdfPCell(new Phrase("CreatedDateTime"));
                PdfPCell cell6 = new PdfPCell(new Phrase("Status"));
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);

                //var bids = _bidInterface.GetAllBidsList().ToList();
                //var totalBoughtPlayersPrice = 0;
                var orders = _context.Order.ToList(); // Make sure you adjust this line to match how you retrieve orders

                foreach (var item in orders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.Id.ToString()));
                    PdfPCell celll2 = new PdfPCell(new Phrase(item.OrderedModel.ToString()));
                    PdfPCell celll3 = new PdfPCell(new Phrase(item.Customization_Price.ToString()));
                    PdfPCell celll4 = new PdfPCell(new Phrase(item.Total_Price.ToString()));
                    PdfPCell celll5 = new PdfPCell(new Phrase(item.CreatedDateTime.ToString()));
                    PdfPCell celll6 = new PdfPCell(new Phrase(item.Status.ToString()));

                    table.AddCell(cell);
                    table.AddCell(celll2);
                    table.AddCell(celll3);
                    table.AddCell(celll4);
                    table.AddCell(celll5);
                    table.AddCell(celll6);
                }






                document.Add(table);
                document.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "playerData.pdf");

            }
        }

        [HttpPost]
        public IActionResult GeneratePDFPlayer()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(6);


                PdfPCell cell1 = new PdfPCell(new Phrase("UserId"));
                PdfPCell cell2 = new PdfPCell(new Phrase("OrderedModel"));
                PdfPCell cell3 = new PdfPCell(new Phrase("Customization_Price"));
                PdfPCell cell4 = new PdfPCell(new Phrase("Total_Price"));
                PdfPCell cell5 = new PdfPCell(new Phrase("CreatedDateTime"));
                PdfPCell cell6 = new PdfPCell(new Phrase("Status"));
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);

                var orders = _context.Order.ToList(); // Make sure you adjust this line to match how you retrieve orders



                foreach (var item in orders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.Id.ToString()));
                    PdfPCell celll2 = new PdfPCell(new Phrase(item.OrderedModel.ToString()));
                    PdfPCell celll3 = new PdfPCell(new Phrase(item.Customization_Price.ToString()));
                    PdfPCell celll4 = new PdfPCell(new Phrase(item.Total_Price.ToString()));
                    PdfPCell celll5 = new PdfPCell(new Phrase(item.CreatedDateTime.ToString()));
                    PdfPCell celll6 = new PdfPCell(new Phrase(item.Status.ToString()));

                    table.AddCell(cell);
                    table.AddCell(celll2);
                    table.AddCell(celll3);
                    table.AddCell(celll4);
                    table.AddCell(celll5);
                    table.AddCell(celll6);
                }


                document.Add(table);
                document.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "players.pdf");

            }
        }

    }
}

