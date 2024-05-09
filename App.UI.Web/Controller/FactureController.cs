using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Interfaces;
using App.ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Entitlements;
using System;
using System.Threading.Tasks;

namespace App.UI.Web.Controller
{
    [Route("[controller]")]
    public class FactureController : ControllerBase
    {
        private readonly StripeService _stripeServiceImplementation;
        private readonly FactureService _factureService;
        private readonly IStripeService _stripeService;

        public FactureController(StripeService stripeServiceImplementation, FactureService factureService, IStripeService stripeService)
        {
            _stripeServiceImplementation = stripeServiceImplementation;
            _factureService = factureService;
            _stripeService = stripeService;
        }






        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] PaymentRequestDto paymentRequest)
        {
            try
            {
                if (paymentRequest == null)
                {
                    return BadRequest("Payment request data is missing.");
                }

                // Validate payment request data
                if (!IsValidPaymentRequest(paymentRequest))
                {
                    return BadRequest("Invalid payment request data.");
                }

                // Create a payment session using Stripe
                var sessionUrl = await _factureService.CreatePaymentSessionAsync(paymentRequest);

                // Save payment details to the database
                await SavePaymentToDatabaseAsync(paymentRequest, sessionUrl);

                return Ok(sessionUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Payment failed.", message = ex.Message });
            }
        }

        
    

    [HttpGet("success")]
        public async Task<IActionResult> Success([FromQuery] string sessionId)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionId))
                {
                    return BadRequest("Session ID is missing.");
                }

                // Retrieve payment details from Stripe
                var paymentStatus = await _stripeService.GetPaymentStatusAsync(sessionId);

                // Update database based on payment status
                await UpdateDatabaseAsync(sessionId, paymentStatus);

                if (paymentStatus == "complete")
                {
                    return Ok("success");
                }
                else
                {
                    return Ok("cancel");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing payment details.", message = ex.Message });
            }
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return Ok("Payment canceled.");
        }

        private bool IsValidPaymentRequest(PaymentRequestDto paymentRequest)
        {
            // Validate payment request data here
            // Return true if data is valid, otherwise false
            return true;
        }

        private async Task SavePaymentToDatabaseAsync(PaymentRequestDto paymentRequest, string sessionUrl)
        {
            // Save payment details to the database here
            // Use paymentRequest and sessionUrl to save relevant data
            await Task.CompletedTask;
        }

        private async Task UpdateDatabaseAsync(string sessionId, string paymentStatus)
        {
            // Update database based on payment status and session ID here
            await Task.CompletedTask;
        }
    }
}
