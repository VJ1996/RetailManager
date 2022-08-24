using Caliburn.Micro;
using RMDesktop.UI.Library.Api;
using RMDesktop.UI.Library.Models;
using RMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private StatusInfoViewModel _status;
        private IWindowManager _window;
        private IUserEndPoint _userEndPoint;
        public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager window, IUserEndPoint userEndPoint)
        {
            _status = status;
            _window = window;
            _userEndPoint = userEndPoint;
        }

        BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }

            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";
                var info = IoC.Get<StatusInfoViewModel>();

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the Sales Form");
                    _window.ShowDialog(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _window.ShowDialog(_status, null, settings);
                }
                TryClose();
                //what do we do here
            }
        }

        private async Task LoadUsers()
        {
            var userList = await _userEndPoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
    }
}
