using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.ViewModels
{
    class UserViewModel
    {
        private List<UserModel> userModels;

        public UserViewModel()
        {
            userModels = new List<UserModel>()
            {
                new UserModel() { FirstName ="JHON DOE", Email="JHON DOE@LoremIposem.com" }
            };
        }


        public List<UserModel> UserModels { get => userModels; set { } }

    }
}
