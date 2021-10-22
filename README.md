# NOMSS
New Order Management System Services (NOMSS)

## Background:

As a warehouse officer.
I want to be able to initiate an order run.
So that I can deliver orders according to my pending orders and inventory availability.

---
## Questions:
    1. Are the product held in a single warehouse, or potentially multiple warehouses where different products are stored?
    2. What is the normal process to complete an order?
    3. Does the warehouse officer fufil other roles in the order process?
    4. How is inventory managed?

---

## Entities

### Main:
- Product
- Order
    - Items

### Related entities
These are likely some other areas that are related to the main entities. This was mainly used as a brain storming exercise. 

  - ProductLocation – Help to locate products within the warehouse.
  - Warehouse
  - Supplier
  - Customer
  - Address 
  
  
   #### Invoice Processing
  - Transaction
  - Invoice
  - Stock

---
## APIs
    • api/v1/warehouse/fulfilment
    • api/v1/warehouse/products
    • api/v1/warehouse/products/{id}
    • api/v1/warehouse/products/restock (an array of ProductIds to be restocked)
    • api/v1/warehouse/orders
    • api/v1/warehouse/orders/{id}

---
## Infrastructure:

To begin with, I'm going to create a simple N-tier design, to flesh out core product requirements and unit test the business requirements. If we wanted to have a more robust design, it would then be split out into microservices, however for this task it's going to be quite small anyway.
    
    • API
    • Services
    • Unit Test
    • Sample dataset - data.json
