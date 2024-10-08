﻿---
uid: Codebelt.Extensions.Xunit.Hosting
summary: *content
---
The `Codebelt.Extensions.Xunit.Hosting` namespace contains types that provides a uniform way of doing unit testing that is used in conjunction with Microsoft Dependency Injection. The namespace relates to the `Xunit.Abstractions` namespace.

[!INCLUDE [availability-default](../../includes/availability-default.md)]

Complements: [xUnit: Shared Context between Tests](https://xunit.net/docs/shared-context) 🔗

### Extension Methods

|Type|Ext|Methods|
|--:|:-:|---|
|ILogger{T}|⬇️|`GetTestStore`|
|IServiceCollection|⬇️|`AddXunitTestOutputHelperAccessor`|
|IServiceProvider|⬇️|`GetRequiredScopedService`|
