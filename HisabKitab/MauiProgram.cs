﻿using HisabKitab.Services;
using HisabKitab.Services.Interface;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace HisabKitab
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IDebtService, DebtService>();
            builder.Services.AddScoped<ITagService, TagService>();

            return builder.Build();
#endif
        }
    }
}
