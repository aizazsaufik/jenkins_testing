using EntityFramework.Extensions;
using ToDoApp.Data.Core;
using ToDoApp.Data.Model;
using ToDoApp.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ToDoApp.Data.Actions
{
    public class UsersActions : BaseActions<AspNetUser>
    {

        public override IQueryable<AspNetUser> GetAll()
        {
            return ctx.AspNetUsers;
        }


        public override AspNetUser Get(long id)
        {
            return ctx.AspNetUsers.First(it => it.UniqueId == id);
        }


        public override void Delete(long id)
        {
            ctx.AspNetUsers.Where(x => x.UniqueId == id).Delete();
            Save();
        }
        public override void Change(AspNetUser t, Guid by)
        {
            Save();
        }

        public override void Add(AspNetUser t, Guid by)
        {
            ctx.AspNetUsers.Add(t);
            Save();
        }

        public void AddUsers(UsersDto ViewModel)
        {
            try
            {
                AspNetUser model = new AspNetUser();
                if (ViewModel != null)
                {
                    Guid SCS = Guid.NewGuid();
                    Guid uid = Guid.NewGuid();
                    model.Id = uid.ToString();
                    model.Email = ViewModel.Email;
                    model.PasswordHash = HashPassword(ViewModel.Password);
                    model.EmailConfirmed = false;
                    model.SecurityStamp = SCS.ToString();
                    model.PhoneNumberConfirmed = false;
                    model.TwoFactorEnabled = false;
                    model.LockoutEnabled = false;
                    model.AccessFailedCount = 0;
                    model.UserName= ViewModel.Email;
                    Add(model, Guid.Empty);
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public UsersDto GetUsersForEdit(int id)
        {
            UsersDto viewModel = new UsersDto();
            if (id != 0)
            {
                AspNetUser model = Get(id);

                viewModel.Email = model.Email;
                viewModel.Password = model.PasswordHash;
               // viewModel.id = model.UniqueId;
             
            }
            return viewModel;

        }

        //Encription Function
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] bytes;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }
    }
    }
