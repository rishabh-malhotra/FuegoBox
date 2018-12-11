using FuegoBox.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuegoBox.DAL.DBObjects;
using AutoMapper;
using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.Exceptions;

namespace FuegoBox.Business.BusinessObjects
{
    public class UserBusinessContext
    {
        UserDetail UserDBObject;
        public UserBusinessContext()
        {
            UserDBObject = new UserDetail();
        }
        //adding new register to the database..
        public UserBasicDTO RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                if (UserDBObject.UserEmailExists(userRegisterDTO.Email))
                {
                    throw new UserNameAlreadyExistsException();
                }
            }
            catch (NotFoundException ex)
            {
                userRegisterDTO.HashPassword = PasswordHasher.PasswordHashing(userRegisterDTO.Password);
                UserBasicDTO newuserBasicDTO = UserDBObject.AddUser(userRegisterDTO);
                return newuserBasicDTO;
            }
            catch (UserNameAlreadyExistsException ex)
            {
                throw new UserNameAlreadyExistsException();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
           

            return null;
        }

        //log in the user...
        public UserBasicDTO LoginUser(UserLoginDTO userLoginDTO)
        {
            try
            {
                if (UserDBObject.UserExists(userLoginDTO))
                {
                    UserBasicDTO newUserBasicDTO = UserDBObject.GetUser(userLoginDTO);
                    if (PasswordHasher.VerifyPassword(userLoginDTO.Password, newUserBasicDTO.HashPassword))
                    {
                        return newUserBasicDTO;
                    }
                }
                throw new InvalidLoginException();
            }
            catch (NotFoundException ex)
            {
                throw new InvalidLoginException();
            }
            
        }

        //passing user's id to dal layer to check whether he is admin or not.
        public bool CheckAdmin(Guid UserID)
        {
            return UserDBObject.CheckAdmin(UserID);
        }


    }

}
