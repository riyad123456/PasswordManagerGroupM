using System.Runtime.Caching;
using PasswordManager.Models;

namespace PasswordManager.Utils;
public static class PasswordCache
{
    private static MemoryCache _cache = MemoryCache.Default;
    public static void AddOrUpdatePasswordEntry(string key, PasswordEntry entry, int durationInSeconds)
    {
        var policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(durationInSeconds)
        };

        _cache.Set(key, entry, policy);
    }

    public static PasswordEntry? GetPasswordEntryById(string key)
    {
        return _cache.Get(key) as PasswordEntry;
    }

    public static List<PasswordEntry> GetPasswordEntriesByUserName(string userName)
    {
        return _cache.Cast<KeyValuePair<string, object>>()
                    .Select(kvp => kvp.Value)
                    .OfType<PasswordEntry>()
                    .Where(entry => entry.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
    }

    public static bool UpdateEntry(string key, PasswordEntry updatedEntry, int durationInSeconds)
    {
        if (_cache.Contains(key))
        {
            AddOrUpdatePasswordEntry(key, updatedEntry, durationInSeconds);
            return true;
        }

        return false;
    }

    public static bool RemovePasswordEntry(string key)
    {
        if (_cache.Get(key) != null)
        {
            _cache.Remove(key);
            return true;
        }
        return false;
    }
}