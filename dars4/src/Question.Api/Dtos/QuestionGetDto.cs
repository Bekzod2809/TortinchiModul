namespace Question.Api.Dtos;

public class QuestionGetDto
{
    public Guid QuestionId { get; set; }
    public string Text { get; set; }
    public string VariantA { get; set; }
    public string VariantB { get; set; }
    public string VariantC { get; set; }
    public string Answer { get; set; }
}
