using System.Security.Claims;
using System.Security.Principal;

namespace BarnamenevisanCompany.Application.Extensions;

public static class IdentityExtensions
{
    #region UserId

    public static UserId GetUserId(this IPrincipal principal)
    {
        if (principal == null)
            return default;

        var user = (ClaimsPrincipal)principal;
        return user.GetUserId();
    }

    public static UserId GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
            return default;

        if (claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) == null)
            return default;

        string userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (string.IsNullOrEmpty(userId))
            return default;

        bool isParseSuccess = int.TryParse(userId, out int parsedUserId);
        if (!isParseSuccess) return default;

        return parsedUserId;
    }
    public static string GetUserMobile(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
            return default;

        if (claimsPrincipal.FindFirst(ClaimTypes.MobilePhone) == null)
            return default;

        string userPhoneNumber = claimsPrincipal.FindFirst(ClaimTypes.MobilePhone).Value;
        if (string.IsNullOrEmpty(userPhoneNumber))
            return default;
        
        return userPhoneNumber;
    }




    #endregion

    #region UserName

    public static string? GetUserName(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
            return default;

        if (claimsPrincipal.FindFirst(ClaimTypes.Name) == null)
            return default;

        string userName = claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
        if (string.IsNullOrEmpty(userName))
            return default;

        return userName;
    }

    public static string? GetUserName(this IPrincipal principal)
    {
        if (principal == null)
            return default;

        var user = (ClaimsPrincipal)principal;

        return user.GetUserName();
    }

    #endregion

    #region IsAdmin

    public static bool IsAdmin(this IPrincipal principal)
    {
        while (true)
        {
            if (principal == null) return false;

            var user = (ClaimsPrincipal)principal;
            principal = user;
        }
    }

    // public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
    // {
    //     if (claimsPrincipal == null)
    //         return false;
    //
    //     var claim = claimsPrincipal.FindFirst(CustomClaimTypes.IsAdmin);
    //     if (claim == null ||  string.IsNullOrEmpty(claim.Value))
    //         return false;
    //     
    //     bool isParseSuccess = bool.TryParse(claim.Value, out bool parsedIsAdmin);
    //     if (!isParseSuccess) return false;
    //
    //     return parsedIsAdmin;
    // }
    //

    #endregion
}

public struct UserId(int? value) : IEquatable<UserId>
{
    private readonly int? _value = value;

    // Implicit conversion from UserId to int?
    
    public static implicit operator int(UserId userId)
    {
        return userId._value ?? default(int);
    }
    
    public static implicit operator int?(UserId userId)
    {
        return userId._value;
    }

    // Implicit conversion from int? to UserId
    public static implicit operator UserId(int? value)
    {
        return new UserId(value);
    }

    // Override ToString for better usability
    public override string ToString()
    {
        return _value?.ToString() ?? "";
    }

    public bool Equals(UserId other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is UserId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}
