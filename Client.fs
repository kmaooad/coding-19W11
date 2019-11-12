namespace OOAD19.Coding.W11

open FSharp.Json
open System

module Domain =
    type Customer =
        { id: int
          name: string
          street: string
          city: string
          state: string
          zip: string }

    type Product =
        { id: int
          name: string
          price: decimal
          taxable: bool }

type OrderStatus =
    | Draft
    | Submitted
    | Paid

type InvoiceStatus =
    | Open
    | Paid

module Client =
    open Domain

    type OrderedProductDto =
        { productId: int
          qty: int }

    type OrderTotalsDto =
            { subtotal: decimal
              taxTotal: decimal
              total: decimal } 

    type InvoiceDto =
        { id: int
          status: InvoiceStatus
          orderId: int
          subtotal: decimal
          taxTotal: decimal
          total: decimal }

    let addProduct name (price: decimal) taxable: int = failwith "not implemented"

    let updateProduct (p: Product): unit = failwith "not implemented"

    let newOrder (c: Customer) (pp: OrderedProductDto list): int = failwith "not implemented"

    let getTotal orderId: OrderTotalsDto = failwith "not implemented"

    let updateOrder orderId (pp: OrderedProductDto list): unit = failwith "not implemented"

    let getStatus orderId: OrderStatus = failwith "not implemented"

    let submitOrder orderId: unit = failwith "not implemented"

    let payInvoice invoiceId: unit = failwith "not implemented"

    let setBillingDay orderId (day: int): unit = failwith "not implemented"

    let invoice orderId : InvoiceDto = failwith "not implemented"

    let getOpenInvoices orderId : InvoiceDto list = failwith "not implemented"