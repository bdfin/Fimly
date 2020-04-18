﻿using Blazored.Toast.Services;
using Fimly.Data.Models;
using Fimly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fimly.Pages
{
    [Authorize]
    public partial class AppSettings : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] UserManager<AppUser> UserManager { get; set; }
        [Inject] ConfigService ConfigService { get; set; }
        [Inject] CurrencyService CurrencyService { get; set; }
        [Inject] IToastService ToastService { get; set; }

        private AppUser CurrentUser;
        private Config UserConfig;
        private List<Currency> Currencies;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            CurrentUser = await UserManager.GetUserAsync(user);
            UserConfig = await ConfigService.GetUserConfigAsync(CurrentUser.Id);
            Currencies = await CurrencyService.GetCurrenciesAsync();
        }

        private async Task HandleValidSubmit()
        {
            await ConfigService.UpdateConfigAsync(UserConfig);

            ToastService.ShowSuccess("App settings have been updated successfully.", "Settings Updated");

            StateHasChanged();
        }
    }
}
