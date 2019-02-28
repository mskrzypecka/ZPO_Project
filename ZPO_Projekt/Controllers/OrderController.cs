using ZPO_Projekt.Data;
using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using ZPO_Projekt.Models;

namespace ZPO_Projekt.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public OrderController(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public ActionResult Index(string id = null)
        {
            if (id is null)
            {
                var userId = User.Identity.GetUserId();
                string firstFoodId = "";

                var firstFood = _context.Orders
                .Include(x => x.Foods)
                .Include(x => x.Client)
                .Where(x => x.Client.Id == userId)
                .OrderBy(x => x.DateOfOrder)
                .FirstOrDefault();

                if (firstFood != null)
                {
                    firstFoodId = firstFood.Id;
                    return RedirectToAction("Index", new { Id = firstFoodId });
                }
                else
                {
                    return RedirectToAction("AddOrder");
                }
            }

            var order = _context
                .Orders
                .Include(x => x.Foods)
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);

            return View(order);
        }
        
        public ActionResult AddOrder()
        {
            ViewBag.Message = "Add new Order.";

            List<Food> list = _context.Foods.ToList();
            OrderViewModel model = new OrderViewModel();
            model.Foods = list;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrder(OrderViewModel model)
        {
            var user = GetUser();

            Order Order = new Order();
            Order.Id = Guid.NewGuid().ToString();
            Order.Client = user;
            Order.DateOfOrder = DateTime.Now;
            Order.Delivery = model.Delivery;

            var foodsInOrder = new List<FoodInOrder>();
            foreach (var food in model.Foods.Where(x => x.IsChecked == true))
            {
                FoodInOrder foodInOrder = new FoodInOrder();
                foodInOrder.id = Guid.NewGuid().ToString();
                foodInOrder.FoodId = food.Id;
                foodInOrder.OrderId = Order.Id;
                foodsInOrder.Add(foodInOrder);
            }
            Order.Foods = foodsInOrder;
            
            user.Orders.Add(Order);
            _context.SaveChanges();
            await SaveChanges();

            return RedirectToAction("Index", "Order", new { id = Order.Id });
        }      
        
        public ActionResult EditOrder(string id)
        {
            ViewBag.Message = "Edit Order.";

            var model = _context.Orders
                .Include(x => x.Foods)
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrder(Order model, string id)
        {
            var order = GetOrder(id);

            order.Client = model.Client;
            order.Delivery = model.Delivery;
            order.Foods = model.Foods;

            await SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DisplayMenu()
        {
            var user = GetUser();
            var model = _context.Orders
                .Include(x => x.Foods)
                .Include(x => x.Client)
                .Where(x => x.Client.Id == user.Id)
                .OrderBy(x => x.DateOfOrder)
                .AsEnumerable();

            return PartialView("_MenuPartial", model);
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users
                .Include(x => x.Orders)
                .FirstOrDefault(x => x.Id == userId);

            return user;
        }

        private Order GetOrder(string OrderId = "")
        {
            if (String.IsNullOrEmpty(OrderId))
            {
                return _context.Orders
                    .Include(x => x.Foods)
                    .Include(x => x.Client)
                        .FirstOrDefault();
            }

            var Order = _context.Orders
                .Where(x => x.Id == OrderId)
                .FirstOrDefault()
                ?? throw new HttpException(404, "Order not found");

            return Order;
        }

        private Food GetFood(string FoodId = "")
        {
            if(String.IsNullOrEmpty(FoodId))
            {
                return _context.Foods
                        .OfType<Food>()
                        .FirstOrDefault();
            }

            var model = _context.Foods
                        .OfType<Food>()
                        .Where(x => x.Id == FoodId)
                        .FirstOrDefault()
                        ?? throw new HttpException(404, "Food not found");

            return model;
        }

        private async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Database exception: " + ex.Message);
            }
        }
    }
}