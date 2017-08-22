# url-shortener
Simple URL shortener service written in ASPNET Core 2.0. Using Postgresql

## Endpoints
`POST /`: with body parameter:
```
{
    "url": "URL"
}
```
where URl is the url that needs to be redirected to.
The reponse of the output have content type `application/json` and returns:
```
{
    id: GUID,
    shortId: string
}
```

`GET /<id | shortId>`: where it can be either id or shortId. This redirects to the page (if it exists) or redirects to a default page (RedirectUrl). If RedirectUrl is empty it will throw a 404.

## Settings
In appsettings there are following settings for the url shortener
 - `Domains`: Domains allowed to be shorten. Multiple domains are separated with comma (,) (example default.com, default2.(com|net))
 - `ShortIdCharacters`: The characters allowed for the shortId (case sensitive).
 - `ShortIdLength`: The length of the shortId.
 - `RedirectUrl`: The default page to redirect to if the url is missing from the database (if empty it just throws 404 page).

The ConnectionString needs to be a Postgresql connection string.

## Database
The database have 3 tables.
`Urls`: Containing the url, shortId, Id and date.
`UrlRedirects`: Containing analytics on how often the urls have been used and when.
`UrlErrors`: Containing errors in form of not found urls, short id generation errors, wrong domains.

Both MSSQL and PostgreSQL are allowed. Just add the settings to the appsettings within `DataSettings`:

MSSQL ```"MSSQLConnectionString": "Data Source=tcp:localhost,1433;Database=UrlShorten;User ID=admin;Password=password;MultipleActiveResultSets=True"```

PostgreSQL ```"PostgresConnectionString": "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=UrlShortener;Pooling=true;"```