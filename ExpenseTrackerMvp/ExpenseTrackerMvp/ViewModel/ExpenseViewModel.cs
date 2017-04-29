using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class ExpenseViewModel : BaseViewModel
    {
        public ObservableCollection<Model.Expense> ExpenseCollection { get; set; }
        



        private readonly IExpenseTrackerWebApiClientService _expenseTrackerWebApiService;

        public ExpenseViewModel(IExpenseTrackerWebApiClientService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            ExpenseCollection = new ObservableCollection<Expense>();

            LoadExpensesCommand = new Command(ExecuteLoadExpense);

            CreateCommand = new Command(ExecuteCreate);
        }
        


        public Command LoadExpensesCommand { get; set; }

        public Command CreateCommand { get; set; }
        




        private async void ExecuteCreate()
        {
            await App.NavigateMasterDetailModal(new ExpensesCreatePage());
        }

        

        private async void ExecuteLoadExpense()
        {

            ExpenseCollection.Clear();

            try
            {
                IsBusy = true;

                
                List<Expense> expenseList = await _expenseTrackerWebApiService.GetExpenseList();
                
                foreach (Expense exp in expenseList)
                {
                    this.ExpenseCollection.Add(exp);
                }
                                
            }
            catch (Exception ex)
            {
                // TODO logging
                await base.ShowErrorMessage("Cannot connect to server. " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }


            // Using Firebase
            // https://github.com/rlamasb/Firebase.Xamarin
            // https://github.com/williamsrz/xamarin-on-fire/blob/master/XOF.Droid/Services/FirebaseService.cs
            /*
            string firebaseToken = UserSettings.GetFirebaseAuthToken();
            var firebase = FirebaseService.GetFirebaseExpenseTrackerClient();

            // Example : get one
            //Expense exp = await firebase.Child("Expenses").Child("1").WithAuth(firebaseToken).OnceSingleAsync<Expense>();
            //ExpenseCollection.Add(exp);
                        
            var items = await firebase
              .Child("Expenses")
              .OrderByKey()
              .WithAuth(firebaseToken)                                         
              .OnceAsync<IList<Expense>>();
                        
            foreach (var item in items)
            {
                Expense exp = (Expense)item.Object;
                ExpenseCollection.Add(exp);                
            }
            */

        }

    }
}