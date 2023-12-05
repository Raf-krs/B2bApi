# 009 - Use of Serilog and Seq for Logging Middleware

**Reporter:** Rafał Karaś

## Context

The decision pertains to the choice of tools for implementing logging middleware in the project. The key aspect is the effective collection and analysis of logs for monitoring the system's operation.

## Considered Options

- **Option #1** - Implementation of logging middleware using Serilog.
- **Option #2** - Use of Seq for collecting and analyzing logs.

## Decision

After a thorough analysis of the available options, the decision has been made to go with **Option #1**, which is the implementation of logging middleware using Serilog, and **Option #2**, which is the use of Seq for collecting and analyzing logs.

This decision is based on the following reasons:
1. **Serilog:** It is a flexible logging tool that allows easy configuration of various log destinations (e.g., console, files, external services).
2. **Seq:** It is a dedicated tool for collecting, browsing, and analyzing logs, facilitating the monitoring of the system's operation.

## Expected result

Adopting Serilog for logging middleware will allow flexible log management, while using Seq will enable efficient collection and analysis of logs, improving the monitoring of the system's operation.

Impact on:
- **Organization:** It will facilitate the management and monitoring of logs in the project.
- **Standardization:** It will provide a consistent approach to logging middleware and log analysis.
- **Concrete Solution:** It will enable the configuration of a flexible logging and monitoring environment.

Potential negative consequences:
- The need for adaptation for individuals who have not worked with Serilog and Seq before.

## Benefits of Using Serilog and Seq for Logging Middleware

1. **Flexibility (Serilog):** Serilog allows easy configuration of various log destinations, adapting to different project needs.
2. **Dedicated Log Analysis Tool (Seq):** Seq is a dedicated tool for collecting, browsing, and analyzing logs, making it easier to monitor the system's operation.
3. **Easy Management (Serilog + Seq):** Combining Serilog with Seq enables easy log management and analysis in one cohesive environment.

## Links

- [Serilog](https://serilog.net/)
- [Seq](https://datalust.co/seq)
