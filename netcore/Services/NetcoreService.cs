﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using netcore.Data;
using netcore.Models;
using netcore.Models.Crm;
using netcore.Models.Invent;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace netcore.Services
{
    public class NetcoreService : INetcoreService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        public NetcoreService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IRoles roles,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
        }

        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);

        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);

            }

        }

        public async Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager)
        {
            bool result = false;
            try
            {
                var user = await userManager.FindByNameAsync(email);
                if (user != null)
                {
                    //Add this to check if the email was confirmed.
                    if (await userManager.IsEmailConfirmedAsync(user))
                    {
                        result = true;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, "uploads");
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }

            return result;
        }

        public async Task UpdateRoles(ApplicationUser appUser,
            ApplicationUser currentLoginUser)
        {
            try
            {
                await _roles.UpdateRoles(appUser, currentLoginUser);

                //so no need to manually re-signIn to make roles changes effective
                if (currentLoginUser.Id == appUser.Id)
                {
                    await _signInManager.SignInAsync(appUser, false);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;
                superAdmin.isSuperAdmin = true;

                Type t = superAdmin.GetType();
                foreach (System.Reflection.PropertyInfo item in t.GetProperties())
                {
                    if (item.Name.Contains("Role"))
                    {
                        item.SetValue(superAdmin, true);
                    }
                }

                await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                //loop all the roles and then fill to SuperAdmin so he become powerfull
                foreach (var item in typeof(netcore.MVC.Pages).GetNestedTypes())
                {
                    var roleName = item.Name;
                    if (!await _roleManager.RoleExistsAsync(roleName)) { await _roleManager.CreateAsync(new IdentityRole(roleName)); }

                    await _userManager.AddToRoleAsync(superAdmin, roleName);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public VMStock GetStockByProductAndWarehouse(string productId, string warehouseId)
        {
            VMStock result = new VMStock();

            try
            {
                Product product = _context.Product.Where(x => x.productId.Equals(productId)).FirstOrDefault();
                Warehouse warehouse = _context.Warehouse.Where(x => x.warehouseId.Equals(warehouseId)).FirstOrDefault();

                if (product != null && warehouse != null)
                {
                    VMStock stock = new VMStock();
                    stock.Product = product.productCode;
                    stock.Warehouse = warehouse.warehouseName;
                    stock.QtyReceiving = _context.ReceivingLine.Where(x => x.productId.Equals(product.productId) && x.warehouseId.Equals(warehouse.warehouseId)).Sum(x => x.qtyReceive);
                    stock.QtyShipment = _context.ShipmentLine.Where(x => x.productId.Equals(product.productId) && x.warehouseId.Equals(warehouse.warehouseId)).Sum(x => x.qtyShipment);
                    stock.QtyTransferIn = _context.TransferInLine.Where(x => x.productId.Equals(product.productId) && x.transferIn.warehouseIdTo.Equals(warehouse.warehouseId)).Sum(x => x.qty);
                    stock.QtyTransferOut = _context.TransferOutLine.Where(x => x.productId.Equals(product.productId) && x.transferOut.warehouseIdFrom.Equals(warehouse.warehouseId)).Sum(x => x.qty);
                    stock.QtyOnhand = stock.QtyReceiving + stock.QtyTransferIn - stock.QtyShipment - stock.QtyTransferOut;

                    result = stock;
                }

                
            }
            catch (Exception)
            {

                throw;
            }

            return result;

        }

        public List<VMStock> GetStockPerWarehouse()
        {
            List<VMStock> result = new List<VMStock>();

            try
            {
                List<VMStock> stocks = new List<VMStock>();
                List<Product> products = new List<Product>();
                List<Warehouse> warehouses = new List<Warehouse>();
                warehouses = _context.Warehouse.ToList();
                products = _context.Product.ToList();
                foreach (var item in products)
                {
                    foreach (var wh in warehouses)
                    {
                        VMStock stock = stock = GetStockByProductAndWarehouse(item.productId, wh.warehouseId);
                        
                        if (stock != null) stocks.Add(stock);

                    }


                }

                result = stocks;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public async Task InitCRM()
        {
            try
            {
                //create activity
                List<Activity> activities = new List<Activity>()
                {
                    new Activity{activityName = "Phone", description = "Phone", colorHex = "#f56954"},
                    new Activity{activityName = "Email", description = "Email", colorHex = "#f39c12"},
                    new Activity{activityName = "Meeting", description = "Meeting", colorHex = "#00a65a"},
                    new Activity{activityName = "Demo", description = "Demo", colorHex = "#00c0ef"}
                };

                _context.Activity.AddRange(activities);

                //create rating
                List<Rating> ratings = new List<Rating>()
                {
                    new Rating{ratingName = "Hot", description = "Hot", colorHex = "#f56954"},
                    new Rating{ratingName = "Cold", description = "Cold", colorHex = "#f39c12"},
                    new Rating{ratingName = "Warm", description = "Warm", colorHex = "#00a65a"}
                };

                _context.Rating.AddRange(ratings);

                //create channel
                List<Channel> channels = new List<Channel>()
                {
                    new Channel{channelName = "Web", description = "Web", colorHex = "#f56954"},
                    new Channel{channelName = "Facebook Pixels", description = "Facebook Pixels", colorHex = "#f39c12"},
                    new Channel{channelName = "Third Party", description = "Third Party", colorHex = "#00a65a"}
                };

                _context.Channel.AddRange(channels);

                //create stage
                List<Stage> stages = new List<Stage>()
                {
                    new Stage{stageName = "Qualification", description = "Qualification", colorHex = "#f56954"},
                    new Stage{stageName = "Discovery", description = "Discovery", colorHex = "#f39c12"},
                    new Stage{stageName = "Evaluation", description = "Evaluation", colorHex = "#00a65a"},
                    new Stage{stageName = "Deal", description = "Deal", colorHex = "#00c0ef"},
                    new Stage{stageName = "No Deal", description = "No Deal", colorHex = "#001F3F"}
                };

                _context.Stage.AddRange(stages);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
