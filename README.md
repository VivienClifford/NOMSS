# NOMSS
New Order Management System Services (NOMSS)

## Background:

As a warehouse officer.

I want to be able to initiate an order run.

So that I can deliver orders according to my pending orders and inventory availability.

---
## Questions\Assumptions:
Normally, these would be some of the questions I would be asking undertaking this kind of task. I have noted the assumptions I have made along the way.

**1. Are the product held in a single warehouse, or potentially multiple warehouses where different products are stored?** 

    For simplicity, I have taken the road that there's only one warehouse. This would increase the complexity to process the order run if we had to calculate quantities of multiple products over multiple warehouses/other locations.

**2. What is the normal process to complete an order?**

    Status transition (New => Pending => (Fulfilled/UnFulfilled))

**3. Does the warehouse officer fulfill other roles in the order process?**

    To see if any other processes need to be accounted for.

**4. Clarification on the data we need to supply to generate a Purchase Order?**

    - Company contact information (name, address, phone number)
    - Shippment method
    - Products (SKU, productId, name/description, qty, unit price)
    - Reorder Date

    I have chosen to create an API Endpoint to create some of these details. I have taken into consideration that we also don't want to be creating multiple Purchase Orders if the restock threshold has already been reached. This has been observed as a potential problem, if there were more order runs, we would need to store this data in a key-value pair or database alternative. This way we would be able to determine whether a Purchase Order has already been ordered for a product recently.

**5. Lead time?**

    This seems to be some sort of time production processed has to fulfill an order. This field has not been used.

---

## Entities

### Main:
- Product
- Order
    - Items

### Related entities
These are likely some other areas that are related to the main entities. This was used as a brainstorming exercise. 

  - ProductLocation – Help to locate products within the warehouse. e.g. shelf/aisle or identifying a location.
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
    Completed:
    • [POST] api/v1/warehouse/fulfilment
    • [GET] api/v1/warehouse/products/restock 

    Not Completed
    • [GET] api/v1/warehouse/products
    • [GET] api/v1/warehouse/products/{id}
    • [GET] api/v1/warehouse/orders
    • [GET] api/v1/warehouse/orders/{id}

---
## Infrastructure:

To begin with, I'm going to create a simple N-tier design, to flesh out core product requirements and unit test the business requirements. If we wanted to have a more robust design, it would then be split out into a microservices design, however, for this task, it's going to be quite small anyway. 

The data will only be supplied through the associated data.json file, no storage implementation has been created for this project.
    
    • API
    • Service
    • Test
    • Sample dataset - data.json

## Reflection:

If I had to redo this task, these would be the items I would add or change. 
    
- For the API endpoints, in particular, I would change them to be asynchronous. I would also create more API endpoints. 
- Write tests for the API endpoints and try to introduce the Carter library
- Add logging (errors and warnings)
- Write more safety checks and also look more deeply into security
