# Coding assignment. Week 11 (2019).

[![Join the chat at https://gitter.im/kmaooad/coding-19W11](https://badges.gitter.im/kmaooad/coding-19W11.svg)](https://gitter.im/kmaooad/coding-19W11?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
<NEVER BUILT>

### Task

**Implement recurrent billing**

This task is evolves the previously built order processing from week 9. Now orders are not paid directly, but through creating invoice first and then paying it. Also, orders need to be paid every month (e.g. for service subscriptions), hence recurrent invoicing should be implemented. For this every order has a _billing day_ setting - day of the month when recurring invoicing will happen. For simplicity max billing day can be 28. **If order is placed on 29th, 30th, or 31st day, billing day is set to 28 automatically and prorated billing is used (see below).**

### Changed API
Some methods in `Client.fs` now have different meaning:

- `submitOrder orderId: unit` When order is submitted two things happen:
    - if not set by customer explicitly (see `setBillingDay` below), billing day is set to current (since order can sit in draft for several days);
    - open invoice is created with calculated totals

- `payInvoice invoiceId: unit` Previous method `payOrder` is now gone, because now invoice gets paid instead of order. When invoice is paid, its status changes to `Paid` and original order is also set to `Paid`.

### New API
`Client.fs` also contains additional API that you need to implement in this task:

- `setBillingDay orderId (day: int): unit` Changes billing day for the order. Only draft order can change billing day. When billing day is changed, order is recalculated with _proration_ (see details below).

- `invoice orderId : InvoiceDto` Creates new recurring invoice. Recurring invoice always bills for whole month, so no proration needed. Invoice should use actual product pricing.

- `getOpenInvoices orderId : InvoiceDto list` Returns open invoices for the order

### Proration calculation

When customers create orders they can decide on which day of month they want to be billed. For simplicity, only 28 days are available for billing (to avoid complexity of handling February and 30/31th day).

Initial order invoice can be issued on the day other than the billing day, so first month of service will not be full, and only partial amount should be billed. This partial amount is called proration and needs to be calculated properly depending on the billing day of the order and current day when order is submitted. Note that billing day can be in the current month e.g. now is 12th, billing is 14th; or in the next month, e.g. if now is 12th and billing is 4th.

**Proration is calculated only if billing day does not match order submission date.**

If P - monthly product price, C - current day, B - billing day, M1 - number of days in current month, M2 - number of days in next month (if applicable - when billing day is in the next month), D1 = P / M1 - daily price for current month, D2 = P / M2 - daily price for next month (if applicable), then prorated amount is 

**(M1 - C) * D1 + B * D2**,

i.e. amount for remaining days of current months plus amount for first days of next month.