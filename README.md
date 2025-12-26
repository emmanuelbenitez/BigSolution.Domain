# BigSolution.Infra.Domain

[![NuGet](https://img.shields.io/nuget/v/BigSolution.Infra.Domain.svg)](https://www.nuget.org/packages/BigSolution.Infra.Domain/)
[![License: Apache-2.0](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)

Core domain abstractions for DDD: entities, aggregate roots, value objects, repositories, unit of work and transactions.

## NuGet

- Package: `BigSolution.Infra.Domain`
- NuGet: https://www.nuget.org/packages/BigSolution.Infra.Domain/
- Install (latest):

```bash
dotnet add package BigSolution.Infra.Domain
```

Or add a specific version:

```bash
dotnet add package BigSolution.Infra.Domain --version <version>
```

## Supported frameworks

- .NET 10 (net10.0)
- .NET 9 (net9.0)
- .NET 8 (net8.0)

## Quick start

This library contains lightweight, testable domain primitives and interfaces to model business rules and persistence boundaries.

Minimal example:

```csharp
public class Order : IAggregateRoot
{
    // implement aggregate root members
}

IRepository<Order> repository = /* resolve repository implementation */;
repository.Add(new Order());
```

## API highlights

- `IEntity`
- `IAggregateRoot`
- `IRepository<T>`
- `IUnitOfWork`
- `ITransaction`
- `Entity` (base class)
- `ValueObject`

For full API, browse the source in `src/Domain` or the generated package on NuGet.

## Development

- Run tests: `dotnet test src/Domain.Tests`
- Build: `dotnet build`

## Contributing

Contributions welcome. Please open issues or pull requests on the repository and follow any contributing guidelines there.

## License

This project is licensed under the Apache License 2.0. See the `LICENSE` file for details.
