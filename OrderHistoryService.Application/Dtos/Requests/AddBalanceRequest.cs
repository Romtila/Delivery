using System.ComponentModel.DataAnnotations;
using Delivery.BaseLib.Core.DataAnnotations;

namespace OrderHistoryService.Application.Dtos.Requests;

public class AddBalanceRequest
{
    [Required] public long Id { get; set; }

    [Required] [Min(0.0)] public decimal Balance { get; set; }
}