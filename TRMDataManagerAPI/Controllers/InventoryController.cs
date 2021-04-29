using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRMDLL.DataAccess;
using TRMDLL.Models;

namespace TRMDataManagerAPI.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        //this is endpoint
        public List<InventoryModel> Get()
        {
            InventoryData inventory = new InventoryData();

            return inventory.GetInventory();
        }

        //this is endpoint
        public void Post(InventoryModel inventory)
        {
            InventoryData item = new InventoryData();
            item.SaveInventoryRecord(inventory);
        }
    }
}
