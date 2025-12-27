# ChainSignal.Crypto

**A .NET-based system that consumes public market APIs and uses LLMs to generate structured, production-ready daily reports.**

---

## Overview

This project demonstrates **practical and opinionated patterns for integrating Large Language Models (LLMs) into .NET backend systems**.

Instead of focusing on chat interfaces or UI-driven scenarios, the goal here is to show how LLMs can be safely used as part of a **backend automation pipeline**, transforming structured market data into reliable, machine-consumable reports.

The project consumes public market data (CoinGecko as a reference provider) and uses an LLM to generate a **strictly validated JSON report**, suitable for further processing, storage, or delivery.

---

## Key Design Goals

- ✅ **Provider-agnostic market abstraction**
- ✅ **Strict JSON output contracts**
- ✅ **LLM guardrails to reduce hallucination**
- ✅ **Prompt versioning**
- ✅ **Controlled retry strategy**
- ✅ **Clear separation between domain, application, and infrastructure**

This repository is intentionally scoped to the **core LLM integration patterns**, leaving scheduling, persistence, and delivery concerns out of scope.

---

## Architecture Overview

At a high level, the flow looks like this:

```
Public Market API (CoinGecko)
        ↓
Market Provider Library
        ↓
Market Snapshot (Domain Model)
        ↓
Application Command Handler
        ↓
LLM (JSON → JSON Transformation)
        ↓
Validated Daily Report
```

### Project Structure

```
ChainSignal.Crypto
├── Core
│   └── Shared abstractions (Mediator, base types)
├── AI.Domain
│   └── Domain models (MarketSnapshot, MarketAsset, Reports)
├── AI.Application
│   ├── Commands
│   │   └── GenerateDailyReport
│   ├── Prompts
│   │   ├── IPromptCatalog
│   │   └── EmbeddedPromptCatalog
│   └── Services
│       └── LlmReportBuilder
├── MarketInfo.CoinGecko
│   └── CoinGecko provider implementation
└── AI.API
    └── HTTP API composition root
```

---

## Prompt Versioning

All prompts used by the LLM are treated as **first-class application assets** and explicitly versioned.

Each prompt is identified by:
- a logical name (e.g. `DailyCryptoReport`)
- a version number (e.g. `v1`)

This allows:
- controlled evolution of prompts
- reproducibility of generated outputs
- safe experimentation without breaking existing behavior

---

## JSON Guardrails and Retry Policy

LLMs are probabilistic systems and may occasionally produce invalid JSON, even when instructed otherwise.

To handle this safely, the project applies the following strategy:

1. The LLM is instructed to output **JSON only**, following a strict schema.
2. The output is immediately deserialized and validated.
3. **If deserialization fails**, a **single retry** is performed using a dedicated *JSON repair prompt*.
4. If the retry also fails, the operation is aborted with a clear error.

This approach keeps the system:
- deterministic
- cost-controlled
- resilient to occasional formatting issues

---

## API Endpoint

The project exposes a minimal HTTP API:

```
POST /api/report
```

This endpoint triggers the report generation flow and returns the generated daily report.

No request body is required for this example, as the report generation logic is fully encapsulated in the application layer.

---

## Why No Background Jobs or Email Delivery?

This repository intentionally focuses on **LLM integration patterns**, not infrastructure concerns.

In a real production system, the same command could be triggered by:
- a background worker
- a scheduler (Quartz, Hangfire, etc.)
- a messaging system

Those concerns are deliberately left out to keep the example focused, readable, and extensible.

---

## Why CoinGecko?

CoinGecko is used as a **reference public API** due to its simplicity and availability.

The architecture does **not** depend on CoinGecko specifically — any market or financial data provider can be integrated by implementing the same abstraction.

---

## Intended Audience

This project is aimed at developers who:

- work with .NET backend systems
- are curious about using LLMs beyond chatbots
- want concrete, production-oriented examples
- care about clean architecture and maintainability

---

## Disclaimer

This project is for **educational and architectural demonstration purposes only**.

It does not provide financial advice, trading signals, or investment recommendations.

---

## Next Steps (Out of Scope)

Possible extensions intentionally left for future iterations:

- Scheduled execution (background jobs)
- Persistence of generated reports
- Email or notification delivery
- Multi-provider aggregation
- Observability and tracing

---

## License

MIT
