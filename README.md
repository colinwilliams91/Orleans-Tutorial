# OrleansURLShortener

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