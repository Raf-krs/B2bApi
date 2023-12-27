# 004 - Time Zone Selection in the System

**Reporter:** Rafał Karaś

## Context

In most information systems, operating on time and dates is a crucial aspect. To avoid data inconsistency and unnecessary conversions, it is necessary to establish a uniform standard for storing and transmitting dates throughout the system.

## Considered Options

- **Option #1** - Use of local Polish time.
- **Option #2** - Use of Coordinated Universal Time (UTC)

## Decision

After considering the available options, the decision has been made to go with **Option #2**, which is the use of UTC time. This is the simplest solution, simultaneously minimizing the risk of errors.

Any issues related to localization will be left to client applications, such as the frontend in the browser or mobile applications.

## Expected result

Adopting the assumption that every date in the system is in UTC format will minimize problems related to storing and transmitting dates in the database and communication between system modules.

Application clients have a clear API contract regarding time, which they can rely on for presenting or transmitting dates.

### Additional Recommendations

It is recommended to use abstractions over the system time API, such as `DateTime.UtcNow`, to facilitate testing of developed solutions.

As part of the `B2bApi.Shared.Clock` project, the `IClock` interface has been provided along with an already registered implementation in the IoC container.

## Links

In .NET 8, there are plans to introduce the equivalent of the `IClock` interface into the standard library: [Link to discussion](https://github.com/dotnet/runtime/issues/36617)
