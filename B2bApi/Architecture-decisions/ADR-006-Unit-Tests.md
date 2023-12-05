# 006 - Choice of xUnit for Unit Testing

**Reporter:** Rafał Karaś

## Context

The decision pertains to the selection of a tool for unit testing in the project. The key aspect is an effective and consistent approach to creating unit tests.

## Considered Options

- **Option #1** - xUnit.
- **Option #2** - NUnit.
- **Option #3** - MSTest.

## Decision

After a thorough analysis of the available options, the decision has been made to go with **Option #1**, which is xUnit, as the unit testing tool for the project.

This decision is based on the following reasons:
1. **Simple and Clear Syntax:** xUnit has a clear and concise syntax, making it easier to write tests.
2. **Test Parallelization:** xUnit provides built-in support for running tests in parallel, speeding up the testing process.
3. **Community Popularity:** It is one of the most popular testing frameworks in .NET, indicating broad support and availability of community resources.
4. **Active Development and Community Collaboration:** xUnit is actively developed and benefits from collaboration with the wider developer community.

## Expected result

Adopting xUnit as the unit testing tool will enable a consistent, clear, and efficient approach to writing tests, utilizing a popular and well-developed framework.

Impact on:
- **Organization:** It will facilitate the management of unit tests, providing a consistent approach.
- **Standardization:** It will deliver a unified practice for the development team.
- **Concrete Solution:** It will allow configuring unit tests in a way that aligns with the team's preferences and needs.

## Links

- [xUnit](https://xunit.net/)
