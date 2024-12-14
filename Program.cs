using PasswordManager.Models;
using PasswordManager.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapSwagger();

const int CachingTime = 1000;

app.MapGet("/", () => { return Results.Ok("Welcome to the password manager app made by Riyad!"); });

// Add the users encrypted password to the list of users password store.
app.MapPost("/passwords", (PasswordEntry passwordEntry) =>
{
    int id = passwordEntry.Id;
    if (PasswordCache.GetPasswordEntryById($"password:{id}") != null)
    {
        return Results.BadRequest($"Id {id}: Id already exists.");
    }
    var validationError = PasswordUtils.ValidateEntry(passwordEntry);
    if (validationError != null)
    {
        return Results.BadRequest(validationError);
    }
    PasswordCache.AddOrUpdatePasswordEntry(
        key: $"password:{id}",
        entry: passwordEntry,
        durationInSeconds: CachingTime
    );
    return Results.Ok("Entry added successfully!");
});

// Get the list of all passwords for the user.
app.MapGet("/passwords/{userName}", (string userName) =>
{
    var userPasswords = PasswordCache.GetPasswordEntriesByUserName(userName); ;
    if (userPasswords == null)
    {
        return Results.NotFound($"User {userName} has no passwords.");
    }

    return Results.Ok(userPasswords);
});

// Get a single item from the password store. 
app.MapGet("/passwords/item/{id}", (int id) =>
{
    var passwordEntry = PasswordCache.GetPasswordEntryById($"password:{id}");
    if (passwordEntry == null)
    {
        return Results.NotFound($"ID {id}: Not found.");
    }

    return Results.Ok(passwordEntry);
});

// Get a single item from the password store with the password decrypted.
app.MapGet("/passwords/item/{id}/decrypted", (int id) =>
{
    var passwordEntry = PasswordCache.GetPasswordEntryById($"password:{id}");
    if (passwordEntry == null)
    {
        return Results.NotFound($"ID {id}: Not found.");
    }

    var decryptedPassword = PasswordUtils.DecryptPassword(passwordEntry.Password);
    return Results.Ok(new 
        { 
            passwordEntry.Id, 
            passwordEntry.Category, 
            passwordEntry.App, 
            passwordEntry.UserName, 
            DecryptedPassword = decryptedPassword 
        });
});

// Update a password store item.
app.MapPut("/passwords/item/{id}", (int id, PasswordEntry updatedEntry) =>
{
    if (!PasswordCache.UpdateEntry($"password:{id}", updatedEntry, CachingTime))
    {
        return Results.NotFound($"ID {id}: Not found.");
    }

    return Results.Ok($"ID {id}: Entry updated.");
});

// Delete a password store item.
app.MapDelete("/passwords/item/{id}", (int id) =>
{
    if (!PasswordCache.RemovePasswordEntry($"password:{id}"))
    {
        return Results.NotFound($"ID {id}: Not found.");
    }

    return Results.Ok($"ID {id}: Entry deleted.");
});

app.Run();