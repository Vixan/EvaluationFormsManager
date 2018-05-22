namespace EvaluationFormsManager.Domain
{
    public class SharedForms
    {
        public int Id { get; set; }
        public virtual Form Form { get; set; }
        public string UserId { get; set; }
    }
}
