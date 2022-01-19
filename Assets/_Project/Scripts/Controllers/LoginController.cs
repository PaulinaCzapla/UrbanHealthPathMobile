﻿using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.PersistentValue;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class LoginController : BaseController
    {
        private readonly BoolPrefsValue _isFirstRun;

        public LoginController(ViewManager viewManager, PopupManager popupManager) : base(viewManager, popupManager)
        {
            _isFirstRun = new BoolPrefsValue("is_first_run", true);
        }

        public void ShowLoginScreenOnFirstRun(Action loginComplete)
        {
            if (!_isFirstRun.Value)
            {
                loginComplete.Invoke();
                return;
            }

            IViewInitializationParameters initParams = new LogInViewInitializationParameters(() => { }, () =>
            {
                _isFirstRun.Value = true;
                loginComplete.Invoke();
            });

            ViewManager.OpenView(ViewType.Login, initParams);
        }
    }
}