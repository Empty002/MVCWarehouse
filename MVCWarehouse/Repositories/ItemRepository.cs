﻿using MVCWarehouse.DataAccess;
using MVCWarehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//comment

namespace MVCWarehouse.Repositories
{
    public class ItemRepository
    {
        StoreContext context;

        public ItemRepository()
        {
            context = new StoreContext();
        }

        public IEnumerable<StockItem> GetAllItems()
        {
            return context.Items;
        }

        public StockItem GetItem(int ArticleNumber)
        {
            return context.Items.FirstOrDefault(item => item.ArticleNumber == ArticleNumber);
        }

        public IEnumerable<StockItem> GetItemByName(string Name)
        {
            return context.Items.Where(item => item.Name.Contains(Name));
        }
    }
}