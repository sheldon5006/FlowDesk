# FlowDesk Warehouse Domain

## Purpose

Define the warehouse operations managed by FlowDesk and how they interact with the existing Workforce Management domain.

The Warehouse domain is an additional business domain inside FlowDesk, not a separate unrelated application.

## Core Entities

* Warehouse
* WarehouseLocation
* Product
* Inventory
* StockMovement
* Order
* OrderLine
* WarehouseTask

## Core Relationships

```text
Warehouse
    ↓
WarehouseLocations
    ↓
Inventory
    ↓
Products

Order
    ↓
OrderLines
    ↓
WarehouseTasks
    ↓
Requires Worker
    ↓
Workforce Management
```

## Core Business Rules

1. Warehouse tasks require workers.
2. Workers must satisfy FlowDesk workforce eligibility rules.
3. A worker cannot be assigned to a task when they are unavailable.
4. A worker cannot receive an overlapping assignment.
5. Worker workload should be considered when assigning work.
6. Warehouse operations must remain connected to the Workforce domain.
7. PostgreSQL remains the source of truth for persistent business data.
8. Warehouse functionality will initially remain inside the existing FlowDesk application.
9. The Warehouse domain may later be extracted into its own service when there is a clear architectural reason to do so.

## Initial Operational Flow

```text
Customer Order
      ↓
Order Created
      ↓
Order Lines
      ↓
Warehouse Tasks Created
      ↓
Tasks Require Workers
      ↓
FlowDesk Workforce
      ↓
Find Eligible Employees
      ↓
Assignment
```

## Deliberately Out of Scope

The initial Warehouse domain will not include:

* Payments
* Pricing
* Shipping carrier integrations
* Advanced demand forecasting
* Multi-warehouse optimization
* Autonomous replenishment algorithms
* Other functionality not required for the core Workforce + Warehouse workflow
