using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.oData.Models;

namespace Web.Api.oData.Controllers
{

    /// <summary>
    /// Storage controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Storage> Get()
        {
            return new List<Storage>
            {
                CreateNewStorage("Storage Hüttlingen"),
                CreateNewStorage("Storage Berlin"),
                CreateNewStorage("Storage München")
            };

        }

        private Storage CreateNewStorage(string name)
        {
            return new Storage()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy ="demo@demo.info",
                Name = name,
                Uuid = Guid.NewGuid()
            };
        }

    }
}
