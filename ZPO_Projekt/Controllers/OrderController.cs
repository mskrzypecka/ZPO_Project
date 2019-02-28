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
using ZPO_Projekt.Models.Foods;

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

            OrderViewModel model = new OrderViewModel();
            model.Order = order;
            var FoodIds = order.Foods.Select(y => y.FoodId).ToList();
            var foods = _context.Foods.Where(x => FoodIds.Contains(x.Id)).ToList();
            List<IFood> listIFood = new List<IFood>();
            foods.ForEach(x => listIFood.Add(FoodAdapter.RestoreIFood(x.Type, x)));
            model.Foods = listIFood;

            return View(model);
        }
        
        public ActionResult AddOrder()
        {
            ViewBag.Message = "Add new Order.";

            List<Food> list = _context.Foods.ToList();
            List<IFood> listOfIFood = new List<IFood>();

            foreach(Food food in list)
            {
                IFood currFood = FoodAdapter.RestoreIFood(food.Type, food);

                listOfIFood.Add(currFood);
            }

            OrderViewModel model = new OrderViewModel();
            model.Foods = listOfIFood;

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
            Order.Delivery = model.Order.Delivery;

            var FoodsInOrder = new List<FoodInOrder>();
            foreach (var Food in model.Foods.Where(x => x.IsChecked == true))
            {
                FoodInOrder FoodInOrder = new FoodInOrder();
                FoodInOrder.Id = Guid.NewGuid().ToString();
                FoodInOrder.FoodId = Food.Id;
                FoodInOrder.OrderId = Order.Id;
                FoodsInOrder.Add(FoodInOrder);
            }
            Order.Foods = FoodsInOrder;
            
            user.Orders.Add(Order);
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

            order.Delivery = model.Delivery;

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

        public ActionResult AddNewDish()
        {
            ViewBag.Message = "Add new dish to or menu.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewDish(Food model)
        {
            Food newDish = new Food();
            newDish.IsChecked = false;
            newDish.Name = model.Name;
            newDish.Price = (new Random()).Next(20)+10;
            newDish.Id = Guid.NewGuid().ToString();
            newDish.Type = model.Type;

            _context.Foods.Add(newDish);
            await SaveChanges();

            return RedirectToAction("AddOrder");
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