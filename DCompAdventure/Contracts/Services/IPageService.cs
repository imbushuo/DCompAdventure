using System;

namespace DCompAdventure.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
