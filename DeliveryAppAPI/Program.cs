
using DeliveryAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MailService;
using Printer_Service;
using Printer_Service.Data;
using Printer_Service.Models;
using PdfGenerator;
using PdfGenerator.Models;
using DeliveryAppAPI.Methods;
using Grpc.Core;
using MailContoller = DeliveryAppAPI.Methods.MailContoller;
using System.Drawing.Printing;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DeliveryAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("api/createdeliverynote", async ([FromBody] FormFields formFields) =>
            {
                PdfModel model = new PdfModel()
                {
                    ClientName = formFields.ClientName,
                    ClientAddress = formFields.ClientAddress,
                    ClientContactDetails = formFields.ClientContactDetails,
                    Location = formFields.Location,
                    ReferenceNumber = new Guid(),
                    ListOfItems =
                    {
                        new PdfGenerator.Models.PdfTable()
                        {
                            ItemName = formFields.ListOfItems[0].ItemName,
                            ItemDescription = formFields.ListOfItems[0].ItemDescription,
                            Qty = formFields.ListOfItems[0].Qty
                        }
                    }
                };

                List<Models.PdfTable> listOfProducts = new();

                foreach (var item in formFields.ListOfItems)
                {
                    var product = new Models.PdfTable
                    {
                        ItemName = item.ItemName,
                        ItemDescription = item.ItemDescription,
                        Qty = item.Qty
                    };

                    listOfProducts.Add(product);
                }

                var numberOfProducts = await ProductCount.GetProductCount(listOfProducts);

                if (numberOfProducts < 1)
                {
                    return Results.BadRequest("You do not have any products listed!");
                }
                Methods.PdfController pdfController = new();
                var pdfDocumentDir = pdfController.GeneratePdf(model.ClientName, model.ClientAddress, model.ClientContactDetails, model.Location, numberOfProducts, listOfProducts);

                if (string.IsNullOrEmpty(pdfDocumentDir))
                {
                    return Results.BadRequest();
                }
                else
                {
                    var listOfReceipients = await GettingEmailAddress.GetEmailReceipients(formFields.EmailAddresses);
                    var numberOfEmailReceipients = listOfReceipients.Count();

                    if (listOfReceipients.Count() < 1)
                    {
                        return Results.BadRequest("List Of Email Receipients is empty.");
                    }

                    bool hasError;
                    MailContoller.SendEmail(pdfDocumentDir, listOfReceipients, formFields.UserEmail, formFields.UserPassword, formFields.ClientName, formFields.StartTime, formFields.EndTime, out hasError);

                    if (!hasError)
                    {
                        return Results.BadRequest();
                    }
                    else
                    {
                        Methods.PrinterController.PrintLabel(formFields.NumberOfLabel);
                    }
                }

                return Results.Ok(model);
            });

            app.Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Delivery Note Application",
                    Description = "d.Code Support Delivery Note Application Hosted via IIS",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "d.Code Support Team",
                        Email = "jonathan@dcode.mobi",
                        Url = new Uri(""),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

    }
}