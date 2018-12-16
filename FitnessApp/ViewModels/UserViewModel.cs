using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;


namespace FitnessApp.ViewModels
{
    class UserViewModel
    {
        private SQLqueries SQLqueriesObject = new SQLqueries(); 
        private List<UserModel> userModels;

        public UserViewModel(string userNameOrEmail)
        {
            userModels = SQLqueriesObject.SearchForUser(userNameOrEmail);
        }


        public List<UserModel> UserModels { get => userModels; set { } }

    }
}
