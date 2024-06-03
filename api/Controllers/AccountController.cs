using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager ;

   

        private readonly ITokenService _tokenService;

        private readonly SignInManager<AppUser> _signInManager;
        public  AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager){
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;

        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO registerdto){

                try{

                    if(!ModelState.IsValid){
                        return BadRequest(ModelState);
                    };

                    var user = new AppUser(){
                       
                        UserName = registerdto.Username,
                        Email = registerdto.Email
                    };

                    IdentityResult result = await _userManager.CreateAsync(user,registerdto.Password);

                    if(result.Succeeded){
                        var role = await _userManager.AddToRoleAsync(user,"User");
                        //sign In
                        await _signInManager.SignInAsync(user,isPersistent: false);

                        if(role.Succeeded){

                                return Ok(new NewUserDTO(){
                                    UserName = user.UserName,
                                    Email = user.Email,
                                
                                    Token = _tokenService.CreateToken(user)
                                }

                            );
                        }

                        else{

                            return StatusCode(500,role.Errors);
                        }
                    }
                    else{

                        foreach(IdentityError error in result.Errors){
                            ModelState.AddModelError("register",error.Description);
                        }
                        return StatusCode(500,ModelState);
                    }

                }catch(Exception e){

                     return StatusCode(500,e);
                }
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginDTO([FromBody] LoginDTO login ){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == login.Username);

            if(user == null) return Unauthorized("Invalid Username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user,login.Password,false);
            if(!result.Succeeded) return Unauthorized("Username is not found and /or password not found");

            return Ok(

                new NewUserDTO{

                    UserName = user.UserName,
                    Email = user.Email ,
                    Token = _tokenService.CreateToken(user)
                
             });

        }
    }
}