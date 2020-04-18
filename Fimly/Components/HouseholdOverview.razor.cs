﻿using Fimly.Data.Models;
using Fimly.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fimly.Components
{
    public partial class HouseholdOverview : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] UserManager<AppUser> UserManager { get; set; }
        [Inject] ConfigService ConfigService { get; set; }
        [Inject] ExpenseService ExpenseService { get; set; }
        [Inject] PersonService PersonService { get; set; }

        private AppUser CurrentUser;
        private Config UserConfig;
        private List<Person> People;
        private List<Expense> Expenses;

        private decimal monthlyIncome;
        private decimal monthlyExpenditure;
        private decimal monthlyNet;

        private bool spendingWarning = false;
        private string spendingWarningCssClass => spendingWarning ? "text-danger" : "text-success";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            CurrentUser = await UserManager.GetUserAsync(user);
            UserConfig = await ConfigService.GetUserConfigAsync(CurrentUser.Id);
            Expenses = await ExpenseService.GetAllExpensesAsync(CurrentUser.Id);
            People = await PersonService.GetPeopleAsync(CurrentUser.Id);

            monthlyIncome = FinanceService.GetTotalMonthlyIncome(People);
            monthlyExpenditure = FinanceService.GetTotalMonthlyExpenditure(Expenses);
            monthlyNet = monthlyIncome - monthlyExpenditure;


            if (monthlyIncome <= monthlyExpenditure)
            {
                spendingWarning = true;
            }
        }
    }
}