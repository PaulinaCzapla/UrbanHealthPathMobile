using System;
using System.Collections;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Base class for all controllers that make use of View and Popup Managers.
    /// </summary>
    public abstract class BaseController
    {
        protected ViewManager ViewManager { get; }
        protected PopupManager PopupManager { get; }

        protected BaseController(ViewManager viewManager, PopupManager popupManager)
        {
            ViewManager = viewManager;
            PopupManager = popupManager;
            
            ViewManager.ViewOpened += ViewOpenedHandler;
            PopupManager.PopupOpened += PopupOpenedHandler;
            PopupManager.PopupClosed += PopupClosedHandler;
        }

        protected void ReturnToPreviousView()
        {
            (ViewType viewType, IViewInitializationParameters initParams) lastView = ViewManager.History.GetLastView();
            ViewManager.OpenView(lastView.viewType, lastView.initParams);
        }

        protected virtual void ViewOpenedHandler(ViewType type)
        {
        }

        protected virtual void PopupOpenedHandler(PopupType type)
        {
        }

        protected virtual void PopupClosedHandler(PopupType type)
        {
        }

        protected void ShowConfirmation(string message, Action confirm)
        {
            ShowConfirmationPopup(message, () => confirm.Invoke());
        }

        private void ShowConfirmationPopup(string message, UnityAction confirm)
        {
            PopupManager.OpenPopup(PopupType.Confirmation,
                new ConfirmationPopupInitializationParameters(message, 2, new[] { "Anuluj", "Potwierdź" },
                    new[] { PopupManager.CloseCurrentPopup, confirm }));
        }
    }
}