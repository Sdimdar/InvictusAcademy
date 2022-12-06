using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServicesContracts.FreeArticles.Models;

namespace ServicesContracts.FreeArticles.Commands;

public class EditFreeArticleCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? VideoLink { get; set; }
    public string Text { get; set; }
    public string ImageLink { get; set; }
    public bool IsVisible { get; set; }
}