﻿using System;
using System.Threading.Tasks;

using DCompAdventure.Contracts.Services;

using Microsoft.UI.Xaml;

namespace DCompAdventure.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        public ElementTheme Theme { get; set; } = ElementTheme.Default;

        private readonly ILocalSettingsService _localSettingsService;

        public ThemeSelectorService(ILocalSettingsService localSettingsService)
        {
            _localSettingsService = localSettingsService;
        }

        public async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
            await Task.CompletedTask;
        }

        public async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(Theme);
        }

        public async Task SetRequestedThemeAsync()
        {
            if (App.MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = Theme;
            }

            await Task.CompletedTask;
        }

        private async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            string themeName = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
            await _localSettingsService.SaveSettingAsync(SettingsKey, theme.ToString());
        }
    }
}
