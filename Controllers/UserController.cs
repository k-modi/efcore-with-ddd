using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Repository;
using efcore2_webapi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace efcore2_webapi.Webapp.Controllers
{
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAppService _userAppService;

        public UserController(IUserRepository userRepository, 
                                IUserAppService userAppService)
        {
            _userRepository = userRepository;
            _userAppService = userAppService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return new ObjectResult(_userRepository.GetAllUsers());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var user = _userRepository.FindById(id);

            if (user == null) 
                return NotFound();
                
            return new ObjectResult(user); 
        }

        [HttpPost]
        public IActionResult Add([FromBodyAttribute]UserDto userDto)
        {
            var user = _userRepository.FindByUsername(userDto.Username);

            if (user != null)
                return BadRequest();

            _userAppService.Save(userDto);

            return Ok();
        }

        // [HttpPut("{userId:int}/assign/{taskId:int}")]
        // public IActionResult Assign(int taskId, int userId)
        // {
        //     try
        //     {
        //         var item = _todoRepository.FindById(taskId);

        //         if (item == null)
        //         {
        //             return NotFound($"{nameof(taskId)}:{taskId}");
        //         }

        //         var user = _userRepository.FindById(userId);

        //         if (user == null)
        //         {
        //             return NotFound($"{nameof(userId)}: {userId}");
        //         }

        //         item.AssignTo(userId);

        //         _todoRepository.SaveOrUpdate(item);

        //         return Ok();
        //     }
        //     catch (System.Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
    }
}
