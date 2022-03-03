# Lab 04

## Part 2 of borathon

Continue building out the customer, account, and transaction management service that was started in <a href="../lab03/README.md" target="_blank">Lab 03</a>.

If you weren't able to finish all of the operations in the previous lab, focus on building out a single operation end-to-end (e.g., opening a new customer account). Alternatively, you can see an implementation of the various operations (to help guide your completion) by reviewing the "CustomerManagementService" solution in the "labs" folder of the GitHub repo.

In this lab, you will add a database to your application.

Build the database in AWS using any of the following options:
1) IaaS - VM's with a database platform installed
2) PaaS - One of AWS's managed services (e.g., RDS)
3) Containers - Containerize your data store

The goal of this lab is to build out the database structures and "wire" them up to your API for persisting the data underlying each of the operations. Again, if you find yourself time-constrained, work to get a single operation running end-to-end.
