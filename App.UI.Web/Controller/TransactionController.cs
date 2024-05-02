using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("solde/{cin}")]
    public async Task<IActionResult> GetSolde(string cin)
    {
        var solde = await _transactionService.GetSolde(cin);
        if (solde == null)
            return NotFound();

        return Ok(solde);
    }

    [HttpGet("usersolde")]
    public async Task<IActionResult> ListUserWithSolde()
    {
        try
        {
            var usersWithSolde = await _transactionService.ListUserWithSolde();
            if (usersWithSolde == null)
                return NotFound();

            return Ok(usersWithSolde);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de la récupération des utilisateurs avec le solde : " + ex.Message);
            return StatusCode(500, "Une erreur s'est produite lors de la récupération des utilisateurs avec le solde.");
        }
    }

    [HttpGet("list-solde")]
    public async Task<IActionResult> ListSolde()
    {
        var soldeList = await _transactionService.ListSolde();
        if (soldeList == null || soldeList.Count == 0)
            return NotFound("Aucun solde trouvé.");

        return Ok(soldeList);
    }

[HttpPost("update-solde")]
    public async Task<IActionResult> UpdateSolde(string cin, decimal montant)
    {
        try
        {
            var updatedSolde = await _transactionService.UpdateSolde(cin, montant);
            if (updatedSolde == null)
                return NotFound("Le compte n'a pas été trouvé.");

            return Ok(updatedSolde);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur de mise à jour du solde : " + ex.Message);
            return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du solde.");
        }
    }

    [HttpPost("recharge")]
    public async Task<IActionResult> RechargeCompte([FromBody]  Facture request)
    {
        try
        {
            // Appel de la méthode de service pour recharger le compte
            var solde = await _transactionService.Recharge( request.Montant);

            // Vérification du succès de l'opération
            if (solde == null)
            {
                return NotFound("Compte non trouvé");
            }

            return Ok(solde);
        }
        catch (Exception ex)
        {
            // Gestion des erreurs
            return StatusCode(500, $"Une erreur s'est produite lors de la recharge du compte : {ex.Message}");
        }
    }
}
 

