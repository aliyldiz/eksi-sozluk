using System.Drawing;
using System.Text.Json.Serialization;


namespace EksiSozluk.Common.Infrastructure.Result;

public class ValidationResponseModel
{
    public ValidationResponseModel()
    {
        
    }
    
    public ValidationResponseModel(string message) : this(new List<string>() { message })
    {
        
    }

    public ValidationResponseModel(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public IEnumerable<string> Errors { get; set; }
    
    [JsonIgnore]
    public string FlattenErrors => Errors != null ? string.Join(Environment.NewLine, Errors) : string.Empty;
}