# Lab 03

## Part 1 of borathon

Following good design principles (SOLID, etc.), create a customer management microservice (in the language of your choice) according to the following high-level requirements:

Your team's goal is to build an API for the customer, account and transaction operations outlined below. This API will be a microservice that can be:
1) Developed in the technology of your team's choosing
2) Independently-deployed
3) Independently-scaled

By building this API/microservice, we are helping to position this client for beginning their journey to the Cloud. Using a Hybrid Cloud approach, the client will be able to move this critical service into the Cloud and integrate their on-prem interfaces and services with the new service.

Ultimately, this API/microservice will have its own data source. However, for part 1, use in-memory data structures to represent storage. In a future part of the bore-a-thon, we will have the opportunity to add a database.

Below, you will find definitions for the data structures and a brief description of each operation targeted for inclusion in your API:

Account
- ID (internal ID - integer) - record ID
- Account Number (external ID - string) - use an algorithm of your choosing to generate or assign a new account number on account opening
- Balance (decimal value)
- Account Status (can be either Open or Closed)

Customer (**NOTE: For borathon purposes, you can assume each customer has only one account**)
- ID (internal ID - integer) - record ID
- First Name (string)
- Last Name (string)
- Associated Account - e.g. foreign key relationship

Transaction
- ID (internal ID - integer) - record ID
- Amount (decimal value - always positive)
- Transaction Type (can be either Debit or Credit)
- Associated Account - e.g. foreign key relationship

Supported Operations
- Retrieve customer/account details by account number - on supply of the account number, retrieve customer/account details, including current account balance
- Open customer account - on supply of the first name and last name for the customer, create a new customer record, create a new account record (with an initial $0.00 balance) and return customer/account details for the new account
- Close customer account - on supply of the account number, update the account to a "Closed" status
- Apply a transaction to the customer account - on supply of the account number, transaction amount and type of transaction ("Debit" or "Credit"), create a new transaction record and update the account balance accordingly

Below are Swagger screenshots to provide some additional info/guidance. You do not have to use Swagger for testing - you can use POSTMan (or similar). Also, the launch-demo-api has been included in the GitHub repo as an example API (C#).

## Overview

![Overview](./images/overview.png)

## Open Account

![Open Account 1](./images/open-account1.png)

![Open Account 2](./images/open-account2.png)

## Apply Credit Transaction

![Credit Transaction 1](./images/credit-xaction1.png)

![Credit Transaction 2](./images/credit-xaction2.png)

## Apply Debit Transaction

![Debit Transaction 1](./images/debit-xaction1.png)

![Debit Transaction 2](./images/debit-xaction2.png)

## Retrieve Account Details

![Get Account 1](./images/get-account1.png)

![Get Account 2](./images/get-account2.png)

## Close Account

![Close Account 1](./images/close-account1.png)

![Close Account 2](./images/close-account2.png)
