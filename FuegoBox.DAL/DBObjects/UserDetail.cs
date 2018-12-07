using AutoMapper;
using FuegoBox.DAL;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.User;
using System;
using System.Linq;

namespace FuegoBox.DAL.DBObjects
{
    public class UserDetail
    {
        FuegoEntities dbContext;
        IMapper userUserRegisterDTOmapper;
        IMapper UserRegisterDTOusermapper;
        IMapper userUserBasicDTOMapper;

        public UserDetail()
        {
            dbContext = new FuegoEntities();
            var userUserRegisterDTOconfig = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<User, UserRegisterDTO>();    
              });
            var UserRegisterDTOuserconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterDTO, User>();
            });
            var userUserBasicDTOconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserBasicDTO>();
            });

            userUserRegisterDTOmapper = new Mapper(userUserRegisterDTOconfig);
            UserRegisterDTOusermapper = new Mapper(UserRegisterDTOuserconfig);
            userUserBasicDTOMapper = new Mapper(userUserBasicDTOconfig);


        }

        public bool UserExists(UserLoginDTO userLoginDTO)
        {
            User user = dbContext.User.Where(a => a.Email == userLoginDTO.Email).FirstOrDefault();
            if (user != null) {
                return true;
            }
            else
            {
                throw new NotFoundException();
            }
       
           
        }

        public bool UserEmailExists(string Email)
        {
            User user = dbContext.User.Where(a => a.Email == Email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public UserBasicDTO GetUser(UserLoginDTO userLoginDTO)
        {
            User user= dbContext.User.Where(a => a.Email == userLoginDTO.Email).FirstOrDefault();
            if (user != null)
            {
                UserBasicDTO newuserBasicDTO = userUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
                return newuserBasicDTO;
            }
            else
            {
                throw new NotFoundException();
            }
        }


        public UserBasicDTO AddUser(UserRegisterDTO userRegisterDTO)
        {
            User user = UserRegisterDTOusermapper.Map<UserRegisterDTO, User>(userRegisterDTO);
            user.ID = Guid.NewGuid();
            user.RoleID= dbContext.Role.Where(r => r.Name == "user").First().ID;
            //user.Role.Add(dbContext.Role.Where(r => r.Name == "user").First());
            dbContext.User.Add(user);
            dbContext.SaveChanges();
            UserBasicDTO userBasicDTO = userUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
            return userBasicDTO;
        }

        public bool CheckAdmin(Guid UserID)
        {
            User user = dbContext.User.Where(u => u.ID == UserID).First();
            Role role = dbContext.Role.Where(r=>r.Name == "admin").FirstOrDefault();
           
                if (user.RoleID == role.ID)
                {
                    return true;
                }
           
                

            return false;
        }
    }
}
