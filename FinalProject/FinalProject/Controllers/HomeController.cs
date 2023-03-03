using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        FinalProjectDBContext context = new FinalProjectDBContext();

        public IActionResult Index()
        {
            List<Product> products = context.Products.ToList();

            return View(products);
        }

        public IActionResult List()
        {
            List<List> lists = context.Lists.ToList();


            return View(lists);
        }

        public IActionResult DeleteList(int id)
        {
            var query = context.Lists.FirstOrDefault(a => a.ListId == id);
            if (query == null)
            {
                return View("Error");
            }

            List<MyList> myLists=context.MyLists.Where(a => a.ListId == id).ToList();
            context.RemoveRange(myLists);

            context.Remove(query);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        
        public IActionResult ChangeIsActive(int id)
        {
            var query = context.Lists.FirstOrDefault(a => a.ListId == id);

            if (query.IsActive)
            {
                query.IsActive = false;
            }
            else
            {
                query.IsActive = true;
            }


            context.SaveChanges();
            return RedirectToAction("list");
        }

        public IActionResult Newlist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Newlist(List list)
        {
            List list1 = new List();
            list1.ListName = list.ListName;
            list1.IsActive = true;
            context.Lists.Add(list1);
            context.SaveChanges();
            return RedirectToAction("List");
        }


        public IActionResult MyList(int id)
        {
            List<MyListViewModel> mylist = context.MyLists.Include(a => a.List).Where(a => a.ListId == id).Select(a => new MyListViewModel()
            {
                ListId= a.ListId,
                ListName = a.List.ListName,
                isActive = (bool)a.List.IsActive,
                ProductName = a.ProductName,
                Quantity = a.Quantity,
                Id = a.CurrentListId

            }).ToList();
            return View(mylist);
        }

        public IActionResult AddToList(int id)
        {
            var query = context.Products.FirstOrDefault(a => a.ProductId == id);
            AddToListAndCategoryViewModel model = new AddToListAndCategoryViewModel
            {
                productName = query.ProductName,
                List = context.Lists.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToList(AddToListAndCategoryViewModel model, int id)
        {
            MyList list = new MyList();
            list.ListId = id;
            list.ProductName = model.productName;
            list.Quantity = model.quantity;
           
            context.MyLists.Add(list);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteListItem(int id, int listId)
        {
            var removeItem = context.MyLists.FirstOrDefault(a => a.CurrentListId == id);
            if (removeItem == null)
            {
                return View("Error");
            }
            context.Remove(removeItem);
            context.SaveChanges();
            return RedirectToAction("MyList", new {id= listId });
        }

        public IActionResult ModifyListItem(int id)
        {
            var query = context.MyLists.FirstOrDefault(a => a.CurrentListId == id);

            EditMyListItemViewModel model = new EditMyListItemViewModel
            {
                Name = query.ProductName,
                Quantity = (int)query.Quantity
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult ModifyListItem(EditMyListItemViewModel model)
        {
            var query = context.MyLists.FirstOrDefault(a => a.CurrentListId == model.id);

            query.Quantity = model.Quantity;
            context.SaveChanges();



            return RedirectToAction("List");
        }

        public IActionResult Privacy()
        {
            return View();
        }








    }
}