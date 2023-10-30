using _Prueba.Server.Models;
using _Prueba.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _Prueba.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet(Name = "MenuGet")]
        public List<ListItemDTO> GetMostrarMenu()
        {
            
            var _return = NavigationBuilderModel.BuildNavigation();
            List<ListItemDTO> _nav = _return.Lists;
            return _nav;
            
        }
    }
}
