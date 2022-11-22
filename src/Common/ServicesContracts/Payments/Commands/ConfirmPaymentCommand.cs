﻿using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Payments.Commands;

public class ConfirmPaymentCommand : IRequest<Result<bool>>
{
    public int PaymentId { get; set; }
    public string AdminEmail { get; set; }
}