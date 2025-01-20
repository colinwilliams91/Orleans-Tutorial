# OrleansURLShortener

## To Use
1. Start the app using the run button at the top of Visual Studio. The app should launch in the browser and display the familiar Hello world! text.
2. In the browser address bar, test the shorten endpoint by entering a URL path such as {localhost}/shorten?url=https://learn.microsoft.com. The page should reload and provide a shortened URL. Copy the shortened URL to your clipboard.
 
![image](https://github.com/user-attachments/assets/879c67e7-a7b6-401f-92f8-440306fabcc5)

3. Paste the shortened URL into the address bar and press enter. The page should reload and redirect you to https://learn.microsoft.com.

## API Documentation
```
- Generated OpenAPI Specification JSON documentation describing endpoints:
https://localhost:<port>/swagger/v1/swagger.json

- Swagger UI for API with annotations from XML and C# decorators
https://localhost:<port>/swagger
```

## Orleans Silos
- Example switch case for DI store provider type
  - Switches between in memory store and Redis
```C#
public static ISiloBuilder UseStorage(this ISiloBuilder siloBuilder, string storeProviderName, IAppInfo appInfo, StorageProviderType? storageProvider = null, string storeName = null)
{
	storeName = storeName.IfNullOrEmptyReturn(storeProviderName);
	storageProvider ??= _defaultProviderType;

	switch (storageProvider)
	{
		case StorageProviderType.Memory:
			siloBuilder.AddMemoryGrainStorage(storeProviderName);
			break;
		case StorageProviderType.Redis:
			siloBuilder
				.AddRedisGrainStorage(storeProviderName)
				.Build(builder =>
					builder.Configure(ConfigureRedisOptions(storeName, appInfo))
				);
			break;
		default:
			throw new ArgumentOutOfRangeException(nameof(storageProvider), $"Storage provider '{storageProvider}' is not supported.");
	}

	return siloBuilder;
}
```
