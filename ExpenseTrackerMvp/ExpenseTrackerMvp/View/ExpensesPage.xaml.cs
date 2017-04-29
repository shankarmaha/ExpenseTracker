﻿
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.ViewModel;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();


            ViewModel.ExpenseViewModel expVM = new ViewModel.ExpenseViewModel(new ExpenseTrackerWebApiClientService());

            BindingContext = expVM;

                        
        }

        protected override void OnAppearing()
        {
            ((ExpenseViewModel)BindingContext).LoadExpensesCommand.Execute(null);

            base.OnAppearing();
        }
        
        
    }
}
