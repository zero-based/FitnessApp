using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;


namespace FitnessApp.ViewModels
{
    class UserViewModel
    {

        private List<UserModel> userModels;

        public UserViewModel(string userNameOrEmail)
        {
            userModels = SQLqueries.SearchForUser(userNameOrEmail);
        }


        public List<UserModel> UserModels { get => userModels; set { } }

    }
}
