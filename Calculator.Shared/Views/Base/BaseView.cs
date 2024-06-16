using Calculator.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Shared.Views.Base
{
    public class BaseView : ContentPage
    {
        public BaseView()
        {
            HandlerChanged += OnHandlerChanged;
        }

        void OnHandlerChanged(object sender, EventArgs e)
        {
            BindingContext = Handler.MauiContext.Services.GetService<HomeViewModel>();
        }
    }


}
