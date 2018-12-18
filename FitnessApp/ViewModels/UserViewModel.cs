using FitnessApp.Models;
using FitnessApp.SQLserver;
using System.Collections.Generic;


namespace FitnessApp.ViewModels
{
    class UserViewModel
    {

        private List<UserModel> userModels;

        public UserViewModel(string userNameOrEmail)
        {
            userModels = Database.SearchForUser(userNameOrEmail);
        }


        public List<UserModel> UserModels { get => userModels; set { } }

    }
}
