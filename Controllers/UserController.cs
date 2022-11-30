using AshMoneyAPI.Data;
using AshMoneyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AshMoneyAPI.Controllers
    
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AshDBContext ashDbContext;

        public UserController(AshDBContext ashDbContext)
        {
            this.ashDbContext = ashDbContext;
        }
        [HttpGet]
        [Route("GetAllUsers")]

        public async Task<IActionResult> GetUser()
        {
            return Ok( await ashDbContext.Users.ToListAsync());
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Users userObj)
        {

            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                foreach (var userReg in userObj.AccountNumber)
                {
                    Random rd = new Random();
                    int rand_num = rd.Next(1000000, 2000000);

                    var user = new Users()
                    {
                        Address = userObj.Address,
                        Email = userObj.Email,
                        PhoneNo = userObj.PhoneNo,
                        Password = userObj.Password,
                        DateOfBirth = userObj.DateOfBirth,
                        Gender = userObj.Gender,
                        Country = userObj.Country,
                        FirstName = userObj.FirstName,
                        LastName = userObj.LastName,
                        State = userObj.State,
                        IsAdmin = userObj.IsAdmin,
                        IsAgent = userObj.IsAgent,
                        AccountNumber = "001" + rand_num

                    };

                    await ashDbContext.Users.AddAsync(user);
                }
                    await ashDbContext.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Successfully Registered",
                    UserData = userObj
                });
                }
            }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Users userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user =  await ashDbContext.Users.Where(a => a.PhoneNo == userObj.PhoneNo && a.Password == userObj.Password).FirstOrDefaultAsync();
                if (user != null)
                {
                    return Ok(new
                    {
                        Message = "Logged In Succesfully",
                        userData = userObj
                    });
                }
                else
                {
                    return NotFound(new { Message = "User Does not exist" });
                }

            }
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int Id, Users userObj)
        {
            var user = await ashDbContext.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
               
                user.Address = userObj.Address;
                user.Country = userObj.Country;
                user.DateOfBirth = userObj.DateOfBirth;
                user.Email = userObj.Email;
                user.FirstName = userObj.FirstName;
                user.Gender = userObj.Gender;
                user.LastName = userObj.LastName;
                user.PhoneNo = userObj.PhoneNo;
                user.State = userObj.State;

                await ashDbContext.SaveChangesAsync();
                return Ok(user);
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var user = await ashDbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                return NotFound(new { Message = "User Not Found" });
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = await ashDbContext.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                ashDbContext.Users.Remove(user);
                await ashDbContext.SaveChangesAsync();
                return Ok(new { user = user.FirstName ,Message = "User Deleted Successfully"});
            }


        }
        }  
    }

