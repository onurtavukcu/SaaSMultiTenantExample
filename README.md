## This repository contains creating multi tenant SaaS project.
# This project has two controller. 
  - Tenants Controller = Creating tenants that creates own seperated DB automaticly 
  - Products Controller = Resolve the requester tenants (in headers parameter) and connect tenant's db and get products.
    That resolve tenant with middleware
# I create services:
  - Tenant service: For creating tenants and db's migrations
  - Current Tenant Service: Decide what is the current tenant
  - Product Service: Get tenant's product
