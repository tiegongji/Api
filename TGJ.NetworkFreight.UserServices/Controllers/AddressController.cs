using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    /// <summary>
    /// 地址控制器
    /// </summary>
    [Route("Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUserAddressService UserAddressService;

        public AddressController(IUserAddressService UserAddressService)
        {
            this.UserAddressService = UserAddressService;
        }

        //[HttpPost]
        //public ActionResult<UserAddress> PostAddress(UserAddress UserAddress)
        //{
        //    UserAddressService.Create(UserAddress);
        //    return CreatedAtAction("GeUserAddress", new { id = UserAddress.Id }, UserAddress);
        //}

        //[HttpGet("{userId}")]
        //public ActionResult<IEnumerable<UserAddress>> GetUserAddresss(int userId)
        //{
        //    return UserAddressService.GetUserAddresss(userId).ToList();
        //}

        //[HttpGet("{userId}/{id}")]
        //public ActionResult<UserAddress> GetUserAddressById(int userId, int id)
        //{
        //    return UserAddressService.GetUserAddressById(userId, id);
        //}

        //[HttpPut("{id}")]
        //public IActionResult PutUserAddress(int id, UserAddress UserAddress)
        //{
        //    if (id != UserAddress.Id)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        UserAddressService.Update(UserAddress);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserAddressService.UserAddressExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{userId}/{id}")]
        //public ActionResult<UserAddress> DeletUserAddress(int userId, int id)
        //{
        //    var UserAddress = UserAddressService.GetUserAddressById(userId, id);
        //    if (UserAddress == null)
        //    {
        //        return NotFound(UserAddress);
        //    }

        //    UserAddressService.Delete(UserAddress);
        //    return UserAddress;
        //}
    }
}
